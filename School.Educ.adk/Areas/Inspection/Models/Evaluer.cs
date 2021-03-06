﻿using School.Educ.adk.Areas.Ecole.Models;
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

        public Ecole.Models.Lecon Lecon { get; set; }
        public virtual Inspecteur Inpecteur { get; set; }
    }
}
