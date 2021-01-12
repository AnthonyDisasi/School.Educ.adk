using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    //[Authorize(Roles = "Inspecteur")]
    public class InspecteursController : Controller
    {
        private readonly InspecteurDb _context;

        public InspecteursController(InspecteurDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inspecteur == null)
            {
                return NotFound();
            }
            return View(inspecteur);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs.FindAsync(id);
            if (inspecteur == null)
            {
                return NotFound();
            }
            return View(inspecteur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Inspecteur inspecteur)
        {
            if (id != inspecteur.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inspecteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InspecteurExists(inspecteur.ID))
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
            return View(inspecteur);
        }

        private bool InspecteurExists(string id)
        {
            return _context.Inspecteurs.Any(e => e.ID == id);
        }
    }
}
