using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Parent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Tell { get; set; }
        public string Address { get; set; }
        public string CerteficateId { get; set; }

        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Negotiations> Negotiations { get; set; }
    }
}
