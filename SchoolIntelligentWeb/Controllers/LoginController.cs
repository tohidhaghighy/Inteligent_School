using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Twitter.Messages;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Filter;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.Controllers
{
    public class LoginController : Controller
    {
        private ILogin _Login = new LoginService();
        //login with android app
        [HttpGet]
        public ActionResult LoginAndroid(string username, string password, string role,string fcm)
        {
            return Json(_Login.GetLoginInfoandroid(username, password, int.Parse(role),fcm), JsonRequestBehavior.AllowGet);
        }
        // GET: Login
        [LoginUser]
        public ActionResult Index()
        {
            return View("Login");
        }
        public ActionResult Forget()
        {
            return View("Forget");
        }
        [HttpPost]
        public ActionResult Forgetaction(string Email, string NationalCode ,int TypePerson)
        {
                LoginInfo SelectedUser = _Login.GetForgetLoginInfo(NationalCode,Email, TypePerson);
                if (SelectedUser != null)
                {
                    if (Email.SendEmailAction(SelectedUser.Password+" : رمز عبور شما ")=="true")
                    {
                        return JavaScript("$('#Error').empty();$('#Error').append('پسورد به ایمیل شما ارسال شد');$('#Error').attr('style','display:normal;color: black; background-color: greenyellow;');");
                    }
                    
                }
            return JavaScript("$('#Error').empty();$('#Error').append('کد ملی یا ایمیل وارده صحیح نمیباشد');$('#Error').attr('style','display:normal;color: black; background-color: #ff4500;');");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Loginaction(User user)
        {
            if (ModelState.IsValid)
            {
                LoginInfo SelectedUser = _Login.GetLoginInfo(user.NationalCode, user.Password.Hashmd5(),user.Roll);
                if (SelectedUser != null)
                {
                    if (SelectedUser.Name != null)
                    {
                        HttpCookie Namecookie=new HttpCookie("Name",SelectedUser.Name+ " "+SelectedUser.Family);
                        Namecookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(Namecookie);
                        
                        HttpCookie Photocookie = new HttpCookie("Photo");
                        Photocookie.Expires = DateTime.Now.AddDays(15);
                        if (SelectedUser.Image == null)
                        {
                            Photocookie.Value= "https://mahamsys.com/Images/user.png";
                            Response.Cookies.Add(Photocookie);
                        }
                        else
                        {
                            Photocookie.Value = SelectedUser.Image;
                            Response.Cookies.Add(Photocookie);
                        }
                        HttpCookie usernamecookie = new HttpCookie("Username", user.NationalCode);
                        usernamecookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(usernamecookie);
                        HttpCookie passwordcookie = new HttpCookie("Password", user.Password);
                        passwordcookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(passwordcookie);
                        HttpCookie rolecookie = new HttpCookie("Role", user.Roll.ToString());
                        rolecookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(rolecookie);
                        HttpCookie idcookie = new HttpCookie("id", SelectedUser.Id.ToString());
                        idcookie.Expires=DateTime.Now.AddDays(15);
                        Response.Cookies.Add(idcookie);
                    }
                    if (user.Roll==1)
                    {
                        return JavaScript("$('#Error').empty();$('#Error').append('با موفقیت انجام شد');$('#Error').attr('style','display:normal;color: black; background-color: greenyellow;');window.location.href = '/Teacher';");
                    }
                    else if (user.Roll==2)
                    {
                        return JavaScript("$('#Error').empty();$('#Error').append('با موفقیت انجام شد');$('#Error').attr('style','display:normal;color: black; background-color: greenyellow;');window.location.href = '/panel';");
                    }
                    else if (user.Roll == 3)
                    {
                        return JavaScript("$('#Error').empty();$('#Error').append('با موفقیت انجام شد');$('#Error').attr('style','display:normal;color: black; background-color: greenyellow;');window.location.href = '/Parent';");
                    }
                } 
            }

            return JavaScript("$('#Error').empty();$('#Error').append('نام کاربری یا پسورد صحیح نمیباشد');$('#Error').attr('style','display:normal;color: black; background-color: #ff4500;');");
        }

        [HttpPost]
        public ActionResult Exitfromsite()
        {
            HttpCookie usernamecookie = new HttpCookie("Username", "");
            usernamecookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(usernamecookie);
            HttpCookie passwordcookie = new HttpCookie("Password", "");
            passwordcookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(passwordcookie);
            HttpCookie rolecookie = new HttpCookie("Role", "");
            rolecookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(rolecookie);
            HttpCookie idcookie = new HttpCookie("id", "");
            idcookie.Expires = DateTime.Now.AddDays(-1);
            HttpCookie Namecookie = new HttpCookie("Name", "");
            Namecookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(Namecookie);

            HttpCookie Photocookie = new HttpCookie("Photo");
            Photocookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(Photocookie);
            return Json("true");
        }

    }
}