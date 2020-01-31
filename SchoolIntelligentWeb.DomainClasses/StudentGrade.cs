using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class StudentGrade
    {
        public int Id { get; set; }
        public int? TypeGrade { get; set; }
        public string Grade { get; set; }
        public string Present { get; set; }
        public System.DateTime Date { get; set; }
        public int? StudentId { get; set; }
        public int? ClassId { get; set; }
        public int? TimeProviderId { get; set; }
        [ForeignKey("TypeGrade")]
        public virtual GradeType GradeType { get; set; }
        public int? LessonId { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        [ForeignKey("TimeProviderId")]
        public virtual TimeProvider TimeProviders { get; set; }

        public virtual ICollection<HomeWork> Homeworks { get; set; }
    }
}
