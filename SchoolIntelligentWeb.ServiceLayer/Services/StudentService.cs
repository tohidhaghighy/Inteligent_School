using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.DomainClasses.ViewModels;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class StudentService : IStudentService
    {
        private DatabaseContext _dbContext = null;

        /// <summary>
        /// لیست دروس دانش آموز
        /// </summary>
        /// <param name="idstudent"></param>
        /// <returns></returns>
        public List<DomainClasses.MoreClasses.IdString> GetStudentClasslist(int idstudent)
        {
            List<DomainClasses.MoreClasses.IdString> Alllist = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == idstudent);
                if (findstudent != null)
                {
                    var findclasslist = _dbContext.Provided.Where(a => a.ClassId == findstudent.ClassId).ToList();
                    foreach (var findclass in findclasslist)
                    {
                        DomainClasses.MoreClasses.IdString classinfo = new IdString() { Id = findclass.LessionId, Name = findclass.Lesson.Name + " " + findclass.Class.Name };
                        Alllist.Add(classinfo);
                    }
                }
            }


            return Alllist;
        }

             /// <summary>
        /// لیست دروس دانش آموز
        /// test
        /// </summary>
        /// <param name="idstudent"></param>
        /// <returns></returns>
        public List<DomainClasses.MoreClasses.IdString> GetStudentClasslistreturnprovide(int idstudent)
        {
            List<DomainClasses.MoreClasses.IdString> Alllist = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == idstudent);
                if (findstudent != null)
                {
                    var findclasslist = _dbContext.Provided.Where(a => a.ClassId == findstudent.ClassId).ToList();
                    foreach (var findclass in findclasslist)
                    {
                        DomainClasses.MoreClasses.IdString classinfo = new IdString() { Id = findclass.Id, Name = findclass.Lesson.Name+" "+findclass.Class.Name };
                        Alllist.Add(classinfo);
                    }
                }
            }

            return Alllist;
        }
        /// <summary>
        /// if(type==0)
        /// نمرات
        /// else    
        /// حضورغیاب
        /// </summary>
        /// <param name="idstudent">شماره دانش آموزی</param>
        /// <param name="idlesson">شماره درس</param>
        /// <param name="type">نمرات یا حضور غیاب</param>
        /// <returns></returns>
        public List<Grade> GetStudentGrades(int idstudent, int idlesson, int type, int typegrade)
        {
            List<Grade> Alllist = new List<Grade>();
            using (_dbContext = new DatabaseContext())
            {
                var findgrades = _dbContext.StudentGrades.Where(a => a.StudentId == idstudent && a.TimeProviders.Provided.LessionId == idlesson && a.TimeProviderId != null).ToList();
                foreach (var items in findgrades)
                {

                    Grade newgrade = new Grade();
                    newgrade.GradeId = items.Id;
                    if (type == 1)
                    {
                        newgrade.StudentGrade = items.Grade;
                        if (string.IsNullOrEmpty(items.Present))
                        {
                            newgrade.StudentPresent = "";
                        }
                        else
                        {
                            newgrade.StudentPresent = items.Present;
                        }
                        newgrade.Date = items.Date.ToPersianDate().ToString("yyyy/MM/dd").toPersianNumber();
                        Alllist.Add(newgrade);
                    }
                    else
                    {
                        if (items.TypeGrade == typegrade)
                        {
                            newgrade.StudentGrade = items.Grade;
                            if (string.IsNullOrEmpty(items.Present))
                            {
                                newgrade.StudentPresent = "";
                            }
                            else
                            {
                                newgrade.StudentPresent = items.Present;
                            }
                            newgrade.Date = items.Date.ToPersianDate().ToString("yyyy/MM/dd").toPersianNumber();
                            Alllist.Add(newgrade);
                        }
                    }

                }
                return Alllist;
            }
            return Alllist;
        }

        public List<Grade> GetStudentGradeswithprovider(int idstudent, int providedid, int type, int typegrade)
        {
            List<Grade> Alllist = new List<Grade>();
            using (_dbContext = new DatabaseContext())
            {
                var findgrades = _dbContext.StudentGrades.Where(a => a.StudentId == idstudent && a.TimeProviders.ProviderId == providedid && a.TimeProviderId != null).ToList();
                foreach (var items in findgrades)
                {

                    Grade newgrade = new Grade();
                    newgrade.GradeId = items.Id;
                    newgrade.StudentGrade = items.Grade;
                    if (string.IsNullOrEmpty(items.Present))
                    {
                        newgrade.StudentPresent = "";
                    }
                    else
                    {
                        newgrade.StudentPresent = items.Present;
                    }
                    newgrade.Date = items.Date.ToString("yyyy/MM/dd").toPersianNumber();
                    Alllist.Add(newgrade);


                }
                return Alllist;
            }
            return Alllist;
        }
        public List<IdString> gettypeGrade()
        {
            List<DomainClasses.MoreClasses.IdString> Alllist = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findtype = _dbContext.GradeTypes.ToList();
                if (findtype != null)
                {

                    foreach (var findtypes in findtype)
                    {
                        DomainClasses.MoreClasses.IdString classinfo = new IdString()
                        {
                            Id = findtypes.Id,
                            Name = findtypes.Type
                        };
                        Alllist.Add(classinfo);
                    }
                }
            }

            return Alllist;
        }

        public List<Assistetmessage> GetAssistentmessagelist(int idstudent)
        {
            List<Assistetmessage> allmessage = new List<Assistetmessage>();
            using (_dbContext = new DatabaseContext())
            {
                var finditems = _dbContext.AssistentMassages.Where(a => a.StudentId == idstudent).ToList();
                foreach (var items in finditems)
                {
                    Assistetmessage newmessage = new Assistetmessage()
                    {
                        Message = items.Message,
                        date = items.SendDate.ToPersian(),
                    };
                    items.Seen = true;
                    allmessage.Add(newmessage);
                }
                _dbContext.SaveChanges();

                return allmessage;
            }

            return null;
        }
        public List<ListofTime> GetListofTime(int idstudent)
        {
            List<ListofTime> alltime = new List<ListofTime>();
            using (_dbContext = new DatabaseContext())
            {
                var findclass = _dbContext.Students.FirstOrDefault(a => a.Id == idstudent);
                if (findclass != null)
                {
                    var providers = _dbContext.Provided.Where(a => a.ClassId == findclass.ClassId).ToList();
                    if (providers.Count > 0)
                    {
                        foreach (var items in providers)
                        {
                            var findtimeprovide = _dbContext.TimeProviders.Where(a => a.ProviderId == items.Id).ToList();
                            foreach (var listofprovider in findtimeprovide)
                            {
                                ListofTime time = new ListofTime()
                                {
                                    Bell = listofprovider.Time.Bell,
                                    Day = listofprovider.Time.Day,
                                    LessonName = listofprovider.Provided.Lesson.Name,
                                };
                                alltime.Add(time);
                            }
                        }
                    }
                }
            }
            return alltime;
        }

        public string[] alldays = { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
        public Schadulerlist GetSchaduler(List<ListofTime> alltime)
        {
            List<IdString> allsaturday = new List<IdString>();
            List<IdString> allsunday = new List<IdString>();
            List<IdString> allMonday = new List<IdString>();
            List<IdString> Tuesday = new List<IdString>();
            List<IdString> wednizday = new List<IdString>();
            List<IdString> thirsday = new List<IdString>();
            List<IdString> friday = new List<IdString>();
            Schadulerlist allschaduler = new Schadulerlist();
            if (alltime.Count() > 0)
            {
                allschaduler.Bell = alltime.Max(a => a.Bell);

                for (int i = 1; i <= allschaduler.Bell; i++)
                {
                    foreach (var days in alldays)
                    {
                        var findday = alltime.FirstOrDefault(a => a.Day == days && a.Bell == i);

                        IdString newday = new IdString()
                        {
                            Name = "",
                            Date = "",
                            Id = 0
                        };
                        if (findday != null)
                        {
                            newday.Name = findday.LessonName;
                        }
                        if (days == "شنبه")
                        {
                            allsaturday.Add(newday);

                        }
                        else if (days == "یکشنبه")
                        {
                            allsunday.Add(newday);
                        }
                        else if (days == "دوشنبه")
                        {
                            allMonday.Add(newday);
                        }
                        else if (days == "سه شنبه")
                        {
                            Tuesday.Add(newday);
                        }
                        else if (days == "چهارشنبه")
                        {
                            wednizday.Add(newday);
                        }
                        else if (days == "پنجشنبه")
                        {
                            thirsday.Add(newday);
                        }
                        else if (days == "جمعه")
                        {
                            friday.Add(newday);
                        }
                    }

                }
            }

            allschaduler.Saturday = allsaturday;
            allschaduler.Sunday = allsunday;
            allschaduler.Monday = allMonday;
            allschaduler.Tuesday = Tuesday;
            allschaduler.Wednesday = wednizday;
            allschaduler.Thursday = thirsday;
            allschaduler.Friday = friday;
            return allschaduler;
        }
        public Boolean Addgrade(int lessonid, string grade, int type, int time_id, int studentid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var find = _dbContext.Students.Where(a => a.Id == studentid).FirstOrDefault();
                if (find != null)
                {
                    StudentGrade newgrade = new StudentGrade()
                    {
                        Date = DateTime.Now,
                        Grade = grade,
                        LessonId = lessonid,
                        Present = "p",
                        StudentId = studentid,
                        TimeProviderId = time_id,
                        TypeGrade = type
                    };
                    _dbContext.StudentGrades.Add(newgrade);
                }
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public Boolean deletegrade(int id)
        {
            using (_dbContext = new DatabaseContext())
            {
                var find = _dbContext.StudentGrades.Where(a => a.Id == id).FirstOrDefault();
                if (find != null)
                {
                    find.Grade = "";
                }
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public Boolean AddPresent(int id, string present)
        {
            using (_dbContext = new DatabaseContext())
            {
                var find = _dbContext.StudentGrades.Find(id);
                if (find != null)
                {
                    find.Present = present;
                }
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<DomainClasses.MoreClasses.IdString> GetStudentAvgforChart(int idstudent, List<IdString> alllesson)
        {
            List<DomainClasses.MoreClasses.IdString> Alllist = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                int count = 1;
                foreach (var lesson in alllesson)
                {
                    var findgrade =
                        _dbContext.StudentGrades.Where(a => a.TimeProviders.Provided.LessionId == lesson.Id && a.StudentId == idstudent && a.Grade != "" && a.GradeType.Evalution!="کیفی")
                            .ToList();
                    int avg = 0;
                    if (findgrade.Count() > 0)
                    {
                        int number = 0;
                        foreach (var grade in findgrade)
                        {
                            if (grade.Grade.Length >= 2)
                            {
                                grade.Grade = grade.Grade.Substring(0, 2);

                            }
                            else if (grade.Grade.Trim() == "100")
                            {
                                grade.Grade = "100";
                            }
                            else
                            {
                                grade.Grade = grade.Grade;
                            }
                            if (number == 0)
                            {
                                avg = ((Convert.ToInt32(grade.Grade) * 20) / grade.GradeType.Grade.Value);
                                number++;
                            }
                            else
                            {
                                avg += ((Convert.ToInt32(grade.Grade) * 20) / grade.GradeType.Grade.Value);
                                number++;
                            }
                        }
                        avg = avg / number;
                    }
                    else
                    {
                        avg = 0;
                    }

                    Alllist.Add(new IdString() { Name = avg.ToString(), Id = count });
                    count++;
                }
            }

            return Alllist;
        }
        public List<DomainClasses.MoreClasses.IdString> GetlessonAvgforChart(List<IdString> alllesson)
        {
            List<DomainClasses.MoreClasses.IdString> Alllist = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                int count = 1;
                foreach (var lesson in alllesson)
                {
                    var findgrade =
                        _dbContext.StudentGrades.Where(a => a.TimeProviders.Provided.LessionId == lesson.Id && a.Grade != "" && a.GradeType.Evalution != "کیفی")
                            .ToList();
                    int avg = 0;
                    if (findgrade.Count() > 0)
                    {
                        int number = 0;
                        foreach (var grade in findgrade)
                        {
                            if (grade.Grade.Length >= 2)
                            {
                                grade.Grade = grade.Grade.Substring(0, 2);

                            }
                            else if (grade.Grade.Trim() == "100")
                            {
                                grade.Grade = "100";
                            }
                            else
                            {
                                grade.Grade = grade.Grade;
                            }
                            if (number == 0)
                            {
                                avg = ((Convert.ToInt32(grade.Grade) * 20) / grade.GradeType.Grade.Value);
                                number++;
                            }
                            else
                            {
                                avg += ((Convert.ToInt32(grade.Grade) * 20) / grade.GradeType.Grade.Value);
                                number++;
                            }
                        }
                        avg = avg / number;
                    }
                    else
                    {
                        avg = 0;
                    }

                    Alllist.Add(new IdString() { Name = avg.ToString(), Id = count });
                    count++;
                }
            }

            return Alllist;
        }
        public List<Grade> GetStudentabsents(int idstudent, int idlesson)
        {
            List<Grade> Alllist = new List<Grade>();
            using (_dbContext = new DatabaseContext())
            {
                var findgrades = _dbContext.StudentGrades.Where(a => a.StudentId == idstudent && a.TimeProviders.Provided.LessionId == idlesson && a.Present != "").ToList();
                foreach (var items in findgrades)
                {
                    Grade newgrade = new Grade();
                    newgrade.Date = items.Date.ToPersianDate().ToString("yyyy/MM/dd").toPersianNumber();
                    newgrade.StudentPresent = items.Present;
                    Alllist.Add(newgrade);
                }
            }
            return Alllist;
        }
        public List<Grade> GetStudentabsentsgrade(int idstudent, int idlesson)
        {
            List<Grade> Alllist = new List<Grade>();
            using (_dbContext = new DatabaseContext())
            {
                var findgrades = _dbContext.StudentGrades.Where(a => a.StudentId == idstudent && a.TimeProviders.Provided.LessionId == idlesson).ToList();
                foreach (var items in findgrades)
                {
                    Grade newgrade = new Grade();
                    newgrade.Date = items.Date.ToPersianDate().ToString("yyyy/MM/dd").toPersianNumber();
                    newgrade.StudentPresent = items.Present;
                    newgrade.StudentGrade = items.Grade;
                    if (items.TypeGrade.ToString() != "")
                    {
                        var findtype = _dbContext.GradeTypes.Where(a => a.Id == items.TypeGrade.Value).FirstOrDefault();
                        newgrade.TypeGrade = findtype.Type;
                    }
                    else
                    {
                        newgrade.TypeGrade = "ندارد";
                    }
                    Alllist.Add(newgrade);
                }
            }
            return Alllist;
        }
        public Boolean Addallpresent(string present, int classid, int lessonid, int time_id)
        {
            using (_dbContext = new DatabaseContext())
            {
                List<string> numbers = new List<string>(present.Split(','));
                var findtime = _dbContext.TimeProviders.FirstOrDefault(a => a.Id == time_id);
                if (findtime != null)
                {
                    var findstudent = _dbContext.Students.Where(a => a.ClassId == findtime.Provided.Class.Id).ToList();
                    foreach (var item in findstudent)
                    {
                        StudentGrade newpresent = new StudentGrade()
                        {
                            StudentId = item.Id,
                            Date = DateTime.Now,
                            Grade = "",
                            LessonId = lessonid,
                            TimeProviderId = time_id
                        };
                        if (numbers.Exists(x => string.Equals(x, item.Id.ToString(), StringComparison.OrdinalIgnoreCase)))
                        {
                            newpresent.Present = "a";
                        }
                        else
                        {
                            newpresent.Present = "p";
                        }
                        _dbContext.StudentGrades.Add(newpresent);

                    }
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public Boolean Addlistgrade(string grade, int lessonid, int type, int time_id)
        {
            using (_dbContext = new DatabaseContext())
            {
                List<string> numbers = new List<string>(grade.Split(','));
                List<string> ids = new List<string>();
                List<string> grades = new List<string>();
                foreach (var item in numbers)
                {
                    List<string> numberandid = new List<string>(item.Split('-'));
                    ids.Add(numberandid[0]);
                    grades.Add(numberandid[1]);
                }

                if (numbers != null)
                {
                    int id = Convert.ToInt32(ids[0]);
                    var findstudent = _dbContext.Students.FirstOrDefault(a => a.Id == id);
                    var findstudents = _dbContext.Students.Where(a => a.ClassId == findstudent.ClassId).ToList();
                    if (findstudents != null)
                    {
                        foreach (var stu in findstudents)
                        {
                            int choise = existinlist(ids, grades, stu.Id);
                            if (existinlist(ids, grades, stu.Id) != -1)
                            {
                                StudentGrade newgrade = new StudentGrade()
                                {
                                    TypeGrade = type,
                                    Present = "p",
                                    Grade = grades[choise],
                                    StudentId = stu.Id,
                                    LessonId = lessonid,
                                    Date = DateTime.Now,
                                    TimeProviderId = time_id
                                };
                                _dbContext.StudentGrades.Add(newgrade);
                            }
                            else
                            {
                                StudentGrade newgrade = new StudentGrade()
                                {
                                    TypeGrade = type,
                                    Present = "a",
                                    Grade = "",
                                    StudentId = stu.Id,
                                    LessonId = lessonid,
                                    Date = DateTime.Now,
                                    TimeProviderId = time_id
                                };
                                _dbContext.StudentGrades.Add(newgrade);
                            }
                        }
                        _dbContext.SaveChanges();
                    }
                }

                return true;
            }
            return false;
        }
        public int existinlist(List<string> ids, List<string> grades, int studentid)
        {
            int count = -1;
            foreach (var item in ids)
            {
                count++;
                if (item == (studentid.ToString()))
                {
                    return count;
                }
            }
            return -1;
        }
        public Boolean updategrade(int id, string grade)
        {
            using (_dbContext = new DatabaseContext())
            {
                var find = _dbContext.StudentGrades.Where(a => a.Id == id).FirstOrDefault();
                if (find != null)
                {
                    find.Grade = grade.ToString();
                }
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public Boolean Adddisciplines(int studentid, string text)
        {
            using (_dbContext = new DatabaseContext())
            {
                Discipline newdiscipline = new Discipline()
                {
                    date = DateTime.Now.ToString(),
                    IDStudent = studentid,
                    DescriptionD = text
                };
                _dbContext.Disciplines.Add(newdiscipline);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<IdString> listdisciplines(int studentid)
        {
            List<IdString> alllist = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var finddis = _dbContext.Disciplines.Where(a => a.IDStudent == studentid).ToList();
                foreach (var item in finddis)
                {
                    string date = Convert.ToDateTime(item.date).ToPersianDateAndroid();
                    alllist.Add(new IdString()
                    {
                        Date = date,
                        Name = item.DescriptionD,
                        Id = studentid,
                    });
                }
                return alllist;
            }
            return alllist;
        }
        public Boolean Updatepresents(int id, int value)
        {
            using (_dbContext = new DatabaseContext())
            {
                var finddis = _dbContext.StudentGrades.FirstOrDefault(a => a.Id == id);
                if (finddis != null)
                {
                    if (value == 0)
                    {
                        finddis.Present = "a";
                    }
                    else
                    {
                        finddis.Present = "p";
                    }
                    _dbContext.SaveChanges();
                }
                return true;
            }
            return false;
        }
    }
}
