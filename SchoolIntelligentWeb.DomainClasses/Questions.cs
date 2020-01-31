using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Questions
    {
        
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int? CorrectAnswerId { get; set; }
        public string AnswerDescription { get; set; }
        public string QuestionPicture { get; set; }
        public string AnswerPicture { get; set; }
        public double QuestionGrade { get; set; }
        public int QuestionType { get; set; }
        public int? TestId { get; set; }
        public virtual ICollection<Multiple_choice_Questions> Multiple_choice_Questions { get; set; }
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }
        public virtual ICollection<TestAnswer> TestAnswer { get; set; }
    }
}
