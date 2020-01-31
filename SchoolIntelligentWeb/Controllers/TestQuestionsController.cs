using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;

namespace SchoolIntelligentWeb.Controllers
{
    public class TestQuestionsController : Controller
    {
        private ITestQuestion _question = new TestQuestion();
        [System.Web.Http.HttpGet]
        public JsonResult GetQuestions(int studentid, int testid)
        {
            var model = _question.GetTests(studentid, testid);
            if (model.Count > 0)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json("soon", JsonRequestBehavior.AllowGet);
        }

    }
}