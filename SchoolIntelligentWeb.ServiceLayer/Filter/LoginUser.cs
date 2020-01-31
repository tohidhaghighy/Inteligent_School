using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using SchoolIntelligentWeb.DomainClasses.MoreClasses;
using SchoolIntelligentWeb.ServiceLayer.Interfaces;
using SchoolIntelligentWeb.ServiceLayer.Services;
using SchoolIntelligentWeb.Utilities;

namespace SchoolIntelligentWeb.ServiceLayer.Filter
{
    public class LoginUser : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ILogin _Login = new LoginService();
            if (filterContext.HttpContext.Request.Cookies["Username"] != null && filterContext.HttpContext.Request.Cookies["Password"] != null && filterContext.HttpContext.Request.Cookies["Role"] != null)
            {
                string username = filterContext.HttpContext.Request.Cookies["Username"].Value;
                string password = filterContext.HttpContext.Request.Cookies["Password"].Value;
                int role = Convert.ToInt32(filterContext.HttpContext.Request.Cookies["Role"].Value);
                User user = new User()
                {
                    NationalCode = username,
                    Password = password,
                    Roll = role
                };
                LoginInfo SelectedUser = _Login.GetLoginInfo(user.NationalCode, user.Password.Hashmd5(), user.Roll);
                if (SelectedUser != null)
                {
                    if (user.Roll == 1)
                    {
                        filterContext.Result = new RedirectToRouteResult(
              new RouteValueDictionary(
                  new
                  {
                      controller = "Teacher",
                      action = "Index"
                  })
              );
                    }
                    else if (user.Roll == 2)
                    {
                        filterContext.Result = new RedirectToRouteResult(
              new RouteValueDictionary(
                  new
                  {
                      controller = "Panel",
                      action = "Index"
                  })
              );
                    }
                    else if (user.Roll == 3)
                    {
                        filterContext.Result = new RedirectToRouteResult(
                               new RouteValueDictionary(
                                    new
                                         {
                                      controller = "Parent",
                                      action = "Index"
                                         })
                                        );
                    }
                }

            }

        }
    }
}
