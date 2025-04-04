using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen_Parcial2.Models
{
    public class Patrocinador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El tipo de patrocinio es obligatorio")]
        [StringLength(50, ErrorMessage = "El tipo de patrocinio no puede exceder los 50 caracteres")]
        [Display(Name = "Tipo de patrocinio")]
        public string TipoPatrocinio { get; set; }

        public ICollection<EventoPatrocinador> EventosPatrocinadores { get; set; }
    }
}
