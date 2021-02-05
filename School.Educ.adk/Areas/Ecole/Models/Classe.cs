using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Ecole.Models
{
    public class Classe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string EcoleID { get; set; }

        [Required(ErrorMessage = "Le niveau est obligatoire")]
        public string Niveau { get; set; }
        [Required(ErrorMessage = "La section est obligatoire"), Display(Name = "Section - option")]
        public string Section { get; set; }
        [Display(Name = "Année scolaire"), Required(ErrorMessage = "L'nnée scolaire est obligatoire")]
        public string AnneeScolaire { get; set; }

        [Display(Name = "Nom de la classe")]
        public string NomComplet
        {
            get
            {
                return Niveau + " " + Section ;
            }
        }
                
        public Ecole Ecole { get; set; }
        public ICollection<Cours> Cours { get; set; }
        public ICollection<Inscription> Inscriptions { get; set; }
    }
}
