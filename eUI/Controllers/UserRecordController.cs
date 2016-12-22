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
    public class UserRecordController : Controller
    {
         [Authentication] 
       public ActionResult Index()
        {
            return View();
        }
         [Authentication] 
        public ActionResult SearchUsers(UserRecord userRecord)
        {
            userRecord.page = int.Parse(Request["page"]); 
            userRecord.rows = int.Parse(Request["rows"]); 
            UserRecordBLL bll = new UserRecordBLL();
            UserRecordList userRecordList = bll.getUserList(userRecord);
            return Json(userRecordList,JsonRequestBehavior.AllowGet);
        }
    }
}