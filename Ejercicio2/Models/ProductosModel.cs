using System.ComponentModel.DataAnnotations;

namespace Ejercicio2.Models

{
    public class ProductosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del Producto es requerido")]
        [Display(Name = "Nombre del Producto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La unidad de medida es requerida")]
        [Display(Name = "Unidad de Medida")]
        public string UnidadMedida { get; set; }

        [Required(ErrorMessage = "El numero de registro sanitario es requerido")]
        [Display(Name = "Registro Sanitario")]
        public string RegSanitario { get; set; }


        [Required(ErrorMessage = "El codigo de barras es requerida")]
        [Display(Name = "Codigo de Barras")]
        [Length(minimumLength: 5, maximumLength: 22,
            ErrorMessage = "El coodigo de barras debe tener minimo 5 caraccterers y maximo 22")]

        public string CodigoBarras { get; set; }
    }
}