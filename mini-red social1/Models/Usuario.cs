namespace mini_red_social1.Models;

public class Usuario
{
    public int id { get; set; }
    
    public string nombre { get; set; }
    
    public string apellido { get; set; }
    
    public string correo { get; set; }
    
    public string descripcion { get; set; }

    public string contrasena_hash { get; set; }

    public enum estado { Activo, Inactivo, Pendiente }
    
    public estado status { get; set; }

    public string visibilidad { get; set; }
    
    public DateTime creado_en { get; set; }

    public DateTime modificado_en { get; set; }

    public DateTime borrado_en { get; set; }


}