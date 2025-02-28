using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ejercicio2.Config;    
using Ejercicio2.Models;    

namespace Ejercicio2.Controllers
{
    public class StockController : Controller
    {
        private readonly NetCoreDbContext _context;

        public StockController(NetCoreDbContext context)
        {
            _context = context;
        }

        // GET: Stock
        public async Task<IActionResult> Index()
        {
            var NetCoreDbContext = _context.Stocks.Include(s => s.ProductosModel).Include(s => s.ProveedorModel);
            return View(await NetCoreDbContext.ToListAsync());
        }

        // GET: Stock/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockModel = await _context.Stocks
                .Include(s => s.ProductosModel)
                .Include(s => s.ProveedorModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return View(stockModel);
        }

        // GET: Stock/Create
        public IActionResult Create()
        {
            ViewData["ProductosModelId"] = new SelectList(_context.Productos, "Id", "CodigoBarras");
            ViewData["ProveedorModelId"] = new SelectList(_context.Proveedores, "Id", "CorreoEmpresa");
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductosModelId,ProveedorModelId,Cantidad,FechaCaducidad,FechaElaboracion,IVA,DetalleIVA")] StockModel stockModel)
        {
            // if (ModelState.IsValid)
            //{
            _context.Add(stockModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            ViewData["ProductosModelId"] = new SelectList(_context.Productos, "Id", "CodigoBarras", stockModel.ProductosModelId);   
            ViewData["ProveedorModelId"] = new SelectList(_context.Proveedores, "Id", "CorreoEmpresa", stockModel.ProveedorModelId);
            return View(stockModel);
        }

        // GET: Stock/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            ViewData["ProductosModelId"] = new SelectList(_context.Productos, "Id", "CodigoBarras", stockModel.ProductosModelId);
            ViewData["ProveedorModelId"] = new SelectList(_context.Proveedores, "Id", "CorreoEmpresa", stockModel.ProveedorModelId);
            return View(stockModel);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductosModelId,ProveedorModelId,Cantidad,FechaCaducidad,FechaElaboracion,IVA,DetalleIVA")] StockModel stockModel)
        {
            if (id != stockModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockModelExists(stockModel.Id))
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
            ViewData["ProductosModelId"] = new SelectList(_context.Productos, "Id", "CodigoBarras", stockModel.ProductosModelId);
            ViewData["ProveedorModelId"] = new SelectList(_context.Proveedores, "Id", "CorreoEmpresa", stockModel.ProveedorModelId);
            return View(stockModel);
        }

        // GET: Stock/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockModel = await _context.Stocks
                .Include(s => s.ProductosModel)
                .Include(s => s.ProveedorModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return View(stockModel);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel != null)
            {
                _context.Stocks.Remove(stockModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockModelExists(int id)
        {
            return _context.Stocks.Any(e => e.Id == id);
        }
    }
}