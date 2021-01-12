using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Admin.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required(ErrorMessage = "L'examen est obligatoire"), Display(Name = "Examen")]
        public string ExamenID { get; set; }

        public string Enoncer { get; set; }
        public string BonneReponse { get; set; }
        public double Cote { get; set; }

        public int NombreAssertion
        {
            get
            {
                return Assertions.Count();
            }
        }

        public Examen Examen { get; set; }
        public ICollection<Reponse> Reponse { get; set; }
        public ICollection<Assertion> Assertions { get; set; }
    }
}
