using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.ProfeArea.Data;
using School.Educ.adk.Areas.ProfeArea.Models;

namespace School.Educ.adk.Areas.ProfeArea.Controllers
{
    [Area("ProfeArea")]
    public class LeconsController : Controller
    {
        private readonly ProfeAreaDb _context;

        public LeconsController(ProfeAreaDb context)
        {
            _context = context;
        }

        // GET: ProfeArea/Lecons
        public async Task<IActionResult> Index()
        {
            var profeAreaDb = _context.Lecons.Include(l => l.Cours).Include(l => l.Professeur);
            return View(await profeAreaDb.ToListAsync());
        }

        // GET: ProfeArea/Lecons/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons
                .Include(l => l.Cours)
                .Include(l => l.Professeur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecon == null)
            {
                return NotFound();
            }

            return View(lecon);
        }

        // GET: ProfeArea/Lecons/Create
        public IActionResult Create()
        {
            ViewData["CoursID"] = new SelectList(_context.Set<Ecole.Models.Cours>(), "ID", "ID");
            ViewData["ProfesseurID"] = new SelectList(_context.Set<Ecole.Models.Professeur>(), "ID", "ID");
            return View();
        }

        // POST: ProfeArea/Lecons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProfesseurID,CoursID,LeconDonnee,DateLecon")] Lecon lecon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoursID"] = new SelectList(_context.Set<Ecole.Models.Cours>(), "ID", "ID", lecon.CoursID);
            ViewData["ProfesseurID"] = new SelectList(_context.Set<Ecole.Models.Professeur>(), "ID", "ID", lecon.ProfesseurID);
            return View(lecon);
        }

        // GET: ProfeArea/Lecons/Edit/5
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
            ViewData["CoursID"] = new SelectList(_context.Set<Ecole.Models.Cours>(), "ID", "ID", lecon.CoursID);
            ViewData["ProfesseurID"] = new SelectList(_context.Set<Ecole.Models.Professeur>(), "ID", "ID", lecon.ProfesseurID);
            return View(lecon);
        }

        // POST: ProfeArea/Lecons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,ProfesseurID,CoursID,LeconDonnee,DateLecon")] Lecon lecon)
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
            ViewData["CoursID"] = new SelectList(_context.Set<Ecole.Models.Cours>(), "ID", "ID", lecon.CoursID);
            ViewData["ProfesseurID"] = new SelectList(_context.Set<Ecole.Models.Professeur>(), "ID", "ID", lecon.ProfesseurID);
            return View(lecon);
        }

        // GET: ProfeArea/Lecons/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons
                .Include(l => l.Cours)
                .Include(l => l.Professeur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecon == null)
            {
                return NotFound();
            }

            return View(lecon);
        }

        // POST: ProfeArea/Lecons/Delete/5
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
