using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class StudentAnswerTest
    {
        public string Name { get; set; }
        public int Idstudent { get; set; }
        public int Idclass { get; set; }
        public int Idtest { get; set; }
        public int Idgradestudent { get; set; }
        public List<ListAnswerInfo> ListAnswerInfos { get; set; }
        public IdString ListGrade { get; set; }
        
    }

    public class ListAnswerInfo
    {
        public int Idquestion { get; set; }
        public int QuestionType { get; set; }
        public string AnswerText { get; set; }
        public string AnswerImage { get; set; }
        public Boolean IsAnswered { get; set; }
        public string Point { get; set; }
        public string QuestionGrade { get; set; }
        public int QuestionAnswer { get; set; }
    }

}
