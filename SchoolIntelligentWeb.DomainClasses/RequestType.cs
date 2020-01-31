using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class RequestType
    {
        public int ID { get; set; }
        public string RequestType1 { get; set; }

        public virtual ICollection<Requests> Requests { get; set; }
    }
}
