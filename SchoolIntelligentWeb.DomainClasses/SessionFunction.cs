using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class SessionFunction
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int session { get; set; }
        public string Description { get; set; }
        public int? ID_Provided { get; set; }
        public string type { get; set; }
        [ForeignKey("ID_Provided")]
        public virtual Provided Provided { get; set; }
    }
}
