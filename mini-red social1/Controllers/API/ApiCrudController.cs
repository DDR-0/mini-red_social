using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

public abstract class ApiCrudController<TEntity> : ControllerBase where TEntity : class
{
    private static readonly JsonSerializerOptions PatchJsonOptions = new(JsonSerializerDefaults.Web);
    private readonly SocialDBcontext _context;

    protected ApiCrudController(SocialDBcontext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        return Ok(entities.Select(ToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        return entity == null ? NotFound() : Ok(ToResponse(entity));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TEntity entity)
    {
        ClearNavigationProperties(entity);
        SetCreationTimestamp(entity);

        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();

        var id = GetEntityId(entity);
        return CreatedAtAction(nameof(GetById), new { id }, ToResponse(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Replace(int id, [FromBody] TEntity entity)
    {
        var entityId = GetEntityId(entity);
        if (entityId != 0 && entityId != id)
        {
            return BadRequest(new { error = "El id de la ruta no coincide con el id del cuerpo." });
        }

        var existing = await _context.Set<TEntity>().FindAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        var entry = _context.Entry(existing);
        var primaryKey = GetPrimaryKey(entry.Metadata);
        entry.CurrentValues.SetValues(entity);
        entry.Property(primaryKey.Name).CurrentValue = id;
        entry.Property(primaryKey.Name).IsModified = false;
        PreserveCreationTimestamp(entry);

        if (!TryValidateModel(existing))
        {
            return ValidationProblem(ModelState);
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonElement changes)
    {
        if (changes.ValueKind != JsonValueKind.Object || !changes.EnumerateObject().Any())
        {
            return BadRequest(new { error = "El cuerpo de PATCH debe ser un objeto JSON no vacío." });
        }

        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        var entry = _context.Entry(entity);
        var primaryKey = GetPrimaryKey(entry.Metadata);

        foreach (var change in changes.EnumerateObject())
        {
            var property = entry.Metadata.GetProperties().FirstOrDefault(
                candidate => string.Equals(candidate.Name, change.Name, StringComparison.OrdinalIgnoreCase));

            if (property == null || property.Name == primaryKey.Name || property.PropertyInfo?.CanWrite != true)
            {
                ModelState.AddModelError(change.Name, "La propiedad no existe o no se puede modificar.");
                continue;
            }

            try
            {
                var value = JsonSerializer.Deserialize(change.Value.GetRawText(), property.ClrType, PatchJsonOptions);
                entry.Property(property.Name).CurrentValue = value;
                entry.Property(property.Name).IsModified = true;
            }
            catch (JsonException)
            {
                ModelState.AddModelError(change.Name, "El valor no tiene un formato válido para esta propiedad.");
            }
        }

        if (!ModelState.IsValid || !TryValidateModel(entity))
        {
            return ValidationProblem(ModelState);
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private void ClearNavigationProperties(TEntity entity)
    {
        foreach (var navigation in _context.Entry(entity).Navigations)
        {
            if (!navigation.Metadata.IsCollection)
            {
                navigation.CurrentValue = null;
            }
        }
    }

    private static IProperty GetPrimaryKey(IEntityType entityType)
    {
        return entityType.FindPrimaryKey()?.Properties.Single()
            ?? throw new InvalidOperationException($"La entidad {typeof(TEntity).Name} no tiene una clave primaria simple.");
    }

    private int GetEntityId(TEntity entity)
    {
        var key = GetPrimaryKey(_context.Entry(entity).Metadata);
        return Convert.ToInt32(_context.Entry(entity).Property(key.Name).CurrentValue);
    }

    private static void SetCreationTimestamp(TEntity entity)
    {
        var property = typeof(TEntity).GetProperty("creado_en")
                       ?? typeof(TEntity).GetProperty("fecha_creacion");

        property?.SetValue(entity, DateTime.Now);
    }

    private static void PreserveCreationTimestamp(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entry)
    {
        var propertyName = entry.Metadata.FindProperty("creado_en") != null
            ? "creado_en"
            : entry.Metadata.FindProperty("fecha_creacion") != null
                ? "fecha_creacion"
                : null;

        if (propertyName == null)
        {
            return;
        }

        var property = entry.Property(propertyName);
        property.CurrentValue = property.OriginalValue;
        property.IsModified = false;
    }

    private static object ToResponse(TEntity entity)
    {
        if (entity is not Usuario usuario)
        {
            return entity;
        }

        return new
        {
            usuario.user_id,
            usuario.rol_id,
            usuario.nombre,
            usuario.apellido,
            usuario.correo,
            usuario.descripcion,
            usuario.estado_usuario,
            usuario.visiblity,
            usuario.creado_en,
            usuario.modificado_en,
            usuario.estado_cuenta
        };
    }
}
