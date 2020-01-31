using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class UpdateApp
    {
        public int Id { get; set; }
        public int versioncode { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string datetime { get; set; }
        public int type { get; set; }
    }
}
