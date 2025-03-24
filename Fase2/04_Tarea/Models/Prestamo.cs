using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _04_Tarea.Models
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de préstamo es obligatoria")]
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La fecha de devolución es obligatoria")]
        public DateTime FechaDevolucion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder 50 caracteres")]
        public string Estado { get; set; } = "Prestado";

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "El libro es obligatorio")]
        public int LibroId { get; set; }

        public Usuario Usuario { get; set; } = null!;   
        public Libro Libro { get; set; } = null!;
        public ICollection<Renovacion> Renovaciones { get; set; } = new List<Renovacion>();
    }
}