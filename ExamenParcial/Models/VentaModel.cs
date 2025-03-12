namespace ExamenParcial.Models 
{
    public class Venta
    {
        public int VentaId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Total { get; set; }
        public List<VentaDetalle> VentaDetalles { get; set; }
    }
}
