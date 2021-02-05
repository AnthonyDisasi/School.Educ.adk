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
    public class SectionsController : Controller
    {
        private readonly DbEcole _context;

        public SectionsController(DbEcole context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string Nom)
        {
            if(Nom != null)
            {
                Section model = new Section
                {
                    Nom = Nom
                };
                if (ModelState.IsValid)
                {
                    _context.Add(model);
                    _context.SaveChanges();
                }
            }
            return View(await _context.Sections.Include(o => o.Options).ToListAsync());
        }

        public async Task<IActionResult> Details(string id, string Nom)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (Nom != null)
            {
                Option model = new Option
                {
                    Nom = Nom,
                    SectionID = id
                };
                if (ModelState.IsValid)
                {
                    _context.Add(model);
                    _context.SaveChanges();
                }
            }

            var section = await _context.Sections
                .Include(o => o.Options)
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom")] Section section)
        {
            if (id != section.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = section.ID });
            }
            return View(section);
        }

        public IActionResult Delete(string id)
        {
            var section = _context.Sections.Find(id);
            _context.Sections.Remove(section);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteOption(string id, string idSec)
        {
            var option = _context.Options.Find(id);
            _context.Options.Remove(option);
            _context.SaveChanges();
            var section = _context.Sections.Find(idSec);
            return RedirectToAction("Details", new { id = idSec });
        }

        private bool SectionExists(string id)
        {
            return _context.Sections.Any(e => e.ID == id);
        }
    }
}
