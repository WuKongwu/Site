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
    public class PaperController : Controller
    {
        public ViewResult Index() {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperInit paperInit = paperBLL.PaperInit();
            return View("Home", paperInit);
        }


        public ViewResult List(string type)
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(type);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Take(9).ToList<PaperInfo>();
            ViewData["type"] = type;
            return View("PaperListV", paperListViewModel);
        }


        public ViewResult SearchList(string key)
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperSearchList(key);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Take(9).ToList<PaperInfo>();
            ViewData["type"] = "99";
            ViewData["key"] = key;
            return View("SearchList", paperListViewModel);
        }




        public ViewResult ListPaging(int pageNo,string type)
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();

            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(type);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Skip((pageNo - 1) * 9).Take(9).ToList<PaperInfo>();

            return View("_paperListVPaging", paperListViewModel);
        }



        public ViewResult SearchListPaging(int pageNo,string key)
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperSearchList(key);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Skip((pageNo - 1) * 9).Take(9).ToList<PaperInfo>();
            ViewData["type"] = "99";
            return View("_searchListPaging", paperListViewModel);
        }

       

        public ViewResult Detail(string id)
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            paperBLL.AddReadCount(id);
            PaperDetailViewModel paperDetailViewModel = paperBLL.PaperDetailById(id);
            return View("PaperDetail", paperDetailViewModel);
        }


        public ViewResult PayGuide() {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null);
            return View("PayGuide", paperListViewModel);
        }

        public ViewResult CreditGuarantee()
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null);
            return View("CreditGuarantee", paperListViewModel);
        }

        public ViewResult ToolDownload()
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null);
            return View("ToolDownload", paperListViewModel);
        }

        public ViewResult TemplateDownload()
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null);
            return View("TemplateDownload", paperListViewModel);
        }

        public ViewResult About()
        {
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null);
            return View("About", paperListViewModel);
        }

    }
}
