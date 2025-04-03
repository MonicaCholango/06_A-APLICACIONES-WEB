using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _07_Tarea.Models; 

namespace _07_Tarea.Data
{
    public class ApplicationDbContext : IdentityDbContext<suariosModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProveedoresModel> Proveedores { get; set; }
        public DbSet<ProductosModel> Productos { get; set; }
        public DbSet<StockModels> Stocks { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<FacturaModel> Facturas { get; set; }
        public DbSet<DetalleFacturaModel> DetalleFactura { get; set; }
        public DbSet<suariosModel> Usuarios { get; set; }
    }
}
