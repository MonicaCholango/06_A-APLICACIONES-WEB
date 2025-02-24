namespace Clase_2.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Cedula_RUC { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int? Edad { get; set; }
        public bool Genero { get; set; }
        public DateOnly? Fecha_Nacimiento { get; set; }
    }
}