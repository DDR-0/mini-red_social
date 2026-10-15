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
    public class SnapshotController : Controller
    {
        private readonly SocialDBcontext _context;

        public SnapshotController(SocialDBcontext context)
        {
            _context = context;
        }

        // GET: Snapshot
        public async Task<IActionResult> Index()
        {
            return View(await _context.SnapshotsDiarios.ToListAsync());
        }

        // GET: Snapshot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snapshot_diaria = await _context.SnapshotsDiarios
                .FirstOrDefaultAsync(m => m.snapshot_id == id);
            if (snapshot_diaria == null)
            {
                return NotFound();
            }

            return View(snapshot_diaria);
        }

        // GET: Snapshot/Create
        public IActionResult Create()
        {
            return View(new Snapshot_diaria());
        }

        // POST: Snapshot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("snapshot_id,fecha,usuarios_activos_totales,interacciones_totales_creadas,interacciones_totales_resueltas,frecuencia_uso_recursos_usados,metadatos")] Snapshot_diaria snapshot_diaria)
        {
            if (ModelState.IsValid)
            {
                snapshot_diaria.creado_en = DateTime.Now;
                _context.Add(snapshot_diaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(snapshot_diaria);
        }

        // GET: Snapshot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snapshot_diaria = await _context.SnapshotsDiarios.FindAsync(id);
            if (snapshot_diaria == null)
            {
                return NotFound();
            }
            return View(snapshot_diaria);
        }

        // POST: Snapshot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("snapshot_id,fecha,usuarios_activos_totales,interacciones_totales_creadas,interacciones_totales_resueltas,frecuencia_uso_recursos_usados,metadatos,creado_en")] Snapshot_diaria snapshot_diaria)
        {
            if (id != snapshot_diaria.snapshot_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(snapshot_diaria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Snapshot_diariaExists(snapshot_diaria.snapshot_id))
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
            return View(snapshot_diaria);
        }

        // GET: Snapshot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snapshot_diaria = await _context.SnapshotsDiarios
                .FirstOrDefaultAsync(m => m.snapshot_id == id);
            if (snapshot_diaria == null)
            {
                return NotFound();
            }

            return View(snapshot_diaria);
        }

        // POST: Snapshot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var snapshot_diaria = await _context.SnapshotsDiarios.FindAsync(id);
            if (snapshot_diaria != null)
            {
                _context.SnapshotsDiarios.Remove(snapshot_diaria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Snapshot_diariaExists(int id)
        {
            return _context.SnapshotsDiarios.Any(e => e.snapshot_id == id);
        }
    }
}
