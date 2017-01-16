using eUI.BLL;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class AdminRecordController : Controller
    {

        [Authentication]
        public ActionResult Index()
        {
            return View();
        }
        [Authentication]
        public ActionResult SearchAdmin(UserRecord userRecord)
        {
            userRecord.page = int.Parse(Request["page"]);
            userRecord.rows = int.Parse(Request["rows"]);
            AdminRecordBLL adminRecordBLL = new AdminRecordBLL();
            UserRecordList userRecordList = adminRecordBLL.getAdminList(userRecord);
            return Json(userRecordList, JsonRequestBehavior.AllowGet);
        }
        [Authentication]
        public ActionResult Save(UserRecord userRecord)
        {
            AdminRecordBLL adminRecordBLL = new AdminRecordBLL();
            //userRecord.Password = Encrypt(userRecord.Password);
            bool b = adminRecordBLL.SaveAdmin(userRecord);
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        [Authentication]
        public ActionResult GetAdminRecordById(int id)
        {
            AdminRecordBLL adminRecordBLL = new AdminRecordBLL();
            var model = adminRecordBLL.GetAdminById(id);
            return Json(new { success = true, models = model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detele(int id)
        {
            AdminRecordBLL adminRecordBLL = new AdminRecordBLL();
            bool b = adminRecordBLL.DeteleAdmin(id);
         
            return Json(new { success = b }, JsonRequestBehavior.AllowGet);
        }
        private string Encrypt(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(strPwd);//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
    }
}
