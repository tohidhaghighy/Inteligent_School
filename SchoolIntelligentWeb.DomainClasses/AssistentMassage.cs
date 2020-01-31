using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class AssistentMassage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int StudentId { get; set; }
        public int? SchoolId { get; set; }
        public int AssistentId { get; set; }
        public System.DateTime SendDate { get; set; }
        public bool Seen { get; set; }
    }
}
