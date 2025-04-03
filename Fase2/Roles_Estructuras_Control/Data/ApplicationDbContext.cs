using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Roles_Estructuras_Control.Models;

namespace Roles_Estructuras_Control.Data
{
    public class ApplicationDbContext : IdentityDbContext<UsuariosModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProveedoresModels> Proveedores { get; set; }
        public DbSet<ProductoModels> Productos { get; set; }
        public DbSet<StockModels> Stocks { get; set; }

        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<FacturaModel> Facturas { get; set; }
        public DbSet<DetalleFacturaModel> DetalleFactura { get; set; }
    }
}
