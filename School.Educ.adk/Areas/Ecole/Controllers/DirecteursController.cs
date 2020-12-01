using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    [Authorize(Roles = "Directeur")]
    public class DirecteursController : Controller
    {
        private readonly DbEcole _context;

        public DirecteursController(DbEcole context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directeur = await _context.Directeurs
                .Include(e => e.Ecole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (directeur == null)
            {
                return NotFound();
            }

            if(directeur.Ecole != null)
            {
                ViewData["Message"] = "";
                return View(directeur);
            }
            else
            {
                ViewData["Message"] = "Pour toute opération, il vous faut être affecté à une école et cela n'est pas votre cas!";
                return View(directeur);
            }
        }
    }
}
