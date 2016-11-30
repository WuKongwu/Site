
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using eUI.Common;
using easyUITest.Filters;

namespace easyUITest.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
         [Authentication] 
        public ActionResult Index()
        {
            //return View(WebFiles.eUIIndex);
            return View();
        }
         [Authentication] 
        public ActionResult UserInfo()
        {
            return View(WebFiles.UserInfoIndex);
        }
         [Authentication] 
        public ActionResult ProductInfo()
        {
            return View(WebFiles.ProductIndex);
        }
         [Authentication] 
        public ActionResult UserReport()
        {
            return View(WebFiles.UserReport);
        }
         [Authentication] 
        public ActionResult UserColumnReport()
        {
            return View(WebFiles.UserColumnReport);
        }
         [Authentication] 
        public ActionResult UserLineReport()
        {
            return View(WebFiles.UserLineReport);
        }
         [Authentication] 
        public ActionResult SubPage()
        {
            return View(WebFiles.SubPage);
        }

        //public ActionResult UserBoard()
        //{
        //    return View(WebFiles.UserBoard);
        //}
         [Authentication] 
        public ActionResult WxPage()
        {
            return View(WebFiles.WxPage);
        }

    }
}
