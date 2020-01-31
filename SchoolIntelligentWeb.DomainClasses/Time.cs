﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Time
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public int Bell { get; set; }

        public virtual ICollection<TimeProvider> TimeProvider { get; set; }
    }
}
