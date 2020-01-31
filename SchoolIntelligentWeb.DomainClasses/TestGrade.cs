using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class TestGrade
    {
        public int Id { get; set; }
        public int? IdStudent { get; set; }
        public int? IdGrade { get; set; }
        public int? IdTest { get; set; }
    }
}
