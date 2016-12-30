using eUI.BLL;
using eUI.Model.Enums;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class ImageManageController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ImagePosition = Enum.GetValues(typeof(ImagePosition)).Cast<ImagePosition>();
            return View();
        }
        public ActionResult SearchImage(ImageManageViewModel model)
        {
            model.page = int.Parse(Request["page"]);
            model.rows = int.Parse(Request["rows"]);
            ImageManageBLL bll = new ImageManageBLL();
            ImageManageList list = bll.getList(model);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save(ImageManageViewModel mode)
        {
            ImageManageBLL bll = new ImageManageBLL();
            string msg = string.Empty;
            bool b = bll.Save(mode, out msg);
            return Json(new { success = b,message = msg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadImg()
        {
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string fileName = string.Empty;
            string imgPath = "";
            string strGuid = Guid.NewGuid().ToString("D");
            if (hfc.Count > 0)
            {
                for (int i = 0; i < hfc.Count; i++)
                {
                    var _strfileName = hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf(".") + 1);
                    if (_strfileName.ToLower() == "jpg" || _strfileName.ToLower() == "png")
                    {

                        imgPath = hfc[0].FileName;
                        string PhysicalPath = Server.MapPath("/ImageManage/" + strGuid + "/");
                        fileName = imgPath;
                        var url = PhysicalPath + imgPath;
                        if (!Directory.Exists(PhysicalPath))
                        {
                            Directory.CreateDirectory(PhysicalPath);
                        }
                        hfc[0].SaveAs(url);
                    }
                    else
                    {
                        imgPath = DateTime.Now.ToString("yyyyMMddHHmmssff") + hfc[0].FileName;
                        string PhysicalPath = Server.MapPath("/ImageManage/" + strGuid + "/");
                        fileName = imgPath;
                        var url = PhysicalPath + imgPath;
                        if (!Directory.Exists(PhysicalPath))
                        {
                            Directory.CreateDirectory(PhysicalPath);
                        }
                        hfc[0].SaveAs(url);
                    }
                }
            }
            return Json(new { Id = nvc.Get("Id"), name = nvc.Get("name"), imgPath1 = fileName, guid = strGuid }, "text/html", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Delete(int id)
        {
            ImageManageBLL bll = new ImageManageBLL();
            bool b = bll.Detele(id);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetById(int id)
        {
            ImageManageBLL bll = new ImageManageBLL();
            var model = bll.GetById(id);
            return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
        }
    }
}
