using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Multiple_choice_Questions
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions Questions { get; set; }
    }
}
