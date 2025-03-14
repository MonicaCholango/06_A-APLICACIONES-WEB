using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenParcial.Models
{
    [Table("Ventas")]
    public class Venta
    {
        [Key]
        [Column("venta_id")]
        public int VentaId { get; set; }

        [Column("cliente_id")]
        [Display(Name = "Cliente")]
        public int? ClienteId { get; set; }

        [Column("fecha_venta")]
        [Display(Name = "Fecha de Venta")]
        public DateTime FechaVenta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El total de la venta es obligatorio")]
        [Column("total")]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser mayor o igual a 0")]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Column("estado")]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "pendiente";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ClienteId")]
        public virtual Cliente? Cliente { get; set; }

        // Corregido: Añadido tipo genérico a ICollection
        public virtual ICollection<VentaDetalle>? VentaDetalle { get; set; }
    }
}