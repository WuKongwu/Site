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
using WxPayAPI;

namespace easyUITest.Controllers
{
    public class PaperController : Controller
    {
        
        public ViewResult Index()
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


        public ViewResult SearchList(string key,string type)
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
            PaperListViewModel paperListViewModel = paperBLL.PaperSearchList(key, type);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Take(9).ToList<PaperInfo>();
            if (string.IsNullOrEmpty(type)) {
                ViewData["type"] = "";
            } else {
                ViewData["type"] = type;
            
            }
            ViewData["key"] = key;
            return View("SearchList", paperListViewModel);
        }
        


        public ViewResult ListPaging(int pageNo, string type)
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



        public ViewResult SearchListPaging(int pageNo, string key,string type)
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
            PaperListViewModel paperListViewModel = paperBLL.PaperSearchList(key,type);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Skip((pageNo - 1) * 9).Take(9).ToList<PaperInfo>();
            if (string.IsNullOrEmpty(type))
            {
                ViewData["type"] = "";
            }
            else
            {
                ViewData["type"] = type;

            }
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


        public ViewResult PayGuide()
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

        public JsonResult WXPayUrl(PaperInfo paperInfo)
        {

            if (Session["user"] == null)
            {
                return Json(new { success = false, data = "请您先登录，才能购买商品哦！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                NativePay nativePay = new NativePay();
                PaperBLL paperBLL = new PaperBLL();
                PaperDetailViewModel paperDetailViewModel = paperBLL.PaperDetailById(paperInfo.Id.ToString());
                string url = nativePay.GetPayUrl(paperDetailViewModel.detail[0]);
                if (string.IsNullOrEmpty(url))
                {
                    return Json(new { success = false, data = "微信生成订单时出现错误，请您重新支付！" }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(new { success = true, data = url }, JsonRequestBehavior.AllowGet); }
            }

        }

    }
}
