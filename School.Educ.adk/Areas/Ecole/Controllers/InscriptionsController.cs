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
    public class InscriptionsController : Controller
    {
        private readonly EcoleDb _context;

        public InscriptionsController(EcoleDb context)
        {
            _context = context;
        }

        public IActionResult Create(string id)
        {
            ViewData["ClasseID"] = new SelectList(_context.Classes.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet");
            ViewData["Eleve"] = _context.Eleves.Find(id).NomComplet;
            ViewData["EleveId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EleveId,ClasseID,DateInscription")] Inscription inscription, string id)
        {
            int model = (from i in _context.Inscriptions where i.ClasseID == inscription.ClasseID & i.EleveId == inscription.EleveId select i).Count();
            if (model > 0)
            {
                ModelState.AddModelError("", "Un élève ne peut être inscrit dans une classe et une même année scolaire !");
                ViewData["ClasseID"] = new SelectList(_context.Classes.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", inscription.ClasseID);
                ViewData["Eleve"] = _context.Eleves.Find(inscription.EleveId).NomComplet;
                ViewData["EleveId"] = id;
                return View(inscription);
            }
            if (ModelState.IsValid)
            {
                _context.Add(inscription);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Eleves", new { id = inscription.EleveId });
            }
            ViewData["ClasseID"] = new SelectList(_context.Classes.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", inscription.ClasseID);
            ViewData["EleveId"] = new SelectList(_context.Eleves.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", inscription.EleveId);
            return View(inscription);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscription = await _context.Inscriptions.FindAsync(id);
            if (inscription == null)
            {
                return NotFound();
            }
            ViewData["ClasseID"] = new SelectList(_context.Classes.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", inscription.ClasseID);
            ViewData["Eleve"] = _context.Eleves.Find(inscription.EleveId).NomComplet;
            ViewData["Ele"] = id;
            return View(inscription);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EleveId,ClasseID,DateInscription")] Inscription inscription)
        {
            if (id != inscription.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscriptionExists(inscription.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Eleves", new { id = inscription.EleveId });
            }
            ViewData["ClasseID"] = new SelectList(_context.Classes.Where(i => i.EcoleID == _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID), "ID", "NomComplet", inscription.ClasseID);
            ViewData["Eleve"] = _context.Eleves.Find(inscription.EleveId).NomComplet;
            ViewData["EleveId"] = id;
            return View(inscription);
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscription = _context.Inscriptions
                .Include(i => i.Classe)
                .Include(i => i.Eleve)
                .FirstOrDefault(m => m.ID == id);
            if (inscription == null)
            {
                return NotFound();
            }
            _context.Inscriptions.Remove(inscription);
            _context.SaveChanges();
            return RedirectToAction("Details", "Eleves", new { id = inscription.EleveId });
        }

        private bool InscriptionExists(string id)
        {
            return _context.Inscriptions.Any(e => e.ID == id);
        }
    }
}
