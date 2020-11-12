using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Reponse
    {
        public string ID { get; set; }
        public string QuestionID { get; set; }
        public string ParticipantID { get; set; }

        public string ReponseDonnee { get; set; }
        public double Point { get; set; }

        public Question Question { get; set; }
        public Participant Participant { get; set; }
    }
}
