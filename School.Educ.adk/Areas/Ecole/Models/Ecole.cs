using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class Ecole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string DirecteurID { get; set; }

        [Display(Name = "Nom"), Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; }

        public string EcoleLatitude { get; set; }
        public string EcoleLongitude { get; set; }

        [Display(Name = "Sous-division"), Required(ErrorMessage = "La sous division est obligatoire")]
        public string SousDivision { get; set; }

        public int nbClasses
        {
            get
            {
                return Classes.Count;
            }
        }

        public Directeur Directeur { get; set; }
        public ICollection<Classe> Classes { get; set; }
        public ICollection<Professeur> Professeurs { get; set; }
    }
}
