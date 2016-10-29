using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class PaperController : Controller
    {
        public ViewResult Index() {

            return View("Home");
        }


        public ViewResult List()
        {
            return View("PaperList");
        }

        public ViewResult Detail()
        {
            return View("PaperDetail");
        }
    }
}
