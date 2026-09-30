using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini_red_social1.Models;

public class Interaccion_social : IValidatableObject
{
    [Key]
    public int interacciones_id { get; set; }
    
    public int tipo_interaccion_id { get; set; }
    [ForeignKey(nameof(tipo_interaccion_id))] public Tipo_Interaccion tipo_interaccion { get; set; } = null!;
    
    public int id_usuario { get; set; }
    [ForeignKey(nameof(id_usuario))] public Usuario usuario_id { get; set; } = null!;
    
    [MaxLength(150)]
    public string titulo  { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? descripcion { get; set; }

    public enum estado_actual {borrador, pendiente, en_progreso, resuelto, cancelado, escalado}
    
    public estado_actual EstadoActual { get; set; }

    public enum nivel_privacidad {publico, privado, interno}
    
    public nivel_privacidad NivelPrivacidad { get; set; }
    
    public DateTime? horario_inicio { get; set; }
    public string? horario_inicio_en => horario_inicio?.ToString("HH:mm:ss");
    
    public DateTime? horario_fin { get; set; }
    public string? horario_fin_en => horario_fin?.ToString("HH:mm:ss");
    
    public DateTime creado_en { get; set; }
    
    public DateTime modificado_en { get; set; }
    
    public DateTime? borrado_en { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (horario_inicio.HasValue != horario_fin.HasValue)
        {
            yield return new ValidationResult(
                "La fecha de inicio y la fecha de fin deben informarse juntas.",
                [nameof(horario_inicio), nameof(horario_fin)]);
        }
        else if (horario_inicio.HasValue && horario_fin.Value <= horario_inicio.Value)
        {
            yield return new ValidationResult(
                "La fecha de fin debe ser posterior a la fecha de inicio.",
                [nameof(horario_inicio), nameof(horario_fin)]);
        }
    }
}