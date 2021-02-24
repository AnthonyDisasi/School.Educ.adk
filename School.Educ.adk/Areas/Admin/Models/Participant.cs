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

        [Required(ErrorMessage = "L'identifiant de l'élève est obligatoire"), Display(Name = "Identifiant de l'élève")]
        public string IdentifiantEleve { get; set; }

        public Examen Examen { get; set; }
    }
}
