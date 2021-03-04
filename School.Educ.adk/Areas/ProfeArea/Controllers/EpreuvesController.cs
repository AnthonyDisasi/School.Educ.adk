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
using School.Educ.adk.Data;

namespace School.Educ.adk.Areas.ProfeArea.Controllers
{
    [Area("ProfeArea")]
    [Authorize(Roles = "Professeur")]
    public class EpreuvesController : Controller
    {
        private readonly EcoleDb _context;
        private readonly UserAuthent userNotif;

        public EpreuvesController(EcoleDb context, UserAuthent userNotif)
        {
            _context = context;
            this.userNotif = userNotif;
        }

        public IActionResult Index(string id, string idEpreuve)
        {
            ViewData["Inscriptions"] = _context.Inscriptions
                .Include(c => c.Classe)
                .Include(e => e.Eleve)
                .Where(l => l.ClasseID == id)
                .ToList();
            ViewBag.IdEpreuve = idEpreuve;
            ViewBag.Description = _context.Epreuve.Find(idEpreuve).Description;
            return View();
        }

        [HttpPost]
        public IActionResult Save_epreuve(string[] EleveID, double[] Point, string idEpreuve)
        {
            int nbr = EleveID.Length;
            var model = _context.Epreuve
                .Include(e => e.CahierCote)
                .ThenInclude(e => e.Cours)
                .FirstOrDefault(e => e.ID == idEpreuve);
            var direct = _context.Professeurs
                .Include(e => e.Ecole)
                .ThenInclude(p => p.Directeur)
                .FirstOrDefault(e => e.Matricule == User.Identity.Name)
                .Ecole.Directeur.Matricule;
            for(int i = 0; i < nbr; i++)
            {
                _context.Cotations.Add(new Cotation { EpreuveID = idEpreuve, EleveID = EleveID[i], Point = Point[i] });
                if(Point[i] < (model.Total / 2))
                {
                    var eleve = _context.Eleves.Find(EleveID[i]).Matricule;
                    userNotif.Notifications.Add(new Models.Notification
                    {
                        Destinataire = direct,
                        DateMessage = System.DateTime.Now,
                        Expediteur = User.Identity.Name,
                        Message = "L'élève dont le matricule est " + eleve + " a eu une cote inferieure à la moyenne dans le cours de " + model.CahierCote.Cours.Intituler + " à l'épreuve de " + model.Description,
                    });
                    userNotif.Notifications.Add(new Models.Notification
                    {
                        Destinataire = eleve,
                        DateMessage = System.DateTime.Now,
                        Expediteur = User.Identity.Name,
                        Message = "La cote obtenue à l'épreuve de " + model.Description + " du cours de " + model.CahierCote.Cours.Intituler + " est inferieure à la moyenne",
                    });
                }
            }
            userNotif.SaveChanges();
            _context.SaveChanges();
            ViewBag.IdEpreuve = idEpreuve;
            return RedirectToAction("Details", "Epreuves", new { id = idEpreuve });
        }

        [HttpPost]
        public IActionResult EditCotation(string cote, string point)
        {
            var model = _context.Cotations.Find(cote);
            model.Point = double.Parse(point);
            _context.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Details", "Epreuves", new { id = model.EpreuveID });
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuve
                .Include(e => e.CahierCote)
                .ThenInclude(o => o.Cours)
                .Include(c => c.Cotations)
                .ThenInclude(l => l.Eleve)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (epreuve == null)
            {
                return NotFound();
            }

            return View(epreuve);
        }

        public IActionResult Create(string id)
        {
            ViewData["CahierCoteID"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CahierCoteID,Description,DateEpreuve,Total")] Epreuve epreuve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epreuve);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "CahierCotes", new { id = epreuve.CahierCoteID });
            }
            ViewData["CahierCoteID"] = epreuve.CahierCoteID;
            return View(epreuve);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuve.FindAsync(id);
            if (epreuve == null)
            {
                return NotFound();
            }
            ViewData["CahierCoteID"] = epreuve.CahierCoteID;
            return View(epreuve);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,CahierCoteID,Description,DateEpreuve,Total")] Epreuve epreuve)
        {
            if (id != epreuve.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epreuve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpreuveExists(epreuve.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "CahierCotes", new { id = epreuve.CahierCoteID });
            }
            ViewData["CahierCoteID"] = epreuve.CahierCoteID;
            return View(epreuve);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epreuve = await _context.Epreuve
                .Include(e => e.CahierCote)
                .ThenInclude(c => c.Cours)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (epreuve == null)
            {
                return NotFound();
            }

            return View(epreuve);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var epreuve = await _context.Epreuve.FindAsync(id);
            _context.Epreuve.Remove(epreuve);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "CahierCotes", new { id = epreuve.CahierCoteID });
        }

        private bool EpreuveExists(string id)
        {
            return _context.Epreuve.Any(e => e.ID == id);
        }
    }
}
