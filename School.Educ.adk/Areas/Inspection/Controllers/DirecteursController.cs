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

namespace School.Educ.adk.Areas.Inspection.Controllers
{
    [Area("Inspection")]
    public class DirecteursController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly DbEcole _context;

        public DirecteursController(DbEcole context,
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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Directeurs.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (directeur == null)
            {
                return NotFound();
            }

            return View(directeur);
        }

        public IActionResult Create() => View();

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
            if (directeur == null)
            {
                return NotFound();
            }
            return View(directeur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Nom,Postnom,Prenom,Genre,Matricule,Email,Password,DateNaissance")] Directeur directeur)
        {
            if (id != directeur.ID)
            {
                return NotFound();
            }

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
            return View(directeur);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs
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
            var directeur = await _context.Directeurs.FindAsync(id);
            _context.Directeurs.Remove(directeur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirecteurExists(string id)
        {
            return _context.Directeurs.Any(e => e.ID == id);
        }
    }
}
