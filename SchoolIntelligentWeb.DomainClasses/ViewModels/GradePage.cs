using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.DomainClasses.ViewModels
{
    public class GradePage
    {
        public IEnumerable<IdString> Lessons { get; set; }
        public IEnumerable<IdString> Typegrade { get; set; }
        public IEnumerable<Grade> AllGrades { get; set; }
        public string Name { get; set; }
    }
}
