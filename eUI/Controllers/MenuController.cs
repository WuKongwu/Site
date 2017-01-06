using eUI.BLL;
using eUI.Model.Enum;
using eUI.Model.ViewModel;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace easyUITest.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.MenuType = Enum.GetValues(typeof(MenuType)).Cast<MenuType>();
            return View();
        }

        public ActionResult SearchMenu(MenuViewModel model)
        {
            model.page = int.Parse(Request["page"]);
            model.rows = int.Parse(Request["rows"]);
            MenuBLL footbll = new MenuBLL();
            MenuList list = footbll.getList(model);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save(MenuViewModel mode)
        {
            MenuBLL bll = new MenuBLL();
            bool b = bll.SaveMenu(mode);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadImg(string oldFileName)
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
                    string PhysicalPath = Server.MapPath("/Menu/");
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
        public ActionResult Delete(int id, string fileName)
        {
            MenuBLL bll = new MenuBLL();
            bool b = bll.DeteleMenu(id);
            string PhysicalPath = Server.MapPath("/Menu/");
            deleteFile(PhysicalPath + fileName);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMenuById(int id)
        {
            MenuBLL bll = new MenuBLL();
            var model = bll.GetMenuById(id);
            return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
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
