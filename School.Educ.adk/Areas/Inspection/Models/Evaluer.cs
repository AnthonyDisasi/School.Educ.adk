using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Evaluer
    {
        public string ID { get; set; }

        public string LeconID { get; set; }
        public string InpecteurID { get; set; }

        public double Cotation { get; set; }
        public string Remarque { get; set; }

        public ProfeArea.Models.Lecon Lecon { get; set; }
        public virtual Admin.Models.Inspecteur Inpecteur { get; set; }
    }
}
