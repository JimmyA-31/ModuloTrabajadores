using Microsoft.EntityFrameworkCore;
using ModuloTrabajadores.Models;

namespace ModuloTrabajadores.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trabajador> Trabajadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapear la tabla si quieres nombre exacto
            modelBuilder.Entity<Trabajador>()
                        .ToTable("Trabajadores");
        }
    }
}
