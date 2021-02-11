using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;
using School.Educ.adk.Models;

namespace School.Educ.adk.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly DbEcole dbEcole;
        private readonly InspecteurDb inspecteurDb;

        public AccountController(UserManager<ApplicationUser> userMgr,
        SignInManager<ApplicationUser> signinMgr,
        DbEcole _dbEcole,
        InspecteurDb _inspecteurDb)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            dbEcole = _dbEcole;
            inspecteurDb = _inspecteurDb;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(details.Matricule);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("identifiant", user.Id);
                        return RedirectToAction("Redirection_", "Home", new { id = user.Id });
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Matricule), "Le mot de passe ou le matricule sont invalids");
            }

            return View(details);
        }

        public async Task<IActionResult> Logout(string returnUrl)
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl ?? "Login");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
