using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Professeur.Models
{
    public class Cotation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string EpreuveID { get; set; }

        [Display(Name = "Identifiant de l'élève")]
        [Required(ErrorMessage = "L'identifiant de l'élève est obligatoire")]
        public string IdentifiantEleve { get; set; }

        [Display(Name = "Point")]
        [Required(ErrorMessage = "Le point est obligatoire")]
        public double Point { get; set; }

        public virtual Epreuve Epreuve { get; set; }
    }
}
