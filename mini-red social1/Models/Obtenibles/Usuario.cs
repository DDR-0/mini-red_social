using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace mini_red_social1.Models;

[Index(nameof(correo), IsUnique = true)]
public class Usuario
{   [Key]
    public int user_id { get; set; }
    
    public int rol_id { get; set; }
    [ForeignKey(nameof(rol_id))] public Roles role { get; set; } = null!;
    
    public string nombre { get; set; } = string.Empty;
    
    public string? apellido { get; set; }
    
    public string correo { get; set; }
    
    public string? descripcion { get; set; }

    public string contrasena_hash { get; set; }

    public enum EstadoUsuario { Activo, Inactivo, Pendiente }
    public EstadoUsuario estado_usuario { get; set; }

    public enum visibilidad { publico, privado }
    public visibilidad visiblity { get; set; }
    
    public DateTime creado_en { get; set; }

    public DateTime modificado_en { get; set; }
    
    public enum EstadoCuenta { Activa, Suspendida }
    public EstadoCuenta estado_cuenta { get; set; }
    
}