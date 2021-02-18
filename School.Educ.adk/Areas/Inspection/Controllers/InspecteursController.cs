using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;
using School.Educ.adk.Areas.Ecole.DataContext;

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    [Authorize(Roles = "Inspecteur")]
    public class InspecteursController : Controller
    {
        private readonly EcoleDb _context;

        public InspecteursController(EcoleDb context)
        {
            _context = context;
        }

        public IActionResult Details(string id)
        {
            var inspecteur = _context.Inspecteurs
               .Include(e => e.Ecole)
               .FirstOrDefault(d => d.Matricule == User.Identity.Name);

            return View(inspecteur);
        }
    }
}
