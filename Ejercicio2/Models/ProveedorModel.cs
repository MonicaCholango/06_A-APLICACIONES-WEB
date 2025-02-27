using System.ComponentModel.DataAnnotations;

namespace Ejercicio2.Models
{
    public class ProveedorModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre de la Empresa")]
        [MaxLength(200, ErrorMessage = "El longitud maximo es de 200 caracteres")]
        public string NombreEmpresa { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Direccion de la Empresa")]
        [MaxLength(200, ErrorMessage = "El longitud maximo es de 200 caracteres")]
        public string DireccionEmpresa { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Telefono de la Empresa")]
        [MaxLength(200, ErrorMessage = "El longitud maximo es de 200 caracteres")]
        public string TelefonoEmpresa { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "RUC de la Empresa")]
        [MaxLength(200, ErrorMessage = "El longitud maximo es de 200 caracteres")]
        public string RUCEmpresa { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Correo de la Empresa")]
        [MaxLength(200, ErrorMessage = "El longitud maximo es de 200 caracteres")]
        public string CorreoEmpresa { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre de Contacto")]
        [MaxLength(200, ErrorMessage = "El longitud maximo es de 200 caracteres")]
        public string NombreContacto { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Telefono de Contacto")]
        [MaxLength(200, ErrorMessage = "El longitud maximo es de 200 caracteres")]
        public string TelefonoContacto { get; set; }

    }
}