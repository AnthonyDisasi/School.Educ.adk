using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Professeur.Models
{
    public class Epreuve
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        public int EpreuveID { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "La description doit être plus petit que 50 charactères")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La période est obligatoire"), Display(Name = "Période")]
        public string Periode { get; set; }

        [Display(Name = "Date d'épreuve")]
        [Required(ErrorMessage = "La date d'épreuve est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime DateEpreuve { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "Le total est obligatoire")]
        public int Total { get; set; }

        public virtual Cotation Cotation { get; set; }
    }
}
