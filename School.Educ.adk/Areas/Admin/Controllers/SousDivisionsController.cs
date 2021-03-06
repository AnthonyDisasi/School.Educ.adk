﻿using System;
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
        private readonly EcoleDb _context;

        public SousDivisionsController(EcoleDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.SousDivisions.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

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
