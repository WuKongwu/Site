using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class AdminLoginController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Admin/AdminLogin.cshtml");
        }

        public JsonResult User(string name,string password)
        {
            string ReName = ConfigurationManager.AppSettings["adminUserName"].ToString();
            string RePassword = ConfigurationManager.AppSettings["adminUserPassword"].ToString();
            if (ReName == name && RePassword == password)
            {
                Session["adminUser"] = ReName;
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LogOut() {
            Session["adminUser"] = null;
            return RedirectToAction("Index", "AdminLogin");

        }
        
    }
}
