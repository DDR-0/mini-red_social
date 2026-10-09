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
    public class RecursoController : Controller
    {
        private readonly SocialDBcontext _context;

        public RecursoController(SocialDBcontext context)
        {
            _context = context;
        }

        // GET: Recurso
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recursos.ToListAsync());
        }

        // GET: Recurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos
                .FirstOrDefaultAsync(m => m.recurso_id == id);
            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // GET: Recurso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recurso_id,nombre,type,capacidad,creado_en,modificado_en")] Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recurso);
        }

        // GET: Recurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }
            return View(recurso);
        }

        // POST: Recurso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recurso_id,nombre,type,capacidad,creado_en,modificado_en")] Recurso recurso)
        {
            if (id != recurso.recurso_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecursoExists(recurso.recurso_id))
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
            return View(recurso);
        }

        // GET: Recurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurso = await _context.Recursos
                .FirstOrDefaultAsync(m => m.recurso_id == id);
            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // POST: Recurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso != null)
            {
                _context.Recursos.Remove(recurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecursoExists(int id)
        {
            return _context.Recursos.Any(e => e.recurso_id == id);
        }
    }
}
