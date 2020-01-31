using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class TestQuestions
    {
        public int TestId { get; set; }
        public int QuestionCount { get; set; }
        public int Id { get; set; }
        public string Question { get; set; }
        public int Type { get; set; }
        public double Grade { get; set; }
        public string ImageUrl { get; set; }
        public int AnswerKey { get; set; }
        public string QuestionAnswerText { get; set; }
        public string QuestionAnswerPhoto { get; set; }
        public List<Multiply_ChoiseItems> Choises { get; set; }

    }
}
