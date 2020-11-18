using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Le matricule est obligatoire"), Display(Name = "matricule")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "L'e - mail est obligatoire"), Display(Name = "E - mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire"), Display(Name = "Mot de passe")]
        public string Password { get; set; }
    }
}
