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

        [Display(Name = "Latitude de l'école"), Required(ErrorMessage = "La latitude de l'école est obligatoire")]
        public string EcoleLatitude { get; set; }
        [Display(Name = "Longitude de l'école"), Required(ErrorMessage = "La longitude de l'école est obligatoire")]
        public string EcoleLongitude { get; set; }

        [Display(Name = "Sous-division"), Required(ErrorMessage = "La sous division est obligatoire")]
        public string SousDivision { get; set; }

        [Display(Name = "Date de création"), DataType(DataType.Date), Required(ErrorMessage = "La date de création est obligatoire")]
        public DateTime DateCreate { get; set; }

        public virtual Directeur Directeur { get; set; }
        public ICollection<Classe> Classes { get; set; }
        public ICollection<Professeur> Professeurs { get; set; }
    }
}
