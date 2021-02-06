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
    public class EcolesController : Controller
    {
        private readonly DbEcole _context;

        public EcolesController(DbEcole context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dbEcole = _context.Ecoles
                .Include(e => e.Directeur)
                .Include(c => c.Classes)
                .Include(el => el.Eleves)
                .Include(p => p.Professeurs);
            return View(await dbEcole.ToListAsync());
        }

        public IActionResult Details()
        {
            var model = _context.Directeurs
                   .Include(e => e.Ecole)
                   .FirstOrDefault(d => d.Matricule == User.Identity.Name)
                   .Ecole;
            if (model != null)
            {
                return View(
                    _context.Ecoles
                    .Include(el => el.Eleves)
                    .Include(cl => cl.Classes)
                    .Include(pr => pr.Professeurs)
                    .AsNoTracking()
                    .FirstOrDefault(e => e.ID == model.ID)
                    );
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.DirecteurID });
            }
        }
    }
}
