using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    [Authorize(Roles = "Directeur")]
    public class CoursController : Controller
    {
        private readonly EcoleDb _context;

        public CoursController(EcoleDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.Cours
                .Include(c => c.Classe)
                .Include(p => p.Professeur)
                .Where(pr => pr.Professeur.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID);
         
            if (dbEcole != null)
            {
                return View(await dbEcole.ToListAsync());
            }
            else
            {
                return RedirectToAction("Details", "Directeurs");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Cours
                .Include(c => c.Classe)
                .Include(c => c.Professeur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        public IActionResult Create(string idcla, string profid)
        {
            if(idcla != null)
            {
                ViewData["Classe"] = idcla;
            }
            else
            {
                ViewData["ClasseID"] = new SelectList(_context.Classes.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet");
            }
            if(profid != null)
            {
                ViewData["Professeur"] = profid;
            }
            else
            {
                ViewData["ProfesseurID"] = new SelectList(_context.Professeurs.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet");
            }
            ViewData["Categorie"] = new SelectList(_context.categories, "Nom", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClasseID,ProfesseurID,Intituler,Categorie")] Cours cours, string idcla, string profid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (idcla != null)
            {
                ViewData["Classe"] = idcla;
            }
            else
            {
                ViewData["ClasseID"] = new SelectList(_context.Classes.Where(id => id.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", cours.ClasseID);
            }
            if (profid != null)
            {
                ViewData["Professeur"] = profid;
            }
            else
            {
                ViewData["ProfesseurID"] = new SelectList(_context.Professeurs.Where(id => id.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", cours.ProfesseurID);
            }
            ViewData["Categorie"] = new SelectList(_context.categories, "Nom", "Nom");
            return View(cours);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Cours.FindAsync(id);
            if (cours == null)
            {
                return NotFound();
            }
            ViewData["ClasseID"] = new SelectList(_context.Classes.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", cours.ClasseID);
            ViewData["Categorie"] = new SelectList(_context.categories, "Nom", "Nom");
            ViewData["ProfesseurID"] = new SelectList(_context.Professeurs.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", cours.ProfesseurID);
            return View(cours);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,ClasseID,ProfesseurID,Intituler,Categorie")] Cours cours)
        {
            if (id != cours.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursExists(cours.ID))
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
            ViewData["ClasseID"] = new SelectList(_context.Classes.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", cours.ClasseID);
            ViewData["Categorie"] = new SelectList(_context.categories, "Nom", "Nom");
            ViewData["ProfesseurID"] = new SelectList(_context.Professeurs.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", cours.ProfesseurID);
            return View(cours);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Cours
                .Include(c => c.Classe)
                .Include(c => c.Professeur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cours = await _context.Cours.FindAsync(id);
            _context.Cours.Remove(cours);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursExists(string id)
        {
            return _context.Cours.Any(e => e.ID == id);
        }
    }
}
