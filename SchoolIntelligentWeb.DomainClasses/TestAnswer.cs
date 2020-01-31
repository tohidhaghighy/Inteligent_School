using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class TestAnswer
    {
        public int Id { get; set; }
        public int? AnswerKey { get; set; }
        public string AnswerText { get; set; }
        public string AnswerPhoto { get; set; }
        public string TestGrades { get; set; }
        public int? QuestionId { get; set; }
        public int? StudentId { get; set; }
        public int? TestId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions Questions { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }
    }
}
