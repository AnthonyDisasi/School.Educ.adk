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
    public class InspecteursController : Controller
    {
        private readonly InspecteurDb _context;

        public InspecteursController(InspecteurDb context)
        {
            _context = context;
        }

        // GET: Admin/Inspecteurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inspecteurs.ToListAsync());
        }

        // GET: Admin/Inspecteurs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inspecteur == null)
            {
                return NotFound();
            }

            return View(inspecteur);
        }

        // GET: Admin/Inspecteurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Inspecteurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Inspecteur inspecteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inspecteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inspecteur);
        }

        // GET: Admin/Inspecteurs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs.FindAsync(id);
            if (inspecteur == null)
            {
                return NotFound();
            }
            return View(inspecteur);
        }

        // POST: Admin/Inspecteurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Inspecteur inspecteur)
        {
            if (id != inspecteur.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inspecteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InspecteurExists(inspecteur.ID))
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
            return View(inspecteur);
        }

        // GET: Admin/Inspecteurs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inspecteur == null)
            {
                return NotFound();
            }

            return View(inspecteur);
        }

        // POST: Admin/Inspecteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var inspecteur = await _context.Inspecteurs.FindAsync(id);
            _context.Inspecteurs.Remove(inspecteur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InspecteurExists(string id)
        {
            return _context.Inspecteurs.Any(e => e.ID == id);
        }
    }
}
