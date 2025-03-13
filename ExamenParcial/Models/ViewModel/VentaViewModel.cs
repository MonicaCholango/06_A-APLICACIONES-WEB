using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamenParcial.Models.ViewModel
{
    public class VentaViewModel
    {
        public Venta Venta { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<Producto> Productos { get; set; }
        public int? Cantidad { get; set; }
    }

}