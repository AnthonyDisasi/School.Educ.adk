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
    public class CoursController : Controller
    {
        private readonly DbEcole _context;

        public CoursController(DbEcole context)
        {
            _context = context;
        }

        // GET: Ecole/Cours
        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.Cours.Include(c => c.Classe);
            return View(await dbEcole.ToListAsync());
        }

        // GET: Ecole/Cours/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Cours
                .Include(c => c.Classe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        // GET: Ecole/Cours/Create
        public IActionResult Create()
        {
            ViewData["ClasseID"] = new SelectList(_context.Classes, "ID", "ID");
            return View();
        }

        // POST: Ecole/Cours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClasseID,Intituler,Categorie")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasseID"] = new SelectList(_context.Classes, "ID", "ID", cours.ClasseID);
            return View(cours);
        }

        // GET: Ecole/Cours/Edit/5
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
            ViewData["ClasseID"] = new SelectList(_context.Classes, "ID", "ID", cours.ClasseID);
            return View(cours);
        }

        // POST: Ecole/Cours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,ClasseID,Intituler,Categorie")] Cours cours)
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
            ViewData["ClasseID"] = new SelectList(_context.Classes, "ID", "ID", cours.ClasseID);
            return View(cours);
        }

        // GET: Ecole/Cours/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cours = await _context.Cours
                .Include(c => c.Classe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cours == null)
            {
                return NotFound();
            }

            return View(cours);
        }

        // POST: Ecole/Cours/Delete/5
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
