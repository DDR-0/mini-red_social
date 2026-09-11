namespace mini_red_social1.Models;

public class Tipo_Interaccion
{
    public int id { get; set; }

    public string nombre { get; set; }

    public string descripcion { get; set; }
    
    public bool requiere_recurso  { get; set; }
    
    public bool esta_activo { get; set; }

    public DateTime creado_en { get; set; }
    
    public DateTime modificado_en { get; set; }
}