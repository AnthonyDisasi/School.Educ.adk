using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Eleve.Controllers
{
    [Area("Eleve")]
    [Authorize(Roles = "Eleve")]
    public class ElevesController : Controller
    {
        private readonly EcoleDb _context;

        public ElevesController(EcoleDb context,
            InspecteurDb _contextdb)
        {
            _context = context;
        }

        public IActionResult Details(string id)
        {
            var eleve = _context.Eleves
               .Include(e => e.Ecole)
               .FirstOrDefault(d => d.Matricule == User.Identity.Name);

            return View(eleve);
        }


    }
}
