using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Tadrisshode
    {
        public int Id { get; set; }
        public int Classid { get; set; }
        public int Teacherid { get; set; }
        public int Lessonid { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public DateTime? Englishdate { get; set; }
    }
}
