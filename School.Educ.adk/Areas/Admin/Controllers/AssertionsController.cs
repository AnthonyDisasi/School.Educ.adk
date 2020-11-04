using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Admin.Models;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AssertionsController : Controller
    {
        private readonly InspecteurDb _context;

        public AssertionsController(InspecteurDb context)
        {
            _context = context;
        }

        public IActionResult Create(string idQuestion, string intituler)
        {
            if (intituler != null)
            {
                Assertion model = new Assertion
                {
                    QuestionID = idQuestion,
                    Intituler = intituler
                };
                if (ModelState.IsValid)
                {
                    _context.Add(model);
                    _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Questions", new { id = idQuestion });
                }
            }
            return RedirectToAction("Details", "Questions", new { id = idQuestion });
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assertion model = _context.Assertions.Find(id);
            _context.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Details", "Questions", new { id = model.QuestionID });
        }
    }
}
