using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class TimeProvider
    {
        public int Id { get; set; }
        public int? TimeId { get; set; }
        public int? ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Provided Provided { get; set; }
        public virtual ICollection<StudentGrade> StudentGrade { get; set; }
        [ForeignKey("TimeId")]
        public virtual Time Time { get; set; }
    }
}
