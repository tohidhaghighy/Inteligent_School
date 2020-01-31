using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.DomainClasses.ViewModels
{
    public class Schadulerlist
    {
        public int Bell { get; set; }
        public List<IdString> Saturday { get; set; }
        public List<IdString> Sunday { get; set; }
        public List<IdString> Monday { get; set; }
        public List<IdString> Tuesday { get; set; }
        public List<IdString> Wednesday { get; set; }
        public List<IdString> Thursday { get; set; }
        public List<IdString> Friday { get; set; }
        
    }
}
