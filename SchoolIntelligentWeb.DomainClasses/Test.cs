using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Test
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public int TestType { get; set; }
        public int DelayTime { get; set; }
        public int TestGrade { get; set; }
        public int? LessonId { get; set; }
        public int? SchoolId { get; set; }
        public int? TeacherId { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<TestAnswer> TestAnswer { get; set; }
    }
}
