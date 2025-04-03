using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roles_Estructuras_Control.Data;
using Roles_Estructuras_Control.Models;

namespace Roles_Estructuras_Control.Controllers
{
    public class FacturasController(ApplicationDbContext context) : Controller
    {
        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View(await context.Facturas.Include(a => a.ClientesModel).ToListAsync());
        }
    }
}