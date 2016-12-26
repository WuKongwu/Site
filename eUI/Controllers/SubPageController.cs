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

        public ActionResult OnePage()
        {
            SubPageBLL subPageBLL = new SubPageBLL();
            PaperSubPageList list = subPageBLL.SearchTemplate();
            return View(list);
        }
        [Authentication]
        public ActionResult TwoPage()
        {
            SubPageBLL subPageBLL = new SubPageBLL();
            PaperSubPageList list = subPageBLL.SearchTemplate();
            return View(list);
        }
        public ActionResult ThreePage()
        {
            SubPageBLL subPageBLL = new SubPageBLL();
            PaperSubPageList list = subPageBLL.SearchTemplate();
            return View(list);
        }


        [Authentication]
        [ValidateInput(false)]
        public ActionResult SaveOne(PaperSubPage param)
        {
            SubPageBLL subPageBLL = new SubPageBLL();

            bool b = subPageBLL.SaveTemplate(param);
            return RedirectToAction("OnePage");
        }
        [Authentication]
        [ValidateInput(false)]
        public ActionResult SaveTwo(PaperSubPage param)
        {
            SubPageBLL subPageBLL = new SubPageBLL();

            bool b = subPageBLL.SaveTemplate(param);
            return RedirectToAction("TwoPage");
        }
        [Authentication]
        [ValidateInput(false)]
        public ActionResult SaveThree(PaperSubPage param)
        {
            SubPageBLL subPageBLL = new SubPageBLL();

            bool b = subPageBLL.SaveTemplate(param);
            return RedirectToAction("ThreePage");
        }
    }
}
