using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        //Celui qui envoie la notification, son matricule
        public string Expediteur { get; set; }
        //Celui qui reçoit la notification, son matricule
        public string Destinataire { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateMessage { get; set; }

        public string Message { get; set; }

        public bool Lu { get; set; }
    }
}
