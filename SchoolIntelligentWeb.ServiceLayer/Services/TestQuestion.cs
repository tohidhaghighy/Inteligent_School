using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SchoolIntelligentWeb.DataLayer;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.DomainClasses.ViewModels;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;

namespace SchoolIntelligentWeb.ServiceLayer.Services
{
    public class TestQuestion : ITestQuestion
    {
        private DatabaseContext _dbContext = null;
        private ITestAnswerService _testAnswer = new TestAnswerService();
        public List<DomainClasses.MoreClasses.TestQuestions> GetTests(int studentid, int testid)
        {
            List<TestQuestions> Allquestions = new List<TestQuestions>();
            using (_dbContext = new DatabaseContext())
            {
                if (_dbContext.Students.Find(studentid) != null)
                {
                    var FindTest = _dbContext.Tests.Find(testid);
                    if (FindTest != null)
                    {
                        if (FindTest.StartTime <= DateTime.Now)
                        {

                            var Findquestions = _dbContext.Questions.Where(a => a.TestId == FindTest.Id).ToList();
                            foreach (var question in Findquestions)
                            {
                                List<Multiply_ChoiseItems> allchoises = new List<Multiply_ChoiseItems>();
                                var FindMultiply_Choisetest =
                                    _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == question.Id).ToList();
                                int count = 1;
                                foreach (var choises in FindMultiply_Choisetest)
                                {
                                    allchoises.Add(new Multiply_ChoiseItems() { Id = choises.Id, Text = choises.Text, Count = count });
                                    count++;
                                }
                                string Answertext = "";
                                string Answerphoto = "";
                                var findquestionanswer =
                                    _dbContext.TestAnswers.FirstOrDefault(
                                        a =>
                                            a.QuestionId == question.Id && a.StudentId == studentid &&
                                            a.TestId == testid);
                                if (findquestionanswer != null)
                                {
                                    if (findquestionanswer.AnswerKey != null)
                                    {
                                        Answertext = findquestionanswer.AnswerKey.ToString();
                                    }
                                    else
                                    {
                                        Answertext = findquestionanswer.AnswerText;
                                    }
                                    Answerphoto = findquestionanswer.AnswerPhoto;
                                }
                                Allquestions.Add(new TestQuestions()
                                {
                                    Type = question.QuestionType,
                                    QuestionAnswerText = Answertext,
                                    QuestionAnswerPhoto = Answerphoto,
                                    Id = question.Id,
                                    ImageUrl = question.QuestionPicture,
                                    Question = question.QuestionText,
                                    Choises = allchoises,
                                    Grade = question.QuestionGrade,
                                });
                            }

                        }
                        else
                        {
                            return Allquestions;
                        }
                    }
                }
            }

            return Allquestions;
        }

        public List<DomainClasses.MoreClasses.TestQuestions> GetListofQuestion(int testid)
        {
            int count = 1;
            List<TestQuestions> Allquestions = new List<TestQuestions>();
            using (_dbContext = new DatabaseContext())
            {
                var FindTest = _dbContext.Tests.FirstOrDefault(a => a.Id == testid);
                if (FindTest != null)
                {
                    var Findquestions = _dbContext.Questions.Where(a => a.TestId == FindTest.Id).ToList();
                    foreach (var question in Findquestions)
                    {
                        Allquestions.Add(new TestQuestions()
                        {
                            TestId = question.TestId.Value,
                            QuestionCount = count,
                            Type = question.QuestionType,
                            Id = question.Id,
                            Question = question.QuestionText,
                            Grade = question.QuestionGrade,

                        });
                        count++;
                    }

                }
            }
            return Allquestions;
        }

