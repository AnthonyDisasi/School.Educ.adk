using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    public class ProfesseursController : Controller
    {
        private readonly DbEcole _context;

        public ProfesseursController(DbEcole context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if (model.Ecole != null)
            {
                var dbEcole = _context.Professeurs.Include(p => p.Ecole).Include(c => c.Cours);
                return View(await dbEcole.ToListAsync());
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.ID });
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professeur = await _context.Professeurs
                .Include(p => p.Ecole)
                .Include(c => c.Cours)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professeur == null)
            {
                return NotFound();
            }

            return View(professeur);
        }

        public IActionResult Create()
        {
            var model = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if (model.Ecole != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.ID });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EcoleID,Nom,Postnom,Prenom,Genre,Matricule,Email,DateNaissance,Password")] Models.Professeur professeur)
        {
            professeur.EcoleID = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID;
            
            if (ModelState.IsValid)
            {
                _context.Add(professeur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professeur);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professeur = await _context.Professeurs.FindAsync(id);
            if (professeur == null)
            {
                return NotFound();
            }
            return View(professeur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EcoleID,Nom,Postnom,Prenom,Genre,Matricule,Email,DateNaissance,Password")] Models.Professeur professeur)
        {
            if (id != professeur.ID)
            {
                return NotFound();
            }
            professeur.EcoleID = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professeur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesseurExists(professeur.ID))
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
            return View(professeur);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professeur = await _context.Professeurs
                .Include(p => p.Ecole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professeur == null)
            {
                return NotFound();
            }

            return View(professeur);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var professeur = await _context.Professeurs.FindAsync(id);
            _context.Professeurs.Remove(professeur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesseurExists(string id)
        {
            return _context.Professeurs.Any(e => e.ID == id);
        }
    }
}
