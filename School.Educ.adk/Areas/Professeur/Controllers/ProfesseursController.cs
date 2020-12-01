using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Professeur.Controllers
{
    [Area("Professeur")]
    public class ProfesseursController : Controller
    {
        private readonly DbEcole _context;

        public ProfesseursController(DbEcole context)
        {
            _context = context;
        }

        // GET: Professeur/Professeurs
        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.Professeurs.Include(p => p.Ecole);
            return View(await dbEcole.ToListAsync());
        }

        // GET: Professeur/Professeurs/Details/5
        public async Task<IActionResult> Details(string id)
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

        // GET: Professeur/Professeurs/Create
        public IActionResult Create()
        {
            ViewData["EcoleID"] = new SelectList(_context.Ecoles, "ID", "ID");
            return View();
        }

        // POST: Professeur/Professeurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EcoleID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Ecole.Models.Professeur professeur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professeur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EcoleID"] = new SelectList(_context.Ecoles, "ID", "ID", professeur.EcoleID);
            return View(professeur);
        }

        // GET: Professeur/Professeurs/Edit/5
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
            ViewData["EcoleID"] = new SelectList(_context.Ecoles, "ID", "ID", professeur.EcoleID);
            return View(professeur);
        }

        // POST: Professeur/Professeurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EcoleID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Ecole.Models.Professeur professeur)
        {
            if (id != professeur.ID)
            {
                return NotFound();
            }

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
            ViewData["EcoleID"] = new SelectList(_context.Ecoles, "ID", "ID", professeur.EcoleID);
            return View(professeur);
        }

        // GET: Professeur/Professeurs/Delete/5
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

        // POST: Professeur/Professeurs/Delete/5
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
