using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Salut";
        }
    }
}
