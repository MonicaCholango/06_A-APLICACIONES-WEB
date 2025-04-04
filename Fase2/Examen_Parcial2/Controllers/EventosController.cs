using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen_Parcial2.Models;
using Examen_Parcial2.Data;

namespace Examen_Parcial2.Controllers
{
    [Authorize(Policy = "RequireOrganizadorRole")]
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eventos
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var eventos = await _context.Eventos
                .Include(e => e.Lugar)
                .ToListAsync();
            return View(eventos);
        }

        // GET: Eventos/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Lugar)
                .Include(e => e.EventosParticipantes)
                    .ThenInclude(ep => ep.Participante)
                .Include(e => e.EventosPatrocinadores)
                    .ThenInclude(ep => ep.Patrocinador)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["LugarId"] = new SelectList(_context.Lugares, "Id", "Nombre");
            return View();
        }

        // POST: Eventos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Fecha,Descripcion,LugarId")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LugarId"] = new SelectList(_context.Lugares, "Id", "Nombre", evento.LugarId);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["LugarId"] = new SelectList(_context.Lugares, "Id", "Nombre", evento.LugarId);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Fecha,Descripcion,LugarId")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
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
            ViewData["LugarId"] = new SelectList(_context.Lugares, "Id", "Nombre", evento.LugarId);
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Lugar)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }

        // GET: Eventos/AgregarParticipante/5
        [Authorize(Policy = "RequireOrganizadorRole")]
        public async Task<IActionResult> AgregarParticipante(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

           
            var participantesRegistrados = await _context.EventosParticipantes
                .Where(ep => ep.EventoId == id)
                .Select(ep => ep.ParticipanteId)
                .ToListAsync();

            var participantesDisponibles = await _context.Participantes
                .Where(p => !participantesRegistrados.Contains(p.Id))
                .ToListAsync();

            ViewData["ParticipanteId"] = new SelectList(participantesDisponibles, "Id", "Nombre");
            ViewData["EventoId"] = id;
            ViewData["EventoNombre"] = evento.Nombre;

            return View();
        }

        // POST: Eventos/AgregarParticipante
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireOrganizadorRole")]
        public async Task<IActionResult> AgregarParticipante(int eventoId, int participanteId)
        {
            var eventoParticipante = new EventoParticipante
            {
                EventoId = eventoId,
                ParticipanteId = participanteId,
                Confirmado = false
            };

            _context.Add(eventoParticipante);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = eventoId });
        }
    }
}