using _4_Clase.Models;
using Microsoft.EntityFrameworkCore;    

namespace _4_Clase.Data
{
    public class Get_PostDbContext : DbContext
    {
        public Get_PostDbContext(DbContextOptions db) : base(db)
        {
            //Conectar con una API de IA
        }

        public DbSet<TipoPreguntaModel> TipoPreguntas { get; set; }
        public DbSet<PreguntasModel> Preguntas { get; set; }

    }
}
