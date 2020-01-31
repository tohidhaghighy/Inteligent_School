using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Tell { get; set; }
        public string CertificateId { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public int? ClassId { get; set; }
        public int? FamilyId { get; set; }
        public int? SchoolId { get; set; }
        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
        public virtual ICollection<HomeWork> HomeWork { get; set; }
        [ForeignKey("FamilyId")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        public virtual ICollection<TestAnswer> TestAnswer { get; set; }
        public virtual ICollection<Requests> Requests { get; set; }
        public virtual ICollection<Discipline> Discipline { get; set; }
        public virtual ICollection<Hint_Student> Hint_Student { get; set; }

    }
}
