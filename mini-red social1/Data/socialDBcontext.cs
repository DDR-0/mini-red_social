using Microsoft.EntityFrameworkCore;
using mini_red_social1.Models;

namespace mini_red_social1.Data
{



    public class socialDBcontext : DbContext
    {
        public socialDBcontext(DbContextOptions<socialDBcontext> options) : base(options)
        {
        }

        public DbSet<Recurso> Recursos { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Tipo_Interaccion> TiposInteraccion { get; set; } = null!;
        public DbSet<Interaccion_social> Interacciones { get; set; } = null!;
        public DbSet<Historial_interaccion> HistorialInteracciones { get; set; } = null!;
        public DbSet<Uso_recurso> UsosRecurso { get; set; } = null!;
        public DbSet<Registro_auditoria> RegistrosAuditoria { get; set; } = null!;
        public DbSet<Snapshot_diaria> SnapshotsDiarios { get; set; } = null!;

    }
}