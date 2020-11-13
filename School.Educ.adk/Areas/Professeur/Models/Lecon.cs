using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Professeur.Models
{
    public class Lecon
    {
        public string ID { get; set; }

        public string IdentifiantProfesseur { get; set; }
        public string IdentifiantCours { get; set; }

        public string LeconDonnee { get; set; }
        public DateTime DateLecon { get; set; }

        public virtual Echange Echange { get; set; }
    }
}
