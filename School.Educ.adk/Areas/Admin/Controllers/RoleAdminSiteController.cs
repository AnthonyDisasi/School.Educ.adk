using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    public class RoleAdminSiteController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private readonly InspecteurDb _context;

        public RoleAdminSiteController(RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            InspecteurDb context)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Inspecteur = await _context.Inspecteurs.ToListAsync();
            return View(roleManager.Roles);
        }
    }
}
