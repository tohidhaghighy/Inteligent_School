using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Unit { get; set; }
        public int? FieldNameId { get; set; }
        public int? SectionNameId { get; set; }
        [ForeignKey("FieldNameId")]
        public virtual Field Field { get; set; }
        public virtual ICollection<File> File { get; set; }
        public virtual ICollection<HomeWork> HomeWork { get; set; }
        [ForeignKey("SectionNameId")]
        public virtual Section Section { get; set; }
        public virtual ICollection<Provided> Provided { get; set; }
        public virtual ICollection<Test> Test { get; set; }

    }
}
