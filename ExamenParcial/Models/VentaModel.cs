using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenParcial.Models
{
    public class VentaModel
    {
        [Key]
        public int VentaId { get; set; }

        [Required(ErrorMessage = "La fecha de la venta es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public ClienteModel Cliente { get; set; }

        public ICollection<VentaDetalleModel> Detalles { get; set; } = new List<VentaDetalleModel>();
    }
}