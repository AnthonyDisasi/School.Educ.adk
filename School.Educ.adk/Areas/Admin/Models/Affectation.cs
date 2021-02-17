﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Admin.Models
{
    public class Affectation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string InspecteurID { get; set; }

        public string IdEcole { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Période d'affectation"), Required(ErrorMessage = "La période d'affectation est obligatoire")]
        public string PeriodeAffectectation { get; set; }
        [Display(Name = "Date d'affectation"), Required(ErrorMessage = "La date d'affectation est obligatoire"), DataType(DataType.Date)]
        public DateTime DateAffectation { get; set; }

        public Inspecteur_ Inspecteur { get; set; }
    }
}
