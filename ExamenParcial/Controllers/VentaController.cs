using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamenParcial.Data;
using ExamenParcial.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ExamenParcial.Controllers
{
    public class VentaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas.Include(v => v.Cliente).ToListAsync();
            return View(ventas);
        }

        public IActionResult Create()
        {
            ViewData["Clientes"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            ViewData["Productos"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Total")] Venta venta, List<int> productoIds, List<int> cantidades)
        {
            if (ModelState.IsValid)
            {
                decimal total = 0;
                venta.VentaDetalles = new List<VentaDetalle>();

                for (int i = 0; i < productoIds.Count; i++)
                {
                    var productoId = productoIds[i];
                    var cantidad = cantidades[i];
                    var producto = await _context.Productos.FindAsync(productoId);
                    if (producto != null)
                    {
                        total += producto.Precio * cantidad;
                        venta.VentaDetalles.Add(new VentaDetalle
                        {
                            ProductoId = productoId,
                            Cantidad = cantidad,
                            Precio = producto.Precio
                        });
                    }
                }

                venta.Total = total;
                _context.Add(venta);

                // Establecer la relación entre la venta y los detalles antes de guardar
                foreach (var detalle in venta.VentaDetalles)
                {
                    detalle.Venta = venta; // Establecer la relación
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Clientes"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", venta.ClienteId);
            ViewData["Productos"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View(venta);
        }

        public async Task<IActionResult> Details(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.VentaDetalles)
                .ThenInclude(vd => vd.Producto)
                .FirstOrDefaultAsync(m => m.VentaId == id);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}   
