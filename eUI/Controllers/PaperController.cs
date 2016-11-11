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
    public class PaperController : Controller
    {
        public ViewResult Index() {

            PaperBLL paperBLL = new PaperBLL();
            PaperInit paperInit = paperBLL.PaperInit();
            return View("Home", paperInit);
        }


        public ViewResult List(string type)
        {
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(type);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Take(9).ToList<PaperInfo>();
            ViewData["type"] = type;
            return View("PaperListV", paperListViewModel);
        }

        public ViewResult ListPaging(int pageNo,string type)
        {
            PaperBLL paperBLL = new PaperBLL();

            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(type);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Skip((pageNo - 1) * 9).Take(9).ToList<PaperInfo>();

            return View("_paperListVPaging", paperListViewModel);
        }


        public ViewResult Detail(string id)
        {
            PaperBLL paperBLL = new PaperBLL();
            paperBLL.AddReadCount(id);
            PaperDetailViewModel paperDetailViewModel = paperBLL.PaperDetailById(id);
            return View("PaperDetail", paperDetailViewModel);
        }
    }
}
