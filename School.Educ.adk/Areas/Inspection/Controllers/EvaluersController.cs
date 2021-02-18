using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Inspection.Models;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    [Authorize(Roles = "Inspecteur")]
    public class EvaluersController : Controller
    {
        private readonly EcoleDb _context;

        public EvaluersController(EcoleDb context)
        {
            _context = context;
        }

        public IActionResult Create(string leconID)
        {
            ViewData["Lecon"] = _context.Lecons.Find(leconID).LeconDonnee;
            ViewData["LeconID"] = _context.Lecons.Find(leconID).ID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LeconID,InpecteurID,Cotation,Remarque")] Evaluer evaluer)
        {
            evaluer.InpecteurID = _context.Inspecteurs.FirstOrDefault(i => i.Matricule == User.Identity.Name).ID;
            if (ModelState.IsValid)
            {
                if((evaluer.Cotation > 10) || (evaluer.Cotation < 0))
                {
                    ModelState.AddModelError(nameof(Evaluer.Cotation), "La cotation doit être comprise entre 0 et 10.");
                    return View(evaluer);
                }

                _context.Add(evaluer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Lecons", new { id = evaluer.LeconID });
            }
            ViewData["Lecon"] = _context.Lecons.Find(evaluer.LeconID).LeconDonnee;
            ViewData["LeconID"] = _context.Lecons.Find(evaluer.LeconID).ID;
            return View(evaluer);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluer = await _context.Evaluer.FindAsync(id);
            if (evaluer == null)
            {
                return NotFound();
            }
            ViewData["Lecon"] = _context.Lecons.Find(evaluer.LeconID).LeconDonnee;
            ViewData["LeconID"] = _context.Lecons.Find(evaluer.LeconID).ID;
            return View(evaluer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,LeconID,InpecteurID,Cotation,Remarque")] Evaluer evaluer)
        {
            if (id != evaluer.ID)
            {
                return NotFound();
            }

            evaluer.InpecteurID = _context.Inspecteurs.FirstOrDefault(i => i.Matricule == User.Identity.Name).ID;
            if (ModelState.IsValid)
            {
                if ((evaluer.Cotation > 10) || (evaluer.Cotation < 0))
                {
                    ModelState.AddModelError(nameof(Evaluer.Cotation), "La cotation doit être comprise entre 0 et 10.");
                    return View(evaluer);
                }

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
                return RedirectToAction("Details", "Lecons", new { id = evaluer.LeconID });
            }
            ViewData["Lecon"] = _context.Lecons.Find(evaluer.LeconID).LeconDonnee;
            ViewData["LeconID"] = _context.Lecons.Find(evaluer.LeconID).ID;
            return View(evaluer);
        }

        private bool EvaluerExists(string id)
        {
            return _context.Evaluer.Any(e => e.ID == id);
        }
    }
}
