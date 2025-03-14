using Microsoft.EntityFrameworkCore;
using ExamenParcial.Models;

namespace ExamenParcial.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Corregido: Añadido tipo genérico a los DbSet
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Corregido: Añadido tipo genérico a Entity()
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Producto>()
                .Property(p => p.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Producto>()
                .Property(p => p.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.Subtotal)
                .HasColumnType("decimal(18,2)");

            // Añadido: Configuración para Cliente
            modelBuilder.Entity<Cliente>()
                .Property(c => c.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Cliente>()
                .Property(c => c.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            // Añadido: Configuración de relaciones
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VentaDetalle>()
                .HasOne(vd => vd.Venta)
                .WithMany(v => v.VentaDetalle)
                .HasForeignKey(vd => vd.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VentaDetalle>()
                .HasOne(vd => vd.Productos)
                .WithMany(p => p.DetalleVenta)
                .HasForeignKey(vd => vd.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}