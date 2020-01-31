﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses
{
    public class Section
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("مقطع تحصیلی")]
        [Required]
        [MaxLength(50)]
        public string SectionName { get; set; }

        public virtual ICollection<Class> Class { get; set; }
        public virtual ICollection<Lesson> Lesson { get; set; }
    }
}
