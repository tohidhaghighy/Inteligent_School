using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.DomainClasses.ViewModels;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class TeacherService:ITeacherService
    {
        private DatabaseContext _dbContext = null;
        private IStudentService _student=new StudentService();
       // private ITeacherService _teacherservice = new TeacherService();


        public List<DomainClasses.MoreClasses.IdString> GetTeacherClasslist(int idteacher)
        {
            List<IdString> allclass = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findteacher = _dbContext.Provided.Where(a => a.TeacherId == idteacher).ToList();
                foreach (var teacher in findteacher)
                {
                    
                        IdString newteacher = new IdString()
                        {
                            Id = teacher.Id,
                            Name = teacher.Lesson.Name+" "+teacher.Class.Name,
                        };
                        newteacher.StudentCount = _dbContext.Students.Where(a => a.ClassId == teacher.ClassId).ToList().Count();
                        newteacher.ClassAvg = (GetAvgofclassnumbers(teacher.Id)).Name;
                        newteacher.TimeList = GetTimelist(teacher.Id);
                        allclass.Add(newteacher);
                    
                }
                return allclass;
            }

            return null;
        }

        

        public List<DomainClasses.MoreClasses.IdString> GetTeacherClassandlessonlist(int idteacher)
        {
            List<IdString> allclass = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findteacher = _dbContext.Provided.Where(a => a.TeacherId == idteacher).ToList();
                foreach (var teacher in findteacher)
                {
                    
                        IdString newteacher = new IdString()
                        {
                            Id = teacher.Id,
                            Name = teacher.Lesson.Name+" " + teacher.Class.Name,
                        };
                        newteacher.StudentCount = _dbContext.Students.Where(a => a.ClassId == teacher.ClassId).ToList().Count();
                        newteacher.ClassAvg = (GetAvgofclassnumbers(teacher.ClassId.Value)).Name;
                        newteacher.TimeList = GetTimelist(teacher.Id);
                        allclass.Add(newteacher);
                    
                }
                return allclass;
            }

            return null;
        }


        /// <summary>
        /// جستجو در تست مشکل داشت این تابع را تغییر دادیم
        /// </summary>
        /// <param name="idteacher"></param>
        /// <returns></returns>
        public List<DomainClasses.MoreClasses.IdString> lessonlistsearchtest(int idteacher)
        {
            List<IdString> allclass = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findteacher = _dbContext.Provided.Where(a => a.TeacherId == idteacher).ToList();
                foreach (var teacher in findteacher)
                {

                    IdString newteacher = new IdString()
                    {
                        Id = teacher.LessionId,
                        Name = teacher.Lesson.Name + " " + teacher.Class.Name,
                    };
                    allclass.Add(newteacher);

                }
                return allclass;
            }

            return null;
        }

        public List<DomainClasses.MoreClasses.IdString> GetTeacherClassandlessonlistreturnclass(int idteacher)
        {
            List<IdString> allclass = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findteacher = _dbContext.Provided.Where(a => a.TeacherId == idteacher).ToList();
                foreach (var teacher in findteacher)
                {
                    if (allclass.FirstOrDefault(a => a.Id == teacher.ClassId) == null)
                    {
                        IdString newteacher = new IdString()
                        {
                            Id = teacher.ClassId,
                            Name = teacher.Lesson.Name + " " + teacher.Class.Name,
                        };
                        newteacher.StudentCount = _dbContext.Students.Where(a => a.ClassId == teacher.ClassId).ToList().Count();
                        newteacher.ClassAvg = (GetAvgofclassnumbers(teacher.ClassId.Value)).Name;
                        newteacher.TimeList = GetTimelist(teacher.Id);
                        allclass.Add(newteacher);
                    }
                }
                return allclass;
            }

            return null;
        }
        public List<IdString> GetTimelist(int providedid)
        {
            List<IdString> alllist = new List<IdString>();
            _dbContext = new DatabaseContext();
                if (providedid!=null)
                {
                    var timeprovide = _dbContext.TimeProviders.Where(a => a.ProviderId == providedid).ToList();
                    foreach (var item in timeprovide)
                    {
                        IdString newtime = new IdString()
                        {
                            Id = item.Id,
                            Name = item.Time.Day+" زنگ "+item.Time.Bell,
                        };
                        alllist.Add(newtime);
                    }
                }
                return alllist;
        }

        public List<IdString> GetSearchStudentinClass(int providedid)
        {
            List<IdString> allclass = new List<IdString>();
            
            using (_dbContext = new DatabaseContext())
            {
                var findprovided = _dbContext.Provided.FirstOrDefault(a => a.Id == providedid);
                if (findprovided!=null)
                {
                    var findstudent = _dbContext.Students.Where(a => a.ClassId == findprovided.ClassId).ToList();
                    foreach (var student in findstudent)
                    {
                        IdString newteacher = new IdString()
                        {
                            Id = student.Id,
                            Name = student.Name + " " + student.Family,
                        };
                        allclass.Add(newteacher);
                    }

                    return allclass;
                }
                  
            }

            return null;
        }

        public List<IdString> GetAllStudentinClasswithprovideid(int provideid)
        {
            List<IdString> allclass = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findprovide = _dbContext.Provided.FirstOrDefault(a => a.Id == provideid);
                if (findprovide!=null)
                {
                    var findstudent = _dbContext.Students.Where(a => a.ClassId == findprovide.ClassId).ToList();
                    foreach (var student in findstudent)
                    {
                        IdString newteacher = new IdString()
                        {
                            Id = student.Id,
                            Name = student.Name + " " + student.Family,
                        };
                        allclass.Add(newteacher);
                    }

                    return allclass;
                }
                
            }

            return null;
        }
        public List<IdString> GetAllStudentinClasswithclassid(int classid)
        {
            List<IdString> allclass = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                    var findstudent = _dbContext.Students.Where(a => a.ClassId == classid).ToList();
                    foreach (var student in findstudent)
                    {
                        IdString newteacher = new IdString()
                        {
                            Id = student.Id,
                            Name = student.Name + " " + student.Family,
                        };
                        allclass.Add(newteacher);
                    }

                return allclass;
            }

            return null;
        }
        public List<IdString> GetAllStudentinClass(int teacherid)
        {
            List<IdString> allclass = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findteacherclass = _dbContext.Provided.FirstOrDefault(a => a.TeacherId == teacherid);
                if (findteacherclass!=null)
                {
                    var findstudent = _dbContext.Students.Where(a => a.ClassId == findteacherclass.ClassId).ToList();
                    foreach (var student in findstudent)
                    {
                        IdString newteacher = new IdString()
                        {
                            Id = student.Id,
                            Name = student.Name + " " + student.Family,
                        };
                        allclass.Add(newteacher);
                    }
                }
                
                return allclass;
            }

            return null;
        }

        public List<IdString> GetAllStudentinClassonlyclass(int classid)
        {
            List<IdString> allclass = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                    var findstudent = _dbContext.Students.Where(a => a.ClassId == classid).ToList();
                    foreach (var student in findstudent)
                    {
                        IdString newteacher = new IdString()
                        {
                            Id = student.Id,
                            Name = student.Name + " " + student.Family,
                        };
                        allclass.Add(newteacher);
                    }

                return allclass;
            }

            return null;
        }

        public List<GradePage> GetAllExistStudent(List<IdString> allstudent,int providedid,int type,int gradetype)
         {
            
            List<GradePage> allexists=new List<GradePage>();
            List<Grade> allexistGrades=new List<Grade>();
            int i = 0;
                foreach (var student in allstudent)
                {
                    GradePage newgrade = new GradePage();
                    if (student.Id != null) allexistGrades = _student.GetStudentGradeswithprovider(student.Id.Value, providedid, type, gradetype);
                    newgrade.Name = student.Name;
                    newgrade.AllGrades = allexistGrades;
                    allexists.Add(newgrade);
                    i++;
                }
                return allexists;

            return null;
        }

        public List<ListofTime> GetListofTime(int idteacher)
        {
            List<ListofTime> alltime=new List<ListofTime>();
            using (_dbContext = new DatabaseContext())
            {
                var providers = _dbContext.Provided.Where(a => a.TeacherId == idteacher).ToList();
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
            return alltime;
        }
        /// <summary>
        /// classid==provideid
        /// </summary>
        /// <param name="lessonid"></param>
        /// <param name="classid"></param>
        /// <returns></returns>
        public List<Listofgradepresentstudents> Listofgradestudent(int lessonid,int classid)
        {
            List<Listofgradepresentstudents> listofstudent=new List<Listofgradepresentstudents>();
            using (_dbContext=new DatabaseContext())
            {
                var findprovide=_dbContext.Provided.FirstOrDefault(a=>a.Id==classid);
                if (findprovide!=null)
                {
                    var findstudents = _dbContext.Students.Where(a => a.ClassId == findprovide.ClassId).ToList();
                    foreach (var student in findstudents)
                    {
                        Listofgradepresentstudents newlistofstudent = new Listofgradepresentstudents();
                        newlistofstudent.Id = student.Id;
                        newlistofstudent.Name = student.Name + " " + student.Family;
                        newlistofstudent.Image = student.Photo;
                        List<gradepresent> alllistpresent = new List<gradepresent>();
                        List<gradepresent> alllistgrade = new List<gradepresent>();
                        var findgradestudent = _dbContext.StudentGrades.Where(a => a.StudentId == student.Id && a.TimeProviders.ProviderId==classid).ToList();
                        foreach (var grade in findgradestudent)
                        {
                            if (!string.IsNullOrEmpty(grade.Present))
                            {
                                alllistpresent.Add(new gradepresent()
                                {
                                    Type = grade.Present,
                                    Date = grade.Date.ToPersianDateAndroid(),
                                    Id = grade.Id
                                });
                            }
                            if (!string.IsNullOrEmpty(grade.Grade))
                            {
                                alllistgrade.Add(new gradepresent()
                                {
                                    Value = grade.Grade,
                                    Date = grade.Date.ToPersianDateAndroid(),
                                    Id = grade.Id,
                                    NoeNomre = grade.GradeType.Type
                                });
                            }
                        }
                        newlistofstudent.Huzur = alllistpresent;
                        newlistofstudent.Nomarat = alllistgrade;
                        listofstudent.Add(newlistofstudent);
                    }
                    return listofstudent;
                }
                
            }
            return listofstudent;
        }
        /// <summary>
        /// classid==provideid
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public IdString GetAvgofclassnumbers(int classid)
        {

            IdString newgrade=new IdString();
            int count = 0;
            double avg = 0;
            _dbContext = new DatabaseContext();
            var findprovideid = _dbContext.Provided.FirstOrDefault(a => a.Id == classid);
            if (findprovideid!=null)
            {
                var findstudent = _dbContext.Students.Where(a => a.ClassId == findprovideid.ClassId).ToList();
                var findgrades = _dbContext.StudentGrades.Where(a => a.TimeProviders.ProviderId==classid && a.GradeType.Evalution!="کیفی" && a.Grade!="").ToList();
                if (findgrades.Any())
                {
                    foreach (var grade in findgrades)
                    {
                        if (grade.Grade != "")
                        {
                            if (grade.Grade.Length>=2)
                            {
                                grade.Grade = grade.Grade.Substring(0, 2);
                            
                            }
                            else if(grade.Grade.Trim()=="100")
                            {
                                grade.Grade = "100";
                            }
                            else
                            {
                                grade.Grade = grade.Grade;
                            }
                            double b = ((Convert.ToDouble(grade.Grade) * 20) / (Convert.ToDouble(grade.GradeType.Grade)));
                            double c = b * 5;
                            avg += (((Convert.ToDouble(grade.Grade) * 20) / (Convert.ToDouble(grade.GradeType.Grade))) * 5);
                            count++;
                        }
                    }
                    newgrade.Name = Convert.ToInt32(avg / count).ToString();
                   
                    newgrade.Id = findstudent.Count();
                    return newgrade;
                }
                else
                {
                    newgrade.Name = "0";
                    newgrade.Id = findstudent.Count();
                    return newgrade;
                }

              
            }
            return newgrade;
        }

        
        public List<IdString> GetTypeofgrade()
        {
            List<IdString> alllist=new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                var findtype = _dbContext.GradeTypes.ToList();
                foreach (var item in findtype)
                {
                    IdString newstring = new IdString()
                    {
                        Id=item.Id,
                        Name=item.Type,
                    };
                    alllist.Add(newstring);
                }
            }
            return alllist;
        }

        public Listofgradepresentstudents Listofgradeandabsent(int lessonid, int studentid)
        {
            Listofgradepresentstudents listofstudent = new Listofgradepresentstudents();
            using (_dbContext = new DatabaseContext())
            {
                var findstudents = _dbContext.Students.FirstOrDefault(a => a.Id == studentid);

                if (findstudents != null)
                {
                    listofstudent.Id = findstudents.Id;
                    listofstudent.Name = findstudents.Name + " " + findstudents.Family;
                    List<gradepresent> alllistpresent = new List<gradepresent>();
                    List<gradepresent> alllistgrade = new List<gradepresent>();
                    var findgradestudent = _dbContext.StudentGrades.Where(a => a.StudentId == findstudents.Id && a.TimeProviders.Provided.LessionId == lessonid).ToList();
                    foreach (var grade in findgradestudent)
                    {
                        if (!string.IsNullOrEmpty(grade.Present))
                        {
                            alllistpresent.Add(new gradepresent()
                            {
                                Type = grade.Present,
                                Date = grade.Date.ToPersian()
                                
                            });
                        }
                        if (!string.IsNullOrEmpty(grade.Grade))
                        {
                            if (grade.GradeType.Evalution=="کیفی")
                            {
                                alllistgrade.Add(new gradepresent()
                                {
                                    Value = grade.Grade,
                                    Date = grade.Date.ToPersian()
                                });
                            }
                           
                        }
                    }
                    listofstudent.Huzur = alllistpresent;
                    listofstudent.Nomarat = alllistgrade;
                    return listofstudent;
                }
            }       
                    
            return listofstudent;
        }
    
    }
}
