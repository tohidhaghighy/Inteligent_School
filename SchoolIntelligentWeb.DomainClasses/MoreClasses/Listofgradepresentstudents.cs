using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class Listofgradepresentstudents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<gradepresent> Huzur { get; set; }
        public List<gradepresent> Nomarat { get; set; }
    }
}
