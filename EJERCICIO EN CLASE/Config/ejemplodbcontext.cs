using Microsoft.EntityFrameworkCore;
using EJERCICIO_EN_CLASE.Models;

namespace EJERCICIO_EN_CLASE.Config
{
    public class ejemplodbcontext : DbContext
    {
        public ejemplodbcontext(DbContextOptions contexto) : base(contexto) { }


        public DbSet<ClienteModel> Clientes { get; set; }
    }
}
