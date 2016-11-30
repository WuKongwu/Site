using easyUITest.Filters;
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
    public class SubPageController : Controller
    {
        //public ActionResult Index()
        //{
        //    SubPageBLL subPageBLL = new SubPageBLL();
        //    var model = subPageBLL.SearchTemplate();
        //    return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
        //}
         [Authentication] 
        public ActionResult Index()
        {
            return View();
        }
         [Authentication] 
        public ActionResult SearchTemplate()
        {
            SubPageBLL subPageBLL = new SubPageBLL();
            PaperSubPageList list = subPageBLL.SearchTemplate();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
         [Authentication] 
        public ActionResult GetSubPageById(string id)
        {
            SubPageBLL subPageBLL = new SubPageBLL();
            PaperSubPageList list = subPageBLL.SearchTemplateById(id);
            return Json(new { success = true, models = list }, JsonRequestBehavior.AllowGet);
        }
         [Authentication] 
        public ActionResult Save(PaperSubPage param)
        {
            SubPageBLL subPageBLL = new SubPageBLL();

            bool b = subPageBLL.SaveTemplate(param);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
    }
}
