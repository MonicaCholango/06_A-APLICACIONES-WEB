using System.ComponentModel.DataAnnotations;

namespace Ejercicio2.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        //relacion entre la tablas
        //relacion uno a varios con productos
        [Display(Name = "Productos")]
        public int ProductosModelId { get; set; }
        public ProductosModel ProductosModel { get; set; }
        //relacion uno a varios con proveedoes
        [Display(Name = "Proveedores")]

        public int ProveedorModelId { get; set; }
        public ProveedorModel ProveedorModel { get; set; }
        public int Cantidad { get; set; }
        public DateOnly FechaCaducidad { get; set; }
        public DateOnly FechaElaboracion { get; set; }
        public bool IVA { get; set; }
        public int DetalleIVA { get; set; }
    }
}