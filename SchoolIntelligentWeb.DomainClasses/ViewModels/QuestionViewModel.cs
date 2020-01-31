using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.DomainClasses.ViewModels
{
    public class QuestionViewModel
    {
        public TestInfo TestInfo { get; set; }
        public List<TestQuestions> AllQuestions { get; set; }
        public TestQuestions QuestionInfo { get; set; }
    }
}
