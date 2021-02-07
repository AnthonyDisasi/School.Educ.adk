using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.ProfeArea.Models
{
    public class Lecon
    {
        public string ID { get; set; }

        public string ProfesseurID { get; set; }
        public string CoursID { get; set; }

        public string LeconDonnee { get; set; }
        public DateTime DateLecon { get; set; }

        public virtual Evaluer Evaluer { get; set; }
        public virtual Ecole.Models.Professeur Professeur { get; set; }
        public virtual Ecole.Models.Cours Cours { get; set; }
    }
}
