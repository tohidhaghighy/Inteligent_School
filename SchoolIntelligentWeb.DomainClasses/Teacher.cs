using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.Utilities.Attributes;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("نام")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [DisplayName("نام خانوادگی")]
        [Required]
        [MaxLength(50)]
        public string Family { get; set; }
        [DisplayName("شماره تلفن")]
        [MaxLength(50)]
        public string Phone { get; set; }
        [DisplayName("آدرس")]
        [MaxLength(500)]
        public string Address { get; set; }
        [DisplayName("کد ملی")]
        [MaxLength(10)]
        [Required(ErrorMessage = "کد ملی خود را وارد کنید")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی خود را بدرستی وارد کنید")]
        [CodeMelli("لطفا کد ملی را بدرستی وارد کنید")]
        public string NationalCode { get; set; }
        public string Photo { get; set; }
        public string Password { get; set; }
        [DisplayName("تاریخ تولد")]
        [MaxLength(10)]
        public string Birthdate { get; set; }
        public string Email { get; set; }
        public int? SchoolId { get; set; }
        public virtual ICollection<File> File { get; set; }
        public virtual ICollection<Provided> Provided { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        public virtual ICollection<Test> Test { get; set; }

        
    }
}
