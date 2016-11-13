using eUI.BLL;
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
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchPaper(PaperList paperList)
        {
            AdminBLL adminBLL = new AdminBLL();
            PaperInfoList paperInfoList = adminBLL.getPaperList(paperList);
            return Json(paperInfoList, JsonRequestBehavior.AllowGet);
        }


        public ViewResult Input(PaperInfo paperInfo)
        {
            var file1 = Request["img1"];
            var file2 = Request["img2"];
            var file3 = Request["img3"];
            var file4 = Request["img4"];
            var file5 = Request["img5"];

            var video = Request["video"];


            ViewBag.msg = "";

            return View("Input");
        }
        public ActionResult Save()
        {
            return Redirect("Index");
        }
        public ActionResult Uploadfile()
        {
            return Json(new { });
        }

    }
}
