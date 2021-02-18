using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    public class LeconsController : Controller
    {
        private readonly EcoleDb _context;

        public LeconsController(EcoleDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ecoleDb = _context.Lecons
                .Include(l => l.Cours)
                .Include(l => l.Professeur)
                .ThenInclude(l => l.Ecole)
                .Include(l => l.Evaluer)
                .Where(l => l.Professeur.Ecole.Inspecteur.Matricule == User.Identity.Name);
            return View(await ecoleDb.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecon = await _context.Lecons
                .Include(l => l.Cours)
                .ThenInclude(l => l.Classe)
                .ThenInclude(l => l.Inscriptions)
                .Include(l => l.Professeur)
                .Include(l => l.Evaluer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lecon == null)
            {
                return NotFound();
            }

            return View(lecon);
        }
    }
}
