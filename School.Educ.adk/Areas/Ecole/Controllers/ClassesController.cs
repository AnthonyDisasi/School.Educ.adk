﻿using System;
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
    public class ClassesController : Controller
    {
        private readonly DbEcole _context;

        public ClassesController(DbEcole context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.Classes.Include(c => c.Ecole);
            return View(await dbEcole.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes
                .Include(c => c.Ecole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (classe == null)
            {
                return NotFound();
            }

            return View(classe);
        }

        public IActionResult Create()
        {
            ViewData["EcoleID"] = new SelectList(_context.Ecoles, "ID", "ID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EcoleID,Niveau,Section,Option")] Classe classe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EcoleID"] = new SelectList(_context.Ecoles, "ID", "ID", classe.EcoleID);
            return View(classe);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes.FindAsync(id);
            if (classe == null)
            {
                return NotFound();
            }
            ViewData["EcoleID"] = new SelectList(_context.Ecoles, "ID", "ID", classe.EcoleID);
            return View(classe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,EcoleID,Niveau,Section,Option")] Classe classe)
        {
            if (id != classe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasseExists(classe.ID))
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
            ViewData["EcoleID"] = new SelectList(_context.Ecoles, "ID", "ID", classe.EcoleID);
            return View(classe);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes
                .Include(c => c.Ecole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (classe == null)
            {
                return NotFound();
            }

            return View(classe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var classe = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(classe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasseExists(string id)
        {
            return _context.Classes.Any(e => e.ID == id);
        }
    }
}