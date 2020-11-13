﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Educ.adk.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    public class AdminSiteController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public AdminSiteController(IPasswordHasher<ApplicationUser> _passwordHasher, 
            IPasswordValidator<ApplicationUser> _passwordValidator,
            IUserValidator<ApplicationUser> _userValidator,
            UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
            userValidator = _userValidator;
            passwordValidator = _passwordValidator;
            passwordHasher = _passwordHasher;
        }

        public IActionResult Index => View(userManager.Users);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create()
    }
}
