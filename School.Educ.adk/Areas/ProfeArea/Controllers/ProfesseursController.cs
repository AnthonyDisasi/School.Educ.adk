using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;

namespace School.Educ.adk.Areas.ProfeArea.Controllers
{
    [Area("ProfeArea")]
    public class ProfesseursController : Controller
    {
        private readonly DbEcole _context;

        public ProfesseursController(DbEcole context)
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
    }
}
