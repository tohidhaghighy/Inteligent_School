using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.DomainClasses.ViewModels
{
    public class ChartItem
    {
        public List<IdString> AllLesson { get; set; }
        public List<IdString> AllStudentAvg { get; set; }
        public List<IdString> AllClassAvg { get; set; }
        public string AllLessonstringformat { get; set; }
        public string AllStudentAvgstringformat { get; set; }
        public string AllClassAvgstringformat { get; set; }
    }
}
