using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;
using School.Educ.adk.Models;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    [Authorize(Roles = "Directeur")]
    public class ProfesseursController : Controller
    {
        private readonly EcoleDb _context;
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public ProfesseursController(EcoleDb context,
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
            var model = _context.Directeurs
                .Include(e => e.Ecole)
                .FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if (model.Ecole != null)
            {
                var dbEcole = _context
                    .Professeurs
                    .Include(p => p.Ecole)
                    .Include(c => c.Cours)
                    .Where(id => id.EcoleID == model.Ecole.ID);
                return View(await dbEcole.ToListAsync());
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.ID });
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professeur = await _context.Professeurs
                .Include(p => p.Ecole)
                .Include(c => c.Cours)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professeur == null)
            {
                return NotFound();
            }

            return View(professeur);
        }

        public IActionResult Create()
        {
            var model = _context.Directeurs
                .Include(e => e.Ecole)
                .FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if (model.Ecole != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.ID });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EcoleID,Nom,Postnom,Prenom,Genre,Matricule,Email,DateNaissance,Password")] Models.Professeur professeur)
        {
            professeur.EcoleID = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID;
            
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = professeur.Matricule,
                    Email = professeur.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, professeur.Password);
                if (result.Succeeded)
                {
                    user = await userManager.FindByEmailAsync(professeur.Email);
                    professeur.ID = user.Id;
                    _context.Add(professeur);
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
            return View(professeur);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professeur = await _context.Professeurs.FindAsync(id);
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (professeur == null && user == null)
            {
                return NotFound();
            }
            return View(professeur);
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
        public async Task<IActionResult> Edit(string id, [Bind("ID,EcoleID,Nom,Postnom,Prenom,Genre,Matricule,Email,DateNaissance,Password")] Models.Professeur professeur)
        {
            if (id != professeur.ID)
            {
                return NotFound();
            }
            professeur.EcoleID = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name).Ecole.ID;

            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = professeur.Matricule;
                user.Email = professeur.Email;
                IdentityResult ValidEmail = await userValidator.ValidateAsync(userManager, user);
                if (!ValidEmail.Succeeded)
                {
                    AddErrorsFromResult(ValidEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(professeur.Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, professeur.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, professeur.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((ValidEmail.Succeeded && validPass == null) || (ValidEmail.Succeeded && professeur.Password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                _context.Update(professeur);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                if (!ProfesseurExists(professeur.ID))
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

            return View(professeur);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professeur = await _context.Professeurs
                .Include(p => p.Ecole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professeur == null)
            {
                return NotFound();
            }

            return View(professeur);
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
                    var professeur = await _context.Professeurs.Include(c => c.Cours).FirstOrDefaultAsync(pr => pr.ID == id);
                    foreach (var item in professeur.Cours)
                    {
                        _context.Cours.Remove(item);
                    }
                    _context.Professeurs.Remove(professeur);
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

        private bool ProfesseurExists(string id)
        {
            return _context.Professeurs.Any(e => e.ID == id);
        }
    }
}
