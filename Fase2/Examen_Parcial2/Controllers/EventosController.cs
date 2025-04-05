using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen_Parcial2.Data;
using Examen_Parcial2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Examen_Parcial2.Controllers
{
    //[Authorize(Policy = "RequireOrganizadorRole")]
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

      // GET: Evento/Edit/5
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

    // Cargar los lugares para el dropdown
    ViewBag.LugarId = new SelectList(_context.Lugares, "Id", "Nombre", evento.LugarId);
    
    // Cargar los participantes actuales del evento
    ViewBag.Participantes = await _context.EventosParticipantes
        .Where(ep => ep.EventoId == id)
        .Select(ep => ep.Participante)
        .ToListAsync();
    
    // Cargar participantes disponibles que aún no están en el evento
    var participantesActualesIds = await _context.EventosParticipantes
        .Where(ep => ep.EventoId == id)
        .Select(ep => ep.ParticipanteId)
        .ToListAsync();
    
    ViewBag.AvailableParticipants = await _context.Participantes
        .Where(p => !participantesActualesIds.Contains(p.Id))
        .ToListAsync();
    
    return View(evento);
}

// POST: Evento/AddParticipant
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> AddParticipant(int eventoId, int? participanteId, Participante participanteModel)
{
    var evento = await _context.Eventos.FindAsync(eventoId);
    if (evento == null)
    {
        return NotFound();
    }

    int selectedParticipanteId;

    // Si se seleccionó un participante existente
    if (participanteId.HasValue && participanteId.Value > 0)
    {
        selectedParticipanteId = participanteId.Value;
    }
    // Si se está creando un nuevo participante
    else if (!string.IsNullOrEmpty(participanteModel.Nombre) && !string.IsNullOrEmpty(participanteModel.Email))
    {
        // Verificar si ya existe un participante con ese email
        var existingParticipante = await _context.Participantes
            .FirstOrDefaultAsync(p => p.Email == participanteModel.Email);
        
        if (existingParticipante != null)
        {
            selectedParticipanteId = existingParticipante.Id;
        }
        else
        {
            var nuevoParticipante = new Participante
            {
                Nombre = participanteModel.Nombre,
                Email = participanteModel.Email
            };
            _context.Participantes.Add(nuevoParticipante);
            await _context.SaveChangesAsync();
            selectedParticipanteId = nuevoParticipante.Id;
        }
    }
    else
    {
        TempData["Error"] = "Debe seleccionar un participante existente o ingresar la información para crear uno nuevo.";
        return RedirectToAction(nameof(Edit), new { id = eventoId });
    }

    var existingRelation = await _context.EventosParticipantes
        .AnyAsync(ep => ep.EventoId == eventoId && ep.ParticipanteId == selectedParticipanteId);

    if (!existingRelation)
    {
        var eventoParticipante = new EventoParticipante
        {
            EventoId = eventoId,
            ParticipanteId = selectedParticipanteId
        };
        _context.EventosParticipantes.Add(eventoParticipante);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Participante agregado con éxito.";
    }
    else
    {
        TempData["Info"] = "El participante ya está registrado en este evento.";
    }

    return RedirectToAction(nameof(Edit), new { id = eventoId });
}

public async Task<IActionResult> ManageParticipants(int? id)
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

    var participantesEvento = await _context.EventosParticipantes
        .Where(ep => ep.EventoId == id)
        .Include(ep => ep.Participante)
        .Select(ep => ep.Participante)
        .ToListAsync();

    var participantesIds = participantesEvento.Select(p => p.Id).ToList();
    var participantesDisponibles = await _context.Participantes
        .Where(p => !participantesIds.Contains(p.Id))
        .ToListAsync();

    ViewBag.Evento = evento;
    ViewBag.ParticipantesEvento = participantesEvento;
    ViewBag.ParticipantesDisponibles = participantesDisponibles;

    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> RemoveParticipant(int eventoId, int participanteId)
{
    var relacion = await _context.EventosParticipantes
        .FirstOrDefaultAsync(ep => ep.EventoId == eventoId && ep.ParticipanteId == participanteId);
    
    if (relacion != null)
    {
        _context.EventosParticipantes.Remove(relacion);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Participante eliminado del evento.";
    }
    
    return RedirectToAction(nameof(Edit), new { id = eventoId });
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
        //[Authorize(Policy = "RequireOrganizadorRole")]
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
        //[Authorize(Policy = "RequireOrganizadorRole")]
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