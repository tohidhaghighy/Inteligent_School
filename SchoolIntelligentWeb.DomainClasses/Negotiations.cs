using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Negotiations
    {
        public int ID { get; set; }
        public int? ID_Parent { get; set; }
        public int? ID_Teacher { get; set; }
        public System.DateTime Date { get; set; }
        public string Discussion { get; set; }
        [ForeignKey("ID_Parent")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("ID_Teacher")]
        public virtual Teacher Teacher { get; set; }
    }
}
