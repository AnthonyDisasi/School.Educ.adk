using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Professeur.Models
{
    public class Echange
    {
        public string ID { get; set; }

        public string LeconID { get; set; }
        public string Inspecteur { get; set; }

        public double Cotation { get; set; }
        public string Remarque { get; set; }

        public Lecon Lecon { get; set; }
    }
}
