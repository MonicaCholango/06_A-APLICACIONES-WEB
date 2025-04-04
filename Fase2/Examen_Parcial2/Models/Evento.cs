using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen_Parcial2.Models
{
    public class Evento
    {
            public int Id { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio")]
            [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "La fecha es obligatoria")]
            [Display(Name = "Fecha del evento")]
            [DataType(DataType.DateTime)]
            public DateTime Fecha { get; set; }

            [Required(ErrorMessage = "La descripción es obligatoria")]
            [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
            public string Descripcion { get; set; }

            [Required(ErrorMessage = "El lugar es obligatorio")]
            [Display(Name = "Lugar")]
            public int LugarId { get; set; }
            public Lugar Lugar { get; set; }

            public ICollection<EventoParticipante> EventosParticipantes { get; set; }
            public ICollection<EventoPatrocinador> EventosPatrocinadores { get; set; }
    }
}
