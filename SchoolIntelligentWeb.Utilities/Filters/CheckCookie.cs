using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolIntelligentWeb.Utilities.Filters
{
    public class CheckCookie : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["id"] == null || filterContext.HttpContext.Request.Cookies["Username"] == null || filterContext.HttpContext.Request.Cookies["Password"] == null || filterContext.HttpContext.Request.Cookies["Role"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary(
                   new
                   {
                       controller = "Home",
                       action = "Index"
                   })
               );
            }
            
        }
    }
}
