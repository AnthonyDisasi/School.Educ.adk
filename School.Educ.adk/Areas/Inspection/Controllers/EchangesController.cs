using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Inspection.Data;
using School.Educ.adk.Areas.Professeur.Models;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    public class EchangesController : Controller
    {
        private readonly ExamenDb _context;

        public EchangesController(ExamenDb context)
        {
            _context = context;
        }

        // GET: Inspection/Echanges
        public async Task<IActionResult> Index()
        {
            var examenDb = _context.Echange.Include(e => e.Lecon);
            return View(await examenDb.ToListAsync());
        }

        // GET: Inspection/Echanges/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var echange = await _context.Echange
                .Include(e => e.Lecon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (echange == null)
            {
                return NotFound();
            }

            return View(echange);
        }

        // GET: Inspection/Echanges/Create
        public IActionResult Create()
        {
            ViewData["LeconID"] = new SelectList(_context.Set<Lecon>(), "ID", "ID");
            return View();
        }

        // POST: Inspection/Echanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LeconID,Inspecteur,Cotation,Remarque")] Echange echange)
        {
            if (ModelState.IsValid)
            {
                _context.Add(echange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeconID"] = new SelectList(_context.Set<Lecon>(), "ID", "ID", echange.LeconID);
            return View(echange);
        }

        // GET: Inspection/Echanges/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var echange = await _context.Echange.FindAsync(id);
            if (echange == null)
            {
                return NotFound();
            }
            ViewData["LeconID"] = new SelectList(_context.Set<Lecon>(), "ID", "ID", echange.LeconID);
            return View(echange);
        }

        // POST: Inspection/Echanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,LeconID,Inspecteur,Cotation,Remarque")] Echange echange)
        {
            if (id != echange.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(echange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EchangeExists(echange.ID))
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
            ViewData["LeconID"] = new SelectList(_context.Set<Lecon>(), "ID", "ID", echange.LeconID);
            return View(echange);
        }

        // GET: Inspection/Echanges/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var echange = await _context.Echange
                .Include(e => e.Lecon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (echange == null)
            {
                return NotFound();
            }

            return View(echange);
        }

        // POST: Inspection/Echanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var echange = await _context.Echange.FindAsync(id);
            _context.Echange.Remove(echange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EchangeExists(string id)
        {
            return _context.Echange.Any(e => e.ID == id);
        }
    }
}
