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
    public class File
    {
       
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("نام فایل")]
        public string Name { get; set; }
        [DisplayName("تاریخ آپلود")]
        public DateTime? Date { get; set; }
        [DisplayName("ساعت آپلود")]
        public TimeSpan? Time { get; set; }
        [DisplayName("توضیحات فایل")]
        public string Description { get; set; }
        public string Url { get; set; }
        public int StudentId { get; set; }
        public int? TypeFile { get; set; }
        public int? LessonId { get; set; }
        public int? SchoolId { get; set; }
        public int? TeacherId { get; set; }
        [ForeignKey("TypeFile")]
        public virtual Filetype Filetype { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
    }
}
