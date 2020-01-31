using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses;

namespace SchoolIntelligentWeb.DataLayer
{
    public class DatabaseContext : System.Data.Entity.DbContext
    {
        ////static DatabaseContext()
        ////{
        ////    System.Data.Entity.Database.SetInitializer
        ////        (new System.Data.Entity.MigrateDatabaseToLatestVersion
        ////            <DatabaseContext, Migrations.Configuration>());
        ////}
        //      : base("InteligentSchool")
        //    : base("Data Source=95.38.61.182,1435;Integrated Security=False;User ID=goldenba_School;Password=Taktazegarn0123;Initial Catalog=goldenba_School;")
        //"Data Source=91.98.50.107;Integrated Security=False;User ID=tohidhaghighi;Password=tohid123;Initial Catalog=InteligentSchool;"
        public DatabaseContext()
            : base("DefaultConnection")
        {

        }
        public System.Data.Entity.DbSet<Visit> Visits { get; set; }
        public System.Data.Entity.DbSet<Discipline> Disciplines { get; set; }
        public System.Data.Entity.DbSet<Hint_Student> HintStudents { get; set; }
        public System.Data.Entity.DbSet<Negotiations> Negotiationses { get; set; }
        public System.Data.Entity.DbSet<Requests> Requestses { get; set; }
        public System.Data.Entity.DbSet<RequestType> RequestTypes { get; set; }
        public System.Data.Entity.DbSet<SessionFunction> SessionFunctions { get; set; }
        public System.Data.Entity.DbSet<File> Files { get; set; }
        public System.Data.Entity.DbSet<Teacher> Teachers { get; set; }
        public System.Data.Entity.DbSet<Lesson> Lessons { get; set; }
        public System.Data.Entity.DbSet<Filetype> Filetypes { get; set; }
        public System.Data.Entity.DbSet<Field> Fields { get; set; }
        public System.Data.Entity.DbSet<School> Schools { get; set; }
        public System.Data.Entity.DbSet<Section> Sections { get; set; }
        public System.Data.Entity.DbSet<Setting> Settings { get; set; }
        public System.Data.Entity.DbSet<Test> Tests { get; set; }
        public System.Data.Entity.DbSet<Questions> Questions { get; set; }
        public System.Data.Entity.DbSet<Class> Classes { get; set; }
        public System.Data.Entity.DbSet<Assistent> Assistents { get; set; }
        public System.Data.Entity.DbSet<Parent> Parents { get; set; }
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<Student> Students { get; set; }
        public System.Data.Entity.DbSet<HomeWork> HomeWorks { get; set; }
        public System.Data.Entity.DbSet<Provided> Provided { get; set; }
        public System.Data.Entity.DbSet<TestAnswer> TestAnswers { get; set; }
        public System.Data.Entity.DbSet<Time> Times { get; set; }
        public System.Data.Entity.DbSet<TimeProvider> TimeProviders { get; set; }
        public System.Data.Entity.DbSet<StudentGrade> StudentGrades { get; set; }
        public System.Data.Entity.DbSet<GradeType> GradeTypes { get; set; }
        public System.Data.Entity.DbSet<AssistentMassage> AssistentMassages { get; set; }
        public System.Data.Entity.DbSet<TestGrade> TestGrades { get; set; }
        public System.Data.Entity.DbSet<Multiple_choice_Questions> MultipleChoiceQuestions { get; set; }
        public System.Data.Entity.DbSet<OnlineClass> OnlineClasses { get; set; }
        public System.Data.Entity.DbSet<OnlineClassFiles> OnlineClassFiles { get; set; }
        public System.Data.Entity.DbSet<Emtehanat> Emtehanats { get; set; }
        public System.Data.Entity.DbSet<Tadrisshode> Tadrisshodes { get; set; }
        public System.Data.Entity.DbSet<Tashvighi> Tashvighies { get; set; }
        public System.Data.Entity.DbSet<UpdateApp> UpdateApp { get; set; }
        public System.Data.Entity.DbSet<MemorizeMassage> MemorizeMassage { get; set; }
    }
}
