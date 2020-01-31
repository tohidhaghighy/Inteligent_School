using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.DomainClasses.MoreClasses
{
    public class FileInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrivateStudentName { get; set; }
        public string Date { get; set; }
        public TimeSpan? Time { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int? FileType { get; set; }
        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public string Grade { get; set; }
    }
}
