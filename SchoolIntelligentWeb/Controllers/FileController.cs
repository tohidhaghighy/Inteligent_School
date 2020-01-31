using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;

namespace SchoolIntelligentWeb.Controllers
{
    public class FileController : Controller
    {
        private IFilesService _file = new FilesService();
        // GET: File
        [System.Web.Http.HttpGet]
        public JsonResult GetFile(int lessonid, int type)
        {
            if (Request.Cookies["id"] != null)
            {
                int id = Convert.ToInt32(Request.Cookies["id"].Value);
                var model = _file.GetFilterChosesFile(lessonid, type,id);
                if (model.Count > 0)
                {
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return Json("empty", JsonRequestBehavior.AllowGet);
            }
            return Json("empty", JsonRequestBehavior.AllowGet);
        }


        [System.Web.Http.HttpGet]
        public JsonResult GetFileAndroid(int lessonid, int type,int idstudent)
        {
                var model = _file.GetFilterChosesFile(lessonid, type, idstudent);
                if (model.Count > 0)
                {
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                return Json("empty", JsonRequestBehavior.AllowGet);
           
        }
    }
}