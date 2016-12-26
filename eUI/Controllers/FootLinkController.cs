using eUI.BLL;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class FootLinkController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchFootLink(FootLinkViewModel model)
        {
            model.page = int.Parse(Request["page"]);
            model.rows = int.Parse(Request["rows"]);
            FootLinkBLL footbll = new FootLinkBLL();
           FootLinkList list =  footbll.getList(model);
           return Json(list,JsonRequestBehavior.AllowGet);
        }

    }
}
