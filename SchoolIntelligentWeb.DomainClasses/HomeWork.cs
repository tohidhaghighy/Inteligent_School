using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class HomeWork
    {
        public int Id { get; set; }
        public DateTime HomeworkDate { get; set; }
        public string Photo { get; set; }
        public int? LessonId { get; set; }
        public int? SchoolId { get; set; }
        public int? GradeId { get; set; }
        public int? StudentId { get; set; }
        [ForeignKey("GradeId")]
        public virtual StudentGrade StudentGrade { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
