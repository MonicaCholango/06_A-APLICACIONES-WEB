using Ejercicio2.Models;
using Microsoft.EntityFrameworkCore;


namespace Ejercicio2.Config
{
    public class NetCoreDbContext : DbContext
    {
        public NetCoreDbContext(DbContextOptions<NetCoreDbContext> options) : base(options)
        {

        }

        /*stock*/

        public DbSet<ProductosModel> Productos { get; set; }
        public DbSet<ProveedorModel> Proveedores { get; set; }
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
    }
}