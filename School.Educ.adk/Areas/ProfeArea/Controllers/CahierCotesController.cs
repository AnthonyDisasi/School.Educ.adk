using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.ProfeArea.Controllers
{
    [Area("ProfeArea")]
    public class CahierCotesController : Controller
    {
        private readonly DbEcole _context;

        public CahierCotesController(DbEcole context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.CahierCote
                .Include(c => c.Cours)
                .ThenInclude(cl => cl.Classe)
                .Include(e => e.Epreuves)
                .Where(id => id.Cours.Professeur.Matricule == User.Identity.Name);
            return View(await dbEcole.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cahierCote = await _context.CahierCote
                .Include(c => c.Cours)
                .ThenInclude(cl => cl.Classe)
                .Include(e => e.Epreuves)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cahierCote == null)
            {
                return NotFound();
            }

            return View(cahierCote);
        }


        public IActionResult Create()
        {
            ViewData["CoursID"] = new SelectList(_context.Cours.Include(p => p.Professeur).Where(id => id.Professeur.Matricule == User.Identity.Name).Include(c => c.CahierCote).Where(ca => ca.CahierCote == null), "ID", "Intituler");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CoursID,Periode,Total")] CahierCote cahierCote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cahierCote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoursID"] = new SelectList(_context.Cours.Include(p => p.Professeur).Where(id => id.Professeur.Matricule == User.Identity.Name).Include(c => c.CahierCote).Where(ca => ca.CahierCote == null), "ID", "Intituler", cahierCote.CoursID);
            return View(cahierCote);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cahierCote = await _context.CahierCote.FindAsync(id);
            if (cahierCote == null)
            {
                return NotFound();
            }
            ViewData["CoursID"] = new SelectList(_context.Cours.Include(p => p.Professeur).Where(i_d => i_d.Professeur.Matricule == User.Identity.Name), "ID", "Intituler", cahierCote.CoursID);
            return View(cahierCote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,CoursID,Periode,Total")] CahierCote cahierCote)
        {
            if (id != cahierCote.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cahierCote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CahierCoteExists(cahierCote.ID))
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
            ViewData["CoursID"] = new SelectList(_context.Cours.Include(p => p.Professeur).Where(i_d => i_d.Professeur.Matricule == User.Identity.Name), "ID", "Intituler", cahierCote.CoursID);
            return View(cahierCote);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cahierCote = await _context.CahierCote
                .Include(c => c.Cours)
                .ThenInclude(cl => cl.Classe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cahierCote == null)
            {
                return NotFound();
            }

            return View(cahierCote);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cahierCote = await _context.CahierCote.FindAsync(id);
            _context.CahierCote.Remove(cahierCote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CahierCoteExists(string id)
        {
            return _context.CahierCote.Any(e => e.ID == id);
        }
    }
}
