using Microsoft.EntityFrameworkCore;
using ExamenParcial.Models;

namespace ExamenParcial.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<ProductoModel> Productos { get; set; }
        public DbSet<VentaModel> Ventas { get; set; }
        public DbSet<VentaDetalleModel> VentaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductoModel>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<VentaDetalleModel>()
                .Property(p => p.PrecioUnitario)
                .HasPrecision(18, 2);
        }
    }
}
