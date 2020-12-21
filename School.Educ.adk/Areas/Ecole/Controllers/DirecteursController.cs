using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    [Authorize(Roles = "Directeur")]
    public class DirecteursController : Controller
    {
        private readonly DbEcole _context;

        public DirecteursController(DbEcole context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (directeur == null)
            {
                return NotFound();
            }

            return View(directeur);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs.FindAsync(id);
            if (directeur == null)
            {
                return NotFound();
            }
            return View(directeur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,DateNaissance")] Directeur directeur)
        {
            if (id != directeur.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directeur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirecteurExists(directeur.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            return View(directeur);
        }

        private bool DirecteurExists(string id)
        {
            return _context.Directeurs.Any(e => e.ID == id);
        }
    }
}
