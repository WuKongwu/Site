using eUI.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class PayController : Controller
    {
        public void PayCallBack() { 
        
        
        }
        

        public ActionResult SearchPayPaper(string Code, string UserGuid)
        {
            try
            {
                PayPaperBLL payPaperBLL = new PayPaperBLL();
                bool result = payPaperBLL.CheckPayInfoForBusiness(Code, UserGuid);
                if (result)
                {
                    string fileName = payPaperBLL.SearchPaperPath(Code);
                    DownLoadPaper(fileName, Code);
                    return new EmptyResult();
                }
                else
                {
                    return null;
                }
            }
            catch {
                return View("Error");
            }
            
        
        }

        //private bool RecordBusinessInfo(string num) { 
        
        
        //}

        private void DownLoadPaper(string fileName,string code)
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
