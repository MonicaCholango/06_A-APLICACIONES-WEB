using System.ComponentModel.DataAnnotations;

namespace Examen_Parcial2.Models
{
    public class Lugar
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public ICollection<Evento> Eventos { get; set; }
    }
}
