﻿using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string ExamenID { get; set; }
        public string ReponseID { get; set; }

        public string Enoncer { get; set; }
        public string BonneReponse { get; set; }
        public double Cote { get; set; }

        public Examen Examen { get; set; }
        public Reponse Reponse { get; set; }
        public ICollection<Assertion> Assertion { get; set; }
    }
}
