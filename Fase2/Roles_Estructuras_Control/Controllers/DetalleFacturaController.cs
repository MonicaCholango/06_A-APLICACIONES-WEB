using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roles_Estructuras_Control.Data;
using Roles_Estructuras_Control.Models;

namespace Roles_Estructuras_Control.Controllers
{
    public class DetalleFacturaController(ApplicationDbContext context) : Controller
    {
        // GET: DetalleFactura
        public async Task<IActionResult> Index()
        {
            return View();
        }


        //# Nombre_Productos, Catidad, Precio Unitario, Total



        // GET: DetalleFactura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFacturaModel = await context.DetalleFactura
                .Include(d => d.FacturaModel)
                .Include(d => d.ProductoModels)
                .Include(d => d.StockModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleFacturaModel == null)
            {
                return NotFound();
            }

            return View(detalleFacturaModel);
        }

        // GET: DetalleFactura/Create
        public IActionResult Create()
        {
            ViewData["FacturaModelId"] = new SelectList(context.Facturas, "Id", "Id");
            ViewData["ProductoModelsId"] = new SelectList(context.Productos, "Id", "NombreProducto");
            ViewData["StockModelsId"] = new SelectList(context.Stocks, "Id", "Id");
            return View();
        }

        // POST: DetalleFactura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cantidad,valor,ProductoModelsId,FacturaModelId,StockModelsId")] DetalleFacturaModel detalleFacturaModel)
        {
            if (ModelState.IsValid)
            {
                context.Add(detalleFacturaModel);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaModelId"] = new SelectList(context.Facturas, "Id", "Id", detalleFacturaModel.FacturaModelId);
            ViewData["ProductoModelsId"] = new SelectList(context.Productos, "Id", "NombreProducto", detalleFacturaModel.ProductoModelsId);
            ViewData["StockModelsId"] = new SelectList(context.Stocks, "Id", "Id", detalleFacturaModel.StockModelsId);
            return View(detalleFacturaModel);
        }

        // GET: DetalleFactura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFacturaModel = await context.DetalleFactura.FindAsync(id);
            if (detalleFacturaModel == null)
            {
                return NotFound();
            }
            ViewData["FacturaModelId"] = new SelectList(context.Facturas, "Id", "Id", detalleFacturaModel.FacturaModelId);
            ViewData["ProductoModelsId"] = new SelectList(context.Productos, "Id", "NombreProducto", detalleFacturaModel.ProductoModelsId);
            ViewData["StockModelsId"] = new SelectList(context.Stocks, "Id", "Id", detalleFacturaModel.StockModelsId);
            return View(detalleFacturaModel);
        }

        // POST: DetalleFactura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,valor,ProductoModelsId,FacturaModelId,StockModelsId")] DetalleFacturaModel detalleFacturaModel)
        {
            if (id != detalleFacturaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(detalleFacturaModel);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleFacturaModelExists(detalleFacturaModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaModelId"] = new SelectList(context.Facturas, "Id", "Id", detalleFacturaModel.FacturaModelId);
            ViewData["ProductoModelsId"] = new SelectList(context.Productos, "Id", "NombreProducto", detalleFacturaModel.ProductoModelsId);
            ViewData["StockModelsId"] = new SelectList(context.Stocks, "Id", "Id", detalleFacturaModel.StockModelsId);
            return View(detalleFacturaModel);
        }

        // GET: DetalleFactura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFacturaModel = await context.DetalleFactura
                .Include(d => d.FacturaModel)
                .Include(d => d.ProductoModels)
                .Include(d => d.StockModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleFacturaModel == null)
            {
                return NotFound();
            }

            return View(detalleFacturaModel);
        }

        // POST: DetalleFactura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleFacturaModel = await context.DetalleFactura.FindAsync(id);
            if (detalleFacturaModel != null)
            {
                context.DetalleFactura.Remove(detalleFacturaModel);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleFacturaModelExists(int id)
        {
            return context.DetalleFactura.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarFactura([FromBody] JsonElement data)
        {
            try
            {
                // Validar que se recibieron datos
                if (data.ValueKind == JsonValueKind.Null)
                {
                    return Json(new { success = false, message = "No se recibieron datos de la factura" });
                }

                // Obtener el último número de factura
                var ultimaFactura = await context.Facturas
                    .OrderByDescending(f => f.NumeroFacrtura)
                    .FirstOrDefaultAsync();

                int nuevoNumeroFactura = 1;
                if (ultimaFactura != null)
                {
                    nuevoNumeroFactura = ultimaFactura.NumeroFacrtura + 1;
                }

                // Validar datos del cliente
                var clienteId = data.GetProperty("cliente").GetProperty("id").GetString();
                if (string.IsNullOrEmpty(clienteId))
                {
                    return Json(new { success = false, message = "El ID del cliente es requerido" });
                }

                // Crear la factura
                var factura = new FacturaModel
                {
                    FechaIngreso = DateOnly.FromDateTime(DateTime.Now),
                    NumeroFacrtura = nuevoNumeroFactura,
                    ClientesModelId = int.Parse(clienteId)
                };

                context.Facturas.Add(factura);
                await context.SaveChangesAsync();

                // Obtener el usuario actual
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, message = "No se pudo obtener el ID del usuario" });
                }

                // Procesar cada producto
                var productos = data.GetProperty("productos").EnumerateArray();
                if (!productos.Any())
                {
                    return Json(new { success = false, message = "La factura debe contener al menos un producto" });
                }

                foreach (var producto in productos)
                {
                    var nombreProducto = producto.GetProperty("nombreProducto").GetString().Trim();
                    var stock = await context.Stocks
                        .Include(s => s.ProductoModels)
                        .FirstOrDefaultAsync(s => s.ProductoModels.NombreProducto.Trim() == nombreProducto);

                    if (stock == null)
                    {
                        return Json(new { success = false, message = $"No se encontró el producto: {nombreProducto}" });
                    }

                    // Convertir cantidad a número de manera segura
                    var cantidadElement = producto.GetProperty("cantidad");
                    int cantidad;
                    if (cantidadElement.ValueKind == JsonValueKind.Number)
                    {
                        cantidad = cantidadElement.GetInt32();
                    }
                    else
                    {
                        cantidad = int.Parse(cantidadElement.GetString());
                    }

                    if (stock.Cantidad < cantidad)
                    {
                        return Json(new { success = false, message = $"No hay suficiente stock para el producto: {nombreProducto}" });
                    }

                    // Convertir precio unitario a número de manera segura
                    var precioElement = producto.GetProperty("precioUnitario");
                    float precioUnitario;
                    if (precioElement.ValueKind == JsonValueKind.Number)
                    {
                        precioUnitario = precioElement.GetSingle();
                    }
                    else
                    {
                        precioUnitario = float.Parse(precioElement.GetString());
                    }

                    // Crear el detalle de factura
                    var detalleFactura = new DetalleFacturaModel
                    {
                        Cantidad = cantidad,
                        valor = precioUnitario,
                        ProductoModelsId = stock.ProductoModelsId,
                        FacturaModelId = factura.Id,
                        StockModelsId = stock.Id,
                        IdentityUserId = userId
                    };

                    context.DetalleFactura.Add(detalleFactura);

                    // Actualizar el stock
                    stock.Cantidad -= cantidad;
                    context.Stocks.Update(stock);
                }

                await context.SaveChangesAsync();

                return Json(new { success = true, message = "Factura guardada exitosamente", numeroFactura = nuevoNumeroFactura });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al guardar la factura: " + ex.Message });
            }
        }
    }
}
