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
    public class LeconsController : Controller
    {
        private readonly ProfesseurDb _context;

        public LeconsController(ProfesseurDb context)
        {
            _context = context;
        }

        // GET: Professeur/Lecons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lecons.ToListAsync());
        }

        // GET: Professeur/Lecons/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecon == null)
            {
                return NotFound();
            }

            return View(lecon);
        }

        // GET: Professeur/Lecons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professeur/Lecons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdentifiantProfesseur,IdentifiantCours,LeconDonnee,DateLecon")] Lecon lecon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecon);
        }

        // GET: Professeur/Lecons/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons.FindAsync(id);
            if (lecon == null)
            {
                return NotFound();
            }
            return View(lecon);
        }

        // POST: Professeur/Lecons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,IdentifiantProfesseur,IdentifiantCours,LeconDonnee,DateLecon")] Lecon lecon)
        {
            if (id != lecon.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeconExists(lecon.ID))
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
            return View(lecon);
        }

        // GET: Professeur/Lecons/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecon == null)
            {
                return NotFound();
            }

            return View(lecon);
        }

        // POST: Professeur/Lecons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lecon = await _context.Lecons.FindAsync(id);
            _context.Lecons.Remove(lecon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeconExists(string id)
        {
            return _context.Lecons.Any(e => e.ID == id);
        }
    }
}
