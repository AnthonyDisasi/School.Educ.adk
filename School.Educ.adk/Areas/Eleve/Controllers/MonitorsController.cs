using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;

namespace School.Educ.adk.Areas.Eleve.Controllers
{
    [Area("Eleve")]
    [Authorize(Roles = "Eleve")]
    public class MonitorsController : Controller
    {
        private readonly EcoleDb _context;

        public MonitorsController(EcoleDb contextDb)
        {
            _context = contextDb;
        }

        public IActionResult Index()
        {
            var model = _context.Cotations
                .Include(c => c.Eleve)
                .Include(c => c.Epreuve)
                .ThenInclude(c => c.CahierCote)
                .ThenInclude(c => c.Cours)
                .ThenInclude(c => c.Professeur)
                .Where(c => c.EleveID == _context.Eleves.FirstOrDefault(e => e.Matricule == User.Identity.Name).ID);
            return View(model);
        }
    }
}
