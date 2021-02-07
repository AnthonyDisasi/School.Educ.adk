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
    public class EvaluersController : Controller
    {
        private readonly ProfeAreaDb _context;

        public EvaluersController(ProfeAreaDb context)
        {
            _context = context;
        }

        // GET: ProfeArea/Evaluers
        public async Task<IActionResult> Index()
        {
            var profeAreaDb = _context.Evaluers.Include(e => e.Inpecteur).Include(e => e.Lecon);
            return View(await profeAreaDb.ToListAsync());
        }

        // GET: ProfeArea/Evaluers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluer = await _context.Evaluers
                .Include(e => e.Inpecteur)
                .Include(e => e.Lecon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (evaluer == null)
            {
                return NotFound();
            }

            return View(evaluer);
        }

        // GET: ProfeArea/Evaluers/Create
        public IActionResult Create()
        {
            ViewData["InpecteurID"] = new SelectList(_context.Set<Inspecteur>(), "ID", "ID");
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID");
            return View();
        }

        // POST: ProfeArea/Evaluers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LeconID,InpecteurID,Cotation,Remarque")] Evaluer evaluer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InpecteurID"] = new SelectList(_context.Set<Inspecteur>(), "ID", "ID", evaluer.InpecteurID);
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", evaluer.LeconID);
            return View(evaluer);
        }

        // GET: ProfeArea/Evaluers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluer = await _context.Evaluers.FindAsync(id);
            if (evaluer == null)
            {
                return NotFound();
            }
            ViewData["InpecteurID"] = new SelectList(_context.Set<Inspecteur>(), "ID", "ID", evaluer.InpecteurID);
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", evaluer.LeconID);
            return View(evaluer);
        }

        // POST: ProfeArea/Evaluers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,LeconID,InpecteurID,Cotation,Remarque")] Evaluer evaluer)
        {
            if (id != evaluer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluerExists(evaluer.ID))
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
            ViewData["InpecteurID"] = new SelectList(_context.Set<Inspecteur>(), "ID", "ID", evaluer.InpecteurID);
            ViewData["LeconID"] = new SelectList(_context.Lecons, "ID", "ID", evaluer.LeconID);
            return View(evaluer);
        }

        // GET: ProfeArea/Evaluers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluer = await _context.Evaluers
                .Include(e => e.Inpecteur)
                .Include(e => e.Lecon)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (evaluer == null)
            {
                return NotFound();
            }

            return View(evaluer);
        }

        // POST: ProfeArea/Evaluers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var evaluer = await _context.Evaluers.FindAsync(id);
            _context.Evaluers.Remove(evaluer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluerExists(string id)
        {
            return _context.Evaluers.Any(e => e.ID == id);
        }
    }
}
