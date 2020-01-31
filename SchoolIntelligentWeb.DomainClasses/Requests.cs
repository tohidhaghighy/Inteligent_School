using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Requests
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public System.DateTime Date { get; set; }
        public string sender { get; set; }
        public string reciver { get; set; }
        public bool? Seen { get; set; }
        
        public int? ID_Student { get; set; }
        public int? ID_RequestType { get; set; }
        [ForeignKey("ID_RequestType")]
        public virtual RequestType RequestType { get; set; }
        [ForeignKey("ID_Student")]
        public virtual Student Student { get; set; }
    }
}
