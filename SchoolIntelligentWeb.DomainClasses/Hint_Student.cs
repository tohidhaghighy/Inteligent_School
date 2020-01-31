using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Hint_Student
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        public int? ID_Teacher { get; set; }
        public int? ID_Lesson { get; set; }
        public int? ID_Student { get; set; }
        public string Type { get; set; }
        [ForeignKey("ID_Lesson")]
        public virtual Lesson Lesson { get; set; }
        [ForeignKey("ID_Student")]
        public virtual Student Student { get; set; }
        [ForeignKey("ID_Teacher")]
        public virtual Teacher Teacher { get; set; }
    }
}
