using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;
using School.Educ.adk.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSiteController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly InspecteurDb _context;

        public AdminSiteController(IPasswordHasher<ApplicationUser> _passwordHasher,
            IPasswordValidator<ApplicationUser> _passwordValidator,
            IUserValidator<ApplicationUser> _userValidator,
            UserManager<ApplicationUser> _userManager,
            InspecteurDb context)
        {
            userManager = _userManager;
            userValidator = _userValidator;
            passwordValidator = _passwordValidator;
            passwordHasher = _passwordHasher;
            _context = context;
        }

        public IActionResult Index() => View(userManager.Users);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Inspecteur model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.NomComplet,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    user = await userManager.FindByEmailAsync(model.Email);
                    model.ID = user.Id;
                    _context.Inspecteurs.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(id);
            Inspecteur model = _context.Inspecteurs.Find(id);
            if((user != null) && (model != null))
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _context.Inspecteurs.Remove(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Erreur non trouvée");
            }
            return RedirectToAction("Index", userManager.Users);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
