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
    
}