using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;
using School.Educ.adk.Areas.Ecole.DataContext;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AffectationsController : Controller
    {
        private readonly InspecteurDb _context;
        private readonly DbEcole _cont;

        public AffectationsController(InspecteurDb context, DbEcole cont)
        {
            _context = context;
            _cont = cont;
        }

        public IActionResult Create(string ID)
        {
            ViewData["InspecteurID"] = ID;
            ViewData["IdEcole"] = (from e in _cont.Ecoles select new SelectListItem { Text = e.Nom, Value = e.ID }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InspecteurID,IdEcole,Description,PeriodeAffectectation,DateAffectation")] Affectation affectation, string ID)
        {
            affectation.InspecteurID = ID;
            if (ModelState.IsValid)
            {
                _context.Add(affectation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Inspecteurs", new { id = ID });
            }
            ViewData["InspecteurID"] = ID;
            ViewData["IdEcole"] = (from e in _cont.Ecoles select new SelectListItem { Text = e.Nom, Value = e.ID }).ToList();
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
            ViewData["Inspecteur_ID"] = affectation.InspecteurID;
            ViewData["IdEcole"] = (from e in _cont.Ecoles select new SelectListItem { Text = e.Nom, Value = e.ID }).ToList();
            return View(affectation);
        }

        // POST: Admin/Affectations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id_ins, string id, [Bind("ID,InspecteurID,IdEcole,Description,PeriodeAffectectation,DateAffectation")] Affectation affectation)
        {
            if (id != affectation.ID)
            {
                return NotFound();
            }
            affectation.InspecteurID = id_ins;
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
                return RedirectToAction("Details", "Inspecteurs", new { id = id_ins });
            }
            ViewData["Inspecteur_ID"] = id_ins;
            ViewData["IdEcole"] = (from e in _cont.Ecoles select new SelectListItem { Text = e.Nom, Value = e.ID }).ToList();
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
            return RedirectToAction("Index", "Inspecteurs");
        }

        private bool AffectationExists(string id)
        {
            return _context.Affectations.Any(e => e.ID == id);
        }
    }
}
