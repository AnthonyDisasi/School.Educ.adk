using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Ecole.Models;

namespace School.Educ.adk.Areas.Ecole.Controllers
{
    [Area("Ecole")]
    [Authorize(Roles = "Directeur")]
    public class CategoriesController : Controller
    {
        private readonly EcoleDb _context;

        public CategoriesController(EcoleDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.categories.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create(string Nom)
        {
            var model = new Categorie { Nom = Nom };
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var categorie = await _context.categories.FindAsync(id);
            _context.categories.Remove(categorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
