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
    public class School
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("نام مدرسه")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Photo { get; set; }
        [DisplayName("آدرس مدرسه")]
        [MaxLength(1000)]
        public string Address { get; set; }
        [DisplayName("سال تحصیلی")]
        [Required]
        public DateTime? Date { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Assistent> Assistent { get; set; }
        public virtual ICollection<Class> Class { get; set; }
        public virtual ICollection<File> File { get; set; }
        public virtual ICollection<HomeWork> HomeWork { get; set; }
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
        public virtual ICollection<Test> Test { get; set; }
    }
}
