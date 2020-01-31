using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class AnswerItems
    {
        public int Id { get; set; }
        public string Grade { get; set; }
        public string Questiontext { get; set; }
        public string QuestionAnswer { get; set; }
        public string QuestionImage { get; set; }
        public string QuestionAnswerImage { get; set; }
        public string Allchoise { get; set; }
        public int QuestionId { get; set; }
        public int QuestionType { get; set; }
        //اگر سوال تستی بود
        public int CorrectAnswer { get; set; }
    }
}
