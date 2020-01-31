using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.DomainClasses.ViewModels;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface IStudentService
    {
        List<IdString> GetStudentClasslist(int idstudent);
        List<IdString> gettypeGrade();
        Boolean AddPresent(int id, string present);
        List<Grade> GetStudentGrades(int idstudent, int idlesson, int type, int typegrade);
        List<DomainClasses.MoreClasses.IdString> GetStudentClasslistreturnprovide(int idstudent);
        List<Assistetmessage> GetAssistentmessagelist(int idstudent);
        List<ListofTime> GetListofTime(int idstudent);
        Schadulerlist GetSchaduler(List<ListofTime> alltime);
        Boolean Addgrade(int lessonid, string grade, int type, int time_id, int studentid);
        Boolean deletegrade(int id);
        List<DomainClasses.MoreClasses.IdString> GetlessonAvgforChart(List<IdString> alllesson);
        List<DomainClasses.MoreClasses.IdString> GetStudentAvgforChart(int idstudent, List<IdString> alllesson);
        List<Grade> GetStudentabsents(int idstudent, int idlesson);
        List<Grade> GetStudentabsentsgrade(int idstudent, int idlesson);
        Boolean Addallpresent(string present, int classid, int lessonid,int time_id);
        Boolean Addlistgrade(string grade, int lessonid,int classid,int time_id);
        int existinlist(List<string> ids, List<string> grades, int studentid);
        Boolean updategrade(int id, string grade);
        Boolean Adddisciplines(int studentid, string text);
        List<IdString> listdisciplines(int studentid);
        Boolean Updatepresents(int id, int value);
        List<Grade> GetStudentGradeswithprovider(int idstudent, int providedid, int type, int typegrade);
    }
}
