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
    public class InscriptionsController : Controller
    {
        private readonly DbEcole _context;

        public InscriptionsController(DbEcole context)
        {
            _context = context;
        }

        // GET: Ecole/Inscriptions
        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.Inscriptions.Include(i => i.Classe).Include(i => i.Eleve);
            return View(await dbEcole.ToListAsync());
        }

        // GET: Ecole/Inscriptions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscription = await _context.Inscriptions
                .Include(i => i.Classe)
                .Include(i => i.Eleve)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inscription == null)
            {
                return NotFound();
            }

            return View(inscription);
        }

        // GET: Ecole/Inscriptions/Create
        public IActionResult Create()
        {
            ViewData["ClasseID"] = new SelectList(_context.Classes, "ID", "ID");
            ViewData["EleveId"] = new SelectList(_context.Eleves, "ID", "ID");
            return View();
        }

        // POST: Ecole/Inscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EleveId,ClasseID,DateInscription,AnneeScolaire")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasseID"] = new SelectList(_context.Classes, "ID", "ID", inscription.ClasseID);
            ViewData["EleveId"] = new SelectList(_context.Eleves, "ID", "ID", inscription.EleveId);
            return View(inscription);
        }

        // GET: Ecole/Inscriptions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscription = await _context.Inscriptions.FindAsync(id);
            if (inscription == null)
            {
                return NotFound();
            }
            ViewData["ClasseID"] = new SelectList(_context.Classes, "ID", "ID", inscription.ClasseID);
            ViewData["EleveId"] = new SelectList(_context.Eleves, "ID", "ID", inscription.EleveId);
            return View(inscription);
        }

        // POST: Ecole/Inscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EleveId,ClasseID,DateInscription,AnneeScolaire")] Inscription inscription)
        {
            if (id != inscription.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscriptionExists(inscription.ID))
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
            ViewData["ClasseID"] = new SelectList(_context.Classes, "ID", "ID", inscription.ClasseID);
            ViewData["EleveId"] = new SelectList(_context.Eleves, "ID", "ID", inscription.EleveId);
            return View(inscription);
        }

        // GET: Ecole/Inscriptions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscription = await _context.Inscriptions
                .Include(i => i.Classe)
                .Include(i => i.Eleve)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inscription == null)
            {
                return NotFound();
            }

            return View(inscription);
        }

        // POST: Ecole/Inscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var inscription = await _context.Inscriptions.FindAsync(id);
            _context.Inscriptions.Remove(inscription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscriptionExists(string id)
        {
            return _context.Inscriptions.Any(e => e.ID == id);
        }
    }
}
