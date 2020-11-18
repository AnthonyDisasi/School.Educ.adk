using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Models
{
    public class RoleModificationModel
    {
        [Required(ErrorMessage = "Le rôle est obligatoire, veuillez le remplire"), Display(Name = "Le nom du rôle")]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}