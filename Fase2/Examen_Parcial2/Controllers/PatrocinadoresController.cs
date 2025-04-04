using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Examen_Parcial2.Models;
using Examen_Parcial2.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen_Parcial2.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class PatrocinadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatrocinadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patrocinadores
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patrocinadores.ToListAsync());
        }

        // GET: Patrocinadores/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patrocinador = await _context.Patrocinadores
                .Include(p => p.EventosPatrocinadores)
                    .ThenInclude(ep => ep.Evento)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (patrocinador == null)
            {
                return NotFound();
            }

            return View(patrocinador);
        }

        // GET: Patrocinadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patrocinadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,TipoPatrocinio")] Patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patrocinador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patrocinador);
        }

        // GET: Patrocinadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patrocinador = await _context.Patrocinadores.FindAsync(id);
            if (patrocinador == null)
            {
                return NotFound();
            }
            return View(patrocinador);
        }

        // POST: Patrocinadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,TipoPatrocinio")] Patrocinador patrocinador)
        {
            if (id != patrocinador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patrocinador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatrocinadorExists(patrocinador.Id))
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
            return View(patrocinador);
        }

        // GET: Patrocinadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patrocinador = await _context.Patrocinadores
                .FirstOrDefaultAsync(m => m.Id == id);

            if (patrocinador == null)
            {
                return NotFound();
            }

            return View(patrocinador);
        }

        // POST: Patrocinadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patrocinador = await _context.Patrocinadores.FindAsync(id);
            if (patrocinador != null)
            {
                _context.Patrocinadores.Remove(patrocinador);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PatrocinadorExists(int id)
        {
            return _context.Patrocinadores.Any(e => e.Id == id);
        }

        // GET: Patrocinadores/AsignarEvento/5
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AsignarEvento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patrocinador = await _context.Patrocinadores.FindAsync(id);
            if (patrocinador == null)
            {
                return NotFound();
            }

            
            var eventosPatrocinados = await _context.EventosPatrocinadores
                .Where(ep => ep.PatrocinadorId == id)
                .Select(ep => ep.EventoId)
                .ToListAsync();

            var eventosDisponibles = await _context.Eventos
                .Where(e => !eventosPatrocinados.Contains(e.Id))
                .ToListAsync();

            ViewData["EventoId"] = new SelectList(eventosDisponibles, "Id", "Nombre");
            ViewData["PatrocinadorId"] = id;
            ViewData["PatrocinadorNombre"] = patrocinador.Nombre;

            return View();
        }

        // POST: Patrocinadores/AsignarEvento
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AsignarEvento(int eventoId, int patrocinadorId, decimal montoPatrocinio)
        {
            var eventoPatrocinador = new EventoPatrocinador
            {
                EventoId = eventoId,
                PatrocinadorId = patrocinadorId,
                MontoPatrocinio = montoPatrocinio
            };

            _context.Add(eventoPatrocinador);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = patrocinadorId });
        }
    }
}