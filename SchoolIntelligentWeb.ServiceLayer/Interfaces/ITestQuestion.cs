using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface ITestQuestion
    {
        List<TestQuestions> GetTests(int studentid, int testid);
        List<DomainClasses.MoreClasses.TestQuestions> GetListofQuestion(int testid);

        Boolean DeleteQuestion(int questionid);

        DomainClasses.MoreClasses.TestQuestions GetQuesrionInfo(int questionid);

        int AddQuestion(string questiontext, int answerKey, string answertext, string questionimage, string answerimage,
            int testid, int questiontype, int questiongrade);

        List<Multiply_ChoiseItems> DeleteMultichoise(int questionid, int idmultichoise);

        int UpdateQuestion(string questiontext, int answerKey, string answertext, string questionimage,
            string answerimage, int questionid, int questiongrade);

        List<Multiply_ChoiseItems> AddMultichoise(string text, int questionid);


        int FindTypeofquestion(int questionid);
        List<DomainClasses.MoreClasses.AnswerItems> GetAllQuestionTest(int testid);

        List<StudentAnswerTest> GetAlllistofAnswerQuestion(int classid, int testid, List<IdString> Allstudent,
            List<AnswerItems> AllQuestion, int teacherid);

        Boolean CopyPaste(int copyquestionid, int pastequestionid);

        List<DomainClasses.MoreClasses.Report> GetTestQuestionsforreport(int testid);
    }
}
