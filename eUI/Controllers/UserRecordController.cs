using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class UserRecordController : Controller
    {
       public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchUsers()
        {
            return Json(new { });
        }
    }
}