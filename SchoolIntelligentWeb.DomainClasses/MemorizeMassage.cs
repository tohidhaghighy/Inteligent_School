using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class MemorizeMassage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
        public int Ref_Id { get; set; }
        public string SendDate { get; set; }
    }
}
