using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;

namespace SchoolIntelligentWeb.Controllers
{
    public class TestController : ApiController
    {
        private ITestService _TestService = new TestService();
        [HttpGet]
        public List<TestInfo> GetTest(int studentid)
        {
            return _TestService.GetTests(studentid);
        }
       
        [HttpPost]
        public Boolean Post(int testtime, int delaytime, string description)
        {
            return _TestService.AddTests(testtime, delaytime, description, DateTime.Now, DateTime.Now, 1,1,100);
            
        }
    }
}
