using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Eleve.Controllers
{
    [Area("Eleve")]
    public class ElevesController : Controller
    {
        private readonly EcoleDb _context;

        public ElevesController(EcoleDb context)
        {
            _context = context;
        }

        // GET: Eleve/Eleves
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eleves.ToListAsync());
        }

        // GET: Eleve/Eleves/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eleve = await _context.Eleves
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eleve == null)
            {
                return NotFound();
            }

            return View(eleve);
        }

        // GET: Eleve/Eleves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eleve/Eleves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Password,DateNaissance")] Ecole.Models.Eleve eleve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eleve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eleve);
        }

        // GET: Eleve/Eleves/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eleve = await _context.Eleves.FindAsync(id);
            if (eleve == null)
            {
                return NotFound();
            }
            return View(eleve);
        }

        // POST: Eleve/Eleves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Password,DateNaissance")] Ecole.Models.Eleve eleve)
        {
            if (id != eleve.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eleve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EleveExists(eleve.ID))
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
            return View(eleve);
        }

        // GET: Eleve/Eleves/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eleve = await _context.Eleves
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eleve == null)
            {
                return NotFound();
            }

            return View(eleve);
        }

        // POST: Eleve/Eleves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var eleve = await _context.Eleves.FindAsync(id);
            _context.Eleves.Remove(eleve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EleveExists(string id)
        {
            return _context.Eleves.Any(e => e.ID == id);
        }
    }
}
