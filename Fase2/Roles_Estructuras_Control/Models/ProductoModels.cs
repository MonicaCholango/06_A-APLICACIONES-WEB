using System.ComponentModel.DataAnnotations;

namespace Roles_Estructuras_Control.Models
{
    public class ProductoModels
    {
        public int Id { get; set; }
        [Display(Name ="Nombre del Producto")]
        [MinLength(3)]
        [Required(ErrorMessage = "El campo es requiro")]
        public string NombreProducto { get; set; }
        [Display(Name = "Presentación del Productos")]
        [MinLength(3)]
        [Required(ErrorMessage = "El campo es requiro")]
        public string Presentacion { get; set; }
        [Display(Name = "Codigo de Brras")]
        [MinLength(5)]
        
        public string CodigoBarras { get; set; }
    }
}
