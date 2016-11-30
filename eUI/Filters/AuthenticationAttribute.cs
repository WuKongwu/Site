using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace easyUITest.Filters
{
    // 登录认证特性
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["adminUser"] == null)
                filterContext.Result = new RedirectToRouteResult("AdminLogin", new RouteValueDictionary { { "Index", "" } },false);

            base.OnActionExecuting(filterContext);
        }
    }
}