using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen_Parcial2.Controllers
{
    //[Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/GestionUsuarios
        public async Task<IActionResult> GestionUsuarios()
        {
            var usuarios = _userManager.Users.ToList();

            
            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                ViewData[$"Roles_{usuario.Id}"] = string.Join(", ", roles);
            }

            return View(usuarios);
        }

        // GET: Admin/AsignarRol/id
        public async Task<IActionResult> AsignarRol(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var rolesUsuario = await _userManager.GetRolesAsync(usuario);
            var todosRoles = _roleManager.Roles.ToList();

            ViewData["Roles"] = new SelectList(todosRoles, "Name", "Name");
            ViewData["UsuarioId"] = id;
            ViewData["UsuarioEmail"] = usuario.Email;
            ViewData["RolesActuales"] = string.Join(", ", rolesUsuario);

            return View();
        }

        // POST: Admin/AsignarRol
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarRol(string usuarioId, string rol)
        {
            if (string.IsNullOrEmpty(usuarioId) || string.IsNullOrEmpty(rol))
            {
                return BadRequest();
            }

            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null)
            {
                return NotFound();
            }

            var rolExiste = await _roleManager.RoleExistsAsync(rol);
            if (!rolExiste)
            {
                ModelState.AddModelError("", $"El rol {rol} no existe.");
                var todosRoles = _roleManager.Roles.ToList();
                ViewData["Roles"] = new SelectList(todosRoles, "Name", "Name");
                ViewData["UsuarioId"] = usuarioId;
                ViewData["UsuarioEmail"] = usuario.Email;
                return View();
            }

            
            var tieneRol = await _userManager.IsInRoleAsync(usuario, rol);
            if (tieneRol)
            {
                ModelState.AddModelError("", $"El usuario ya tiene el rol {rol}.");
                var todosRoles = _roleManager.Roles.ToList();
                ViewData["Roles"] = new SelectList(todosRoles, "Name", "Name");
                ViewData["UsuarioId"] = usuarioId;
                ViewData["UsuarioEmail"] = usuario.Email;
                var rolesUsuario = await _userManager.GetRolesAsync(usuario);
                ViewData["RolesActuales"] = string.Join(", ", rolesUsuario);
                return View();
            }

           
            var resultado = await _userManager.AddToRoleAsync(usuario, rol);
            if (resultado.Succeeded)
            {
                return RedirectToAction(nameof(GestionUsuarios));
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            var roles = _roleManager.Roles.ToList();
            ViewData["Roles"] = new SelectList(roles, "Name", "Name");
            ViewData["UsuarioId"] = usuarioId;
            ViewData["UsuarioEmail"] = usuario.Email;
            var usuarioRoles = await _userManager.GetRolesAsync(usuario);
            ViewData["RolesActuales"] = string.Join(", ", usuarioRoles);

            return View();
        }

        
        public async Task<IActionResult> QuitarRol(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var rolesUsuario = await _userManager.GetRolesAsync(usuario);

            ViewData["Roles"] = new SelectList(rolesUsuario);
            ViewData["UsuarioId"] = id;
            ViewData["UsuarioEmail"] = usuario.Email;

            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuitarRol(string usuarioId, string rol)
        {
            if (string.IsNullOrEmpty(usuarioId) || string.IsNullOrEmpty(rol))
            {
                return BadRequest();
            }

            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null)
            {
                return NotFound();
            }

          
            var tieneRol = await _userManager.IsInRoleAsync(usuario, rol);
            if (!tieneRol)
            {
                ModelState.AddModelError("", $"El usuario no tiene el rol {rol}.");
                var rolesUsuario = await _userManager.GetRolesAsync(usuario);
                ViewData["Roles"] = new SelectList(rolesUsuario);
                ViewData["UsuarioId"] = usuarioId;
                ViewData["UsuarioEmail"] = usuario.Email;
                return View();
            }

        
            var resultado = await _userManager.RemoveFromRoleAsync(usuario, rol);
            if (resultado.Succeeded)
            {
                return RedirectToAction(nameof(GestionUsuarios));
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            var usuarioRoles = await _userManager.GetRolesAsync(usuario);
            ViewData["Roles"] = new SelectList(usuarioRoles);
            ViewData["UsuarioId"] = usuarioId;
            ViewData["UsuarioEmail"] = usuario.Email;

            return View();
        }
    }
}
