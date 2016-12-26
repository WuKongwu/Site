using eUI.BLL;
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
        public ActionResult Save(FootLinkViewModel mode)
        {
            FootLinkBLL bll = new FootLinkBLL();
            bool b = bll.SaveFootLink(mode);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadImg()
        {
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string fileName = string.Empty;
            string imgPath = "";
            string strGuid = Guid.NewGuid().ToString("D");
            //if (!string.IsNullOrEmpty(guid))
            //{
            //    strGuid = guid;
            //}
            if (hfc.Count > 0)
            {
                for (int i = 0; i < hfc.Count; i++)
                {
                    var _strfileName = hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf(".") + 1);
                    if (_strfileName.ToLower() == "jpg" || _strfileName.ToLower() == "png")
                    {

                        imgPath = hfc[0].FileName;
                        string PhysicalPath = Server.MapPath("/FootLink/" + strGuid + "/");
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
                        string PhysicalPath = Server.MapPath("/FootLink/" + strGuid + "/");
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
            FootLinkBLL bll = new FootLinkBLL();
            bool b = bll.DeteleFootLink(id);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFootLinkById(int id)
        {
            FootLinkBLL bll = new FootLinkBLL();
            var model = bll.GetFootLinkById(id);
            return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
        }
    }
}
