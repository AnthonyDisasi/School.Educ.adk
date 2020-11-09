using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class Cours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string ClasseID { get; set; }

        public string Intituler { get; set; }
        public string Categorie { get; set; }

        public Classe Classe { get; set; }
    }
}
