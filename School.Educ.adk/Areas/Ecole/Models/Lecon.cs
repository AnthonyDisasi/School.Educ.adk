using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class Lecon
    {
        public string ID { get; set; }

        public string ProfesseurID { get; set; }
        public string CoursID { get; set; }

        public string LeconDonnee { get; set; }
        public DateTime DateLecon { get; set; }

        public virtual Professeur Professeur { get; set; }
        public virtual Cours Cours { get; set; }
    }
}
