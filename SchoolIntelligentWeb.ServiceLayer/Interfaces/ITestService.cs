using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface ITestService
    {
        List<TestInfo> GetTests(int studentid);

        Boolean AddTests(int testtime, int delaytime, string description, DateTime startdate, DateTime finishdate,int lesson, int teacherid, int grade);
        List<DomainClasses.MoreClasses.TestInfo> GetclassTests(int provideid);
        Boolean DeleteTest(int testid);
        DomainClasses.MoreClasses.TestInfo Gettestinfo(int testid);
        Boolean UpdateTests(int testtime, int delaytime, string description, DateTime startdate, DateTime finishdate,int grade,int testid);
    }
}
