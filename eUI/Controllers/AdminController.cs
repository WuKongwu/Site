using eUI.BLL;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchPaper(PaperList paperList)
        {
            AdminBLL adminBLL = new AdminBLL();
            PaperInfoList paperInfoList = adminBLL.getPaperList(paperList);
            return Json(paperInfoList, JsonRequestBehavior.AllowGet);
        }


        public ViewResult Input(PaperInfo paperInfo)
        {
            var file1 = Request["img1"];
            var file2 = Request["img2"];
            var file3 = Request["img3"];
            var file4 = Request["img4"];
            var file5 = Request["img5"];

            var video = Request["video"];


            ViewBag.msg = "";

            return View("Input");
        }
        public ActionResult Save(PaperInfo paperInfo)
        {
            AdminBLL admin = new AdminBLL();
           
            bool b = admin.SavePaper(paperInfo);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPaperById(int id)
        {
            AdminBLL admin = new AdminBLL();
            var model = admin.GetPaperById(id);
            return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detele(int id)
        {
            AdminBLL admin = new AdminBLL();
            bool b = admin.DetelePaper(id);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Uploadfile()//HttpContext context
        {
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string fileName = string.Empty;
            string imgPath = "";
            string strGuid = Guid.NewGuid().ToString().Replace("-", "");
            if (hfc.Count > 0)
            {
                for (int i = 0; i < hfc.Count; i++)
                {
                    var _strfileName = hfc[0].FileName.Substring(hfc[0].FileName.LastIndexOf(".") + 1);
                    if (_strfileName.ToLower() == "zip" || _strfileName.ToLower() == "rar")
                    {
                   
                        imgPath =  hfc[0].FileName;
                        string PhysicalPath = Server.MapPath("/fileData/"+ strGuid + "/");
                        fileName = imgPath;
                        var url = PhysicalPath + imgPath;
                        if (!Directory.Exists(PhysicalPath))
                        {
                            Directory.CreateDirectory(PhysicalPath);
                        }
                        hfc[0].SaveAs(url);
                        //return Json(new { Id = imgPath, guid = strGuid }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        imgPath = DateTime.Now.ToString("yyyyMMddHHmmssff") + hfc[0].FileName;
                        string PhysicalPath = Server.MapPath("/TemImg/" + imgPath);
                        fileName = imgPath;
                        hfc[0].SaveAs(PhysicalPath);
                    }
                   
                }
            }
            return Json(new { Id = nvc.Get("Id"), name = nvc.Get("name"), imgPath1 = fileName ,guid = strGuid }, "text/html", JsonRequestBehavior.AllowGet);
        
        }
        public ActionResult MultiUpload(HttpPostedFileBase files)
        {
            if (files != null)
            {
                if (files.ContentLength > 0)
                {
                    string filePath = files.FileName;      //获得文件的完整路径名
                    //以年月日时分秒-毫秒将文件重新命名
                    string filename2 = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fffffff");
                    string filename = filename2 + filePath.Substring(filePath.LastIndexOf('.'), filePath.Length - filePath.LastIndexOf('.'));
                    //设定上传路径（绝对路径）
                    string upPath = Server.MapPath("~/Uploads/") + filename;
                    //文件上传到绝对路径       
                    files.SaveAs(upPath);
                    //设定数据库的存储路径
                    string savePath = "//Uploads//" + filename;
                    //CreateImg(upPath, filename2);
                    ////下面是把相对路径保存到数据库表中
                    //Information info = new Information();
                    //files = Request.Files["files"];
                    //info.title = files.FileName;
                    //info.content = savePath;
                    //db.Information.Add(info);
                    //db.SaveChanges();
                }
            }
            return RedirectToAction("videoIndex");
        }

        public ActionResult UploadVideo(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];

           // string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"] + "\\");
            string uploadPath = Server.MapPath("~/Uploads/") + "Video";
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string name = DateTime.Now.ToString("yyyyMMddhhmmss") + System.IO.Path.GetExtension(file.FileName).ToLower();
                file.SaveAs(uploadPath + name);

                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
            }
            return Json(new { sta = false, msg = "请上传文件！" });
            
        }

        //public ActionResult DownLoad(string code)
        //{
        //    return File(new byte);
        //}
    }

    public class uploadpic : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string pic = context.Request.QueryString["pic"];

            string[] arr = pic.Split('|');
            string sstr = "";
            UpLoadIMG st = new UpLoadIMG();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].IndexOf("http://") >= 0 || arr[i].IndexOf("https://") >= 0)
                {
                    string std = st.SaveUrlPics(arr[i], "../../uploads/image/");
                    if (std.Length > 0)
                    {
                        if (i == arr.Length - 1)
                            sstr += std;
                        else
                            sstr += std + "|";
                    }
                }
            }
            context.Response.Write(sstr);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class UpLoadIMG
    {
        public string SaveUrlPics(string imgurlAry, string path)
        {
            string strHTML = "";
            string dirPath = HttpContext.Current.Server.MapPath(path);

            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                dirPath += ymd + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + imgurlAry.Substring(imgurlAry.LastIndexOf("."));

                WebClient wc = new WebClient();
                wc.DownloadFile(imgurlAry, dirPath + newFileName);
                strHTML = ymd + "/" + newFileName;
            }
            catch (Exception ex)
            {
                //return ex.Message;
            }
            return strHTML;
        }
    }
}
