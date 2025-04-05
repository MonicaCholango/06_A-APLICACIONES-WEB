using Microsoft.AspNetCore.Identity;
//UUID
using System.ComponentModel.DataAnnotations;

namespace Examen_Parcial2.Models
{
    public class UsuariosModel : IdentityUser
    {
        [Required]
        public string Cedula { get; set; }
    }
}
