using System.ComponentModel.DataAnnotations;

namespace mini_red_social1.Models;

public class Recurso
{   [Key]
    public int id { get; set; }
    
    public string nombre { get; set; }

    public enum tipo {espacio, equipo, servicio}
    public tipo type { get; set; }
    
    public int capacidad  { get; set; }
    
    public DateTime creado_en { get; set; }
    
    public DateTime modificado_en { get; set; }
}