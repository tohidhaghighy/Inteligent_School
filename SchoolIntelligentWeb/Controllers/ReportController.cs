using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace SchoolIntelligentWeb.Controllers
{
    public class ReportController : Controller
    {
        private ITestQuestion _testquestion=new TestQuestion();
        // GET: Report
        public ActionResult Index(int testid)
        {
            return View();
        }
        public ActionResult LoadReportSnapshot(int testid)
        {
            var report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/QuestionTest.mrt"));
            report.RegBusinessObject("Questions", _testquestion.GetTestQuestionsforreport(testid));
            //report.Dictionary.Variables.Add("today", DateTime.Today.ToPersianString(PersianDateTimeFormat.Date));
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }

        public virtual ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }

        public virtual ActionResult PrintReport()
        {
            return StiMvcViewer.PrintReportResult(this.HttpContext);
        }


    }
}