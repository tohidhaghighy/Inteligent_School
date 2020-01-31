using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Discipline
    {
        public int ID { get; set; }
        public string DescriptionD { get; set; }
        public int IDStudent { get; set; }
        public string date { get; set; }
        [ForeignKey("IDStudent")]
        public virtual Student Student { get; set; }
    }
}
