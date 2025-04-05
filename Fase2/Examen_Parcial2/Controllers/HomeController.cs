using System.Diagnostics;
using Examen_Parcial2.Data;
using Examen_Parcial2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen_Parcial2.Controllers;

public class HomeController : Controller
{

    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
        try
        {
            // Obtener la fecha actual
            var fechaActual = DateTime.Now;
                
            // Consultar eventos próximos (eventos a partir de hoy)
            // Ordenados por fecha, los más próximos primero
            var proximosEventos = await _context.Eventos
                .Include(e => e.Lugar) // Incluir la relación con Lugar
                .Where(e => e.Fecha >= fechaActual)
                .OrderBy(e => e.Fecha)
                .Take(5) // Limitar a 5 eventos próximos
                .ToListAsync();
                
            // Pasar los eventos a la vista mediante ViewBag
            ViewBag.ProximosEventos = proximosEventos;
                
            // También puedes pasar otros datos relevantes para tu página principal
            // Por ejemplo, contadores o estadísticas
            ViewBag.TotalEventos = await _context.Eventos.CountAsync();
            ViewBag.TotalLugares = await _context.Lugares.CountAsync();
            ViewBag.TotalParticipantes = await _context.Participantes.CountAsync();
            ViewBag.TotalPatrocinadores = await _context.Patrocinadores.CountAsync();
                
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar los eventos próximos");
            ViewBag.ProximosEventos = null;
            return View();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
