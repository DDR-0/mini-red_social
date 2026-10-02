using Microsoft.EntityFrameworkCore;
using mini_red_social1.Models;

namespace mini_red_social1.Data
{



    public class SocialDBcontext : DbContext
    {
        public SocialDBcontext(DbContextOptions<SocialDBcontext> options) : base(options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Historial_interaccion>()
                .HasOne(h => h.id_interaccion)
                .WithMany()
                .HasForeignKey(h => h.interaccion_id)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Historial_interaccion>()
                .HasOne(h=> h.usuario_id)
                .WithMany()
                .HasForeignKey(h => h.cambio_por_usuario)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Snapshot_diaria>()
                .Property(s => s.frecuencia_uso_recursos_usados)
                .HasPrecision(9, 6);
        }
    }
}