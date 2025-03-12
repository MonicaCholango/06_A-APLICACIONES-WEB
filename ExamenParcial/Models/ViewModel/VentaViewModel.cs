using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamenParcial.Models.ViewModel
{
    public class VentaViewModel
    {
        public Venta Venta { get; set; }
        public List<DetalleVentaViewModel> DetalleVentas { get; set; } = new List<DetalleVentaViewModel>();
        public SelectList Clientes { get; set; }
        public SelectList Productos { get; set; }
        public int? SelectedClienteId { get; set; }
        public int? SelectedProductoId { get; set; }
        public int? Cantidad { get; set; }
    }

    public class DetalleVentaViewModel
    {
        public int? ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}