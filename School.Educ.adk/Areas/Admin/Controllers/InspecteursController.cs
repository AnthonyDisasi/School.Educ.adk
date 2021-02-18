using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;
using School.Educ.adk.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InspecteursController : Controller
    {
        private readonly EcoleDb _context;
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public InspecteursController(EcoleDb context,
            UserManager<ApplicationUser> usrMgr,
            IUserValidator<ApplicationUser> userValid,
            IPasswordValidator<ApplicationUser> passValid,
            IPasswordHasher<ApplicationUser> passwordHash)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            _context = context;
        }

        public IActionResult Index()
        {
            var ecoleDb = _context.Inspecteurs.Include(i => i.Ecole).AsNoTracking().ToList();
            return View(ecoleDb);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .Include(i => i.Ecole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inspecteur == null)
            {
                return NotFound();
            }

            return View(inspecteur);
        }

        public IActionResult Create()
        {
            ViewData["EcoleID"] = new SelectList(_context.Ecoles.Where(i => i.Inspecteur == null), "ID", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EcoleID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Inspecteur inspecteur)
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
            ViewData["EcoleID"] = new SelectList(_context.Ecoles.Where(i => i.Inspecteur == null), "ID", "Nom", inspecteur.EcoleID);
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
            ViewData["EcoleID"] = new SelectList(_context.Ecoles.Where(i => i.Inspecteur == null), "ID", "Nom", inspecteur.EcoleID);
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
        public async Task<IActionResult> Edit(string id, [Bind("ID,EcoleID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Inspecteur inspecteur)
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
            ViewData["EcoleID"] = new SelectList(_context.Ecoles.Where(i => i.Inspecteur == null), "ID", "Nom", inspecteur.EcoleID);
            return View(inspecteur);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .Include(i => i.Ecole)
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
