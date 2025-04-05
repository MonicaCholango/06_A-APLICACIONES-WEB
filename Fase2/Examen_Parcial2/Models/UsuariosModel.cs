using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
//UUID

namespace Examen_Parcial2.Models
{
    public class UsuariosModel : IdentityUser
    {
        [Required]
        public string Cedula { get; set; }
    }
}
