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
    public class CotationsController : Controller
    {
        private readonly ProfesseurDb _context;

        public CotationsController(ProfesseurDb context)
        {
            _context = context;
        }

        // GET: Professeur/Cotations
        public async Task<IActionResult> Index()
        {
            var professeurDb = _context.Cotations.Include(c => c.Epreuve);
            return View(await professeurDb.ToListAsync());
        }

        // GET: Professeur/Cotations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotation = await _context.Cotations
                .Include(c => c.Epreuve)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cotation == null)
            {
                return NotFound();
            }

            return View(cotation);
        }

        // GET: Professeur/Cotations/Create
        public IActionResult Create()
        {
            ViewData["EpreuveID"] = new SelectList(_context.Epreuves, "ID", "ID");
            return View();
        }

        // POST: Professeur/Cotations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EpreuveID,IdentifiantEleve,Point")] Cotation cotation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EpreuveID"] = new SelectList(_context.Epreuves, "ID", "ID", cotation.EpreuveID);
            return View(cotation);
        }

        // GET: Professeur/Cotations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotation = await _context.Cotations.FindAsync(id);
            if (cotation == null)
            {
                return NotFound();
            }
            ViewData["EpreuveID"] = new SelectList(_context.Epreuves, "ID", "ID", cotation.EpreuveID);
            return View(cotation);
        }

        // POST: Professeur/Cotations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EpreuveID,IdentifiantEleve,Point")] Cotation cotation)
        {
            if (id != cotation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotationExists(cotation.ID))
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
            ViewData["EpreuveID"] = new SelectList(_context.Epreuves, "ID", "ID", cotation.EpreuveID);
            return View(cotation);
        }

        // GET: Professeur/Cotations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotation = await _context.Cotations
                .Include(c => c.Epreuve)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cotation == null)
            {
                return NotFound();
            }

            return View(cotation);
        }

        // POST: Professeur/Cotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cotation = await _context.Cotations.FindAsync(id);
            _context.Cotations.Remove(cotation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotationExists(string id)
        {
            return _context.Cotations.Any(e => e.ID == id);
        }
    }
}
