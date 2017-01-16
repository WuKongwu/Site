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

         [Authentication]
         public ActionResult Detele(int id)
         {
             UserRecordBLL bll = new UserRecordBLL();
             bool b = bll.DeteleUserRecord(id);
             return Json(new { success = b }, JsonRequestBehavior.AllowGet);
         }

         public ActionResult GetUsersById(int id)
         {
             UserRecordBLL bll = new UserRecordBLL();
             var model = bll.GetUserRecordById(id);
             return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
         }

         [Authentication]
         public ActionResult Update(UserRecord userRecord)
         {
             UserRecordBLL bll = new UserRecordBLL();
             bool b = bll.Update(userRecord);
             return Json(new { success = b }, JsonRequestBehavior.AllowGet);
         }

    }
}