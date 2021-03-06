﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;
using School.Educ.adk.Data;

namespace School.Educ.adk.Areas.ProfeArea.Controllers
{
    [Area("ProfeArea")]
    [Authorize(Roles = "Professeur")]
    public class LeconsController : Controller
    {
        private readonly EcoleDb _context;

        public LeconsController(EcoleDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dbEcole = _context.Lecons
                .Include(o => o.Cours)
                .Include(l => l.Evaluer)
                .Include(l => l.Professeur)
                .AsNoTracking()
                .Where(c => c.Professeur.Matricule == User.Identity.Name)
                .ToList();
            return View(dbEcole);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons
                .Include(c => c.Cours)
                .Include(l => l.Professeur)
                .Include(e => e.Evaluer)
                .ThenInclude(l => l.Inpecteur)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecon == null)
            {
                return NotFound();
            }

            return View(lecon);
        }

        public IActionResult Create()
        {
            ViewData["CoursID"] = new SelectList(_context.Cours.Where(d_i => d_i.Professeur.Matricule == User.Identity.Name), "ID", "Intituler");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProfesseurID,CoursID,LeconDonnee,DateLecon")] Lecon lecon)
        {
            lecon.ProfesseurID = _context.Professeurs.FirstOrDefault(i_d => i_d.Matricule == User.Identity.Name).ID;
            if (ModelState.IsValid)
            {
                _context.Add(lecon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoursID"] = new SelectList(_context.Cours.Where(d_i => d_i.Professeur.Matricule == User.Identity.Name), "ID", "Intituler", lecon.CoursID);
            return View(lecon);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons.FindAsync(id);
            if (lecon == null)
            {
                return NotFound();
            }
            ViewData["CoursID"] = new SelectList(_context.Cours.Where(d_i => d_i.Professeur.Matricule == User.Identity.Name), "ID", "Intituler", lecon.CoursID);
            return View(lecon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,ProfesseurID,CoursID,LeconDonnee,DateLecon")] Lecon lecon)
        {
            if (id != lecon.ID)
            {
                return NotFound();
            }

            lecon.ProfesseurID = _context.Professeurs.FirstOrDefault(i_d => i_d.Matricule == User.Identity.Name).ID;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeconExists(lecon.ID))
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
            ViewData["CoursID"] = new SelectList(_context.Cours.Where(d_i => d_i.Professeur.Matricule == User.Identity.Name), "ID", "Intituler", lecon.CoursID);
            return View(lecon);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons
                .Include(l => l.Cours)
                .Include(l => l.Professeur)
                .Include(l => l.Evaluer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecon == null)
            {
                return NotFound();
            }

            return View(lecon);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lecon = await _context.Lecons.FindAsync(id);
            _context.Lecons.Remove(lecon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeconExists(string id)
        {
            return _context.Lecons.Any(e => e.ID == id);
        }
    }
}
