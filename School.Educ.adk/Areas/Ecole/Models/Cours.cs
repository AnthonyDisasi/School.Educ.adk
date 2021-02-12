using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class Cours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string ClasseID { get; set; }

        [Required(ErrorMessage = "Le professeur est obligatoire")]
        public string ProfesseurID { get; set; }

        [Required(ErrorMessage = "L'intituler est obligatoire")]
        public string Intituler { get; set; }
        [Required(ErrorMessage = "La categorie est obligatoire")]
        public string Categorie { get; set; }

        public Professeur Professeur { get; set; }
        public Classe Classe { get; set; }
        public CahierCote CahierCote { get; set; }
    }
}
