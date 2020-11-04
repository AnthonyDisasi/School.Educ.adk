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
    public class AssertionsController : Controller
    {
        private readonly InspecteurDb _context;

        public AssertionsController(InspecteurDb context)
        {
            _context = context;
        }

        // GET: Admin/Assertions
        public async Task<IActionResult> Index()
        {
            var inspecteurDb = _context.Assertions.Include(a => a.Question);
            return View(await inspecteurDb.ToListAsync());
        }

        // GET: Admin/Assertions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assertion = await _context.Assertions
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assertion == null)
            {
                return NotFound();
            }

            return View(assertion);
        }

        // GET: Admin/Assertions/Create
        public IActionResult Create()
        {
            ViewData["QuestionID"] = new SelectList(_context.Questions, "ID", "ID");
            return View();
        }

        // POST: Admin/Assertions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,QuestionID,Intituler")] Assertion assertion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assertion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionID"] = new SelectList(_context.Questions, "ID", "ID", assertion.QuestionID);
            return View(assertion);
        }

        // GET: Admin/Assertions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assertion = await _context.Assertions.FindAsync(id);
            if (assertion == null)
            {
                return NotFound();
            }
            ViewData["QuestionID"] = new SelectList(_context.Questions, "ID", "ID", assertion.QuestionID);
            return View(assertion);
        }

        // POST: Admin/Assertions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,QuestionID,Intituler")] Assertion assertion)
        {
            if (id != assertion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assertion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssertionExists(assertion.ID))
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
            ViewData["QuestionID"] = new SelectList(_context.Questions, "ID", "ID", assertion.QuestionID);
            return View(assertion);
        }

        // GET: Admin/Assertions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assertion = await _context.Assertions
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assertion == null)
            {
                return NotFound();
            }

            return View(assertion);
        }

        // POST: Admin/Assertions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var assertion = await _context.Assertions.FindAsync(id);
            _context.Assertions.Remove(assertion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssertionExists(string id)
        {
            return _context.Assertions.Any(e => e.ID == id);
        }
    }
}
