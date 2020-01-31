using SchoolIntelligentWeb.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface IOnlineClassService
    {
        List<OnlineClass> ListClass(int teacherid);
        OnlineClass GetClass(int teacherid);
        Boolean AddClass(int teacherid, string name, string family, string email, string phone, string detail, string maghtae, string date, string username, string password);
        Boolean DeleteClass(int classid);
        OnlineClass allinfo(int teacherid);
        OnlineClass Logininfo(string username, string password);
        Boolean Addfiletoonlineclass(string name, string url, int id);
        List<OnlineClassFiles> allfiles(int classid);
        List<OnlineClassFiles> allfileswithname(string classname);
        Boolean deletefiles(int id);
    }
}
