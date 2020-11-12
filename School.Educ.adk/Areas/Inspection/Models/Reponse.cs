﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Reponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string QuestionID { get; set; }
        public string ParticipantID { get; set; }

        [Display(Name = "Réponse de l'école"), Required(ErrorMessage = "La réponse est obligatoire")]
        public string ReponseDonnee { get; set; }
        [Required(ErrorMessage = "Le point est obligatoire")]
        public double Point { get; set; }

        public Question Question { get; set; }
        public Participant Participant { get; set; }
    }
}
