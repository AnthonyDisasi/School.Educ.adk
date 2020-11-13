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
    public class EchangesController : Controller
    {
        private readonly ProfesseurDb _context;

        public EchangesController(ProfesseurDb context)
        {
            _context = context;
        }

        // GET: Professeur/Echanges
        public async Task<IActionResult> Index()
        {
            var professeurDb = _context.Echanges.Include(e => e.Lecon);
            return View(await professeurDb.ToListAsync());
        }

        // GET: Professeur/Echanges/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var echange = await _context.Echanges
                .Include(e => e.Lecon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (echange == null)
            {
                return NotFound();
            }

            return View(echange);
        }

        // GET: Professeur/Echanges/Create
        public IActionResult Create()
        {
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID");
            return View();
        }

        // POST: Professeur/Echanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LeconID,Inspecteur,Cotation,Remarque")] Echange echange)
        {
            if (ModelState.IsValid)
            {
                _context.Add(echange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", echange.LeconID);
            return View(echange);
        }

        // GET: Professeur/Echanges/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var echange = await _context.Echanges.FindAsync(id);
            if (echange == null)
            {
                return NotFound();
            }
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", echange.LeconID);
            return View(echange);
        }

        // POST: Professeur/Echanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,LeconID,Inspecteur,Cotation,Remarque")] Echange echange)
        {
            if (id != echange.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(echange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EchangeExists(echange.ID))
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
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", echange.LeconID);
            return View(echange);
        }

        // GET: Professeur/Echanges/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var echange = await _context.Echanges
                .Include(e => e.Lecon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (echange == null)
            {
                return NotFound();
            }

            return View(echange);
        }

        // POST: Professeur/Echanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var echange = await _context.Echanges.FindAsync(id);
            _context.Echanges.Remove(echange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EchangeExists(string id)
        {
            return _context.Echanges.Any(e => e.ID == id);
        }
    }
}
