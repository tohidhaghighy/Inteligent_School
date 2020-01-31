using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;
using SchoolIntelligentWeb.Utilities;
using SchoolIntelligentWeb.DomainClasses.ViewModels;

namespace SchoolIntelligentWeb.Controllers
{
    public class AndroidController : Controller
    {
        private ITeacherService _teacherService=null;
        private IStudentService _studentService = null;
        private IMessageService _messageService = null;
        private IEmtehanat _emtehanatService = null;
        private ITadrisshodeha _tadrisshodeha = null;
        // GET: Android
        public ActionResult GetStudentList(int classid,int lessonid)
        {
            _teacherService = new TeacherService();
            return Json(_teacherService.Listofgradestudent(lessonid,classid), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// میانگین نمرات از 100 را بر میگرداند
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ClassAvg(int classid)
        {
            _teacherService = new TeacherService();
            return Json(_teacherService.GetAvgofclassnumbers(classid), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ای دی درس رو میفرستی من کل غیبت های دانش اموز تو اون درس رو بدم
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetStudentAbsents(int lessonid,int studentid)
        {
            _studentService = new StudentService();
            return Json(_studentService.GetStudentabsents(studentid, lessonid), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ای دی درس رو میفرستی من کل غیبت های دانش اموز و نمرات یک دانش آموز را تو اون درس رو بدم
        /// مثل تابع GetStudentList کار میکند فقط برای یک دانش آموز 
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetStudentGradeAndAbsents(int lessonid, int studentid)
        {
            _teacherService = new TeacherService();
            return Json(_teacherService.Listofgradeandabsent(lessonid, studentid), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// انواع نمرات را باز میگرداند
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTypeGrade()
        {
            _teacherService = new TeacherService();
            return Json(_teacherService.GetTypeofgrade(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// insert grade to android
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddOneNomre(string grade,int type,int lessonid,int time_id,int student_id)
        {
            _studentService = new StudentService();

            if (_studentService.Addgrade(lessonid, grade, type, time_id, student_id) == true)
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }

            return Json("false", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// insert grade to android
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdateOneNomre(string grade, int id)
        {
            _studentService = new StudentService();
                if (_studentService.updategrade(id, grade) == true)
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
          
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// insert list of grade to android
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddlistNomre(string grade,int lessonid,int type,int time_id)
        {
            _studentService = new StudentService();
            if (_studentService.Addlistgrade(grade,lessonid,type,time_id) == true)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// hozur o ghiab all android
        /// </summary>
        /// <param name="present"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Addallhozur(string present,int classid,int lessonid,int time_id)
        {
            _studentService = new StudentService();
            if (_studentService.Addallpresent(present, classid,lessonid,time_id) == true)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
       /// <summary>
       /// mavarede enzebati ezafe mikonad
       /// </summary>
       /// <param name="text"></param>
       /// <param name="studentid"></param>
       /// <returns></returns>
        [HttpGet]
        public ActionResult Adddiscipline(string text, int studentid)
        {
            _studentService = new StudentService();
            if (_studentService.Adddisciplines(studentid,text) == true)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// list mavarede enzebati
        /// </summary>
        /// <param name="studentid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Listdiscipline(int studentid)
        {
            _studentService = new StudentService();
            return Json(_studentService.listdisciplines(studentid), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Updatepresent(int id,int value)
        {
            _studentService = new StudentService();
            return Json(_studentService.Updatepresents(id,value), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Deletegrade(int id)
        {
            _studentService = new StudentService();
            return Json(_studentService.deletegrade(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// برنامه کلاسی هر دانش آموز نمایش در اندروید
        /// </summary>
        /// <param name="studentid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult scheduleListStudent(int studentid)
        {
            _studentService = new StudentService();
            List<ListofTime> alltime = new List<ListofTime>();
            Schadulerlist allschaduler = new Schadulerlist();
            alltime = _studentService.GetListofTime(studentid);
            allschaduler = _studentService.GetSchaduler(alltime);
            return Json(allschaduler, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///نمایش در اندروید برنامه کلاسی هر معلم
        /// </summary>
        /// <param name="teacherid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult scheduleListTeacher(int teacherid)
        {
            _studentService = new StudentService();
            _teacherService = new TeacherService();
            List<ListofTime> alltime = new List<ListofTime>();
            Schadulerlist allschaduler = new Schadulerlist();
            alltime = _teacherService.GetListofTime(teacherid);
            allschaduler = _studentService.GetSchaduler(alltime);
            return Json(allschaduler, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// نمایش نمودار در اندروید دانش آموز
        /// </summary>
        /// <param name="studentid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult chartdata(int studentid)
        {
            _studentService = new StudentService();
            ChartItem chart = new ChartItem();
            chart.AllLesson = _studentService.GetStudentClasslist(studentid);
            chart.AllStudentAvg = _studentService.GetStudentAvgforChart(studentid, chart.AllLesson);
            chart.AllClassAvg = _studentService.GetlessonAvgforChart(chart.AllLesson);
            return Json(chart, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// پیام ارسال شده از سمت مدیر به اولیا را نشان میدهد
        /// </summary>
        /// <param name="studentid"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult ParentMessage(int studentid)
        {
            _messageService = new MessageService();
            
            return Json(_messageService.Getalllist(studentid), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// پیام های ازسالی از سمت مدیر به معلم را نشان میدهد
        /// </summary>
        /// <param name="teacherid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TeacherMessage(int teacherid)
        {
            _messageService = new MessageService();

            return Json(_messageService.GetalllistTeacher(teacherid), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SendMessageToModir(int teacherid,string message)
        {
            _messageService = new MessageService();
            return Json(_messageService.SendMessagetomodir(teacherid,message), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// یافتن پیام جدید برای معلم برای دادن نوتیفیکیشن
        /// </summary>
        /// <param name="teacherid"></param>
        /// <param name="messageid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NewTeacherMessage(int teacherid, int messageid)
        {
            _messageService = new MessageService();
            return Json(_messageService.NewMassageTeacher(teacherid, messageid), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///اولیا یافتن پیام جدید برای نوتیفیکیشن به 
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="messageid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NewParentMessage(int studentid, int messageid)
        {
            _messageService = new MessageService();
            return Json(_messageService.NewMassageParent(studentid, messageid), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// افزودن تاریخ امتحانات برای یاد آوری معلم
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="text"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddtoEmtehanat(int classid, string text,string date)
        {
            _emtehanatService = new EmtehanatService();
            return Json(_emtehanatService.AddEmtehanat(classid, text,date), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// گرفتن لیست امتحانات یک کلاس
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEmtehanat(int classid)
        {
            _emtehanatService = new EmtehanatService();
            return Json(_emtehanatService.GetEmtehanat(classid), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// گرفتن لیست موارد تدریس شده در یک درس
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="teacherid"></param>
        /// <param name="lessonid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTadrisshodeha(int classid,int teacherid,int lessonid)
        {
            _tadrisshodeha = new TadrisshodehaService();
            return Json(_tadrisshodeha.GetTadrisshodeha(classid,teacherid,lessonid), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// افزودن موارد تدریس شده برای مشاهده معلم
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="teacherid"></param>
        /// <param name="lessonid"></param>
        /// <param name="text"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddTadrisshodeha(int classid, int teacherid, int lessonid,string text,string date)
        {
            _tadrisshodeha = new TadrisshodehaService();
            return Json(_tadrisshodeha.Addtadrisshodeha(classid, teacherid, lessonid,text,date), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// mavarede tashvighi 
        /// </summary>
        /// <param name="studentid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTashvighi(int studentid)
        {
            _tadrisshodeha = new TadrisshodehaService();
            return Json(_tadrisshodeha.Getalltashvighi(studentid), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// afzudane mavarede tashvighi
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddTashvighi(int studentid,string text)
        {
            _tadrisshodeha = new TadrisshodehaService();
            return Json(_tadrisshodeha.addtashvighi(studentid,text), JsonRequestBehavior.AllowGet);
        }


        UpdateAndroid newserviceupdate = new UpdateAndroid();
        [HttpGet]
        public ActionResult Addupdate(int versioncode, string description, string url, int type)
        {
            return Json(newserviceupdate.addversion(description, type, url, versioncode),JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// olia=0  & student=1  & teacher=2
        /// </summary>
        /// <param name="versioncode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult checkupdate(int versioncode,int type)
        {
            return Json(newserviceupdate.checkversion(versioncode,type), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult listmessage(int studentid)
        {
            TokenService token = new TokenService();
            return Json(token.listmassege(studentid), JsonRequestBehavior.AllowGet);
        }



    }
}