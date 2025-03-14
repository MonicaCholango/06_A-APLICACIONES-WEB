using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenParcial.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        [Column("cliente_id")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [StringLength(50)]
        [Column("nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del cliente es obligatorio")]
        [StringLength(50)]
        [Column("apellido")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electr�nico no es v�lido")]
        [Column("email")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "El formato del tel�fono no es v�lido")]
        [Column("telefono")]
        [Display(Name = "Tel�fono")]
        public string? Telefono { get; set; }

        [Column("direccion")]
        [Display(Name = "Direcci�n")]
        public string? Direccion { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public virtual ICollection<Venta>? Ventas { get; set; }
    }
}