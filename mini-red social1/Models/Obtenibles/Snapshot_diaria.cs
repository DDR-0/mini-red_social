using System.ComponentModel.DataAnnotations;

namespace mini_red_social1.Models;
using Microsoft.EntityFrameworkCore;


[Index(nameof(fecha), IsUnique = true)]

public class Snapshot_diaria
{   [Key]
    public int id { get; set; }
    
    public DateTime fecha { get; set; }
    
    public int usuarios_activos_totales { get; set; }
    
    public int interacciones_totales_creadas { get; set; }
    
    public int interacciones_totales_resueltas { get; set; }
    
    public decimal frecuencia_uso_recursos_usados { get; set; }
    
    public string metadatos { get; set; }
    
    public DateTime creado_en { get; set; }
}