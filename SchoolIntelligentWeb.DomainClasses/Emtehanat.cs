using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Emtehanat
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Classid { get; set; }
        public string Date { get; set; }
        public DateTime? EnglishDate { get; set; }
    }
}
