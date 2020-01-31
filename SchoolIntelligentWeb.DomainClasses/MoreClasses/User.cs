using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class User
    {
        [StringLength(30, ErrorMessage = "این فیلد باید حداکثر 30 کاراکتر باشد")]
        [Required(AllowEmptyStrings = false,ErrorMessage = "کد ملی خود را وارد کنید")]
        public string NationalCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور خود را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int Roll { get; set; }
        //[Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [Display(Name = "ایمیل (نام کاربری)")]
        [DisplayName("ایمیل (نام کاربری)")]
        [RegularExpression(@"^[_A-Za-z0-9-\+]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$", ErrorMessage = "ایمیل را بدرستی وارد کنید")]
        [StringLength(50, ErrorMessage = "این فیلد باید حداکثر 50 کاراکتر باشد")]
        public string Email { get; set; }

        public string Rememberme { get; set; }
    }
}
