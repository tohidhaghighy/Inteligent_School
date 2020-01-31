using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface IFilesService
    {
        List<FileInfo> GetFiles(int id);

        List<FileInfo> GetChosesFilewithprovide(int provideid, int type);

        List<FileInfo> GetChosesFile(int idlesson, int type);
        string AddFiles(string link, string description, string name, int lessonid, int filetype, int schoolid, int teacherid,int studentid);
        List<IdString> GetFileVideo();
        List<FileInfo> GetChosesFileProvide(int idprovide, int type);
        Boolean DeleteFile(int id);

        List<FileInfo> GetFilterChosesFile(int idlesson, int type, int idstudent);
    }
}
