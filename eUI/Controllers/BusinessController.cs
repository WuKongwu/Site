using eUI.BLL;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class BusinessController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchPay(Business business)
        {
            BusinessBLL bll = new BusinessBLL();
            BusinessList businessList = bll.getBusinessList(business);
            return Json(businessList, JsonRequestBehavior.AllowGet);
        }
    }
}
