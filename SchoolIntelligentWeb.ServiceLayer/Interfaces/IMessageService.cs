using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface IMessageService
    {
        List<Messagelist> Getalllist(int studentid);
        List<Messagelist> GetalllistTeacher(int teacherid);
        Boolean SendMessagetomodir(int teacherid, string Message);
        List<Messagelist> NewMassageTeacher(int teacherid, int massageid);
        List<Messagelist> NewMassageParent(int studentid, int massageid);

    }
}
