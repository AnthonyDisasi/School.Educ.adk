using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EcolesController : Controller
    {
        private readonly EcoleDb _context;

        public EcolesController(EcoleDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.Ecoles
                .Include(e => e.Directeur)
                .Include(p => p.Professeurs)
                .Include(c => c.Classes);
            return View(await dbEcole.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecole = await _context.Ecoles
                .Include(e => e.Directeur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ecole == null)
            {
                return NotFound();
            }

            return View(ecole);
        }

        public IActionResult Create()
        {
            ViewData["DirecteurID"] = (from d in _context.Directeurs where d.Ecole == null select new SelectListItem { Text = d.Matricule, Value = d.ID }).ToList();
            ListSousDivision();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DirecteurID,Nom,EcoleLatitude,EcoleLongitude,SousDivision,DateCreate")] Ecole.Models.Ecole ecole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ecole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirecteurID"] = (from d in _context.Directeurs where d.Ecole == null select new SelectListItem { Text = d.Matricule, Value = d.ID }).ToList();
            ListSousDivision();
            return View(ecole);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecole = await _context.Ecoles.FindAsync(id);
            if (ecole == null)
            {
                return NotFound();
            }
            ViewData["DirecteurID"] =  (from d in _context.Directeurs where d.Ecole == null select new SelectListItem { Text = d.Matricule, Value = d.ID }).ToList();
            ListSousDivision();
            return View(ecole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,DirecteurID,Nom,EcoleLatitude,EcoleLongitude,SousDivision,DateCreate,Niveau")] Ecole.Models.Ecole ecole)
        {
            if (id != ecole.ID)
            {
                return NotFound();
            }
            if(ecole.Niveau < 0 || ecole.Niveau > 10)
            {
                ModelState.AddModelError(nameof(ecole.Niveau), "La valeur doit être comprise entre 0 et 10.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ecole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EcoleExists(ecole.ID))
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
            ViewData["DirecteurID"] = (from d in _context.Directeurs where d.Ecole == null select new SelectListItem { Text = d.Matricule, Value = d.ID }).ToList();
            ListSousDivision();
            return View(ecole);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecole = await _context.Ecoles
                .Include(e => e.Directeur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ecole == null)
            {
                return NotFound();
            }

            return View(ecole);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ecole = await _context.Ecoles.FindAsync(id);
            _context.Ecoles.Remove(ecole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EcoleExists(string id)
        {
            return _context.Ecoles.Any(e => e.ID == id);
        }
         
        private void ListSousDivision()
        {
            ViewData["SousDivision"] = (from s in _context.SousDivisions select new SelectListItem { Text = s.Nom, Value = s.Nom }).ToList();
        }
    }
}
