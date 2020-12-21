using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Models;

namespace School.Educ.adk.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly InspecteurDb Inspe;
        private readonly DbEcole Eco;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, InspecteurDb _Inspe, DbEcole _Eco)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            Inspe = _Inspe;
            Eco = _Eco;
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
                ApplicationUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        string idUser;
                        if (details.Role == "Inspecteur")
                        {
                            idUser = Inspe.Inspecteurs.FirstOrDefault(i => i.Email == details.Email).ID;
                            //return RedirectToAction("Details", "Inspecteurs", new { id = idUser }, "Admin" ?? returnUrl);
                            return Redirect(returnUrl ?? "AccueilCI");
                        }
                        else if (details.Role == "Directeur")
                        {
                            return RedirectToAction();
                        }
                        else if (details.Role == "Professeur")
                        {
                            return RedirectToAction();
                        }
                        else if (details.Role == "Eleve")
                        {
                            return RedirectToAction();
                        }
                    }
                    ModelState.AddModelError(nameof(LoginModel.Password), "Le mot de passe est incorrect");
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Le mail est incorrect");
            }

            return View(details);
        }

        public string AccueilCI()
        {
            return "Salut";
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
