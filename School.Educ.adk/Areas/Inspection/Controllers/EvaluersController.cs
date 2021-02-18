using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Inspection.Models;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    public class EvaluersController : Controller
    {
        private readonly EcoleDb _context;

        public EvaluersController(EcoleDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ecoleDb = _context.Evaluer
                .Include(e => e.Inpecteur)
                .Include(e => e.Lecon)
                .ThenInclude(e => e.Cours)
                .Where(e => e.Inpecteur.Matricule == User.Identity.Name);
            return View(await ecoleDb.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluer = await _context.Evaluer
                .Include(e => e.Inpecteur)
                .Include(e => e.Lecon)
                .ThenInclude(e => e.Cours)
                .Where(e => e.Inpecteur.Matricule == User.Identity.Name)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (evaluer == null)
            {
                return NotFound();
            }

            return View(evaluer);
        }

        public IActionResult Create()
        {
            ViewData["InpecteurID"] = new SelectList(_context.Inspecteurs, "ID", "ID");
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LeconID,InpecteurID,Cotation,Remarque")] Evaluer evaluer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InpecteurID"] = new SelectList(_context.Inspecteurs, "ID", "ID", evaluer.InpecteurID);
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", evaluer.LeconID);
            return View(evaluer);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluer = await _context.Evaluer.FindAsync(id);
            if (evaluer == null)
            {
                return NotFound();
            }
            ViewData["InpecteurID"] = new SelectList(_context.Inspecteurs, "ID", "ID", evaluer.InpecteurID);
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", evaluer.LeconID);
            return View(evaluer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,LeconID,InpecteurID,Cotation,Remarque")] Evaluer evaluer)
        {
            if (id != evaluer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluerExists(evaluer.ID))
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
            ViewData["InpecteurID"] = new SelectList(_context.Inspecteurs, "ID", "ID", evaluer.InpecteurID);
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", evaluer.LeconID);
            return View(evaluer);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluer = await _context.Evaluer
                .Include(e => e.Inpecteur)
                .Include(e => e.Lecon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (evaluer == null)
            {
                return NotFound();
            }

            return View(evaluer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var evaluer = await _context.Evaluer.FindAsync(id);
            _context.Evaluer.Remove(evaluer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluerExists(string id)
        {
            return _context.Evaluer.Any(e => e.ID == id);
        }
    }
}
