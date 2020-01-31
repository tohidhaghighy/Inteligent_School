using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class QuestionInfo
    {
        public List<AnswerItems> Answers { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
    }
}
