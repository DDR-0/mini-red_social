using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace mini_red_social1.Models;

public class Registro_auditoria
{   [Key]
    public int id { get; set; }
    
    public int id_usuario { get; set; }
    [ForeignKey(nameof(id_usuario))] public Usuario id_user { get; set; } = null!;
    
    public string accion { get; set; }
    
    public string entidad_afectada  { get; set; }
    
    public string id_entidad { get; set; }
    
    public string direccion_ip { get; set; }
    
    public string usuario_agente { get; set; }
    
    public string payload_antes { get; set; }
    
    public string payload_despues { get; set; }
    
    public DateTime creado_en { get; set; }
}