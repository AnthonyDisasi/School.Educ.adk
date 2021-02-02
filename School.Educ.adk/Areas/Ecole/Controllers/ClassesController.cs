using System;
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

        private void ListSect()
        {
            List<SelectListItem> section = new List<SelectListItem>();
            SelectListItem model = new SelectListItem();
            var Sections = _context.Sections.Include(o => o.Options).AsNoTracking().ToList();
            foreach (var sect in Sections)
            {
                if (sect.Options != null)
                {
                    foreach (var opt in sect.Options)
                    {
                        model = new SelectListItem() { Text = sect.Nom + " - " + opt.Nom, Value = sect.Nom + " - " + opt.Nom };
                        section.Add(model);
                    }
                }
                if(sect.Options.Count < 1)
                {
                    model = new SelectListItem() { Text = sect.Nom, Value = sect.Nom };
                    section.Add(model);
                }
            }
            ViewBag.Sections = section;
        }

        public async Task<IActionResult> Index()
        {
            var model = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if (model.Ecole != null)
            {
                var dbEcole = _context.Classes.Include(c => c.Ecole).Include(c => c.Cours).Include(i => i.Inscriptions);
                return View(await dbEcole.ToListAsync());
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.ID });
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classe = await _context.Classes
                .Include(c => c.Ecole)
                .Include(co => co.Cours)
                .ThenInclude(p => p.Professeur)
                .Include(i => i.Inscriptions)
                .ThenInclude(e => e.Eleve)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (classe == null)
            {
                return NotFound();
            }

            return View(classe);
        }

        public IActionResult Create()
        {
            var model = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if (model.Ecole != null)
            {
                ListSect();
                return View();
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.ID });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Niveau,Section")] Classe classe)
        {
            classe.EcoleID = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID;
            if (ModelState.IsValid)
            {

                _context.Add(classe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ListSect();
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
            ListSect();
            return View(classe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Niveau,Section")] Classe classe)
        {

            classe.EcoleID = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID;
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
            ListSect();
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
        public IActionResult DeleteConfirmed(string id)
        {
            var classe = _context.Classes.Include(c => c.Cours).Include(i => i.Inscriptions).FirstOrDefault(cl => cl.ID == id);
            foreach(var item in classe.Inscriptions)
            {
                _context.Inscriptions.Remove(item);
            }
            foreach(var item in classe.Cours)
            {
                _context.Cours.Remove(item);
            }
            _context.SaveChanges();
            _context.Classes.Remove(classe);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasseExists(string id)
        {
            return _context.Classes.Any(e => e.ID == id);
        }
    }
}
