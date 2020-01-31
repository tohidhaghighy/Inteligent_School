using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Services;
using SchoolIntelligentWeb.Utilities.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.Utilities;
using SchoolIntelligentWeb.DomainClasses.ViewModels;

namespace SchoolIntelligentWeb.Controllers
{
    public class OnlineClassController : Controller
    {
        private OnlineClassService _onlineclass = new OnlineClassService();
        // GET: OnlineClass
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Deleteclass(int classid)
        {
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_onlineclass.DeleteClass(classid))
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult addclass(string name, string family, string email, string phone, string detail, string maghtae, string date, string username, string password)
        {
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                if (_onlineclass.AddClass(id, name, family, email, phone, detail, maghtae, date, username, password))
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpGet]
        public JsonResult LoginApp(string username, string password)
        {
            if (_onlineclass.Logininfo(username, password) != null)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteFiles(int id)
        {
            if (Request.Cookies["id"] != null)
            {

                if (_onlineclass.deletefiles(id) == true)
                {
                    return Json("true");
                }
            }
            return Json("false");
        }


        [System.Web.Http.HttpGet]
        public ActionResult OnlineClassFiles(string classname)
        {
            return Json(_onlineclass.allfileswithname(classname), JsonRequestBehavior.AllowGet);
        }
    }
}