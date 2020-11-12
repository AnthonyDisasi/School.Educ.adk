using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Inspection.Data;
using School.Educ.adk.Areas.Inspection.Models;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    public class ReponsesController : Controller
    {
        private readonly ExamenDb _context;

        public ReponsesController(ExamenDb context)
        {
            _context = context;
        }

        // GET: Inspection/Reponses
        public async Task<IActionResult> Index()
        {
            var examenDb = _context.Reponses.Include(r => r.Participant).Include(r => r.Question);
            return View(await examenDb.ToListAsync());
        }

        // GET: Inspection/Reponses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reponse = await _context.Reponses
                .Include(r => r.Participant)
                .Include(r => r.Question)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reponse == null)
            {
                return NotFound();
            }

            return View(reponse);
        }

        // GET: Inspection/Reponses/Create
        public IActionResult Create()
        {
            ViewData["ParticipantID"] = new SelectList(_context.Participants, "ID", "ID");
            ViewData["QuestionID"] = new SelectList(_context.Questions, "ID", "ID");
            return View();
        }

        // POST: Inspection/Reponses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,QuestionID,ParticipantID,ReponseDonnee,Point")] Reponse reponse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reponse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParticipantID"] = new SelectList(_context.Participants, "ID", "ID", reponse.ParticipantID);
            ViewData["QuestionID"] = new SelectList(_context.Questions, "ID", "ID", reponse.QuestionID);
            return View(reponse);
        }

        // GET: Inspection/Reponses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reponse = await _context.Reponses.FindAsync(id);
            if (reponse == null)
            {
                return NotFound();
            }
            ViewData["ParticipantID"] = new SelectList(_context.Participants, "ID", "ID", reponse.ParticipantID);
            ViewData["QuestionID"] = new SelectList(_context.Questions, "ID", "ID", reponse.QuestionID);
            return View(reponse);
        }

        // POST: Inspection/Reponses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,QuestionID,ParticipantID,ReponseDonnee,Point")] Reponse reponse)
        {
            if (id != reponse.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reponse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReponseExists(reponse.ID))
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
            ViewData["ParticipantID"] = new SelectList(_context.Participants, "ID", "ID", reponse.ParticipantID);
            ViewData["QuestionID"] = new SelectList(_context.Questions, "ID", "ID", reponse.QuestionID);
            return View(reponse);
        }

        // GET: Inspection/Reponses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reponse = await _context.Reponses
                .Include(r => r.Participant)
                .Include(r => r.Question)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reponse == null)
            {
                return NotFound();
            }

            return View(reponse);
        }

        // POST: Inspection/Reponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var reponse = await _context.Reponses.FindAsync(id);
            _context.Reponses.Remove(reponse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReponseExists(string id)
        {
            return _context.Reponses.Any(e => e.ID == id);
        }
    }
}
