using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;
using School.Educ.adk.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleAdminSiteController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private readonly EcoleDb _context;

        public RoleAdminSiteController(RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            EcoleDb context)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            _context = context;
        }

        public IActionResult Index() => View(roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Role non trouvé");
            }
            return View("Index", roleManager.Roles);
        }

        public async Task<IActionResult> Edit(string role_, string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            if(role_ == "Inspecteur")
            {
                List<Inspecteur> model = _context.Inspecteurs.ToList();
                foreach (ApplicationUser user in userManager.Users)
                {
                    foreach (Inspecteur inspecteur in model)
                    {
                        if (inspecteur.ID == user.Id)
                        {
                            var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                            list.Add(user);
                        }
                    }
                }
            }
            if(role_ == "Directeur")
            {
                List<Directeur> model = _context.Directeurs.ToList();
                foreach(ApplicationUser user in userManager.Users)
                {
                    foreach(Directeur directeur in model)
                    {
                        if(directeur.ID == user.Id)
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
