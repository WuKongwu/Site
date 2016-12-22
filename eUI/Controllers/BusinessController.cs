using easyUITest.Filters;
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
         [Authentication] 
        public ActionResult Index()
        {
            return View();
        }
         [Authentication] 
        public ActionResult SearchPay(Business business)
        {
            business.page = int.Parse(Request["page"]);
            business.rows = int.Parse(Request["rows"]); 
            BusinessBLL bll = new BusinessBLL();
            BusinessList businessList = bll.getBusinessList(business);
            return Json(businessList, JsonRequestBehavior.AllowGet);
        }
    }
}
