using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _04_Tarea.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El título debe tener entre 2 y 100 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El autor es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El autor debe tener entre 2 y 100 caracteres")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año de publicación es obligatorio")]
        [Range(1000, 2100, ErrorMessage = "El año debe estar entre 1000 y 2100")]
        public int AnioPublicacion { get; set; }

        [Required(ErrorMessage = "La disponibilidad es obligatoria")]
        public bool Disponible { get; set; } = true;

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; } = null!;
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}