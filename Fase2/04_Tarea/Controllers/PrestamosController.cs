using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _04_Tarea.Data;
using _04_Tarea.Models;
using Microsoft.AspNetCore.Authorization;

namespace _04_Tarea.Controllers
{
    [Authorize]
    public class PrestamosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrestamosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prestamos.Include(p => p.Libro).Include(p => p.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .Include(p => p.Renovaciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros.Where(l => l.Disponible), "Id", "Titulo");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Nombre");
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaPrestamo,FechaDevolucion,Estado,Observaciones,UserId,LibroId")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                var libro = await _context.Libros.FindAsync(prestamo.LibroId);
                if (libro != null && libro.Disponible)
                {
                    libro.Disponible = false;
                    _context.Update(libro);
                    _context.Add(prestamo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("LibroId", "El libro no está disponible para préstamo.");
                }
            }
            ViewData["LibroId"] = new SelectList(_context.Libros.Where(l => l.Disponible), "Id", "Titulo", prestamo.LibroId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Nombre", prestamo.UserId);
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.LibroId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Nombre", prestamo.UserId);
            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaPrestamo,FechaDevolucion,Estado,Observaciones,UserId,LibroId")] Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Si el estado cambia a "Devuelto", actualizar disponibilidad del libro
                    if (prestamo.Estado == "Devuelto")
                    {
                        var libro = await _context.Libros.FindAsync(prestamo.LibroId);
                        if (libro != null)
                        {
                            libro.Disponible = true;
                            _context.Update(libro);
                        }
                    }

                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.LibroId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Nombre", prestamo.UserId);
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                // Actualizar disponibilidad del libro
                var libro = await _context.Libros.FindAsync(prestamo.LibroId);
                if (libro != null && !libro.Disponible)
                {
                    libro.Disponible = true;
                    _context.Update(libro);
                }

                _context.Prestamos.Remove(prestamo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
