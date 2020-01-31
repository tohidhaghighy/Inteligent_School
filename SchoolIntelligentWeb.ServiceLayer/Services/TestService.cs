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
    public class TestService : ITestService
    {
        private DatabaseContext _dbContext = null;
        private IStudentService _student = new StudentService();
        public List<DomainClasses.MoreClasses.TestInfo> GetTests(int studentid)
        {
            List<TestInfo> AllTestInfo = new List<TestInfo>();
            List<DomainClasses.MoreClasses.IdString> Alllessons = new List<IdString>();
            using (_dbContext = new DatabaseContext())
            {
                Alllessons = _student.GetStudentClasslistreturnprovide(studentid);
                foreach (var lessons in Alllessons)
                {
                    var findTest = _dbContext.Tests.Where(a => a.LessonId == lessons.Id).OrderByDescending(a=>a.Id).ToList();
                    foreach (var test in findTest)
                    {
                        int questioncounter = _dbContext.Questions.Count(a => a.TestId == test.Id);
                        AllTestInfo.Add(new TestInfo()
                        {
                            Id = test.Id,
                            Lesson = test.Lesson.Name,
                            Teacher = test.Teacher.Name + " " + test.Teacher.Family,
                            School = "",
                            Duration = test.Duration,
                            MaxDelay = test.DelayTime,
                            Text = test.Description,
                            Type = test.TestType,
                            QuestionCount = questioncounter,
                            StartDate = test.StartTime.ToPersian(),
                            FinishDate = test.EndTime.ToPersian()
                        });
                    }
                }

                return AllTestInfo;
            }
        }

        public Boolean AddTests(int testtime, int delaytime, string description, DateTime startdate, DateTime finishdate, int provideid, int teacherid,int grade)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findprovide = _dbContext.Provided.FirstOrDefault(a => a.Id == provideid);
                if (findprovide!=null)
                {
                    _dbContext.Tests.Add(new Test() { DelayTime = delaytime, Description = description, Duration = testtime, StartTime = startdate, EndTime = finishdate, LessonId = provideid, TeacherId = teacherid, TestGrade = grade });
                    _dbContext.SaveChanges();
                    return true;
                }
                
            }
            return false;
        }


        public Boolean DeleteTest(int testid)
        {
            List<TestInfo> AllTestInfo = new List<TestInfo>();
            using (_dbContext = new DatabaseContext())
            {
                var findquestions = _dbContext.Questions.Where(a => a.TestId == testid).ToList();
                foreach (var item in findquestions)
                {
                    _dbContext.Questions.Remove(item);
                    var findsubquestions = _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == item.Id).ToList();
                    foreach (var items in findsubquestions)
                    {
                        _dbContext.MultipleChoiceQuestions.Remove(items);
                    }
                }
                var findgrade = _dbContext.TestGrades.Where(a => a.IdTest == testid).ToList();
                foreach (var itemsub in findgrade)
                {
                    _dbContext.TestGrades.Remove(itemsub);
                }
                try
                {
                    _dbContext.SaveChanges();
                    var findtest = _dbContext.Tests.FirstOrDefault(a => a.Id == testid);
                    _dbContext.Tests.Remove(findtest);
                    _dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    
                }
               
            }
            return false;
        }
        public List<DomainClasses.MoreClasses.TestInfo> GetclassTests(int provideid)
        {
            List<TestInfo> AllTestInfo = new List<TestInfo>();
            using (_dbContext = new DatabaseContext())
            {
                var findprovide = _dbContext.Provided.FirstOrDefault(a => a.Id == provideid);
                if (findprovide!=null)
                {
                    var findTest = _dbContext.Tests.Where(a => a.LessonId == provideid).OrderByDescending(a => a.Id).ToList();
                    int number = 1;
                    foreach (var test in findTest)
                    {
                        int questioncounter = _dbContext.Questions.Count(a => a.TestId == test.Id);
                        if (test.Teacher != null)
                        {
                            AllTestInfo.Add(new TestInfo()
                            {
                                Number = number,
                                Id = test.Id,
                                Lesson = test.Lesson.Name,
                                Teacher = test.Teacher.Name + " " + test.Teacher.Family,
                                School = "",
                                Duration = test.Duration,
                                MaxDelay = test.DelayTime,
                                Text = test.Description,
                                Type = test.TestType,
                                QuestionCount = questioncounter,
                                StartDate = test.StartTime.ToPersian(),
                                FinishDate = test.EndTime.ToPersian(),
                                ClassId = findprovide.ClassId.Value,
                            });
                            number++;
                        }

                    }
                }
                
                return AllTestInfo;
            }
        }


        public DomainClasses.MoreClasses.TestInfo Gettestinfo(int testid)
        {
            TestInfo TestInfo = new TestInfo();
            using (_dbContext = new DatabaseContext())
            {
                var findTest = _dbContext.Tests.FirstOrDefault(a => a.Id == testid);

                if (findTest.Teacher != null)
                {
                    TestInfo.Id = findTest.Id;
                    TestInfo.Lesson = findTest.Lesson.Name;
                    TestInfo.Teacher = findTest.Teacher.Name + " " + findTest.Teacher.Family;
                    TestInfo.School = "";
                    TestInfo.Duration = findTest.Duration;
                    TestInfo.MaxDelay = findTest.DelayTime;
                    TestInfo.Text = findTest.Description;
                    TestInfo.Type = findTest.TestType;
                    TestInfo.StartDate = findTest.StartTime.ToPersianDate().ToString().toPersianNumber();
                    TestInfo.FinishDate = findTest.EndTime.ToPersianDate().ToString().toPersianNumber();
                    TestInfo.TestGrade = findTest.TestGrade;
                }

            }
            return TestInfo;
        }

        public Boolean UpdateTests(int testtime, int delaytime, string description, DateTime startdate, DateTime finishdate,int grade,int testid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findtest = _dbContext.Tests.FirstOrDefault(a => a.Id == testid);
                if (findtest!=null)
                {
                    findtest.Description = description;
                    findtest.DelayTime = delaytime;
                    findtest.Duration = testtime;
                    findtest.EndTime = finishdate;
                    findtest.StartTime = startdate;
                    findtest.TestGrade = grade;
                    _dbContext.SaveChanges();
                }
                return true;
            }
            return false;
        }
    }
}
