using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers
{
    public class HistorialController : Controller
    {
        private readonly SocialDBcontext _context;

        public HistorialController(SocialDBcontext context)
        {
            _context = context;
        }

        // GET: Historial
        public async Task<IActionResult> Index()
        {
            var socialDBcontext = _context.HistorialInteracciones.Include(h => h.id_interaccion).Include(h => h.usuario_id);
            return View(await socialDBcontext.ToListAsync());
        }

        // GET: Historial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historial_interaccion = await _context.HistorialInteracciones
                .Include(h => h.id_interaccion)
                .Include(h => h.usuario_id)
                .FirstOrDefaultAsync(m => m.historial_id == id);
            if (historial_interaccion == null)
            {
                return NotFound();
            }

            return View(historial_interaccion);
        }

        // GET: Historial/Create
        public IActionResult Create()
        {
            ViewData["interaccion_id"] = new SelectList(_context.Interacciones, "interacciones_id", "interacciones_id");
            ViewData["cambio_por_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id");
            return View();
        }

        // POST: Historial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("historial_id,interaccion_id,cambio_por_usuario,estado_previo,estado_nuevo,motivo,creado_en")] Historial_interaccion historial_interaccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historial_interaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["interaccion_id"] = new SelectList(_context.Interacciones, "interacciones_id", "interacciones_id", historial_interaccion.interaccion_id);
            ViewData["cambio_por_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", historial_interaccion.cambio_por_usuario);
            return View(historial_interaccion);
        }

        // GET: Historial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historial_interaccion = await _context.HistorialInteracciones.FindAsync(id);
            if (historial_interaccion == null)
            {
                return NotFound();
            }
            ViewData["interaccion_id"] = new SelectList(_context.Interacciones, "interacciones_id", "interacciones_id", historial_interaccion.interaccion_id);
            ViewData["cambio_por_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", historial_interaccion.cambio_por_usuario);
            return View(historial_interaccion);
        }

        // POST: Historial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("historial_id,interaccion_id,cambio_por_usuario,estado_previo,estado_nuevo,motivo,creado_en")] Historial_interaccion historial_interaccion)
        {
            if (id != historial_interaccion.historial_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historial_interaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Historial_interaccionExists(historial_interaccion.historial_id))
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
            ViewData["interaccion_id"] = new SelectList(_context.Interacciones, "interacciones_id", "interacciones_id", historial_interaccion.interaccion_id);
            ViewData["cambio_por_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", historial_interaccion.cambio_por_usuario);
            return View(historial_interaccion);
        }

        // GET: Historial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historial_interaccion = await _context.HistorialInteracciones
                .Include(h => h.id_interaccion)
                .Include(h => h.usuario_id)
                .FirstOrDefaultAsync(m => m.historial_id == id);
            if (historial_interaccion == null)
            {
                return NotFound();
            }

            return View(historial_interaccion);
        }

        // POST: Historial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historial_interaccion = await _context.HistorialInteracciones.FindAsync(id);
            if (historial_interaccion != null)
            {
                _context.HistorialInteracciones.Remove(historial_interaccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Historial_interaccionExists(int id)
        {
            return _context.HistorialInteracciones.Any(e => e.historial_id == id);
        }
    }
}
