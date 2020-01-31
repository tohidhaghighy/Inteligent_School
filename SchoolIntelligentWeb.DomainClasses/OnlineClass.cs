using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class OnlineClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Maghtae { get; set; }
        public string Date { get; set; }
        public DateTime SearchDatetime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int TeacherId { get; set; }
    }
}
