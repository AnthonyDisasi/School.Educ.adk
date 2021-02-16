using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class Lecon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string ProfesseurID { get; set; }
        [Required(ErrorMessage = "Le cours est obligatoire"), Display(Name = "Intituler du cours")]
        public string CoursID { get; set; }

        [Required(ErrorMessage = "La leçon est obligatoire"), Display(Name = "Leçon")]
        public string LeconDonnee { get; set; }
        [Required(ErrorMessage = "La date est obligatoire"), Display(Name = "Date de leçon")]
        public DateTime DateLecon { get; set; }

        public virtual Professeur Professeur { get; set; }
        public virtual Cours Cours { get; set; }
    }
}
