using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;

namespace School.Educ.adk.Areas.ProfeArea.Controllers
{
    [Area("ProfeArea")]
    [Authorize(Roles = "Professeur")]
    public class ProfesseursController : Controller
    {
        private readonly EcoleDb _context;

        public ProfesseursController(EcoleDb context)
        {
            _context = context;
        }

        public IActionResult Details()
        {
            var professeur = _context.Professeurs
               .Include(e => e.Ecole)
               .FirstOrDefault(d => d.Matricule == User.Identity.Name);

            return View(professeur);
        }

        public IActionResult DetailsEcole()
        {
            string id_ecole = _context
                .Professeurs
                .Include(e => e.Ecole)
                .FirstOrDefault(id => id.Matricule == User.Identity.Name)
                .EcoleID;

            var ecole = _context
                .Ecoles
                .Include(d => d.Directeur)
                .Include(cl => cl.Classes)
                .ThenInclude(co => co.Cours)
                .Include(c => c.Classes)
                .ThenInclude(i => i.Inscriptions)
                .ThenInclude(el => el.Eleve)
                .FirstOrDefault(id => id.ID == id_ecole);

            return View(ecole);
        }
    }
}
