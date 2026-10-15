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
    public class InteraccionController : Controller
    {
        private readonly SocialDBcontext _context;

        public InteraccionController(SocialDBcontext context)
        {
            _context = context;
        }

        // GET: Interaccion
        public async Task<IActionResult> Index()
        {
            var socialDBcontext = _context.Interacciones.Include(i => i.tipo_interaccion).Include(i => i.usuario_id);
            return View(await socialDBcontext.ToListAsync());
        }

        // GET: Interaccion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interaccion_social = await _context.Interacciones
                .Include(i => i.tipo_interaccion)
                .Include(i => i.usuario_id)
                .FirstOrDefaultAsync(m => m.interacciones_id == id);
            if (interaccion_social == null)
            {
                return NotFound();
            }

            return View(interaccion_social);
        }

        // GET: Interaccion/Create
        public IActionResult Create()
        {
            ViewData["tipo_interaccion_id"] = new SelectList(_context.TiposInteraccion, "tipo_interaccion_id", "tipo_interaccion_id");
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id");
            return View(new Interaccion_social());
        }

        // POST: Interaccion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("interacciones_id,tipo_interaccion_id,id_usuario,titulo,descripcion,EstadoActual,NivelPrivacidad,horario_inicio,horario_fin,modificado_en,borrado_en")] Interaccion_social interaccion_social)
        {
            if (ModelState.IsValid)
            {
                interaccion_social.creado_en = DateTime.Now;
                _context.Add(interaccion_social);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["tipo_interaccion_id"] = new SelectList(_context.TiposInteraccion, "tipo_interaccion_id", "tipo_interaccion_id", interaccion_social.tipo_interaccion_id);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", interaccion_social.id_usuario);
            return View(interaccion_social);
        }

        // GET: Interaccion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interaccion_social = await _context.Interacciones.FindAsync(id);
            if (interaccion_social == null)
            {
                return NotFound();
            }
            ViewData["tipo_interaccion_id"] = new SelectList(_context.TiposInteraccion, "tipo_interaccion_id", "tipo_interaccion_id", interaccion_social.tipo_interaccion_id);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", interaccion_social.id_usuario);
            return View(interaccion_social);
        }

        // POST: Interaccion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("interacciones_id,tipo_interaccion_id,id_usuario,titulo,descripcion,EstadoActual,NivelPrivacidad,horario_inicio,horario_fin,creado_en,modificado_en,borrado_en")] Interaccion_social interaccion_social)
        {
            if (id != interaccion_social.interacciones_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interaccion_social);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Interaccion_socialExists(interaccion_social.interacciones_id))
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
            ViewData["tipo_interaccion_id"] = new SelectList(_context.TiposInteraccion, "tipo_interaccion_id", "tipo_interaccion_id", interaccion_social.tipo_interaccion_id);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", interaccion_social.id_usuario);
            return View(interaccion_social);
        }

        // GET: Interaccion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interaccion_social = await _context.Interacciones
                .Include(i => i.tipo_interaccion)
                .Include(i => i.usuario_id)
                .FirstOrDefaultAsync(m => m.interacciones_id == id);
            if (interaccion_social == null)
            {
                return NotFound();
            }

            return View(interaccion_social);
        }

        // POST: Interaccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interaccion_social = await _context.Interacciones.FindAsync(id);
            if (interaccion_social != null)
            {
                _context.Interacciones.Remove(interaccion_social);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Interaccion_socialExists(int id)
        {
            return _context.Interacciones.Any(e => e.interacciones_id == id);
        }
    }
}
