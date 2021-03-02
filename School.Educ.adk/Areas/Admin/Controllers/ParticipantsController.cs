using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Ecole.DataContext;

namespace School.Educ.adk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParticipantsController : Controller
    {
        private readonly EcoleDb ecoleDb;
        private readonly InspecteurDb inspecteursDb;

        public ParticipantsController(EcoleDb _ecoleDb, InspecteurDb _inspecteursDb)
        {
            inspecteursDb = _inspecteursDb;
            ecoleDb = _ecoleDb;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListeEcoles(string id)
        {
            var model = ecoleDb.Ecoles
                .Include(e => e.Directeur)
                .Include(e => e.Classes)
                .AsNoTracking().ToList();
            ViewBag.IdExamen = id;
            return View(model);
        }

        public IActionResult ListeClasses(string idEcole, string idExamen)
        {
            var model = ecoleDb.Classes
                .Include(c => c.Inscriptions)
                .Where(c => c.EcoleID == idEcole)
                .AsNoTracking().ToList();
            
            //var 
            ViewBag.IdExamen = idExamen;
            return View(model);
        }

        public void ClasseChosen(string idExamen, string idClasse)
        {
            //inspecteursDb
            var model = ecoleDb.Classes
                .Include(c => c.Inscriptions)
                .ThenInclude(i => i.Eleve)
                .AsNoTracking()
                .FirstOrDefault(c => c.ID == idClasse);
            foreach(Ecole.Models.Inscription item in model.Inscriptions)
            {
                inspecteursDb.Participants.Add(
                    new Models.Participant
                    {
                        IdentifiantEleve = item.EleveId,
                        ExamenID = idExamen
                    });
            }
            inspecteursDb.SaveChanges();
            ViewBag.modife = 1;
            ViewBag.IdExamen = idExamen;
        }
    }
}
