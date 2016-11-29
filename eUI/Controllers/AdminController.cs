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
            List<string> imgList = paperInfo.imgPath.Split('|').ToList();
            string imgStr = "";
            foreach (var item in imgList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    imgStr += System.IO.Path.GetFileName(item) + ",";
                }
            }
            imgStr = imgStr.Substring(0, imgStr.Length - 1);
            paperInfo.imgPath = imgStr;
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
            if (hfc.Count > 0)
            {
                for (int i = 0; i < hfc.Count; i++)
                {
                    imgPath = DateTime.Now.ToString("yyyyMMddHHmmssff") + hfc[0].FileName;
                    string PhysicalPath = Server.MapPath("/Img/" + imgPath);
                    fileName = imgPath;

                    hfc[0].SaveAs(PhysicalPath);
                }
                //imgPath = "/testUpload" + hfc[0].FileName;
                //string PhysicalPath = Server.MapPath(imgPath);
                //hfc[0].SaveAs(PhysicalPath);
            }
            //注意要写好后面的第二第三个参数
            return Json(new { Id = nvc.Get("Id"), name = nvc.Get("name"), imgPath1 = fileName }, "text/html", JsonRequestBehavior.AllowGet);
            //context.Response.ContentType = "text/plain";
            //string pic = context.Request.QueryString["pic"];

            //string[] arr = pic.Split('|');
            //string sstr = "";
            //UpLoadIMG st = new UpLoadIMG();
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    if (arr[i].IndexOf("http://") >= 0 || arr[i].IndexOf("https://") >= 0)
            //    {
            //        string std = st.SaveUrlPics(arr[i], "../../uploads/image/");
            //        if (std.Length > 0)
            //        {
            //            if (i == arr.Length - 1)
            //                sstr += std;
            //            else
            //                sstr += std + "|";
            //        }
            //    }
            //}
            //context.Response.Write(sstr);
            //string fileName = System.IO.Path.GetFileName(upImg.FileName);
            //string filePhysicalPath = Server.MapPath("~/upload/" + fileName);
            //string pic = "", error = "";
            //try
            //{
            //    upImg.SaveAs(filePhysicalPath);
            //    pic = "/upload/" + fileName;
            //}
            //catch (Exception ex)
            //{
            //    error = ex.Message;
            //}
            //return Json(new
            //{
            //    pic = pic,
            //    error = error
            //});
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
