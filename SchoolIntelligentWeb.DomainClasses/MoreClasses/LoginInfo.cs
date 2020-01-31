using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class LoginInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Image { get; set; }
        public int? ClassId { get; set; }
        public string Classname { get; set; }
        public string ClassAvg { get; set; }
        public string Password { get; set; }
        public string ParentName { get; set; }
        public List<IdString> ClassList { get; set; }
        public List<IdString> TypeList { get; set; }
    }
}
