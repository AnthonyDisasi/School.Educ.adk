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
    public class SousDivisionsController : Controller
    {
        private readonly DbEcole _context;

        public SousDivisionsController(DbEcole context)
        {
            _context = context;
        }

        // GET: Admin/SousDivisions
        public async Task<IActionResult> Index()
        {
            return View(await _context.SousDivisions.ToListAsync());
        }

        // GET: Admin/SousDivisions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sousDivision = await _context.SousDivisions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sousDivision == null)
            {
                return NotFound();
            }

            return View(sousDivision);
        }

        // GET: Admin/SousDivisions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SousDivisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,LocalDescript")] SousDivision sousDivision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sousDivision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sousDivision);
        }

        // GET: Admin/SousDivisions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sousDivision = await _context.SousDivisions.FindAsync(id);
            if (sousDivision == null)
            {
                return NotFound();
            }
            return View(sousDivision);
        }

        // POST: Admin/SousDivisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,LocalDescript")] SousDivision sousDivision)
        {
            if (id != sousDivision.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sousDivision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SousDivisionExists(sousDivision.ID))
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
            return View(sousDivision);
        }

        // GET: Admin/SousDivisions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sousDivision = await _context.SousDivisions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sousDivision == null)
            {
                return NotFound();
            }

            return View(sousDivision);
        }

        // POST: Admin/SousDivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sousDivision = await _context.SousDivisions.FindAsync(id);
            _context.SousDivisions.Remove(sousDivision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SousDivisionExists(string id)
        {
            return _context.SousDivisions.Any(e => e.ID == id);
        }
    }
}
