using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Visit
    {
        
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        public int? ID_Teacher { get; set; }
        public int? ID_Assistent { get; set; }
        [ForeignKey("ID_Assistent")]
        public virtual Assistent Assistant { get; set; }
        [ForeignKey("ID_Teacher")]
        public virtual Teacher Teacher { get; set; }
    }
}
