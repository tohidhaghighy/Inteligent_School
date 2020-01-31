using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.DomainClasses.ViewModels;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;
using SchoolIntelligentWeb.Utilities;
using SchoolIntelligentWeb.Utilities.Filters;

namespace SchoolIntelligentWeb.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        private IFilesService _file = new FilesService();
        private ITeacherService _teacher = new TeacherService();
        List<IdString> lessons = new List<IdString>();
        List<GradePage> Gradelist = new List<GradePage>();
        List<IdString> Allstudent = new List<IdString>();
        List<IdString> Alltypegrade = new List<IdString>();
        private IStudentService _student = new StudentService();
        private ITestService _TestService = new TestService();
        private ITestQuestion _TestQuestionService = new TestQuestion();
        PersianCalendar pc = new PersianCalendar();
        private ITestAnswerService _testAnswer = new TestAnswerService();
        private IHomework _homework = new HomeWork();
        private IOnlineClassService _onlineclass = new OnlineClassService();
        [CheckCookie]
        public ActionResult Index()
        {
            List<ListofTime> alltime = new List<ListofTime>();
            Schadulerlist allschaduler = new Schadulerlist();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                alltime = _teacher.GetListofTime(id);
                allschaduler = _student.GetSchaduler(alltime);
            }
            return View("Main", allschaduler);
        }
        [CheckCookie]
        public ActionResult Grade()
        {
            List<GradePage> Gradelist = new List<GradePage>();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                lessons = _teacher.GetTeacherClassandlessonlist(id);
                Allstudent = _teacher.GetAllStudentinClass(id);
                Alltypegrade = _student.gettypeGrade();
                if (Allstudent.Count > 0)
                {
                    var providedid = lessons.FirstOrDefault();
                    var typegrade = Alltypegrade.FirstOrDefault();
                    if (typegrade != null)
                    {
                        if (providedid != null)
                            Gradelist = _teacher.GetAllExistStudent(Allstudent, providedid.Id.Value, 0, typegrade.Id.Value);
                        
                        if (lessons.Any() && Gradelist.Any())
                        {
                            Gradelist[0].Lessons = lessons;
                            Gradelist[0].Typegrade = Alltypegrade;
                        }
                            
                        
                        
                    }
                    return View("Grade", Gradelist);
                }
                return View("Grade", Gradelist);
            }
            return View("Grade", Gradelist);
        }
        [CheckCookie]
        public ActionResult Exist()
        {

            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                lessons = _teacher.GetTeacherClassandlessonlist(id);
                Allstudent = _teacher.GetAllStudentinClass(id);
                if (Allstudent.Count > 0)
                {
                    var providedid = lessons.FirstOrDefault();
                    if (providedid != null)
                        Gradelist = _teacher.GetAllExistStudent(Allstudent, providedid.Id.Value, 1, 0);
                    Gradelist[0].Lessons = lessons;

                }

            }
            return View("Exist", Gradelist);
        }
        [CheckCookie]
        public ActionResult Test()
        {
            TestViewModel newtest = new TestViewModel();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                newtest.Lessons = _teacher.lessonlistsearchtest(id);
                 if (newtest.Lessons != null && newtest.Lessons.Count()>0)
                {
                    newtest.Tests = _TestService.GetclassTests(newtest.Lessons.FirstOrDefault().Id.Value);
                }
            }
            return View("Test", newtest);
        }
        [CheckCookie]
        public ActionResult OnlineClassFile(int classid)
        {
            TestViewModel newtest = new TestViewModel();
            if (Request.Cookies["id"] != null)
            {
                Filepage filelist = new Filepage();
                filelist.Id = classid;
                filelist.Onlineclass = _onlineclass.allfiles(classid);
                return View("OnlineClassfiles", filelist);
            }
            return View("OnlineClassfiles", newtest);
        }
        [CheckCookie]
        public ActionResult Questions(int testid)
        {
            QuestionViewModel newquestions = new QuestionViewModel();
            List<TestQuestions> Allquestions = new List<TestQuestions>();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                newquestions.AllQuestions = _TestQuestionService.GetListofQuestion(testid);
                newquestions.TestInfo = _TestService.Gettestinfo(testid);
            }
            return View("Questions", newquestions);
        }
        [CheckCookie]
        public ActionResult Files()
        {
            Filepage filelist = new Filepage();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                filelist.Lessons = _teacher.GetTeacherClassandlessonlist(id);
               
                var firstOrDefault = filelist.Lessons.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    int providedid = Convert.ToInt32(firstOrDefault.Id);
                    filelist.Students = _teacher.GetAllStudentinClasswithprovideid(providedid);
                    filelist.Typegrade = _file.GetFileVideo();
                    var orDefault = filelist.Typegrade.FirstOrDefault();
                    if (orDefault != null)
                    {
                        int filetypeid = Convert.ToInt32(orDefault.Id);
                        filelist.AllFiles = _file.GetChosesFileProvide(providedid, filetypeid);
                    }

                }
            }
            return View("Files", filelist);
        }
        [CheckCookie]
        public ActionResult OnlineClass()
        {
            Onlineclassall allinfo = new Onlineclassall();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                allinfo.allonlineclass=_onlineclass.ListClass(id);
                allinfo.allinfo = _onlineclass.allinfo(id);
                return View("OnlineClass", allinfo);
            }
            return View("OnlineClass",null);
        }
        [CheckCookie]
        public ActionResult Homework()
        {
            Filepage filelist = new Filepage();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                filelist.Lessons = _teacher.GetTeacherClasslist(id);
                var firstOrDefault = filelist.Lessons.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    int lessonid = Convert.ToInt32(firstOrDefault.Id);
                    filelist.AllFiles = _homework.GetTeacherHomeworkswithprovide(lessonid, DateTime.Now);
                }
            }
            return View("Homework", filelist);
        }

        [CheckCookie]
        public ActionResult Correction(int testid, int classid)
        {
            CorrectionList alldata = new CorrectionList();
            if (Request.Cookies["id"] != null)
            {
                int idteacher = Convert.ToInt32(Request.Cookies["id"].Value);
                alldata.Classid = classid;
                alldata.Testid = testid;
                alldata.Allstudent = _teacher.GetAllStudentinClasswithclassid(classid);
                alldata.AllQuestion = _TestQuestionService.GetAllQuestionTest(testid);
                alldata.AllGradeQuestion = _TestQuestionService.GetAlllistofAnswerQuestion(classid, testid,
                    alldata.Allstudent, alldata.AllQuestion, idteacher);
            }
            return View("Correction", alldata);
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult TeacherGrade(int lesson, int gradetype, int type)
        {
            IdString flagjson = new IdString();
            var allstudent = _teacher.GetSearchStudentinClass(lesson);
            flagjson.Id = 1;
            flagjson.Name = this.RenderPartialToString("_GradePartial", _teacher.GetAllExistStudent(allstudent, lesson, type, gradetype));

            return Json(flagjson);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult ExistSearch(int classid, int gradetype, int type)
        {
            IdString flagjson = new IdString();
            var allstudent = _teacher.GetSearchStudentinClass(classid);
            flagjson.Id = 1;
            flagjson.Name = this.RenderPartialToString("_ExistPartial", _teacher.GetAllExistStudent(allstudent, classid, 1, 0));

            return Json(flagjson);
        }
        [HttpPost]
        [AjaxOnly]
        public ActionResult AddGrade(int id, string grade)
        {
            //if (_student.Addgrade(id, grade,2))
            //{
            //    return Json(1);
            //}
            return Json(0);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult TeacherFiles(int lesson, int type)
        {
            Filepage filelist = new Filepage();
            IdString flagjson = new IdString();
            filelist.AllFiles = _file.GetChosesFilewithprovide(lesson, type);
            flagjson.Id = 1;
            flagjson.Name = this.RenderPartialToString("_FilePartial", filelist);
            return Json(flagjson);
        }
        [HttpPost]
        [AjaxOnly]
        public ActionResult AddFilesClassOnline(string description, string name, int classid)
        {
            IdString flagjson = new IdString();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (!string.IsNullOrEmpty(name))
                {
                    if (_onlineclass.Addfiletoonlineclass(description, "/Upload/" + name, classid) == true)
                    {
                        Filepage filelist = new Filepage();
                        filelist.Onlineclass = _onlineclass.allfiles(classid);
                        flagjson.Id = 1;
                        flagjson.Name = this.RenderPartialToString("_PartialOnlineClassFiles", filelist);
                        return Json(flagjson);
                    }
                }
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }
        public JsonResult FileUpload(string description)
        {
            UploadFile uf = new UploadFile();
            var response = uf.FileUpload();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult AddFiles(string description, int Lesson, int TypeFile, string name, int StudentId)
        {
            IdString flagjson = new IdString();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (!string.IsNullOrEmpty(name))
                {
                    if (_file.AddFiles("http://sheydairan.mahamsys.com/Upload/" + name, description, name, Lesson, TypeFile, 0,
                        id,StudentId) == "true")
                    {
                        Filepage filelist = new Filepage();
                        filelist.AllFiles = _file.GetChosesFilewithprovide(Lesson, TypeFile);
                        flagjson.Id = 1;
                        flagjson.Name = this.RenderPartialToString("_FilePartial", filelist);
                        return Json(flagjson);
                    }
                }
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteFiles(int fileid, int lesson, int typefile)
        {
            IdString flagjson = new IdString();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_file.DeleteFile(fileid))
                {
                    Filepage filelist = new Filepage();
                    filelist.AllFiles = _file.GetChosesFile(lesson, typefile);
                    flagjson.Id = 1;
                    flagjson.Name = this.RenderPartialToString("_FilePartial", filelist);
                    return Json(flagjson);
                }
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult SearchTest(int Lesson)
        {
            IdString flagjson = new IdString();
            TestViewModel newtest = new TestViewModel();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                newtest.Lessons = _teacher.lessonlistsearchtest(id);
                if (newtest.Lessons != null)
                {
                    newtest.Tests = _TestService.GetclassTests(Lesson);
                    flagjson.Id = 1;
                    flagjson.Name = this.RenderPartialToString("_TestPartial", newtest);
                    return Json(flagjson);
                }
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult classstudentchange(int classid)
        {
            List<IdString> students = new List<IdString>();
            students = _teacher.GetAllStudentinClasswithprovideid(classid);

            return Json(students);
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult InsertTest(int whitchclass, string description, int testtime, string starttime, string endtime, int delaytime, int grade)
        {
            starttime = starttime.toEnglishNumber();
            endtime = endtime.toEnglishNumber();
            DateTime stime = starttime.tomiladi();
            DateTime etime = endtime.tomiladi();
            IdString flagjson = new IdString();
            TestViewModel newtest = new TestViewModel();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                newtest.Lessons = _teacher.lessonlistsearchtest(id);
                if (newtest.Lessons != null)
                {
                    if (_TestService.AddTests(testtime, delaytime, description, stime, etime, whitchclass, id, grade) == true)
                    {
                        newtest.Tests = _TestService.GetclassTests(whitchclass);
                        flagjson.Id = 1;
                        flagjson.Name = this.RenderPartialToString("_TestPartial", newtest);
                        return Json(flagjson);
                    }

                }
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateTest(int testid, string description, int testtime, string starttime, string endtime, int delaytime, int testGrde)
        {
            IdString flagjson = new IdString();
            QuestionViewModel newQuestion = new QuestionViewModel();
            starttime = starttime.toEnglishNumber();
            endtime = endtime.toEnglishNumber();
            DateTime stime = starttime.tomiladi();
            DateTime etime = endtime.tomiladi();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_TestService.UpdateTests(testtime, delaytime, description, stime, etime, testGrde, testid))
                {
                    newQuestion.AllQuestions = _TestQuestionService.GetListofQuestion(testid);
                    return Json(1);
                }
            }
            return Json(0);
        }

        [HttpGet]
        public ActionResult DeleteTest(int testid)
        {
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_TestService.DeleteTest(testid))
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteQuestion(int questionid, int testid)
        {
            IdString flagjson = new IdString();
            QuestionViewModel newQuestion = new QuestionViewModel();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_TestQuestionService.DeleteQuestion(questionid))
                {
                    newQuestion.AllQuestions = _TestQuestionService.GetListofQuestion(testid);
                    flagjson.Id = 1;
                    flagjson.Name = this.RenderPartialToString("_QuestionPartial", newQuestion);
                    return Json(flagjson);
                }

            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateQuestion(int questionid)
        {
            IdString flagjson = new IdString();
            QuestionViewModel newQuestion = new QuestionViewModel();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                newQuestion.QuestionInfo = _TestQuestionService.GetQuesrionInfo(questionid);
                flagjson.Id = 1;
                flagjson.Name = this.RenderPartialToString("_AddQuestionPartial", newQuestion);
                return Json(flagjson);
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }

        [HttpPost]
        public ActionResult AddQuestion(int testid, string description, HttpPostedFileBase QuestionImage, int WhitchQuestion, int scoretest, string AnswerDescription, HttpPostedFileBase AnswerImage, int? answerkey, HttpPostedFileBase AnswerImagetest)
        {
            
            if (answerkey==null)
            {
                answerkey = 0;
            }
            if (description != "" || QuestionImage != null)
            {
                if (scoretest != 0)
                {
                    string QuestionImagename = "";
                    string AnswerImagename = "";
                    string AnswerImagetestname = "";
                    if (QuestionImage != null)
                    {
                        QuestionImagename = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                        string extension = Path.GetExtension(QuestionImage.FileName);
                        QuestionImagename += extension;
                        string path = Server.MapPath("~") + "Upload\\" + QuestionImagename ;
                        QuestionImage.SaveAs(path);

                    }
                    int idquestion;
                    //testi
                    if (WhitchQuestion == 0)
                    {
                        if (AnswerImagetest != null)
                        {
                            AnswerImagetestname = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                            string extension = Path.GetExtension(AnswerImagetest.FileName);
                            AnswerImagetestname += extension;
                            string path = Server.MapPath("~") + "Upload\\" + AnswerImagetestname ;
                            AnswerImagetest.SaveAs(path);
                        }
                        if (answerkey != 0)
                        {
                            idquestion = _TestQuestionService.AddQuestion(description, Convert.ToInt32(answerkey), AnswerDescription, QuestionImagename, AnswerImagetestname, testid, WhitchQuestion, scoretest);
                        }
                        else
                        {
                            return JavaScript("$('#erroemodelquestion').slideDown('slow');$('#messagequestion').empty(); $('#messagequestion').append('<p>گزینه درست باید را وارد کنید</p>');");
                        }
                    }
                    //tashrihi
                    else if (WhitchQuestion == 1)
                    {
                        if (AnswerImage != null)
                        {
                            AnswerImagename = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                            string extension = Path.GetExtension(AnswerImage.FileName);
                            AnswerImagename += extension;
                            string path = Server.MapPath("~") + "Upload\\" + AnswerImagename;
                            AnswerImage.SaveAs(path);
                        }
                        idquestion = _TestQuestionService.AddQuestion(description, Convert.ToInt32(answerkey), AnswerDescription, QuestionImagename, AnswerImagename, testid, WhitchQuestion, scoretest);
                    }

                    if (WhitchQuestion == 0 || WhitchQuestion == 1)
                    {
                        return JavaScript("$('#erroemodelquestion').hide('slow');$('#successmodelquestion').slideDown('slow'); window.location.href = '/Teacher/Questions?testid=" + testid + "';");

                    }
                    else
                    {
                        return JavaScript("$('#erroemodelquestion').slideDown('slow');$('#messagequestion').empty(); $('#messagequestion').append('<p>نوع سوال را باید مشخص کنید</p>');");
                    }
                }
                return JavaScript("$('#erroemodelquestion').slideDown('slow');$('#messagequestion').empty(); $('#messagequestion').append('<p>متن سوال نباید خالی باشد</p>');");
            }
            return JavaScript("$('#erroemodelquestion').slideDown('slow');$('#messagequestion').empty(); $('#messagequestion').append('<p>امتیاز سوال نباید خالی باشد</p>');");
        }
        [HttpPost]
        [AjaxOnly]
        public ActionResult ModifyQuestion(int questionid, string description, HttpPostedFileBase QuestionImage, int scoretest, string AnswerDescription, HttpPostedFileBase AnswerImage, int answerkey, HttpPostedFileBase AnswerImagetest)
        {
            if (description != "" || QuestionImage != null)
            {
                if (scoretest != 0)
                {
                    string QuestionImagename = "";
                    string AnswerImagename = "";
                    string AnswerImagetestname = "";
                    if (QuestionImage != null)
                    {
                        QuestionImagename = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                        string extension = Path.GetExtension(QuestionImage.FileName);
                        QuestionImagename += extension;
                        string path = Server.MapPath("~") + "Upload\\" + QuestionImagename ;
                        QuestionImage.SaveAs(path);

                    }


                    //testi
                    if (_TestQuestionService.FindTypeofquestion(questionid) == 0)
                    {
                        if (AnswerImagetest != null)
                        {
                            AnswerImagetestname = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                            string extension = Path.GetExtension(AnswerImagetest.FileName);
                            AnswerImagetestname += extension;
                            string path = Server.MapPath("~") + "Upload\\" + AnswerImagetestname;
                            AnswerImagetest.SaveAs(path);
                        }
                        if (answerkey != 0)
                        {
                            _TestQuestionService.UpdateQuestion(description, answerkey, AnswerDescription, QuestionImagename, AnswerImagetestname, questionid, scoretest);
                        }
                        else
                        {
                            return JavaScript("$('#erroemodelmultichoise').slideDown('slow');$('#messagemultichoise').empty(); $('#messagemultichoise').append('<p>گزینه درست باید را وارد کنید</p>');");
                        }
                    }
                    //tashrihi
                    else if (_TestQuestionService.FindTypeofquestion(questionid) == 1)
                    {
                        if (AnswerImage != null)
                        {
                            AnswerImagename = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                            string extension = Path.GetExtension(AnswerImage.FileName);
                            AnswerImagename += extension;
                            string path = Server.MapPath("~") + "Upload\\" + AnswerImagename ;
                            AnswerImage.SaveAs(path);
                        }
                        _TestQuestionService.UpdateQuestion(description, answerkey, AnswerDescription, QuestionImagename, AnswerImagename, questionid, scoretest);
                    }

                }
                return JavaScript("$('#erroemodelmultichoise').slideDown('slow');$('#messagemultichoise').empty(); $('#messagemultichoise').append('<p>متن سوال نباید خالی باشد</p>');");
            }
            return JavaScript("$('#erroemodelmultichoise').slideDown('slow');$('#messagemultichoise').empty(); $('#messagemultichoise').append('<p>امتیاز سوال نباید خالی باشد</p>');");
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteMultichoise(int choiseid, int questionid)
        {
            IdString flagjson = new IdString();
            List<Multiply_ChoiseItems> items = new List<Multiply_ChoiseItems>();
            if (Request.Cookies["id"] != null)
            {
                items = _TestQuestionService.DeleteMultichoise(questionid, choiseid);
                flagjson.Id = 1;
                flagjson.Name = this.RenderPartialToString("_MultichoiseQuestion", items);
                return Json(flagjson);
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult AddMultichoise(string text, int questionid)
        {
            IdString flagjson = new IdString();
            List<Multiply_ChoiseItems> items = new List<Multiply_ChoiseItems>();
            if (Request.Cookies["id"] != null)
            {
                items = _TestQuestionService.AddMultichoise(text, questionid);
                flagjson.Id = 1;
                flagjson.Name = this.RenderPartialToString("_MultichoiseQuestion", items);
                return Json(flagjson);
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }

        [HttpPost]
        public ActionResult SearchHomework(int lessonid, string date)
        {
            IdString flagjson = new IdString();
            Filepage filelist = new Filepage();
            if (Request.Cookies["id"] != null)
            {
                date = date.toEnglishNumber();
                DateTime searchdate = date.tomiladi();
                int idteacher = Convert.ToInt32(Request.Cookies["id"].Value);
                filelist.AllFiles = _homework.GetTeacherHomeworkswithprovide(lessonid, searchdate);
                flagjson.Id = 1;
                flagjson.Name = this.RenderPartialToString("_HomeworkPartial", filelist);
                return Json(flagjson);
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }

        [HttpPost]
        public ActionResult AddGradeHomework(int lessonid, int studentid, string grade, int homeworkid)
        {
            Filepage filelist = new Filepage();
            if (Request.Cookies["id"] != null)
            {
                int idteacher = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_homework.AddGrade(studentid, lessonid, grade, idteacher, homeworkid))
                {
                    return Json("$('#successmodel').slideDown('slow');$('#erroemodel').hide();");
                }
            }
            return Json("$('#erroemodel').slidedown('slow');$('#successmodel').hide();");
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult GradeCorrection(int classid, int testid, string grade, int studentid, int testanswerid)
        {
            IdString flagjson = new IdString();
            CorrectionList alldata = new CorrectionList();
            if (Request.Cookies["id"] != null)
            {

                int teacherid = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_testAnswer.InsertGrade(grade, testanswerid))
                {
                    alldata.Classid = classid;
                    alldata.Testid = testid;
                    alldata.Allstudent = _teacher.GetAllStudentinClass(classid);
                    alldata.AllQuestion = _TestQuestionService.GetAllQuestionTest(testid);
                    alldata.AllGradeQuestion = _TestQuestionService.GetAlllistofAnswerQuestion(classid, testid,
                        alldata.Allstudent, alldata.AllQuestion, teacherid);

                    flagjson.Id = 1;
                    flagjson.Name = this.RenderPartialToString("_GradeCorrectionPartial", alldata);
                    return Json(flagjson);
                }

            }

            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }


        [HttpPost]
        public string InsertallstudentGrades(int classid, int testid)
        {
            CorrectionList alldata = new CorrectionList();
            if (Request.Cookies["id"] != null)
            {
                alldata.Allstudent = _teacher.GetAllStudentinClasswithclassid(classid);
                alldata.AllQuestion = _TestQuestionService.GetAllQuestionTest(testid);
                if (_testAnswer.AddGradetoAllStudent(testid, alldata.Allstudent, alldata.AllQuestion))
                {
                    return "true";
                }
            }
            return "false";
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult CopyPaste(int questionidcopy, int questionidpaste)
        {
            CorrectionList alldata = new CorrectionList();
            if (Request.Cookies["id"] != null)
            {
                if (_TestQuestionService.CopyPaste(questionidcopy, questionidpaste))
                {
                    return Json("true");
                }
            }
            return Json("false");
        }
    }
}