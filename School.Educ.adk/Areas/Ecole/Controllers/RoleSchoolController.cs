using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Models;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    [Authorize(Roles = "Directeur")]
    public class RoleSchoolController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private readonly DbEcole _context;
        private readonly InspecteurDb _cont;

        public RoleSchoolController(DbEcole context,
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            InspecteurDb cont)
        {
            _context = context;
            _cont = cont;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public IActionResult Index()
        {
            var model = _context.Directeurs.Include(e => e.Ecole).FirstOrDefault(d => d.Matricule == User.Identity.Name);
            if(model.Ecole != null)
            {
                return View(roleManager.Roles);
            }
            else
            {
                return RedirectToAction("Details", "Directeurs", new { id = model.ID });
            }
        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> Edit(string role_, string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            if (role_ == "Professeur")
            {
                List<Models.Professeur> model = _context.Professeurs.Include(e => e.Ecole).Where(i_d => i_d.EcoleID == (_context.Directeurs.Include(e => e.Ecole).FirstOrDefault(id_ => id_.Matricule == User.Identity.Name)).Ecole.ID).ToList();
                foreach (ApplicationUser user in userManager.Users)
                {
                    foreach (Models.Professeur professeur in model)
                    {
                        if (professeur.ID == user.Id)
                        {
                            var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                            list.Add(user);
                        }
                    }
                }
            }
            if (role_ == "Eleve")
            {
                List<Models.Eleve> model = _context.Eleves.Include(e => e.Ecole).Where(i_d => i_d.EcoleID == (_context.Directeurs.Include(e => e.Ecole).FirstOrDefault(id_ => id_.Matricule == User.Identity.Name)).Ecole.ID).ToList();
                foreach (ApplicationUser user in userManager.Users)
                {
                    foreach (Models.Eleve eleve in model)
                    {
                        if (eleve.ID == user.Id)
                        {
                            var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                            list.Add(user);
                        }
                    }
                }
            }

            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Edit(model.RoleName, model.RoleId);
            }
        }
    }
}
