using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class Cotation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string EpreuveID { get; set; }

        [Display(Name = "Matricule")]
        [Required(ErrorMessage = "Le matricule de l'élève est obligatoire")]
        public string EleveID { get; set; }

        [Display(Name = "Point")]
        [Required(ErrorMessage = "Le point est obligatoire")]
        public double Point { get; set; }

        public Epreuve Epreuve { get; set; }
        public virtual Eleve Eleve { get; set; }
    }
}
