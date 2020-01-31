using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FieldId { get; set; }
        public int? SchoolId { get; set; }
        public int? SectionId { get; set; }
        [ForeignKey("FieldId")]
        public virtual Field Field { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Provided> Provided { get; set; }
    }
}
