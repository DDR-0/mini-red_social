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
    public class Uso_recursoController : Controller
    {
        private readonly SocialDBcontext _context;

        public Uso_recursoController(SocialDBcontext context)
        {
            _context = context;
        }

        // GET: Uso_recurso
        public async Task<IActionResult> Index()
        {
            var socialDBcontext = _context.UsosRecurso.Include(u => u.InteraccionSocial).Include(u => u.Recurso);
            return View(await socialDBcontext.ToListAsync());
        }

        // GET: Uso_recurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uso_recurso = await _context.UsosRecurso
                .Include(u => u.InteraccionSocial)
                .Include(u => u.Recurso)
                .FirstOrDefaultAsync(m => m.uso_recurso_id == id);
            if (uso_recurso == null)
            {
                return NotFound();
            }

            return View(uso_recurso);
        }

        // GET: Uso_recurso/Create
        public IActionResult Create()
        {
            ViewData["interaccion_id"] = new SelectList(_context.Interacciones, "interacciones_id", "interacciones_id");
            ViewData["recurso_id"] = new SelectList(_context.Recursos, "recurso_id", "recurso_id");
            return View(new Uso_recurso());
        }

        // POST: Uso_recurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("uso_recurso_id,interaccion_id,recurso_id,fecha_inicio,fecha_fin,Estado_recurso,fecha_eliminacion")] Uso_recurso uso_recurso)
        {
            if (ModelState.IsValid)
            {
                uso_recurso.fecha_creacion = DateTime.Now;
                _context.Add(uso_recurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["interaccion_id"] = new SelectList(_context.Interacciones, "interacciones_id", "interacciones_id", uso_recurso.interaccion_id);
            ViewData["recurso_id"] = new SelectList(_context.Recursos, "recurso_id", "recurso_id", uso_recurso.recurso_id);
            return View(uso_recurso);
        }

        // GET: Uso_recurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uso_recurso = await _context.UsosRecurso.FindAsync(id);
            if (uso_recurso == null)
            {
                return NotFound();
            }
            ViewData["interaccion_id"] = new SelectList(_context.Interacciones, "interacciones_id", "interacciones_id", uso_recurso.interaccion_id);
            ViewData["recurso_id"] = new SelectList(_context.Recursos, "recurso_id", "recurso_id", uso_recurso.recurso_id);
            return View(uso_recurso);
        }

        // POST: Uso_recurso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("uso_recurso_id,interaccion_id,recurso_id,fecha_inicio,fecha_fin,Estado_recurso,fecha_creacion,fecha_eliminacion")] Uso_recurso uso_recurso)
        {
            if (id != uso_recurso.uso_recurso_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uso_recurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Uso_recursoExists(uso_recurso.uso_recurso_id))
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
            ViewData["interaccion_id"] = new SelectList(_context.Interacciones, "interacciones_id", "interacciones_id", uso_recurso.interaccion_id);
            ViewData["recurso_id"] = new SelectList(_context.Recursos, "recurso_id", "recurso_id", uso_recurso.recurso_id);
            return View(uso_recurso);
        }

        // GET: Uso_recurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uso_recurso = await _context.UsosRecurso
                .Include(u => u.InteraccionSocial)
                .Include(u => u.Recurso)
                .FirstOrDefaultAsync(m => m.uso_recurso_id == id);
            if (uso_recurso == null)
            {
                return NotFound();
            }

            return View(uso_recurso);
        }

        // POST: Uso_recurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uso_recurso = await _context.UsosRecurso.FindAsync(id);
            if (uso_recurso != null)
            {
                _context.UsosRecurso.Remove(uso_recurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Uso_recursoExists(int id)
        {
            return _context.UsosRecurso.Any(e => e.uso_recurso_id == id);
        }
    }
}
