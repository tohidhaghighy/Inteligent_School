using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Assistent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Tell { get; set; }
        public string Photo { get; set; }
        public string CerteficateId { get; set; }
        public int? SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        public virtual ICollection<Visit> Visit { get; set; }
    }
}
