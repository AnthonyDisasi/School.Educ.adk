using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Assertion
    {
        public string ID { get; set; }
        public string QuestionID { get; set; }

        public string Intituler { get; set; }

        public Question Question { get; set; }
    }
}
