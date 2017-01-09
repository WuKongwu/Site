using eUI.BLL;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WxPayAPI;

namespace easyUITest.Controllers
{
    public class PayController : Controller
    {
        public void PayCallBack()
        {
            System.Web.UI.Page page = new System.Web.UI.Page();
            ResultNotify resultNotify = new ResultNotify(page);
            resultNotify.ProcessNotify();

        }


        public ActionResult GetPaperDownloadUrl(string orderNumber)
        {
            try
            {
                if (Session["user"] == null)
                {
                    return Json(new { success = false, data = "您离开太久了，登录超时，请您重新登录！" }, JsonRequestBehavior.AllowGet);
                }
                List<UserRecord> rows = (List<UserRecord>)Session["user"];
                string userGuid = rows[0].UserId;
                PayPaperBLL payPaperBLL = new PayPaperBLL();

                PaperDetailViewModel paperDetailViewModel = payPaperBLL.GetPaperDownloadUrl(orderNumber, userGuid);
                if (paperDetailViewModel.detail.Count > 0)
                {
                    string fileName = paperDetailViewModel.detail[0].FileUrl;
                    string code = paperDetailViewModel.detail[0].Code;
                    if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(code))
                    {
                        return Json(new { success = "NO", data = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, fileName = fileName, code = code }, JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    return Json(new { success = "NO", data = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return View("Error");
            }
        }
       
        public void DownLoadPaper(string orderNumber)
        {
            try
            {
                if (Session["user"] != null)
                {
                    List<UserRecord> rows = (List<UserRecord>)Session["user"];
                    string userGuid = rows[0].UserId;
                    PayPaperBLL payPaperBLL = new PayPaperBLL();

                    PaperDetailViewModel paperDetailViewModel = payPaperBLL.GetPaperDownloadUrl(orderNumber, userGuid);
                    if (paperDetailViewModel.detail.Count > 0)
                    {
                        string fileName = paperDetailViewModel.detail[0].FileUrl;
                        string code = paperDetailViewModel.detail[0].Code;
                        if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(code))
                        {
                            string filePath = Server.MapPath("/fileData/" + code + "/" + fileName);
                            FileStream fs = new FileStream(filePath, FileMode.Open);
                            byte[] bytes = new byte[(int)fs.Length];
                            fs.Read(bytes, 0, bytes.Length);
                            fs.Close();
                            Response.Charset = "UTF-8";
                            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                            Response.ContentType = "application/octet-stream";

                            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }
                    }
                  
                }
            }
            catch {
                
            }
        }
    }
}
