using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface IHomework
    {
        List<FileInfo> GetHomeworks(int idlesson, int studentid);
        Boolean AddHomework(string url, int lessonid, int studentid);
        List<FileInfo> GetTeacherHomeworkswithprovide(int provideid, DateTime news);
        Boolean DeleteHomework(int id);
        List<FileInfo> GetTeacherHomeworks(int idlesson, DateTime date);

        Boolean AddGrade(int studentid, int lessonid, string grade, int teacherid,int homeworkid);
    }
}
