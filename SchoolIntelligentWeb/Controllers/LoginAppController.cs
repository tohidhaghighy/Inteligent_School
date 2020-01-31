using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;

namespace SchoolIntelligentWeb.Controllers
{
    public class LoginAppController : ApiController
    {
        private ILogin _Login = new LoginService();
        
        [HttpPost]
        public async Task<List<Multiply_ChoiseItems>> LoginApp(string username, string password)
        {
            int teacherid =_Login.LoginRequest(username, password);
            return _Login.GetAllInfo(teacherid);
        }
        
    }
}
