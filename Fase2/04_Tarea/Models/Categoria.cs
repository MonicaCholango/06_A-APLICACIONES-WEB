using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _04_Tarea.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ubicación es obligatoria")]
        [StringLength(50, ErrorMessage = "La ubicación no puede exceder 50 caracteres")]
        public string Ubicacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad de libros es obligatoria")]
        [Range(0, 1000, ErrorMessage = "La cantidad debe estar entre 0 y 1000")]
        public int CantidadLibros { get; set; } = 0;

        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}