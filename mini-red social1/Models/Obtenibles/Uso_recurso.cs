using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace mini_red_social1.Models;

public class Uso_recurso
{   
    [Key]
    public int uso_recurso_id { get; set; }
    
    public int interaccion_id { get; set; }
    [ForeignKey(nameof(interaccion_id))]
    [ValidateNever]
    public Interaccion_social InteraccionSocial { get; set; } = null!;
    
    public int recurso_id { get; set; }
    [ForeignKey(nameof(recurso_id))]
    [ValidateNever]
    public Recurso Recurso { get; set; } = null!;
    
    public DateTime fecha_inicio { get; set; } = DateTime.Now;
    
    public DateTime? fecha_fin { get; set; }
    
    public enum estado {reservado, confirmado, lanzado, cancelado}
    public estado Estado_recurso { get; set; }
    
    public DateTime fecha_creacion { get; set; } = DateTime.Now;
    
    public DateTime? fecha_eliminacion { get; set; }
}