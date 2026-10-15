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
    public class AuditoriaController : Controller
    {
        private readonly SocialDBcontext _context;

        public AuditoriaController(SocialDBcontext context)
        {
            _context = context;
        }

        // GET: Auditoria
        public async Task<IActionResult> Index()
        {
            var socialDBcontext = _context.RegistrosAuditoria.Include(r => r.id_user);
            return View(await socialDBcontext.ToListAsync());
        }

        // GET: Auditoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro_auditoria = await _context.RegistrosAuditoria
                .Include(r => r.id_user)
                .FirstOrDefaultAsync(m => m.auditlog_id == id);
            if (registro_auditoria == null)
            {
                return NotFound();
            }

            return View(registro_auditoria);
        }

        // GET: Auditoria/Create
        public IActionResult Create()
        {
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id");
            return View(new Registro_auditoria());
        }

        // POST: Auditoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("auditlog_id,id_usuario,accion,entidad_afectada,id_entidad,direccion_ip,usuario_agente,payload_antes,payload_despues")] Registro_auditoria registro_auditoria)
        {
            if (ModelState.IsValid)
            {
                registro_auditoria.creado_en = DateTime.Now;
                _context.Add(registro_auditoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", registro_auditoria.id_usuario);
            return View(registro_auditoria);
        }

        // GET: Auditoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro_auditoria = await _context.RegistrosAuditoria.FindAsync(id);
            if (registro_auditoria == null)
            {
                return NotFound();
            }
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", registro_auditoria.id_usuario);
            return View(registro_auditoria);
        }

        // POST: Auditoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("auditlog_id,id_usuario,accion,entidad_afectada,id_entidad,direccion_ip,usuario_agente,payload_antes,payload_despues,creado_en")] Registro_auditoria registro_auditoria)
        {
            if (id != registro_auditoria.auditlog_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registro_auditoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Registro_auditoriaExists(registro_auditoria.auditlog_id))
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
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "user_id", "user_id", registro_auditoria.id_usuario);
            return View(registro_auditoria);
        }

        // GET: Auditoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registro_auditoria = await _context.RegistrosAuditoria
                .Include(r => r.id_user)
                .FirstOrDefaultAsync(m => m.auditlog_id == id);
            if (registro_auditoria == null)
            {
                return NotFound();
            }

            return View(registro_auditoria);
        }

        // POST: Auditoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registro_auditoria = await _context.RegistrosAuditoria.FindAsync(id);
            if (registro_auditoria != null)
            {
                _context.RegistrosAuditoria.Remove(registro_auditoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Registro_auditoriaExists(int id)
        {
            return _context.RegistrosAuditoria.Any(e => e.auditlog_id == id);
        }
    }
}
