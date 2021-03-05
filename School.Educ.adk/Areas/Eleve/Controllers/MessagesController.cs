using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Educ.adk.Data;

namespace School.Educ.adk.Areas.Eleve.Controllers
{
    [Area("Eleve")]
    [Authorize(Roles = "Eleve")]
    public class MessagesController : Controller
    {
        private readonly UserAuthent userAut;

        public MessagesController(UserAuthent userAut)
        {
            this.userAut = userAut;
        }

        public IActionResult Lecture()
        {
            var model = userAut.Notifications
                .Where(n => n.Destinataire == User.Identity.Name)
                .ToList();
            return View(model);
        }
    }
}
