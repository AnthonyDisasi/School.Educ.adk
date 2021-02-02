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

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    public class ElevesController : Controller
    {
        private readonly DbEcole _context;
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public ElevesController(DbEcole context,
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
            var model = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if (model.Ecole != null)
            {
                return View(
                await _context.Eleves
                .Include(i => i.Inscriptions)
                .AsNoTracking()
                .ToListAsync()
                );
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

            var eleve = await _context.Eleves
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eleve == null)
            {
                return NotFound();
            }

            return View(eleve);
        }

        public IActionResult Create()
        {
            var model = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name);
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
        public async Task<IActionResult> Create([Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Password,DateNaissance")] Models.Eleve eleve)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = eleve.Matricule
                };
                IdentityResult result = await userManager.CreateAsync(user, eleve.Password);
                if (result.Succeeded)
                {
                    user = await userManager.FindByNameAsync(eleve.Matricule);
                    eleve.ID = user.Id;
                    _context.Add(eleve);
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

            return View(eleve);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var eleve = await _context.Eleves.FindAsync(id);
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if ((eleve == null) && (user == null))
            {
                return NotFound();
            }
            return View(eleve);
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
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Password,DateNaissance")] Models.Eleve eleve)
        {
            if (id != eleve.ID)
            {
                return NotFound();
            }

            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = eleve.Matricule;
                IdentityResult ValidEmail = await userValidator.ValidateAsync(userManager, user);
                if (!ValidEmail.Succeeded)
                {
                    AddErrorsFromResult(ValidEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(eleve.Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, eleve.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, eleve.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((ValidEmail.Succeeded && validPass == null) || (ValidEmail.Succeeded && eleve.Password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                _context.Update(eleve);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                if (!EleveExists(eleve.ID))
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
            return View(eleve);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eleve = await _context.Eleves
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eleve == null)
            {
                return NotFound();
            }

            return View(eleve);
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
                    var eleve = await _context.Eleves.FindAsync(id);
                    _context.Eleves.Remove(eleve);
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

        private bool EleveExists(string id)
        {
            return _context.Eleves.Any(e => e.ID == id);
        }
    }
}
