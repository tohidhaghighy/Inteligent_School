using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface ITestAnswerService
    {
        Boolean InsertAnsert(int? answerkey,string answertext,int studentid,int testid,int questionid,string photourl);
        Boolean InsertGrade(string grade, int testanswerid);
        Boolean TestAnserUpdategrade(int testid, int studentid);
        Boolean AddGradetoAllStudent(int testid, List<IdString> allstudent, List<AnswerItems> allQuestion);
        string ReturnGradeforstudent(int studentid, int testid);
    }
}
