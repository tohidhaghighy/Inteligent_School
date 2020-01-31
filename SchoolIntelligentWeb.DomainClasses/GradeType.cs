using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class GradeType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Evalution { get; set; }
        public int? Grade { get; set; }

        public virtual ICollection<StudentGrade> StudentGrade { get; set; }
    }
}
