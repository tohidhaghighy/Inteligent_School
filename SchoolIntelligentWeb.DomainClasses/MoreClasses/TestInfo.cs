using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class TestInfo
    {
        public int Number { get; set; }
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "این فیلد باید حداکثر 100 کاراکتر باشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام خود را وارد کنید")]
        public string Text { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "تاریخ شروع خود را وارد کنید")]
        
        public string StartDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "تاریخ پایان خود را وارد کنید")]
        
        public string FinishDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "مدت آزمون را وارد کنید")]
        public int Duration { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "مدت تاخیر آزمون را وارد کنید")]
        public int MaxDelay { get; set; }
        public int Type { get; set; }
        public int QuestionCount { get; set; }
        public int TestGrade { get; set; }
        public string Lesson { get; set; }
        public string Teacher { get; set; }
        public string School { get; set; }
        public int ClassId { get; set; }
    }
}
