using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Assertion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required(ErrorMessage = "La question est obligatoire"), Display(Name = "Question")]
        public string QuestionID { get; set; }

        [Required(ErrorMessage = "L'intituler est obligatoire"), Display(Name = "Intituler")]
        public string Intituler { get; set; }

        public Question Question { get; set; }
    }
}
