using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace mini_red_social1.Models;

[Index(nameof(nombre), IsUnique = true)]
public class Usuario
{
    public int id { get; set; }
    
    public int rol_id { get; set; }
    [ForeignKey(nameof(rol_id))] public Roles role { get; set; } = null!;
    
    public string nombre { get; set; } = string.Empty;
    
    public string? apellido { get; set; }
    
    public string correo { get; set; }
    
    public string? descripcion { get; set; }

    public string contrasena_hash { get; set; }

    public enum estado { Activo, Inactivo, Pendiente }
    public estado status { get; set; }

    public enum visibilidad { publico, privado }
    public visibilidad visiblity { get; set; }
    
    public DateTime creado_en { get; set; }

    public DateTime modificado_en { get; set; }

    public DateTime borrado_en { get; set; }


}