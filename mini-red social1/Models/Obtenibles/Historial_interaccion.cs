using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini_red_social1.Models;

public class Historial_interaccion
{   [Key]
    public int id { get; set; }

    public int interaccion_id { get; set; }
    [ForeignKey(nameof(interaccion_id))] public Tipo_Interaccion id_interaccion { get; set; } = null!;

    public int cambio_por_usuario { get; set; }
    [ForeignKey(nameof(cambio_por_usuario))] public Usuario usuario_id { get; set; } = null!;

    public string estado_previo { get; set; }

    public string estado_nuevo { get; set; }

    public string motivo { get; set; }

    public DateTime creado_en { get; set; }
    
}