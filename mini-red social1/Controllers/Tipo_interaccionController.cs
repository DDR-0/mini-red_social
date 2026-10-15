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
    public class Tipo_interaccionController : Controller
    {
        private readonly SocialDBcontext _context;

        public Tipo_interaccionController(SocialDBcontext context)
        {
            _context = context;
        }

        // GET: Tipo_interaccion
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposInteraccion.ToListAsync());
        }

        // GET: Tipo_interaccion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Interaccion = await _context.TiposInteraccion
                .FirstOrDefaultAsync(m => m.tipo_interaccion_id == id);
            if (tipo_Interaccion == null)
            {
                return NotFound();
            }

            return View(tipo_Interaccion);
        }

        // GET: Tipo_interaccion/Create
        public IActionResult Create()
        {
            return View(new Tipo_Interaccion());
        }

        // POST: Tipo_interaccion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("tipo_interaccion_id,nombre,descripcion,requiere_recurso,esta_activo,modificado_en,borrado_en")] Tipo_Interaccion tipo_Interaccion)
        {
            if (ModelState.IsValid)
            {
                tipo_Interaccion.creado_en = DateTime.Now;
                _context.Add(tipo_Interaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo_Interaccion);
        }

        // GET: Tipo_interaccion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Interaccion = await _context.TiposInteraccion.FindAsync(id);
            if (tipo_Interaccion == null)
            {
                return NotFound();
            }
            return View(tipo_Interaccion);
        }

        // POST: Tipo_interaccion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("tipo_interaccion_id,nombre,descripcion,requiere_recurso,esta_activo,creado_en,modificado_en,borrado_en")] Tipo_Interaccion tipo_Interaccion)
        {
            if (id != tipo_Interaccion.tipo_interaccion_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo_Interaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tipo_InteraccionExists(tipo_Interaccion.tipo_interaccion_id))
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
            return View(tipo_Interaccion);
        }

        // GET: Tipo_interaccion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo_Interaccion = await _context.TiposInteraccion
                .FirstOrDefaultAsync(m => m.tipo_interaccion_id == id);
            if (tipo_Interaccion == null)
            {
                return NotFound();
            }

            return View(tipo_Interaccion);
        }

        // POST: Tipo_interaccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipo_Interaccion = await _context.TiposInteraccion.FindAsync(id);
            if (tipo_Interaccion != null)
            {
                _context.TiposInteraccion.Remove(tipo_Interaccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Tipo_InteraccionExists(int id)
        {
            return _context.TiposInteraccion.Any(e => e.tipo_interaccion_id == id);
        }
    }
}
