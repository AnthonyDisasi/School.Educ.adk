using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InspecteursController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly InspecteurDb _context;
        private readonly EcoleDb _cont;

        public InspecteursController(UserManager<ApplicationUser> usrMgr,
        IUserValidator<ApplicationUser> userValid,
        IPasswordValidator<ApplicationUser> passValid,
        IPasswordHasher<ApplicationUser> passwordHash,
        InspecteurDb context,
        EcoleDb cont)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            _context = context;
            _cont = cont;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Inspecteurs.Include(a => a.Affectation).ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .Include(a => a.Affectation)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            ViewData["ecole"] = null;
            
            Ecole.Models.Ecole ecole_ = new Ecole.Models.Ecole();

            if (inspecteur.Affectation != null)
            {
                ecole_= _cont.Ecoles.Find(inspecteur.Affectation.IdEcole);
                if (ecole_ == null)
                {
                    ViewData["ecole"] = "L'école à laquelle vous a été affectée, a été retirée; votre affectation est supprimée";
                    var affect = inspecteur.Affectation;
                    _context.Affectations.Remove(affect);
                    _context.SaveChanges();
                }
                else
                {
                    ViewData["ecole"] = ecole_.Nom;
                }

            }

            if (inspecteur == null)
            {
                return NotFound();
            }

            return View(inspecteur);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Inspecteur inspecteur)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = inspecteur.Matricule,
                    Email = inspecteur.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, inspecteur.Password);
                if (result.Succeeded)
                {
                    user = await userManager.FindByEmailAsync(inspecteur.Email);
                    inspecteur.ID = user.Id;
                    _context.Add(inspecteur);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(inspecteur);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs.FindAsync(id);
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if ((inspecteur == null) && (user == null))
            {
                return NotFound();
            }
            return View(inspecteur);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Inspecteur inspecteur)
        {
            if (id != inspecteur.ID)
            {
                return NotFound();
            }
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = inspecteur.Email;
                IdentityResult ValidEmail = await userValidator.ValidateAsync(userManager, user);
                if (!ValidEmail.Succeeded)
                {
                    AddErrorsFromResult(ValidEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(inspecteur.Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, inspecteur.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, inspecteur.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((ValidEmail.Succeeded && validPass == null) || (ValidEmail.Succeeded && inspecteur.Password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                _context.Update(inspecteur);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                if (!InspecteurExists(inspecteur.ID))
                                {
                                    return NotFound();
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Utilisateur non trouvé");
            }
            return View(inspecteur);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .Include(a => a.Affectation)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inspecteur == null)
            {
                return NotFound();
            }

            return View(inspecteur);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    var inspecteur = await _context.Inspecteurs.FindAsync(id);
                    _context.Inspecteurs.Remove(inspecteur);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }


        private bool InspecteurExists(string id)
        {
            return _context.Inspecteurs.Any(e => e.ID == id);
        }
    }
}
