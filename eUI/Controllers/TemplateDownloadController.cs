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
using eUI.Model;


namespace easyUITest.Controllers
{
    public class TemplateDownloadController : Controller
    {
        [Authentication]
        public ActionResult Index()
        {
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            TemplateTypeList templateTypeList = new TemplateTypeList();
            TemplateTypeInfo templateTypeInfo = new TemplateTypeInfo();
            templateTypeList = templateDownloadBLL.getTemplateTypeData();
            List<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "--",
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

            ViewData["selTmpType"] = new SelectList(select, "Value", "Text", "");
            return View();
        }

        public ActionResult Type()
        {
            return View();
        }


        [Authentication]
        public ActionResult SearchTmpType(TemplateTypeInfo templateTypeInfo)
        {
            templateTypeInfo.page = int.Parse(Request["page"]);
            templateTypeInfo.rows = int.Parse(Request["rows"]);
            TemplateTypeList templateTypeList = new TemplateTypeList();
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            templateTypeList = templateDownloadBLL.getTemplateTypeList(templateTypeInfo);
            return Json(templateTypeList, JsonRequestBehavior.AllowGet);
        }


        [Authentication]
        public ActionResult SearchTemplate(TemplateDownloadInfo templateDownloadInfo)
        {
            templateDownloadInfo.page = int.Parse(Request["page"]);
            templateDownloadInfo.rows = int.Parse(Request["rows"]);
            TemplateDownload templateDownload = new TemplateDownload();
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            templateDownload = templateDownloadBLL.getTemplateDownloadList(templateDownloadInfo);
            return Json(templateDownload, JsonRequestBehavior.AllowGet);
        }

        [Authentication]
        public ActionResult SaveTemplateDownload(TemplateDownloadInfo templateDownloadInfo)
        {
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();

            bool b = templateDownloadBLL.SaveTemplate(templateDownloadInfo);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }

        [Authentication]
        public ActionResult SaveTmpType(TemplateTypeInfo templateTypeInfo)
        {
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();

            bool b = templateDownloadBLL.SaveTmpType(templateTypeInfo);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTemplateById(int id)
        {
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            var model = templateDownloadBLL.GetTemplateById(id);
            return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTemplateTypeById(int id)
        {
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            var model = templateDownloadBLL.GetTemplateTypeById(id);
            return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
        }

        [Authentication]
        public ActionResult DeteleTemplateDownload(int id, string fileName)
        {
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            bool b = templateDownloadBLL.DeteleTemplate(id);
            string PhysicalPath = Server.MapPath("/TemplateData/");
            deleteFile(PhysicalPath + fileName);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeteleTemplateType(int id)
        {
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            bool b = templateDownloadBLL.DeteleTemplateType(id);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }

        [Authentication]
        public ActionResult Uploadfile(string oldFileName)//HttpContext context
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
                    imgPath = hfc[0].FileName;
                    string PhysicalPath = Server.MapPath("/TemplateData/");
                    fileName = imgPath;

                    if (!Directory.Exists(PhysicalPath))
                    {
                        Directory.CreateDirectory(PhysicalPath);
                    }
                    var url = PhysicalPath + imgPath;
                    if (System.IO.File.Exists(url))
                    {
                        fileName = GetNewPathForDupes(url);
                    }
                    hfc[0].SaveAs(PhysicalPath + fileName);
                    deleteFile(PhysicalPath + oldFileName);
                }
            }
            return Json(new { Id = nvc.Get("Id"), name = nvc.Get("name"), imgPath1 = fileName }, "text/html", JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddDownloadNum(int id)//HttpContext context
        {
            TemplateDownloadBLL templateDownloadBLL = new TemplateDownloadBLL();
            int num = templateDownloadBLL.AddDownloadNum(id);
            return Json(new { success = true, num = num }, JsonRequestBehavior.AllowGet);
        }

        private string GetNewPathForDupes(string path)
        {
            string directory = Path.GetDirectoryName(path);
            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            int counter = 1;
            string newFullPath;
            string newFilename;
            do
            {
                //string newFilename = "{0}({1}).{2}".FormatWith(filename, counter, extension);
                newFilename = string.Format("{0}({1}){2}", filename, counter, extension);
                newFullPath = Path.Combine(directory, newFilename);
                counter++;
            } while (System.IO.File.Exists(newFullPath));
            return newFilename;
        }
        private void deleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
