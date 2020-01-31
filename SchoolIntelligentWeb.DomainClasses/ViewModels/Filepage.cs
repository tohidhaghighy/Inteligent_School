using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.DomainClasses.ViewModels
{
    public class Filepage
    {
        public int Id { get; set; }
        public IEnumerable<IdString> Lessons { get; set; }
        public IEnumerable<IdString> Typegrade { get; set; }
        public IEnumerable<IdString> Students { get; set; }
        public IEnumerable<FileInfo> AllFiles { get; set; }
        public IEnumerable<OnlineClassFiles> Onlineclass { get; set; }
    }
}
