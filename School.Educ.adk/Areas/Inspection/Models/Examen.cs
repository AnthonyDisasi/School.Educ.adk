using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Educ.adk.Areas.Inspection.Models
{
    public class Examen
    {
        public string ID { get; set; }

        public string Description { get; set; }
        public string Periode { get; set; }
        public string Serie { get; set; }
        public string CodeAcces { get; set; }

        public string IdInspecteur { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
