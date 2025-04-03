using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamenParcial.Models;
using ExamenParcial.Data;
using ExamenParcial.Models.ViewModel;

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
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .ToListAsync();
            return View(ventas);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.VentaId == id);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }


        public IActionResult Create()
        {
            var vm = new VentaViewModel() { Clientes=_context.Clientes.ToList() , Venta=new(), Productos = _context.Productos.ToList()};
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VentaViewModel model, int[] ProductoIds, int[] Cantidades)
        {
            if (ModelState.IsValid)
            {
                var venta = model.Venta;
                venta.CreatedAt = DateTime.Now;
                venta.UpdatedAt = DateTime.Now;

                decimal total = 0;
                var detalles = new List<VentaDetalle>();
                for (int i = 0; i < ProductoIds.Length; i++)
                {
                    var producto = _context.Productos.Find(ProductoIds[i]);
                    if (producto != null)
                    {
                        var cantidad = Cantidades[i];
                        var subtotal = producto.Precio * cantidad;
                        total += subtotal;

                        detalles.Add(new VentaDetalle
                        {
                            ProductoId = producto.ProductoId,
                            Cantidad = cantidad,
                            PrecioUnitario = producto.Precio,
                            Subtotal = subtotal
                        });
                    }
                }

                venta.Total = total;
                venta.VentaDetalle = detalles;

                _context.Ventas.Add(venta);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            model.Clientes = _context.Clientes.ToList();
            model.Productos = _context.Productos.ToList();
            return View(model);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VentaId,ClienteId,FechaVenta")] Venta venta)
        {
            if (id != venta.VentaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.VentaId))
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
            return View(venta);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
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
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.VentaId == id);
        }
    }
}
