using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;

namespace SchoolIntelligentWeb.Controllers
{
    public class TestAnswerController : Controller
    {


        private ITestAnswerService _TestAnswerService = new TestAnswerService();
        [System.Web.Http.HttpPost]
        public Boolean InsertAnswerTest(int answerkey, string answertext, int studentid, int testid, int questionid, string photourl)
        {
            return _TestAnswerService.InsertAnsert(answerkey, answertext, studentid, testid, questionid, photourl);
        }

        /// <summary>
        /// بازگرداندن نمره آزمون برای مشاهده در اندروید
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        public string Grade(int studentId, int testId)
        {
            return _TestAnswerService.ReturnGradeforstudent(studentId, testId);
        }
    }
}