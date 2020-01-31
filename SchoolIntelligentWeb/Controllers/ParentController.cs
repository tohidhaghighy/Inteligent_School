using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.ServiceLayer.Filter;
using SchoolIntelligentWeb.Utilities.Filters;
using SchoolIntelligentWeb.DomainClasses.ViewModels;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.Controllers
{
    public class ParentController : Controller
    {

        private IStudentService _student = new StudentService();
        private IFilesService _file = new FilesService();
        private IHomework _homework = new SchoolIntelligentWeb.ServiceLayer.Services.HomeWork();
        // GET: Parent
        [CheckCookie]
        public ActionResult Index()
        {
            return View("Parent");
        }
         [CheckCookie]
        public ActionResult Grade()
        {
            GradePage Gradelist = new GradePage();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);

                Gradelist.Lessons = _student.GetStudentClasslist(id);
                Gradelist.Typegrade = _student.gettypeGrade();
                var firstOrDefault = Gradelist.Lessons.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    int lessonid = Convert.ToInt32(firstOrDefault.Id);
                    var orDefault = Gradelist.Typegrade.FirstOrDefault();
                    if (orDefault != null)
                    {
                        int gradetypeid = Convert.ToInt32(orDefault.Id);
                        Gradelist.AllGrades = _student.GetStudentGrades(id, lessonid, 0, gradetypeid);
                    }

                }
            }

            return View("Grade", Gradelist);
        }
         [CheckCookie]
        public ActionResult Exist()
        {
            GradePage Gradelist = new GradePage();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);

                Gradelist.Lessons = _student.GetStudentClasslist(id);
                var firstOrDefault = Gradelist.Lessons.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    int lessonid = Convert.ToInt32(firstOrDefault.Id);
                    Gradelist.AllGrades = _student.GetStudentGrades(id, lessonid, 1, 0);

                }
            }

            return View("Exist", Gradelist);
        }
        [CheckCookie]
        public ActionResult Report()
        {
            List<Assistetmessage> allmessage = new List<Assistetmessage>();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                allmessage = _student.GetAssistentmessagelist(id);
            }
            return View("Report", allmessage);
        }
        [CheckCookie]
        public ActionResult Homework()
        {
            Filepage filelist = new Filepage();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                filelist.Lessons = _student.GetStudentClasslist(id);
                var firstOrDefault = filelist.Lessons.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    int lessonid = Convert.ToInt32(firstOrDefault.Id);
                    filelist.AllFiles = _homework.GetHomeworks(lessonid, id);
                }
            }
            return View("Home_Work", filelist);
        }
        [CheckCookie]
        public ActionResult schedule()
        {
            List<ListofTime> alltime = new List<ListofTime>();
            Schadulerlist allschaduler = new Schadulerlist();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                alltime = _student.GetListofTime(id);
                allschaduler = _student.GetSchaduler(alltime);
            }
            return View("schedule", allschaduler);
        }
        [CheckCookie]
        public ActionResult Files()
        {
            Filepage filelist = new Filepage();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                filelist.Lessons = _student.GetStudentClasslist(id);
                filelist.Typegrade = _file.GetFileVideo();
                var firstOrDefault = filelist.Lessons.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    int lessonid = Convert.ToInt32(firstOrDefault.Id);
                    var orDefault = filelist.Typegrade.FirstOrDefault();
                    if (orDefault != null)
                    {
                        int filetypeid = Convert.ToInt32(orDefault.Id);
                        filelist.AllFiles = _file.GetFilterChosesFile(lessonid, filetypeid, id);
                    }

                }
            }
            return View("Files", filelist);
        }
    }
}