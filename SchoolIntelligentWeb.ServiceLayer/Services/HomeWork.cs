using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class HomeWork:IHomework
    {
        private DatabaseContext _dbContext = null;
        public List<FileInfo> GetHomeworks(int idlesson, int studentid)
        {
            List<FileInfo> allfile = new List<FileInfo>();
            using (_dbContext = new DatabaseContext())
            {
                try
                {

                    var findFiles = _dbContext.HomeWorks.Where(a => a.LessonId == idlesson && a.StudentId==studentid).OrderByDescending(a => a.Id).ToList();
                    foreach (var item in findFiles)
                    {
                        FileInfo newfile = new FileInfo()
                        {
                            Id = item.Id,
                            Date = item.HomeworkDate.ToPersian(),
                            Url = item.Photo,
                        };
                        allfile.Add(newfile);
                    }
                    return allfile;
                }
                catch (Exception ex)
                {
                    return allfile;
                }

            }
            return allfile;
        }

        public Boolean AddHomework(string url,int lessonid,int studentid)
        {
            using (_dbContext = new DatabaseContext())
            {
                SchoolIntelligentWeb.DomainClasses.HomeWork newhomework = new SchoolIntelligentWeb.DomainClasses.HomeWork()
                {
                    HomeworkDate = DateTime.Now,
                    Photo = url,
                    LessonId = lessonid,
                    StudentId = studentid
                };
                _dbContext.HomeWorks.Add(newhomework);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean DeleteHomework(int id)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findhomework = _dbContext.HomeWorks.FirstOrDefault(a => a.Id == id);
                _dbContext.HomeWorks.Remove(findhomework);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }


        public List<FileInfo> GetTeacherHomeworkswithprovide(int provideid, DateTime news)
        {
            List<FileInfo> allfile = new List<FileInfo>();
            using (_dbContext = new DatabaseContext())
            {
                try
                {
                    var findprovide = _dbContext.Provided.FirstOrDefault(a => a.Id == provideid);
                    if (findprovide!=null)
                    {
                        DateTime finish = news.AddHours(24);
                        DateTime start = news.AddHours(-24);
                        var findFiles = _dbContext.HomeWorks.Where(a => a.LessonId == findprovide.LessionId && a.HomeworkDate <= finish && a.HomeworkDate >= start).OrderByDescending(a => a.Id).ToList();
                        foreach (var item in findFiles)
                        {
                            if (item.StudentGrade != null)
                            {
                                FileInfo newfile = new FileInfo()
                                {
                                    Id = item.Id,
                                    Date = item.HomeworkDate.ToPersian(),
                                    Url = item.Photo,
                                    StudentId = item.StudentId.Value,
                                    LessonId = item.LessonId.Value,
                                    StudentName = item.Student.Name + " " + item.Student.Family,
                                    Grade = item.StudentGrade.Grade
                                };
                                allfile.Add(newfile);
                            }
                            else
                            {
                                FileInfo newfile = new FileInfo()
                                {
                                    Id = item.Id,
                                    Date = item.HomeworkDate.ToPersian(),
                                    Url = item.Photo,
                                    StudentId = item.StudentId.Value,
                                    LessonId = item.LessonId.Value,
                                    StudentName = item.Student.Name + " " + item.Student.Family,
                                    Grade = ""
                                };
                                allfile.Add(newfile);
                            }
                        }
                    }
                   
                    return allfile;
                }
                catch (Exception ex)
                {
                    return allfile;
                }

            }
            return allfile;
        }


        public List<FileInfo> GetTeacherHomeworks(int idlesson,DateTime news)
        {
            List<FileInfo> allfile = new List<FileInfo>();
            using (_dbContext = new DatabaseContext())
            {
                try
                {
                    DateTime finish=news.AddHours(24);
                    DateTime start=news.AddHours(-24);
                    var findFiles = _dbContext.HomeWorks.Where(a => a.LessonId == idlesson && a.HomeworkDate <=finish  && a.HomeworkDate >=start ).OrderByDescending(a => a.Id).ToList();
                    foreach (var item in findFiles)
                    {
                        if (item.StudentGrade != null)
                        {
                            FileInfo newfile = new FileInfo()
                            {
                                Id = item.Id,
                                Date = item.HomeworkDate.ToPersian(),
                                Url = item.Photo,
                                StudentId = item.StudentId.Value,
                                LessonId = item.LessonId.Value,
                                StudentName = item.Student.Name+ " "+item.Student.Family,
                                Grade = item.StudentGrade.Grade
                            };
                            allfile.Add(newfile);
                        }
                        else
                        {
                            FileInfo newfile = new FileInfo()
                            {
                                Id = item.Id,
                                Date = item.HomeworkDate.ToPersian(),
                                Url = item.Photo,
                                StudentId = item.StudentId.Value,
                                LessonId = item.LessonId.Value,
                                StudentName = item.Student.Name + " " + item.Student.Family,
                                Grade = ""
                            };
                            allfile.Add(newfile);
                        }
                    }
                    return allfile;
                }
                catch (Exception ex)
                {
                    return allfile;
                }

            }
            return allfile;
        }

        public Boolean AddGrade(int studentid, int lessonid, string grade, int teacherid, int homeworkid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findhomework = _dbContext.HomeWorks.FirstOrDefault(a => a.Id == homeworkid);
                if (findhomework.GradeId != null)
                {
                    var findgrade = _dbContext.StudentGrades.FirstOrDefault(a => a.Id == findhomework.GradeId);
                    findgrade.Grade = grade;
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    var findclassid = _dbContext.Students.FirstOrDefault(a => a.Id == studentid);
                    if (findclassid != null)
                    {

                        var findprovide =
                            _dbContext.Provided.FirstOrDefault(
                                a => a.LessionId == lessonid && a.TeacherId == teacherid && a.ClassId == findclassid.ClassId);
                        var findtimeprovider = _dbContext.TimeProviders.FirstOrDefault(a => a.ProviderId == findprovide.Id);
                        if (findtimeprovider != null)
                        {
                            StudentGrade newgrade = new StudentGrade()
                            {
                                Date = DateTime.Now,
                                Grade = grade,
                                LessonId = lessonid,
                                StudentId = studentid,
                                TypeGrade = 3,
                                TimeProviderId = findtimeprovider.Id
                            };
                            _dbContext.StudentGrades.Add(newgrade);
                            _dbContext.SaveChanges();
                            findhomework.GradeId = newgrade.Id;
                            _dbContext.SaveChanges();
                            return true;
                        }

                    }
                }
                
                
            }
            return false;
        }
    }
}
