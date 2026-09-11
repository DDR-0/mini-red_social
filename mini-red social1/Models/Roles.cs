using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace mini_red_social1.Models;

public class Roles
{   [Key] public int id { get; set; }

    public string nombre { get; set; }
    
    public string? descripcion { get; set; }

    public bool activo { get; set; }
    
    public DateTime creado_en { get; set; }
    
    public DateTime modificado_en { get; set; }
}