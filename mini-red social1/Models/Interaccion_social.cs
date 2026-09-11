namespace mini_red_social1.Models;

public class Interaccion_social
{
    public int id { get; set; }
    
    public int id_tipo_interaccion { get; set; }
    
    public int id_usuario { get; set; }
    
    public string titulo  { get; set; }
    
    public string descripcion { get; set; }

    public enum estado_actual {borrador, pendiente, en_progreso, resuelto, cancelado, escalado}
    
    public estado_actual EstadoActual { get; set; }

    public enum nivel_privacidad {publico, privado, interno}
    
    public nivel_privacidad NivelPrivacidad { get; set; }
    
    public DateTime horario_inicio { get; set; }
    public string horario_inicio_en => horario_inicio.ToString("HH:mm:ss");
    
    public DateTime horario_fin { get; set; }
    public string horario_fin_en => horario_fin.ToString("HH:mm:ss");
    
    public DateTime creado_en { get; set; }
    
    public DateTime modificado_en { get; set; }
    
    public DateTime borrado_en { get; set; }
}