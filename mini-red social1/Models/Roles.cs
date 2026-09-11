namespace mini_red_social1.Models;

public class Roles
{
    public int id { get; set; }
    
    public string nombre { get; set; }
    
    public string descripcion { get; set; }

    public bool activo { get; set; }
    
    public DateTime creacion { get; set; }
    
    public DateTime modificacion { get; set; }
}