        public Boolean DeleteQuestion(int questionid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var FindQuestions = _dbContext.Questions.FirstOrDefault(a => a.Id == questionid);
                if (FindQuestions != null)
                {
                    var findmultiply =
                        _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == FindQuestions.Id).ToList();
                    foreach (var item in findmultiply)
                    {
                        _dbContext.MultipleChoiceQuestions.Remove(item);
                    }
                    try
                    {
                        _dbContext.Questions.Remove(FindQuestions);
                        _dbContext.SaveChanges();
                        return true;
                    }
                    catch 
                    {
                        
                    }
                }
            }
            return false;
        }

        public DomainClasses.MoreClasses.TestQuestions GetQuesrionInfo(int questionid)
        {
            TestQuestions questions = new TestQuestions();
            List<Multiply_ChoiseItems> allchoises = new List<Multiply_ChoiseItems>();
            using (_dbContext = new DatabaseContext())
            {
                var Findquestion = _dbContext.Questions.FirstOrDefault(a => a.Id == questionid);
                if (Findquestion.QuestionType == 0)
                {
                    int count = 1;
                    var FindMultiply_Choisetest = _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == questionid).ToList();
                    foreach (var choises in FindMultiply_Choisetest)
                    {

                        allchoises.Add(new Multiply_ChoiseItems() { Id = choises.Id, Text = choises.Text, Count = count, QuestionId = choises.QuestionId.Value });
                        count++;
                    }
                }
                if (Findquestion.TestId != null) questions.TestId = Findquestion.TestId.Value;
                questions.Type = Findquestion.QuestionType;
                questions.QuestionAnswerText = Findquestion.AnswerDescription;
                questions.QuestionAnswerPhoto = Findquestion.AnswerPicture;
                questions.Id = Findquestion.Id;
                questions.ImageUrl = Findquestion.QuestionPicture;
                questions.Question = Findquestion.QuestionText;
                questions.Choises = allchoises;
                questions.Grade = Findquestion.QuestionGrade;
                if (Findquestion.CorrectAnswerId != null)
                {
                    questions.AnswerKey = Findquestion.CorrectAnswerId.Value;
                }
                else
                {
                    questions.AnswerKey = 0;
                }
            }
            return questions;
        }


        public int AddQuestion(string questiontext, int answerKey, string answertext, string questionimage, string answerimage, int testid, int questiontype, int questiongrade)
        {
            using (_dbContext = new DatabaseContext())
            {
                if (questionimage != "")
                {
                    questionimage = "http://sheydairan.mahamsys.com/Upload/" + questionimage;
                }
                if (answerimage != "")
                {
                    answerimage = "http://sheydairan.mahamsys.com/Upload/" + answerimage;
                }
                Questions newquestion = new Questions()
                {
                    AnswerDescription = answertext,
                    CorrectAnswerId = answerKey,
                    AnswerPicture = answerimage,
                    QuestionGrade = questiongrade,
                    QuestionPicture = questionimage,
                    QuestionText = questiontext,
                    QuestionType = questiontype,
                    TestId = testid,
                };
                _dbContext.Questions.Add(newquestion);
                _dbContext.SaveChanges();
                return newquestion.Id;
            }
            return 0;
        }


        public int UpdateQuestion(string questiontext, int answerKey, string answertext, string questionimage, string answerimage, int questionid, int questiongrade)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findquestion = _dbContext.Questions.FirstOrDefault(a => a.Id == questionid);
                if (findquestion != null)
                {
                    findquestion.AnswerDescription = answertext;
                    findquestion.CorrectAnswerId = answerKey;
                    if (answerimage != "")
                    {
                        findquestion.AnswerPicture = answerimage;
                    }
                    findquestion.QuestionGrade = questiongrade;
                    if (questionimage != "")
                    {
                        findquestion.QuestionPicture = questionimage;
                    }
                    findquestion.QuestionText = questiontext;
                }
                _dbContext.SaveChanges();
                return findquestion.Id;
            }
            return 0;
        }


        public List<Multiply_ChoiseItems> DeleteMultichoise(int questionid, int idmultichoise)
        {
            List<Multiply_ChoiseItems> allchoises = new List<Multiply_ChoiseItems>();
            using (_dbContext = new DatabaseContext())
            {
                var findmultichoise = _dbContext.MultipleChoiceQuestions.FirstOrDefault(a => a.Id == idmultichoise);
                if (findmultichoise != null)
                {
                    _dbContext.MultipleChoiceQuestions.Remove(findmultichoise);
                    _dbContext.SaveChanges();
                }
                int count = 1;
                var FindMultiply_Choisetest = _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == questionid).ToList();
                foreach (var choises in FindMultiply_Choisetest)
                {

                    allchoises.Add(new Multiply_ChoiseItems() { Id = choises.Id, Text = choises.Text, Count = count, QuestionId = choises.QuestionId.Value });
                    count++;
                }
            }
            return allchoises;
        }



        public List<Multiply_ChoiseItems> AddMultichoise(string text, int questionid)
        {
            List<Multiply_ChoiseItems> allchoises = new List<Multiply_ChoiseItems>();
            using (_dbContext = new DatabaseContext())
            {
                if (text != "")
                {
                    Multiple_choice_Questions newmuliti = new Multiple_choice_Questions()
                    {
                        QuestionId = questionid,
                        Text = text,
                    };
                    _dbContext.MultipleChoiceQuestions.Add(newmuliti);
                    _dbContext.SaveChanges();

                }
                int count = 1;
                var FindMultiply_Choisetest = _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == questionid).ToList();
                foreach (var choises in FindMultiply_Choisetest)
                {

                    allchoises.Add(new Multiply_ChoiseItems() { Id = choises.Id, Text = choises.Text, Count = count, QuestionId = choises.QuestionId.Value });
                    count++;
                }
            }
            return allchoises;
        }


        public int FindTypeofquestion(int questionid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findquestion = _dbContext.Questions.FirstOrDefault(a => a.Id == questionid);
                if (findquestion != null)
                {
                    return findquestion.QuestionType;
                }
            }
            return 2;
        }

        public List<DomainClasses.MoreClasses.AnswerItems> GetAllQuestionTest(int testid)
        {
            List<DomainClasses.MoreClasses.AnswerItems> Allquestions = new List<DomainClasses.MoreClasses.AnswerItems>();
            List<Multiply_ChoiseItems> allchoises = new List<Multiply_ChoiseItems>();
            using (_dbContext = new DatabaseContext())
            {
                var findtest = _dbContext.Questions.Where(a => a.TestId == testid).ToList();
                if (findtest.Any())
                {
                    foreach (var item in findtest)
                    {
                        //تستی

                        if (item.QuestionType == 0)
                        {
                            int count = 1;
                            string multiplyitem = "";
                            var FindMultiply_Choisetest = _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == item.Id).ToList();
                            foreach (var choises in FindMultiply_Choisetest)
                            {
                                multiplyitem += "-" + choises.Text + "<br/>";
                            }
                            //اگر سوال ها را برگردانیم Questionid را پر میکنیم
                            //در غیر اینصورت Id را
                            AnswerItems newanswer = new AnswerItems()
                            {
                                QuestionId = item.Id,
                                QuestionImage = item.QuestionPicture,
                                QuestionAnswer = item.AnswerDescription,
                                QuestionAnswerImage = item.AnswerPicture,
                                Allchoise = multiplyitem,
                                QuestionType = item.QuestionType,
                                Questiontext = item.QuestionText,
                                CorrectAnswer = item.CorrectAnswerId.Value,
                                Grade = item.QuestionGrade.ToString()
                            };
                            Allquestions.Add(newanswer);
                        }
                        else if (item.QuestionType == 1)
                        {
                            AnswerItems newanswer = new AnswerItems()
                            {
                                Id = item.Id,
                                QuestionImage = item.QuestionPicture,
                                QuestionAnswer = item.AnswerDescription,
                                QuestionAnswerImage = item.AnswerPicture,
                                QuestionType = item.QuestionType,
                                Questiontext = item.QuestionText,
                                QuestionId = item.Id,
                                Grade = item.QuestionGrade.ToString()
                            };
                            Allquestions.Add(newanswer);
                        }

                    }
                    return Allquestions;
                }
            }
            return Allquestions;
        }

        public List<StudentAnswerTest> GetAlllistofAnswerQuestion(int classid, int testid, List<IdString> Allstudent, List<AnswerItems> AllQuestion, int teacherid)
        {
            List<StudentAnswerTest> allanswer = new List<StudentAnswerTest>();
            IdString allgrade = new IdString();
            using (_dbContext = new DatabaseContext())
            {
                if (Allstudent != null)
                {
                    foreach (var student in Allstudent)
                    {
                        float MaxGrade = 0;
                        StudentAnswerTest newanswer = new StudentAnswerTest();
                        newanswer.Idstudent = student.Id.Value;
                        newanswer.Idclass = classid;
                        newanswer.Idtest = testid;
                        newanswer.Name = student.Name;
                        List<ListAnswerInfo> allansweritem = new List<ListAnswerInfo>();
                        foreach (var question in AllQuestion)
                        {
                            var findanswer = _dbContext.TestAnswers
                                    .FirstOrDefault(a => a.TestId == testid && a.StudentId == student.Id && a.QuestionId == question.QuestionId);

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

                                if (question.CorrectAnswer == findanswer.AnswerKey)
                                {
                                    allansweritem.Add(new ListAnswerInfo()
                                    {
                                        QuestionType = question.QuestionType,
                                        AnswerImage = findanswer.AnswerPhoto,
                                        AnswerText = findanswer.AnswerText,
                                        Idquestion = question.QuestionId,
                                        Point = question.Grade,
                                        IsAnswered = true,
                                        QuestionGrade = findanswer.TestGrades,
                                        QuestionAnswer = findanswer.Id
                                    });

                                }
                                else
                                {
                                    allansweritem.Add(new ListAnswerInfo()
                                    {

                                        QuestionType = question.QuestionType,
                                        AnswerImage = findanswer.AnswerPhoto,
                                        AnswerText = findanswer.AnswerText,
                                        Idquestion = question.QuestionId,
                                        Point = question.Grade,
                                        IsAnswered = true,
                                        QuestionGrade = findanswer.TestGrades,
                                        QuestionAnswer = findanswer.Id
                                    });

                                }

                            }
                            else
                            {
                                allansweritem.Add(new ListAnswerInfo()
                                {
                                    QuestionType = question.QuestionType,
                                    AnswerImage = "",
                                    AnswerText = "",
                                    Idquestion = question.QuestionId,
                                    Point = question.Grade,
                                    IsAnswered = false,
                                    QuestionAnswer = 0
                                });
                            }
                        }
                        allgrade = new IdString() { Name = MaxGrade.ToString(), Id = student.Id };
                        newanswer.ListAnswerInfos = allansweritem;
                        newanswer.ListGrade = allgrade;
                        allanswer.Add(newanswer);

                    }

                    return allanswer;
                }
            }
            return allanswer;
        }

        public Boolean CopyPaste(int copyquestionid, int pastequestionid)
        {
            using (_dbContext = new DatabaseContext())
            {
                var findquestions = _dbContext.Questions.Where(a => a.TestId == copyquestionid).ToList();
                foreach (var questions in findquestions)
                {
                    if (questions.QuestionType == 0)
                    {
                        //تستی
                        Questions newquestion = new Questions()
                        {
                            QuestionType = questions.QuestionType,
                            TestId = pastequestionid,
                            AnswerDescription = questions.AnswerDescription,
                            AnswerPicture = questions.AnswerPicture,
                            CorrectAnswerId = questions.CorrectAnswerId,
                            QuestionGrade = questions.QuestionGrade,
                            QuestionPicture = questions.QuestionPicture,
                            QuestionText = questions.QuestionText
                        };
                        _dbContext.Questions.Add(newquestion);
                        _dbContext.SaveChanges();
                        var findmultichoise =
                            _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == questions.Id).ToList();
                        foreach (var multichoise in findmultichoise)
                        {
                            _dbContext.MultipleChoiceQuestions.Add(new Multiple_choice_Questions()
                            {
                                QuestionId = newquestion.Id,
                                Text = multichoise.Text,
                            });

                        }
                        _dbContext.SaveChanges();

                    }
                    else if (questions.QuestionType == 1)
                    {
                        //تشریحی
                        _dbContext.Questions.Add(new Questions()
                        {
                            QuestionType = questions.QuestionType,
                            TestId = pastequestionid,
                            AnswerDescription = questions.AnswerDescription,
                            AnswerPicture = questions.AnswerPicture,
                            CorrectAnswerId = questions.CorrectAnswerId,
                            QuestionGrade = questions.QuestionGrade,
                            QuestionPicture = questions.QuestionPicture,
                            QuestionText = questions.QuestionText
                        });
                        _dbContext.SaveChanges();

                    }
                }
                return true;
            }
            return false;
        }

        public List<DomainClasses.MoreClasses.Report> GetTestQuestionsforreport(int testid)
        {
            List<Report> Allreports = new List<Report>();
            using (_dbContext = new DatabaseContext())
            {
                int number = 1;
                var Findquestions = _dbContext.Questions.Where(a => a.TestId == testid).ToList();
                foreach (var question in Findquestions)
                {
                    string answeritems = "";
                    List<Multiply_ChoiseItems> allchoises = new List<Multiply_ChoiseItems>();
                    var FindMultiply_Choisetest =
                        _dbContext.MultipleChoiceQuestions.Where(a => a.QuestionId == question.Id).ToList();
                    int count = 1;
                    foreach (var choises in FindMultiply_Choisetest)
                    {
                        answeritems += (choises.Text)+"\n";
                        count++;
                    }
                    
                        Allreports.Add(new Report()
                        {
                            Id = number,
                            Answerkey = answeritems,
                            questiontext = question.QuestionText,
                            questionimage = question.QuestionPicture,
                        });
                   
                    number++;
                }
            }

            return Allreports;
        }
    }
}
