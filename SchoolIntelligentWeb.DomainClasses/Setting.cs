using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Setting
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int PeopleId { get; set; }
        public int Type { get; set; }
    }
}
