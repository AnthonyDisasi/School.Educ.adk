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
    public class QuestionsController : Controller
    {
        private readonly InspecteurDb _context;

        public QuestionsController(InspecteurDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Examen)
                .Include(a => a.Assertions)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        public IActionResult Create(string idExamen, string enoncer, string bonneReponse, string cote, string lettre)
        {
            if ((enoncer != null) & (bonneReponse != null) & (cote != null) & (lettre != null))
            {
                Question model = new Question
                {
                    ExamenID = idExamen,
                    Enoncer = enoncer,
                    BonneReponse = bonneReponse,
                    Cote = double.Parse(cote),
                    Lettre = lettre
                };
                if (ModelState.IsValid)
                {
                    _context.Add(model);
                    _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Examen", new { id = idExamen });
                }
            }
            return RedirectToAction("Details", "Examen", new { id = idExamen });
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["ExamenID"] = new SelectList(_context.Examens, "ID", "ID", question.ExamenID);
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,ExamenID,Enoncer,BonneReponse,Lettre,Cote")] Question question)
        {
            if (id != question.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Examen", new { id = question.ExamenID });
            }
            ViewData["ExamenID"] = new SelectList(_context.Examens, "ID", "ID", question.ExamenID);
            return View(question);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Examen)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Examen", new { id = question.ExamenID });
        }

        private bool QuestionExists(string id)
        {
            return _context.Questions.Any(e => e.ID == id);
        }
    }
}
