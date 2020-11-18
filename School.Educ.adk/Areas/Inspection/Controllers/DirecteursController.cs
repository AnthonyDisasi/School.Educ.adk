using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    public class DirecteursController : Controller
    {
        private readonly DbEcole _context;

        public DirecteursController(DbEcole context)
        {
            _context = context;
        }

        // GET: Inspection/Directeurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Directeurs.ToListAsync());
        }

        // GET: Inspection/Directeurs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (directeur == null)
            {
                return NotFound();
            }

            return View(directeur);
        }

        // GET: Inspection/Directeurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inspection/Directeurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,DateNaissance")] Directeur directeur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(directeur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(directeur);
        }

        // GET: Inspection/Directeurs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs.FindAsync(id);
            if (directeur == null)
            {
                return NotFound();
            }
            return View(directeur);
        }

        // POST: Inspection/Directeurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,DateNaissance")] Directeur directeur)
        {
            if (id != directeur.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directeur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirecteurExists(directeur.ID))
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
            return View(directeur);
        }

        // GET: Inspection/Directeurs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (directeur == null)
            {
                return NotFound();
            }

            return View(directeur);
        }

        // POST: Inspection/Directeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var directeur = await _context.Directeurs.FindAsync(id);
            _context.Directeurs.Remove(directeur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirecteurExists(string id)
        {
            return _context.Directeurs.Any(e => e.ID == id);
        }
    }
}
