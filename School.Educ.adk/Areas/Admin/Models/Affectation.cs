using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Admin.Models
{
    public class Affectation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string EcoleID { get; set; }
        public string InspecteurID { get; set; }

        [Display(Name = "Date d'affectation"), Required(ErrorMessage = "La date d'affectation est obligatoire"), DataType(DataType.Date)]
        public DateTime DateAffectation { get; set; }

        public Ecole.Models.Ecole Ecole { get; set; }
        public Inspecteur Inspecteur { get; set; }
    }
}
