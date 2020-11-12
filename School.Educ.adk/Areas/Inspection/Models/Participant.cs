using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required(ErrorMessage = "La question est obligatoire"), Display(Name = "Question")]
        public string IdentifiantEleve { get; set; }
        [Required(ErrorMessage = "La question est obligatoire"), Display(Name = "Question"), DataType(DataType.Date)]
        public DateTime DateExamen { get; set; }

        public ICollection<Reponse> Reponses { get; set; }
    }
}
