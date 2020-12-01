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
    public class DirecteursController : Controller
    {
        private readonly DbEcole _context;
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public DirecteursController(DbEcole context,
            UserManager<ApplicationUser> usrMgr,
            IUserValidator<ApplicationUser> userValid,
            IPasswordValidator<ApplicationUser> passValid,
            IPasswordHasher<ApplicationUser> passwordHash)
        {
            _context = context;
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Directeurs.Include(e => e.Ecole).ToListAsync());
        }
        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs
                .Include(e => e.Ecole)
                .FirstOrDefaultAsync(m => m.ID == id);

            if(directeur.Ecole == null)
            {
                ViewData["ecole"] = null;
            }
            else
            {
                ViewData["ecole"] = 1;
            }
            
            if (directeur == null)
            {
                return NotFound();
            }

            return View(directeur);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Directeur directeur)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = directeur.Matricule,
                    Email = directeur.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, directeur.Password);
                if (result.Succeeded)
                {
                    user = await userManager.FindByEmailAsync(directeur.Email);
                    directeur.ID = user.Id;
                    _context.Add(directeur);
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
            return View(directeur);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var directeur = await _context.Directeurs.FindAsync(id);
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if ((directeur == null) && (user == null))
            {
                return NotFound();
            }
            return View(directeur);
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
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Directeur directeur)
        {
            if (id != directeur.ID)
            {
                return NotFound();
            }

            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = directeur.Email;
                IdentityResult ValidEmail = await userValidator.ValidateAsync(userManager, user);
                if (!ValidEmail.Succeeded)
                {
                    AddErrorsFromResult(ValidEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(directeur.Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, directeur.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, directeur.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((ValidEmail.Succeeded && validPass == null) || (ValidEmail.Succeeded && directeur.Password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                _context.Update(directeur);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                if (!DirecteurExists(directeur.ID))
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
            return View(directeur);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs
                .Include(e => e.Ecole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (directeur == null)
            {
                return NotFound();
            }

            return View(directeur);
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
                    var directeur = await _context.Directeurs.FindAsync(id);
                    _context.Directeurs.Remove(directeur);
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

        private bool DirecteurExists(string id)
        {
            return _context.Directeurs.Any(e => e.ID == id);
        }
    }
}
