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
    public class EpreuvesController : Controller
    {
        private readonly ProfeAreaDb _context;

        public EpreuvesController(ProfeAreaDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var profeAreaDb = _context.Epreuves.Include(e => e.CahierCote);
            return View(await profeAreaDb.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuves
                .Include(e => e.CahierCote)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (epreuve == null)
            {
                return NotFound();
            }

            return View(epreuve);
        }

        public IActionResult Create()
        {
            ViewData["CahierCoteID"] = new SelectList(_context.CahierCotes, "ID", "ID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CahierCoteID,Description,DateEpreuve,Total")] Epreuve epreuve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epreuve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CahierCoteID"] = new SelectList(_context.CahierCotes, "ID", "ID", epreuve.CahierCoteID);
            return View(epreuve);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuves.FindAsync(id);
            if (epreuve == null)
            {
                return NotFound();
            }
            ViewData["CahierCoteID"] = new SelectList(_context.CahierCotes, "ID", "ID", epreuve.CahierCoteID);
            return View(epreuve);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,CahierCoteID,Description,DateEpreuve,Total")] Epreuve epreuve)
        {
            if (id != epreuve.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epreuve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpreuveExists(epreuve.ID))
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
            ViewData["CahierCoteID"] = new SelectList(_context.CahierCotes, "ID", "ID", epreuve.CahierCoteID);
            return View(epreuve);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuves
                .Include(e => e.CahierCote)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (epreuve == null)
            {
                return NotFound();
            }

            return View(epreuve);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var epreuve = await _context.Epreuves.FindAsync(id);
            _context.Epreuves.Remove(epreuve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpreuveExists(string id)
        {
            return _context.Epreuves.Any(e => e.ID == id);
        }
    }
}
