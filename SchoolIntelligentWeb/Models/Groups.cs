using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolIntelligentWeb.Models
{
    public class Groups
    {
        public string ChatId { get; set; }
        public string Chatname { get; set; }
        public List<User> AlluserinGroup { get; set; }
    }
}