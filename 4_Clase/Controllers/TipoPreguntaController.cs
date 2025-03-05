using _4_Clase.Data;
using _4_Clase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _4_Clase.Controllers
{
    public class TipoPreguntaController : Controller
    {
        private readonly Get_PostDbContext _context;

        public TipoPreguntaController(Get_PostDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoPreguntas.ToListAsync());
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nuevo([Bind("Detalle")] TipoPreguntaModel tipoPregunta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPregunta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoPregunta);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var tipoPreguntaModel = await _context.TipoPreguntas.FindAsync(id);
            if (tipoPreguntaModel == null) return NotFound();

            return View(tipoPreguntaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Detalle")] TipoPreguntaModel tipoPreguntaModel)
        {
            if (id != tipoPreguntaModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPreguntaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPreguntaExiste(tipoPreguntaModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPreguntaModel);
        }

        public bool TipoPreguntaExiste(int id)
        {
            return _context.TipoPreguntas.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var tipoPreguntaModel = await _context.TipoPreguntas.FirstOrDefaultAsync(tp => tp.Id == id);
            if (tipoPreguntaModel == null) return NotFound();

            return View(tipoPreguntaModel);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmacionEliminar(int id)
        {
            var tipoPreguntaModel = await _context.TipoPreguntas.FindAsync(id);
            if (tipoPreguntaModel == null) return NotFound();

            try
            {
                _context.TipoPreguntas.Remove(tipoPreguntaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}