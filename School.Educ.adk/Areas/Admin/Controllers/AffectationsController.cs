using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AffectationsController : Controller
    {
        private readonly InspecteurDb _context;

        public AffectationsController(InspecteurDb context)
        {
            _context = context;
        }

        // GET: Admin/Affectations
        public async Task<IActionResult> Index()
        {
            var inspecteurDb = _context.Affectations.Include(a => a.Ecole).Include(a => a.Inspecteur);
            return View(await inspecteurDb.ToListAsync());
        }

        // GET: Admin/Affectations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectation = await _context.Affectations
                .Include(a => a.Ecole)
                .Include(a => a.Inspecteur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (affectation == null)
            {
                return NotFound();
            }

            return View(affectation);
        }

        // GET: Admin/Affectations/Create
        public IActionResult Create()
        {
            ViewData["EcoleID"] = new SelectList(_context.Set<Ecole.Models.Ecole>(), "ID", "ID");
            ViewData["InspecteurID"] = new SelectList(_context.Inspecteurs, "ID", "ID");
            return View();
        }

        // POST: Admin/Affectations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EcoleID,InspecteurID,Description,PeriodeAffectectation,DateAffectation")] Affectation affectation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(affectation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EcoleID"] = new SelectList(_context.Set<Ecole.Models.Ecole>(), "ID", "ID", affectation.EcoleID);
            ViewData["InspecteurID"] = new SelectList(_context.Inspecteurs, "ID", "ID", affectation.InspecteurID);
            return View(affectation);
        }

        // GET: Admin/Affectations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectation = await _context.Affectations.FindAsync(id);
            if (affectation == null)
            {
                return NotFound();
            }
            ViewData["EcoleID"] = new SelectList(_context.Set<Ecole.Models.Ecole>(), "ID", "ID", affectation.EcoleID);
            ViewData["InspecteurID"] = new SelectList(_context.Inspecteurs, "ID", "ID", affectation.InspecteurID);
            return View(affectation);
        }

        // POST: Admin/Affectations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EcoleID,InspecteurID,Description,PeriodeAffectectation,DateAffectation")] Affectation affectation)
        {
            if (id != affectation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(affectation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AffectationExists(affectation.ID))
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
            ViewData["EcoleID"] = new SelectList(_context.Set<Ecole.Models.Ecole>(), "ID", "ID", affectation.EcoleID);
            ViewData["InspecteurID"] = new SelectList(_context.Inspecteurs, "ID", "ID", affectation.InspecteurID);
            return View(affectation);
        }

        // GET: Admin/Affectations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectation = await _context.Affectations
                .Include(a => a.Ecole)
                .Include(a => a.Inspecteur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (affectation == null)
            {
                return NotFound();
            }

            return View(affectation);
        }

        // POST: Admin/Affectations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var affectation = await _context.Affectations.FindAsync(id);
            _context.Affectations.Remove(affectation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AffectationExists(string id)
        {
            return _context.Affectations.Any(e => e.ID == id);
        }
    }
}
