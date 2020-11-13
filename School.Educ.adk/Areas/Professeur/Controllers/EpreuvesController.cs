using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Professeur.Data;
using School.Educ.adk.Areas.Professeur.Models;

namespace School.Educ.adk.Areas.Professeur.Controllers
{
    [Area("Professeur")]
    public class EpreuvesController : Controller
    {
        private readonly ProfesseurDb _context;

        public EpreuvesController(ProfesseurDb context)
        {
            _context = context;
        }

        // GET: Professeur/Epreuves
        public async Task<IActionResult> Index()
        {
            return View(await _context.Epreuves.ToListAsync());
        }

        // GET: Professeur/Epreuves/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuves
                .FirstOrDefaultAsync(m => m.ID == id);
            if (epreuve == null)
            {
                return NotFound();
            }

            return View(epreuve);
        }

        // GET: Professeur/Epreuves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professeur/Epreuves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdentifiantCours,IdentifiantProfesseur,Description,Periode,DateEpreuve,Total")] Epreuve epreuve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epreuve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(epreuve);
        }

        // GET: Professeur/Epreuves/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuves.FindAsync(id);
            if (epreuve == null)
            {
                return NotFound();
            }
            return View(epreuve);
        }

        // POST: Professeur/Epreuves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,IdentifiantCours,IdentifiantProfesseur,Description,Periode,DateEpreuve,Total")] Epreuve epreuve)
        {
            if (id != epreuve.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epreuve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpreuveExists(epreuve.ID))
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
            return View(epreuve);
        }

        // GET: Professeur/Epreuves/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuves
                .FirstOrDefaultAsync(m => m.ID == id);
            if (epreuve == null)
            {
                return NotFound();
            }

            return View(epreuve);
        }

        // POST: Professeur/Epreuves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var epreuve = await _context.Epreuves.FindAsync(id);
            _context.Epreuves.Remove(epreuve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpreuveExists(string id)
        {
            return _context.Epreuves.Any(e => e.ID == id);
        }
    }
}
