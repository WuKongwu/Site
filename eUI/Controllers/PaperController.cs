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
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            return View("Home", paperInit);
        }


        public ViewResult List(string type, string flag)
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
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(type, flag);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Take(9).ToList<PaperInfo>();
            ViewData["type"] = type;
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            return View("PaperListV", paperListViewModel);
        }


        public ViewResult SearchList(string key, string type)
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
            if (string.IsNullOrEmpty(type))
            {
                ViewData["type"] = "";
            }
            else
            {
                ViewData["type"] = type;

            }
            ViewData["key"] = key;
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
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

            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(type, null);
            paperListViewModel.paperInfoList.total = paperListViewModel.paperInfoList.rows.Count();
            paperListViewModel.paperInfoList.rows = paperListViewModel.paperInfoList.rows.Skip((pageNo - 1) * 9).Take(9).ToList<PaperInfo>();

            return View("_paperListVPaging", paperListViewModel);
        }



        public ViewResult SearchListPaging(int pageNo, string key, string type)
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
            string userId = string.Empty;
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
                userId = list[0].UserId;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            paperBLL.AddReadCount(id);
            PaperDetailViewModel paperDetailViewModel = paperBLL.PaperDetailById(id, userId);
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            return View("PaperDetail", paperDetailViewModel);
        }

        public ViewResult TmpDetail(string id, string type)
        {
            string userId = string.Empty;
            if (Session["user"] != null)
            {
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                ViewData["login"] = list[0].Name;
                userId = list[0].UserId;
            }
            else
            {
                ViewData["login"] = string.Empty;
            }
            PaperBLL paperBLL = new PaperBLL();
            PaperDetailViewModel paperDetailViewModel = new PaperDetailViewModel();
            if (string.IsNullOrEmpty(type))
            {
                paperDetailViewModel = paperBLL.PaperDetailById(id, userId);
            }
            else
            {
                paperDetailViewModel = paperBLL.TmpDetailById(id);
            }
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            return View("TemplateDetail", paperDetailViewModel);
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
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null, null);
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            ViewBag.PayGuide = paperBLL.SearchPayGuide();
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
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null, null);
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            ViewBag.CreditGuarantee = paperBLL.SearchCreditGuarantee();
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
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null, null);
            paperListViewModel.paperToolPage.total = paperListViewModel.paperToolPage.rows.Count();
            paperListViewModel.paperToolPage.rows = paperListViewModel.paperToolPage.rows.Take(6).ToList<ToolDownloadInfo>();
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            return View("ToolDownload", paperListViewModel);
        }
        public ViewResult ToolListPaging(int pageNo)
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

            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null, null);
            paperListViewModel.paperToolPage.total = paperListViewModel.paperToolPage.rows.Count();
            paperListViewModel.paperToolPage.rows = paperListViewModel.paperToolPage.rows.Skip((pageNo - 1) * 6).Take(6).ToList<ToolDownloadInfo>();

            return View("_toolDownloadPaging", paperListViewModel);
        }


        public ViewResult TemplateDownload(string flag)
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
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null, flag);
            paperListViewModel.paperTmpPage.total = paperListViewModel.paperTmpPage.rows.Count();
            paperListViewModel.paperTmpPage.rows = paperListViewModel.paperTmpPage.rows.Take(6).ToList<TemplateDownloadInfo>();

            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            TemplateTypeList templateTypeList = new TemplateTypeList();
            templateTypeList = templateDownloadBLL.getTemplateTypeData();
            List<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "全部分类",
                Value = "0"
            });

            for (int i = 0; i < templateTypeList.rows.Count; i++)
            {
                select.Add(new SelectListItem
                {
                    Text = templateTypeList.rows[i].TemplateType.ToString(),
                    Value = templateTypeList.rows[i].Id.ToString()
                });
            }
            if (string.IsNullOrEmpty(flag))
            {
                ViewData["Tmptype"] = "0";
            }
            else
            {
                ViewData["Tmptype"] = flag;

            }
            ViewData["selTmpType"] = new SelectList(select, "Value", "Text", "");
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            return View("TemplateDownload", paperListViewModel);
        }


        public ViewResult TmpListPaging(int pageNo, string type, string flag)
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

            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null, flag);
            paperListViewModel.paperTmpPage.total = paperListViewModel.paperTmpPage.rows.Count();
            paperListViewModel.paperTmpPage.rows = paperListViewModel.paperTmpPage.rows.Skip((pageNo - 1) * 6).Take(6).ToList<TemplateDownloadInfo>();
            if (string.IsNullOrEmpty(flag))
            {
                ViewData["Tmptype"] = "0";
            }
            else
            {
                ViewData["Tmptype"] = flag;

            }
            return View("_tmplateDownloadPaging", paperListViewModel);
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
            PaperListViewModel paperListViewModel = paperBLL.PaperTypeList(null, null);
            ViewBag.ImageModel = paperBLL.SearchImgManage().rows;
            ViewBag.About = paperBLL.SearchAboutUs();
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
                string userId = string.Empty;
                List<UserRecord> list = (List<UserRecord>)Session["user"];
                userId = list[0].UserId;

                NativePay nativePay = new NativePay();
                PaperBLL paperBLL = new PaperBLL();
                PaperDetailViewModel paperDetailViewModel = paperBLL.PaperDetailById(paperInfo.Id.ToString(), userId);
                string _orderNumber = string.Empty;
                string url = nativePay.GetPayUrl(paperDetailViewModel.detail[0], out _orderNumber);
                if (string.IsNullOrEmpty(url))
                {
                    return Json(new { success = false, data = "微信生成订单时出现错误，请您重新支付！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    Business business = new Business();
                    List<UserRecord> rows = (List<UserRecord>)Session["user"];
                    business.UserId = rows[0].UserId;
                    business.Name = rows[0].Name;
                    business.CreateTime = DateTime.Now;
                    business.OrderNumber = _orderNumber;
                    business.PaperCode = paperDetailViewModel.detail[0].Code;
                    business.PaperId = paperDetailViewModel.detail[0].Id;
                    business.Title = paperDetailViewModel.detail[0].Title;
                    business.Version = paperDetailViewModel.detail[0].Version;
                    business.Price = paperDetailViewModel.detail[0].Price;
                    business.PayState = 0;
                    bool result = paperBLL.CreateBusiness(business);
                    if (result == true)
                    {
                        return Json(new { success = true, data = url, orderNumber = _orderNumber }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, data = "微信生成订单时出现错误，请您重新支付！" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

        }

    }
}
