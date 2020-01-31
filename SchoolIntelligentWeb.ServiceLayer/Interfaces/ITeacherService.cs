using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.DomainClasses.ViewModels;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface ITeacherService
    {
        List<IdString> GetTeacherClasslist(int idteacher);
        List<IdString> GetAllStudentinClasswithprovideid(int provideid);
        List<IdString> GetAllStudentinClass(int idclass);
        List<IdString> GetAllStudentinClasswithclassid(int classid);
        List<IdString> GetAllStudentinClassonlyclass(int classid);
        List<IdString> GetSearchStudentinClass(int providedid);
        List<GradePage> GetAllExistStudent(List<IdString> allstudent, int idlesson, int type, int gradetype);
        List<ListofTime> GetListofTime(int idteacher);
        List<Listofgradepresentstudents> Listofgradestudent(int lessonid, int classid);
        IdString GetAvgofclassnumbers(int classid);
        Listofgradepresentstudents Listofgradeandabsent(int lessonid, int studentid);
        List<IdString> GetTypeofgrade();
        List<DomainClasses.MoreClasses.IdString> GetTeacherClassandlessonlist(int idteacher);
        List<DomainClasses.MoreClasses.IdString> GetTeacherClassandlessonlistreturnclass(int idteacher);

        List<DomainClasses.MoreClasses.IdString> lessonlistsearchtest(int idteacher);
    }
}
