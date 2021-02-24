using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Eleve.Controllers
{
    [Area("Eleve")]
    public class ElevesController : Controller
    {
        private readonly EcoleDb _context;
        private readonly InspecteurDb contextdb;

        public ElevesController(EcoleDb context,
            InspecteurDb _contextdb)
        {
            _context = context;
            contextdb = _contextdb;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eleve = await _context.Eleves
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eleve == null)
            {
                return NotFound();
            }

            return View(eleve);
        }


    }
}
