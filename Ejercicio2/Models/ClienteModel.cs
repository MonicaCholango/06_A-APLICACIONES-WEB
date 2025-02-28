using System.ComponentModel.DataAnnotations;

namespace Ejercicio2.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        //[MaxLength(10, ErrorMessage ="La cantidad maxima es 10 caracteres")]
        [Display(Name = "Cédula o RUC")]
        [Length(10, 13, ErrorMessage = "El valor del campo es entre 10 y 13 caracteres")]
        public string Cedula_RUC { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        //[MaxLength(10, ErrorMessage ="La cantidad maxima es 10 caracteres")]
        [Display(Name = "Nombre")]
        [Length(10, 13, ErrorMessage = "El valor del campo es entre 10 y 13 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        //[MaxLength(10, ErrorMessage ="La cantidad maxima es 10 caracteres")]
        [Display(Name = "Apellido")]
        [Length(10, 13, ErrorMessage = "El valor del campo es entre 10 y 13 caracteres")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        //[MaxLength(10, ErrorMessage ="La cantidad maxima es 10 caracteres")]
        [Display(Name = "Direccion")]
        [Length(10, 13, ErrorMessage = "El valor del campo es entre 10 y 13 caracteres")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        //[MaxLength(10, ErrorMessage ="La cantidad maxima es 10 caracteres")]
        [Display(Name = "Telefono")]
        [Length(10, 13, ErrorMessage = "El valor del campo es entre 10 y 13 caracteres")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        //[MaxLength(10, ErrorMessage ="La cantidad maxima es 10 caracteres")]
        [Display(Name = "Edad")]
        [Length(1, 3, ErrorMessage = "El valor del campo es entre 1 y 3 caracteres")]
        public int edad { get; set; }
    }
}