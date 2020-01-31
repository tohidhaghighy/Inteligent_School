using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class TestAnswerService : ITestAnswerService
    {
        private DatabaseContext _dbContext = null;
        public Boolean InsertAnsert(int? answerkey, string answertext, int studentid, int testid, int questionid, string photourl)
        {
            using (_dbContext = new DatabaseContext())
            {
                if (answerkey == 0)
                {
                    answerkey = null;
                }
                var findtest = _dbContext.Tests.FirstOrDefault(a => a.Id == testid);
                if (findtest != null)
                {
                    if (DateTime.Now.AddMinutes(findtest.DelayTime) >= findtest.StartTime &&
                   DateTime.Now.AddMinutes(findtest.DelayTime) <= findtest.EndTime)
                    {
                        //پیدا کردن جواب
                        var findbeforeanswer = _dbContext.TestAnswers.FirstOrDefault(a => a.QuestionId == questionid && a.StudentId == studentid && a.TestId == testid);
                        if (findbeforeanswer != null)
                        {
                            findbeforeanswer.AnswerKey = answerkey;
                            findbeforeanswer.AnswerText = answertext;
                            findbeforeanswer.AnswerPhoto = photourl;
                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            findbeforeanswer = new TestAnswer()
                            {
                                AnswerKey = answerkey,
                                AnswerPhoto = photourl,
                                AnswerText = answertext,
                                QuestionId = questionid,
                                StudentId = studentid,
                                TestId = testid,
                            };
                            _dbContext.TestAnswers.Add(findbeforeanswer);
                            _dbContext.SaveChanges();
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean InsertGrade(string grade, int testanswerid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findtestgrade =
                    _dbContext.TestAnswers.FirstOrDefault(a => a.Id == testanswerid);
                if (findtestgrade != null)
                {
                    double s;
                    if (double.TryParse(grade, out s))
                    {
                        findtestgrade.TestGrades = grade;
                        _dbContext.SaveChanges();
                        return true;
                    }
                }

            }
            return false;
        }

        public Boolean TestAnserUpdategrade(int testid, int studentid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findanswers =
                    _dbContext.TestAnswers.Where(a => a.TestId == testid && a.StudentId == studentid).ToList();
                if (findanswers != null)
                {
                    double fullgrade = 0;
                    foreach (var answers in findanswers)
                    {
                        if (answers.TestGrades != "")
                        {
                            fullgrade += double.Parse(answers.TestGrades);
                        }
                    }
                    var findgradestudentid =
                        _dbContext.TestGrades.FirstOrDefault(a => a.IdTest == testid && a.IdStudent == studentid);
                    if (findgradestudentid != null)
                    {
                        var findgradestudent =
                            _dbContext.StudentGrades.FirstOrDefault(a => a.Id == findgradestudentid.IdGrade);
                        findgradestudent.Grade = fullgrade.ToString();
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean AddGradetoAllStudent(int testid, List<IdString> allstudent, List<AnswerItems> allQuestion)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findlessonid = _dbContext.Tests.FirstOrDefault(a => a.Id == testid);
                if (allstudent != null)
                {
                    foreach (var student in allstudent)
                    {
                        float MaxGrade = 0;
                        StudentAnswerTest newanswer = new StudentAnswerTest();
                        List<ListAnswerInfo> allansweritem = new List<ListAnswerInfo>();
                        foreach (var question in allQuestion)
                        {
                            var findanswer = _dbContext.TestAnswers
                                .FirstOrDefault(
                                    a =>
                                        a.TestId == testid && a.StudentId == student.Id &&
                                        a.QuestionId == question.QuestionId);

                            if (findanswer != null)
                            {
                                if (question.CorrectAnswer == findanswer.AnswerKey)
                                {
                                    if (string.IsNullOrEmpty(findanswer.TestGrades))
                                    {
                                        findanswer.TestGrades = question.Grade;
                                        _dbContext.SaveChanges();
                                    }

                                }
                                if (findanswer.TestGrades != null)
                                {
                                    MaxGrade += float.Parse(findanswer.TestGrades);
                                }
                                else
                                {
                                    MaxGrade += float.Parse(question.Grade);
                                }

                            }
                        }
                        //MaxGrade.ToString(),student.Id
                        if (findlessonid != null)
                        {
                            var findtest = _dbContext.TestGrades.Where(a => a.IdStudent == student.Id && a.IdTest == testid).FirstOrDefault();
                            if (findtest!=null)
                            {
                                var findstudentgradewithtest = _dbContext.StudentGrades.FirstOrDefault(a => a.Id == findtest.IdGrade);
                                if (findstudentgradewithtest!=null)
                                {
                                    findstudentgradewithtest.Grade = MaxGrade.ToString();
                                    _dbContext.SaveChanges();
                                }
                            }
                            else if (findtest==null)
                            {
                                StudentGrade studentinsertgrade = new StudentGrade()
                                {
                                    Date = DateTime.Now,
                                    Grade = MaxGrade.ToString(),
                                    TypeGrade = 2,
                                    StudentId = student.Id,
                                    LessonId = findlessonid.LessonId
                                };
                                _dbContext.StudentGrades.Add(studentinsertgrade);
                                _dbContext.SaveChanges();
                                _dbContext.TestGrades.Add(new TestGrade()
                                {
                                    IdGrade = studentinsertgrade.Id,
                                    IdStudent = student.Id,
                                    IdTest = testid
                                });
                                _dbContext.SaveChanges();
                            }
                           
                        }

                    }
                }
                return true;
            }
            return false;
        }

        public string ReturnGradeforstudent(int studentid, int testid)
        {
            string result = "فعلا نمره ثبت نشده است";
            using (_dbContext = new DatabaseContext())
            {
                var findgrade = _dbContext.TestGrades.FirstOrDefault(a => a.IdStudent == studentid && a.IdTest == testid);
                if (findgrade!=null)
                {
                    var findgradeinlist = _dbContext.StudentGrades.FirstOrDefault(a => a.Id == findgrade.Id);
                    if (findgradeinlist != null) return findgradeinlist.Grade;
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return result;
                }
            }
            return result;
        }
        

    }
}
