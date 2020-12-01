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
    public class RoleSchoolController : Controller
    {
        private readonly DbEcole _context;

        public RoleSchoolController(DbEcole context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var model = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if(model.Ecole != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.ID });
            }
        }
    }
}
