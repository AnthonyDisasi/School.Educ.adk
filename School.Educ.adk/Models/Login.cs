using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Models
{
    public class Login
    {
        [Required(ErrorMessage = "L'identifiant est obligatoire"), Display(Name = "Identifiant")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire"), Display(Name = "Mot de passe")]
        public string Password { get; set; }
    }
}
