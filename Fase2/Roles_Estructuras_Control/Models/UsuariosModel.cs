using Microsoft.AspNetCore.Identity;
//UUID
using System.ComponentModel.DataAnnotations;

namespace Roles_Estructuras_Control.Models
{
    public class UsuariosModel : IdentityUser
    {
        [Required]
        public string Cedula { get; set; }
    }
}
