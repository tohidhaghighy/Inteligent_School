using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Provided
    {
        public int Id { get; set; }
        public int Turn { get; set; }
        public int? ClassId { get; set; }
        public int? LessionId { get; set; }
        public int? TeacherId { get; set; }
        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
        [ForeignKey("LessionId")]
        public virtual Lesson Lesson { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
    }
}
