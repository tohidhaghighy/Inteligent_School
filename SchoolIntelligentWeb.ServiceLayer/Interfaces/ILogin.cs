using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface ILogin
    {
        int LoginRequest(string username,string password);
        List<Multiply_ChoiseItems> GetAllInfo(int id);
        LoginInfo GetLoginInfo(string username, string password, int roll);
        LoginInfo GetForgetLoginInfo(string username, string email, int roll);
        LoginInfo GetLoginInfoandroid(string username, string password, int roll,string fcm);
    }
}
