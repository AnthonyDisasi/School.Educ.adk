﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    [Authorize(Roles = "Directeur")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Ecoles");
        }
    }
}
