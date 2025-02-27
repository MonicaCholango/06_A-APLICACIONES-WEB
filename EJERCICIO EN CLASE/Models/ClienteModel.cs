using System.Security.Cryptography;

namespace EJERCICIO_EN_CLASE.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Cedula_RUC { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }


        //propiedad opcional
        public int? Edad { get; set; }

        public bool Genero { get; set; } = false;
        public DateOnly? Fecha_Nacimiento { get; set; }



        //DateTime   =>   2025/02/17T 19:12:25.21255
        //Date => 2025/02/17
        //Time


    }
}


