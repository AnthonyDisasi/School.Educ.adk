using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class CahierCote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        [Display(Name = "Cours")]
        [Required(ErrorMessage = "Le cours est obligatoire")]
        public string CoursID { get; set; }

        [Display(Name = "Période")]
        [Required(ErrorMessage = "La période est obligatoire")]
        public string Periode { get; set; }

        [Required(ErrorMessage = "Le total est obligatoire")]
        public double Total { get; set; }

        public Cours Cours { get; set; }
        public ICollection<Epreuve> Epreuves { get; set; }
    }
}
