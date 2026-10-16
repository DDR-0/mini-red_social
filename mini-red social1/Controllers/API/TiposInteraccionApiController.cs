using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/tipos-interaccion")]
public class TiposInteraccionApiController : ApiCrudController<Tipo_Interaccion>
{
    public TiposInteraccionApiController(SocialDBcontext context) : base(context)
    {
    }

    [HttpGet]
    public Task<IActionResult> GetAll() => GetAllEntities();

    [HttpGet("{id:int}")]
    public Task<IActionResult> GetById(int id) => GetEntityById(id);

    [HttpPost]
    public Task<IActionResult> Create([FromBody] Tipo_Interaccion entity) => CreateEntity(entity);

    [HttpPut("{id:int}")]
    public Task<IActionResult> Replace(int id, [FromBody] Tipo_Interaccion entity) => ReplaceEntity(id, entity);

    [HttpPatch("{id:int}")]
    public Task<IActionResult> Patch(int id, [FromBody] JsonElement changes) => PatchEntity(id, changes);

    [HttpDelete("{id:int}")]
    public Task<IActionResult> Delete(int id) => DeleteEntity(id);
}