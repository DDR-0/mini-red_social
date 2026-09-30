using System.ComponentModel.DataAnnotations.Schema;

namespace mini_red_social1.Models;

public class Uso_recurso
{
    public int uso_recurso_id { get; set; }
    
    public int interaccion_id { get; set; }
    [ForeignKey(nameof(interaccion_id))] public Interaccion_social InteraccionSocial { get; set; } = null!;
    
    public int recurso_id { get; set; }
    [ForeignKey(nameof(recurso_id))] public Recurso Recurso { get; set; } = null!;
    
    public DateTime fecha_inicio { get; set; }
    
    public DateTime fecha_fin { get; set; }
    
    public enum estado {reservado, confirmado, lanzado, cancelado}
    public estado Estado_recurso { get; set; }
    
    public DateTime fecha_creacion { get; set; }
    
    public DateTime fecha_eliminacion { get; set; }
}