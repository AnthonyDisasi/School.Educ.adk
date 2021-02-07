using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.ProfeArea.Data;
using School.Educ.adk.Areas.ProfeArea.Models;

namespace School.Educ.adk.Areas.ProfeArea.Controllers
{
    [Area("ProfeArea")]
    public class CotationsController : Controller
    {
        private readonly ProfeAreaDb _context;

        public CotationsController(ProfeAreaDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var profeAreaDb = _context.Cotations.Include(c => c.Eleve).Include(c => c.Epreuve);
            return View(await profeAreaDb.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotation = await _context.Cotations
                .Include(c => c.Eleve)
                .Include(c => c.Epreuve)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cotation == null)
            {
                return NotFound();
            }

            return View(cotation);
        }

        public IActionResult Create()
        {
            ViewData["EleveID"] = new SelectList(_context.Set<Ecole.Models.Eleve>(), "ID", "ID");
            ViewData["EpreuveID"] = new SelectList(_context.Epreuves, "ID", "ID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EpreuveID,EleveID,Point")] Cotation cotation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EleveID"] = new SelectList(_context.Set<Ecole.Models.Eleve>(), "ID", "ID", cotation.EleveID);
            ViewData["EpreuveID"] = new SelectList(_context.Epreuves, "ID", "ID", cotation.EpreuveID);
            return View(cotation);
        }

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
            ViewData["EleveID"] = new SelectList(_context.Set<Ecole.Models.Eleve>(), "ID", "ID", cotation.EleveID);
            ViewData["EpreuveID"] = new SelectList(_context.Epreuves, "ID", "ID", cotation.EpreuveID);
            return View(cotation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EpreuveID,EleveID,Point")] Cotation cotation)
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
            ViewData["EleveID"] = new SelectList(_context.Set<Ecole.Models.Eleve>(), "ID", "ID", cotation.EleveID);
            ViewData["EpreuveID"] = new SelectList(_context.Epreuves, "ID", "ID", cotation.EpreuveID);
            return View(cotation);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotation = await _context.Cotations
                .Include(c => c.Eleve)
                .Include(c => c.Epreuve)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cotation == null)
            {
                return NotFound();
            }

            return View(cotation);
        }

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
