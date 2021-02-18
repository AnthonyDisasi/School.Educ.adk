using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Models;

namespace School.Educ.adk.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly EcoleDb dbEcole;
        private readonly InspecteurDb inspecteurDb;

        public HomeController(EcoleDb _dbEcole)
        {
            dbEcole = _dbEcole;
        }

        public IActionResult Redirection_(string id)
        {
            if (dbEcole.Inspecteurs.Find(id) != null)
            {
                return Redirect("/Inspection/Inspecteurs/Details");
            }
            else if (dbEcole.Directeurs.Find(id) != null)
            {
                return Redirect("/Ecole/Directeurs/Details");
            }
            else if (dbEcole.Professeurs.Find(id) != null)
            {
                return Redirect("/ProfeArea/Professeurs/Details");
            }
            else if (dbEcole.Eleves.Find(id) != null)
            {
                return Redirect("/Eleve/Eleves/Details");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
