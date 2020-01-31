using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.DomainClasses.ViewModels
{
    public class CorrectionList
    {
        public int Classid { get; set; }
        public int Testid { get; set; }
        public List<IdString> Allstudent { get; set; }
        public List<AnswerItems> AllQuestion { get; set; }
        public List<StudentAnswerTest> AllGradeQuestion { get; set; }
    }
}
