using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;
using School.Educ.adk.Models;

namespace School.Educ.adk.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DbEcole dbEcole;
        private readonly InspecteurDb inspecteurDb;

        public HomeController(InspecteurDb _inspecteurDb, DbEcole _dbEcole)
        {
            dbEcole = _dbEcole;
            inspecteurDb = _inspecteurDb;
        }
        public ActionResult Index(string id)
        {
            if (inspecteurDb.Inspecteurs.Find(id) != null)
            {
                return Redirect("/Inspection/Inspecteurs/Details?id=" + id);
            }
            else if (dbEcole.Directeurs.Find(id) != null)
            {
                return Redirect("/Ecole/Directeurs/Details?id=" + id);
            }
            else if (dbEcole.Professeurs.Find(id) != null)
            {
                return Redirect("/Professeur/Professeurs/Details?id=" + id);
            }
            else if (dbEcole.Eleves.Find(id) != null)
            {
                return Redirect("/Eleve/Eleves/Details?id=" + id);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
