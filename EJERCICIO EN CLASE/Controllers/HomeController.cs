using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EJERCICIO_EN_CLASE.Models;

namespace EJERCICIO_EN_CLASE.Controllers;


    public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;



    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string cedula, string nombre, string apellido, int edad)
    {
        List<ClienteModel> _listaCliente = new List<ClienteModel>();
        var cliente = new ClienteModel
        {
            Apellido = apellido,
            Cedula_RUC = cedula,
            Direccion = "Quito",
            Edad = edad,
            Genero = true,
            Nombre = nombre,
            Telefono = "0988333720",
            Id = 1
        };
        _listaCliente.Add(cliente);
        return View(_listaCliente);
    }

    public IActionResult Privacy(string nombre, string apellido, int edad, int? estado)
    {
        int _estado = estado == null ? 0 : (int)estado;

        var cliente = new ClienteModel
        {
            Apellido = apellido,
            Cedula_RUC="1752135911",
            Direccion="Ambato",
            Edad=edad,
            Genero=true,
            Nombre=nombre,
            Telefono="098765432",
            Id=1
        };
        return View(cliente);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
