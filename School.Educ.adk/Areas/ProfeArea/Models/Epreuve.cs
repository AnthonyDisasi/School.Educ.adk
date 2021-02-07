using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.ProfeArea.Models
{
    public class Epreuve
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string CahierCoteID { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "La description doit être plus petit que 50 charactères")]
        public string Description { get; set; }

        [Display(Name = "Date d'épreuve")]
        [DataType(DataType.Date)]
        public DateTime DateEpreuve { get; set; }

        [Required(ErrorMessage = "Le total est obligatoire")]
        public double Total { get; set; }

        public CahierCote CahierCote { get; set; }
        public ICollection<Cotation> Cotations { get; set; }
    }
}
