using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class IdString
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string ClassAvg { get; set; }
        public int StudentCount { get; set; }
        public List<IdString> TimeList { get; set; }
    }
}
