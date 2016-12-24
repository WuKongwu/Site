using eUI.BLL;
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
          LoginBLL loginBLL = new LoginBLL();
          int result=  loginBLL.AdminLogin(name, password);
          if (result==1)
            {
                Session["adminUser"] = name;
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
        public JsonResult ResetPws(string name, string Opassword, string Npassword, string ReNadminName)
        {
            LoginBLL loginBLL = new LoginBLL();
            int result = loginBLL.AdminResetPws(name, Opassword, Npassword, ReNadminName);
            if (result==1)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
