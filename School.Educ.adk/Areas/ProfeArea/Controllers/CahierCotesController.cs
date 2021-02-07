﻿using System;
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
    public class CahierCotesController : Controller
    {
        private readonly ProfeAreaDb _context;

        public CahierCotesController(ProfeAreaDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var profeAreaDb = _context.CahierCotes.Include(c => c.Cours);
            return View(await profeAreaDb.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cahierCote = await _context.CahierCotes
                .Include(c => c.Cours)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cahierCote == null)
            {
                return NotFound();
            }

            return View(cahierCote);
        }

        public IActionResult Create()
        {
            ViewData["CoursID"] = new SelectList(_context.Set<Ecole.Models.Cours>(), "ID", "ID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CoursID,Periode,Total")] CahierCote cahierCote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cahierCote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoursID"] = new SelectList(_context.Set<Ecole.Models.Cours>(), "ID", "ID", cahierCote.CoursID);
            return View(cahierCote);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cahierCote = await _context.CahierCotes.FindAsync(id);
            if (cahierCote == null)
            {
                return NotFound();
            }
            ViewData["CoursID"] = new SelectList(_context.Set<Ecole.Models.Cours>(), "ID", "ID", cahierCote.CoursID);
            return View(cahierCote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,CoursID,Periode,Total")] CahierCote cahierCote)
        {
            if (id != cahierCote.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cahierCote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CahierCoteExists(cahierCote.ID))
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
            ViewData["CoursID"] = new SelectList(_context.Set<Ecole.Models.Cours>(), "ID", "ID", cahierCote.CoursID);
            return View(cahierCote);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cahierCote = await _context.CahierCotes
                .Include(c => c.Cours)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cahierCote == null)
            {
                return NotFound();
            }

            return View(cahierCote);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cahierCote = await _context.CahierCotes.FindAsync(id);
            _context.CahierCotes.Remove(cahierCote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CahierCoteExists(string id)
        {
            return _context.CahierCotes.Any(e => e.ID == id);
        }
    }
}
