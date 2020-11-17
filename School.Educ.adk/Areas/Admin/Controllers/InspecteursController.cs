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

        public InspecteursController(UserManager<ApplicationUser> usrMgr,
        IUserValidator<ApplicationUser> userValid,
        IPasswordValidator<ApplicationUser> passValid,
        IPasswordHasher<ApplicationUser> passwordHash,
        InspecteurDb context)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            _context = context;

        }


        // GET: Admin/Inspecteurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inspecteurs.ToListAsync());
        }

        // GET: Admin/Inspecteurs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inspecteur == null)
            {
                return NotFound();
            }

            return View(inspecteur);
        }

        // GET: Admin/Inspecteurs/Create
        public IActionResult Create() => View();

        // POST: Admin/Inspecteurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Add(inspecteur);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(inspecteur);
        }

        // GET: Admin/Inspecteurs/Edit/5
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inspecteur);
                    await _context.SaveChangesAsync();
                    user
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
            return View(inspecteur);
        }

        // GET: Admin/Inspecteurs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inspecteur = await _context.Inspecteurs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inspecteur == null)
            {
                return NotFound();
            }

            return View(inspecteur);
        }

        // POST: Admin/Inspecteurs/Delete/5
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

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        private bool InspecteurExists(string id)
        {
            return _context.Inspecteurs.Any(e => e.ID == id);
        }
    }
}
