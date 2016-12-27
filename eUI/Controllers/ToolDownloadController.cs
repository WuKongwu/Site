using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using eUI.BLL;
using eUI.Model.ViewModel;
using System.Collections.Specialized;
using System.Web;
using System.IO;

namespace easyUITest.Controllers
{
    public class ToolDownloadController : Controller
    {
        [Authentication]
        public ActionResult Index()
        {
            return View();
        }

        [Authentication]
        public ActionResult SearchTool(ToolDownloadInfo toolDownloadInfo)
        {
            toolDownloadInfo.page = int.Parse(Request["page"]);
            toolDownloadInfo.rows = int.Parse(Request["rows"]);
            ToolDownloadList toolDownloadList = new ToolDownloadList();
            ToolDownloadBLL toolDownloadBLL = new ToolDownloadBLL();
            toolDownloadList = toolDownloadBLL.getToolDownloadList(toolDownloadInfo);
            return Json(toolDownloadList, JsonRequestBehavior.AllowGet);
        }

        [Authentication]
        public ActionResult Save(ToolDownloadInfo toolDownloadInfo)
        {
            ToolDownloadBLL toolDownloadBLL = new ToolDownloadBLL();

            bool b = toolDownloadBLL.SaveTool(toolDownloadInfo);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetToolById(int id)
        {
            ToolDownloadBLL toolDownloadBLL = new ToolDownloadBLL();
            var model = toolDownloadBLL.GetToolById(id);
            return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
        }
        [Authentication]
        public ActionResult Detele(int id)
        {
            ToolDownloadBLL toolDownloadBLL = new ToolDownloadBLL();
            bool b = toolDownloadBLL.DeteleTool(id);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        [Authentication]
        public ActionResult Uploadfile()//HttpContext context
        {
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string fileName = string.Empty;
            string imgPath = "";
            
            if (hfc.Count > 0)
            {
                for (int i = 0; i < hfc.Count; i++)
                {
                    var _strfileName = hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf(".") + 1);

                    imgPath = DateTime.Now.ToString("yyyyMMddHHmmssff") + hfc[0].FileName;
                    string PhysicalPath = Server.MapPath("/ToolImgData/");
                    fileName = imgPath;
                    var url = PhysicalPath + imgPath;
                    if (!Directory.Exists(PhysicalPath))
                    {
                        Directory.CreateDirectory(PhysicalPath);
                    }
                    hfc[0].SaveAs(url);
                }
            }
            return Json(new { Id = nvc.Get("Id"), name = nvc.Get("name"), imgPath1 = fileName}, "text/html", JsonRequestBehavior.AllowGet);

        }

    }
}
