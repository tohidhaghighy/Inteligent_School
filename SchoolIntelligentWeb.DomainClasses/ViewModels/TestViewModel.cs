using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.DomainClasses.ViewModels
{
    public class TestViewModel
    {
        public List<TestInfo> Tests { get; set; }
        public IEnumerable<IdString> Lessons { get; set; }
    }
}
