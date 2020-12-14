using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Inspection.Data;
using School.Educ.adk.Areas.Inspection.Models;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    public class ExamenController : Controller
    {
        private readonly ExamenDb _context;

        public ExamenController(ExamenDb context)
        {
            _context = context;
        }

        [Authorize(Roles = "Inspecteur")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Examens.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examen = await _context.Examens
                .FirstOrDefaultAsync(m => m.ID == id);
            if (examen == null)
            {
                return NotFound();
            }

            return View(examen);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,Periode,Serie,CodeAcces,IdInspecteur")] Examen examen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(examen);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examen = await _context.Examens.FindAsync(id);
            if (examen == null)
            {
                return NotFound();
            }
            return View(examen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Description,Periode,Serie,CodeAcces,IdInspecteur")] Examen examen)
        {
            if (id != examen.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamenExists(examen.ID))
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
            return View(examen);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examen = await _context.Examens
                .FirstOrDefaultAsync(m => m.ID == id);
            if (examen == null)
            {
                return NotFound();
            }

            return View(examen);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var examen = await _context.Examens.FindAsync(id);
            _context.Examens.Remove(examen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamenExists(string id)
        {
            return _context.Examens.Any(e => e.ID == id);
        }
    }
}
