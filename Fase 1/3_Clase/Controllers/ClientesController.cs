using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Clase_3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Clase_3.Controllers;


    public class ClientesController : Controller
{
    private static List<ClienteModel> clientes = new List<ClienteModel>();

    public IActionResult Index()
    {
        return View(clientes);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(ClienteModel cliente)
    {
        cliente.Id = clientes.Count + 1;
        clientes.Add(cliente);
        return RedirectToAction("Index");
    }

    public IActionResult Detalle(int id)
    {
        var cliente = clientes.FirstOrDefault(c => c.Id == id);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

    public IActionResult Editar(int id)
    {
        var cliente = clientes.FirstOrDefault(c => c.Id == id);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

    [HttpPost]
    public IActionResult Editar(ClienteModel clienteEditado)
    {
        var cliente = clientes.FirstOrDefault(c => c.Id == clienteEditado.Id);
        if (cliente == null) return NotFound();

        cliente.Cedula_RUC = clienteEditado.Cedula_RUC;
        cliente.Nombre = clienteEditado.Nombre;
        cliente.Apellido = clienteEditado.Apellido;
        cliente.Edad = clienteEditado.Edad;
        cliente.Direccion = clienteEditado.Direccion;
        cliente.Telefono = clienteEditado.Telefono;
        cliente.Genero = clienteEditado.Genero;

        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id)
    {
        var cliente = clientes.FirstOrDefault(c => c.Id == id);
        if (cliente != null)
        {
            clientes.Remove(cliente);
        }
        return RedirectToAction("Index");
    }
}

