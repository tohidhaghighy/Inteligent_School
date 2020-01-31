using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.DomainClasses.ViewModels;
using SchoolIntelligentWeb.ServiceLayer.Filter;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;
using SchoolIntelligentWeb.Utilities;
using SchoolIntelligentWeb.Utilities.Filters;

namespace SchoolIntelligentWeb.Controllers
{
    public class PanelController : Controller
    {
        private IStudentService _student = new StudentService();
        private IFilesService _file = new FilesService();
        private IHomework _homework = new SchoolIntelligentWeb.ServiceLayer.Services.HomeWork();
        // GET: Panel
        [CheckCookie]
        public ActionResult Index()
        {
            return View("Main");
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
                        filelist.AllFiles = _file.GetFilterChosesFile(lessonid, filetypeid,id);
                    }

                }
            }
            return View("Files", filelist);
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
        public ActionResult Home_Work()
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
        [HttpPost]
        public ActionResult Chart()
        {

            ChartItem chart = new ChartItem();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                chart.AllLesson = _student.GetStudentClasslist(id);
                chart.AllStudentAvg = _student.GetStudentAvgforChart(id, chart.AllLesson);
                chart.AllClassAvg = _student.GetlessonAvgforChart(chart.AllLesson);
            }
            return Json(chart);

        }

        public string converttostring(List<IdString> alllist)
        {
            string result = "['";
            int count = 1;
            foreach (var list in alllist)
            {
                if (alllist.Count() != count)
                {
                    result += list.Name + "','";
                }
                else
                {
                    result += list.Name + "'";
                }
                count++;
            }
            result += "']";
            return result;
        }

        [AjaxOnly]
        [HttpPost]
        public ActionResult Gethomework(int lesson)
        {
            IdString flagjson = new IdString();
            Filepage filelist = new Filepage();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                filelist.AllFiles = _homework.GetHomeworks(lesson, id);
                flagjson.Id = 1;
                flagjson.Name = this.RenderPartialToString("_HomeWorkPartial", filelist);
                return Json(flagjson);
            }
            flagjson.Id = 0;
            flagjson.Name = "";
            return Json(flagjson);
        }
        [AjaxOnly]
        [HttpPost]
        public ActionResult AddHomework(int lesson, HttpPostedFileBase homeworkfile)
        {
            IdString flagjson = new IdString();
            if (Request.Cookies["id"] != null)
            {
                string HomeworkImagename = "";
                if (homeworkfile != null)
                {
                    HomeworkImagename = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                    string extension = Path.GetExtension(homeworkfile.FileName);
                    HomeworkImagename += extension;
                    string path = Server.MapPath("~") + "Upload\\" + HomeworkImagename;
                    homeworkfile.SaveAs(path);

                }
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (!string.IsNullOrEmpty(HomeworkImagename))
                {
                    if (_homework.AddHomework("http://sheydairan.mahamsys.com/Upload/" + HomeworkImagename, lesson, id) == true)
                    {
                        return JavaScript("location.reload();");
                    }
                }

            }
            return JavaScript("location.reload();");
        }


        [AjaxOnly]
        [HttpPost]
        public ActionResult Deletehomework(int homeworkid)
        {
            IdString flagjson = new IdString();
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_homework.DeleteHomework(homeworkid))
                {
                    return JavaScript("location.reload();");
                }
            }
            return JavaScript("location.reload();");
        }
    }
}