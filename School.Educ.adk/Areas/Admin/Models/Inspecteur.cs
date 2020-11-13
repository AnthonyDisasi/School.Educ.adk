using School.Educ.adk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Admin.Models
{
    public class Inspecteur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required(ErrorMessage = "Le Nom est obligatoire")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Le Postnom est obligatoire")]
        public string Postnom { get; set; }
        [Required(ErrorMessage = "Le Prenom est obligatoire")]
        public string Prenom { get; set; }
        [EnumDataType(typeof(Genre))]
        public Genre? Genre { get; set; }
        [Required(ErrorMessage = "Le matricule est obligatoire")]
        public string Matricule { get; set; }
        [Required(ErrorMessage = "L'adresse mail est obligatoire")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Le mot de passe est obligatoire"), Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Date de naissance"), Required(ErrorMessage = "La date de naissance est obligatoire"), DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        public string NomComplet
        {
            get
            {
                return Nom + " " + Postnom + " " + Prenom;
            }
        }
    }
}

