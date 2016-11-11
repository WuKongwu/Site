using eUI.BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class LoginController : Controller
    {
        public JsonResult UserLogin(string userId, string password)
        {

            return null;
        }
        
        public JsonResult UserRegist(string password, string name, string email)
        {
            if (string.IsNullOrEmpty(password)||checkPassword(password))
            {
                return Json(JsonResult("false","密码格式不正确！"));
                    
                 
            }
            else if (string.IsNullOrEmpty(name))
            {
                return Json(JsonResult("false", "昵称不能为空！"));
            }
            else if (string.IsNullOrEmpty(email) || !checkEmail(email))
            {
                return Json(JsonResult("false", "邮箱格式不正确！"));
            }
            password = Encrypt(password);
            LoginBLL loginBLL = new LoginBLL();
            int result=loginBLL.RegistUser(password, name, email);
            if (result==-1)
            {
                return Json(JsonResult("false", "邮箱已注册！"));
            }
            else if (result == 0) {
                return Json(JsonResult("false", "注册失败，请重新注册！"));
            }
            else
            {
                return Json(JsonResult("false", "注册成功请到注册邮箱激活登录！"));
            }
            
        }

        private bool checkEmail(string email)
        {
            String strExp = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex r = new Regex(strExp);
            Match m = r.Match(email);
            return m.Success;
        }
        private bool checkPassword(string password)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(password, "[A-Z]") ||
                System.Text.RegularExpressions.Regex.IsMatch(password, "[a-z]"))
            {
                if (password.Length > 6 && password.Length < 12) 
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            else
            {
                return false;
            }
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

        private string JsonResult(string result, string data) 
        {
            JObject JData = new JObject();
            JData.Add("Result", result);
            JData.Add("data", data);
            return JData.ToString();
        }
    }
}
