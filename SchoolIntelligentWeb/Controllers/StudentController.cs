using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;

namespace SchoolIntelligentWeb.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService _student = new StudentService();

        //------------------------برگرداندن دروس دانش اموز-------------------

        [System.Web.Http.HttpGet]
        public JsonResult GetStudentclass(int studentid)
        {
            var model = _student.GetStudentClasslist(studentid);
            if (model.Count > 0)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json("empty", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// بازگرداندن نمره یا حضورغیاب به ازای type=0 || type=1
        /// </summary>
        /// <param name="studentid"></param>
        /// <param name="lessonid"></param>
        /// <param name="type"></param>
        /// <param name="gradetype"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public JsonResult GetStudentsGrade(int studentid,int lessonid,int type,int gradetype)
        {
            var model = _student.GetStudentGrades(studentid,lessonid,type,gradetype);
            if (model.Count > 0)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json("empty", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// پیام های مدیر به اولیا
        /// </summary>
        /// <param name="studentid"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAssistentMessage(int studentid)
        {
            var model = _student.GetAssistentmessagelist(studentid);
            if (model.Count > 0)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json("empty", JsonRequestBehavior.AllowGet);
        }

    }
}