using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class Inscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string EleveId { get; set; }
        public string ClasseID { get; set; }
        [Display(Name = "Date d'inscription"), Required(ErrorMessage = "La date d'incription est obligatoire"), DataType(DataType.Date)]
        public DateTime DateInscription { get; set; }

        public Eleve Eleve { get; set; }
        public Classe Classe { get; set; }
    }
}
