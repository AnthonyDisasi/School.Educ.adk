using School.Educ.adk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Admin.Models
{
    public class Examen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "La période est obligatoire"), Display(Name = "Période"), EnumDataType(typeof(Periode))]
        public Periode Periode { get; set; }
        [Required(ErrorMessage = "La série est obligatoire"), Display(Name = "Série")]
        public string Serie { get; set; }
        [Required(ErrorMessage = "Le code d'accès est obligatoire"), Display(Name = "Code d'accès")]
        public string CodeAcces { get; set; }

        [Display(Name = "Date de l'examen"), DataType(DataType.Date), Required(ErrorMessage = "La date est obligatoire")]
        public DateTime DateExamen { get; set; }

        [Required(ErrorMessage = "La durée est obligatoire"), Display(Name = "Durée")]
        [DataType(DataType.Time)]
        public DateTime Duree { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
