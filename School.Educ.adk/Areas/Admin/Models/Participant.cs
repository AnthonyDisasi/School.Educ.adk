using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Admin.Models
{
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string ExamenID { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'élève est obligatoire"), Display(Name = "Identifiant de l'élève")]
        public string IdentifiantEleve { get; set; }

        //Celui sera remplit lorsque l'utilisateur aura commencer l'epreuve
        public bool Voir { get; set; }
        //Celui sera remplit quand l'utilisateur sera connecté et aura vu le message à l'invitation pour l'epreuve
        public bool Lecture { get; set; }

        public Examen Examen { get; set; }
    }
}
