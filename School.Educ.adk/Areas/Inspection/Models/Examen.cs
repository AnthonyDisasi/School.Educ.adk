using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Examen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "La période est obligatoire"), Display(Name = "Période")]
        public string Periode { get; set; }
        [Required(ErrorMessage = "La série est obligatoire"), Display(Name = "Série")]
        public string Serie { get; set; }
        [Required(ErrorMessage = "Le code d'accès est obligatoire"), Display(Name = "Code d'accès")]
        public string CodeAcces { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'inspecteur est obligatoire"), Display(Name = "L'identifiant de l'inspecteur")]
        public string IdInspecteur { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
