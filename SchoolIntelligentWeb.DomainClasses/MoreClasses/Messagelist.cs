using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class Messagelist
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string Seen { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
