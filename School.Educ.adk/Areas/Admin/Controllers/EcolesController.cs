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
        private readonly DbEcole _context;

        public EcolesController(DbEcole context)
        {
            _context = context;
        }

        // GET: Admin/Ecoles
        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.Ecoles.Include(e => e.Directeur);
            return View(await dbEcole.ToListAsync());
        }

        // GET: Admin/Ecoles/Details/5
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

        // GET: Admin/Ecoles/Create
        public IActionResult Create()
        {
            //ViewData["DirecteurID"] = new SelectList(_context.Directeurs, "ID", "ID");
            ViewData["DirecteurID"] = (from d in _context.Directeurs where d.Ecole == null select new SelectListItem { Text = d.Matricule, Value = d.ID }).ToList();
            return View();
        }

        // POST: Admin/Ecoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            //ViewData["DirecteurID"] = new SelectList(_context.Directeurs, "ID", "ID", ecole.DirecteurID);
            ViewData["DirecteurID"] = (from d in _context.Directeurs where d.Ecole == null select new SelectListItem { Text = d.Matricule, Value = d.ID }).ToList();
            return View(ecole);
        }

        // GET: Admin/Ecoles/Edit/5
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
            //ViewData["DirecteurID"] = new SelectList(_context.Directeurs, "ID", "ID", ecole.DirecteurID);
            ViewData["DirecteurID"] =  (from d in _context.Directeurs where d.Ecole == null select new SelectListItem { Text = d.Matricule, Value = d.ID }).ToList();
            return View(ecole);
        }

        // POST: Admin/Ecoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,DirecteurID,Nom,EcoleLatitude,EcoleLongitude,SousDivision,DateCreate")] Ecole.Models.Ecole ecole)
        {
            if (id != ecole.ID)
            {
                return NotFound();
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
            //ViewData["DirecteurID"] = new SelectList(_context.Directeurs, "ID", "ID", ecole.DirecteurID);
            ViewData["DirecteurID"] = (from d in _context.Directeurs where d.Ecole == null select new SelectListItem { Text = d.Matricule, Value = d.ID }).ToList();
            return View(ecole);
        }

        // GET: Admin/Ecoles/Delete/5
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

        // POST: Admin/Ecoles/Delete/5
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
    }
}
