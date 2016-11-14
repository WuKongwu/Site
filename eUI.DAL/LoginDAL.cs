using eUI.DAL.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class LoginDAL
    {

        public int RegistUser(string password, string name, string email)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from userrecord where Email='{0}'", email);
            DataTable dt = DBHelper.SearchSql(sbSI.ToString());
            if (dt.Rows.Count == 0)
            {
                string userId = Guid.NewGuid().ToString("D");
                StringBuilder sbSIOne = new StringBuilder();
                sbSIOne.AppendFormat("INSERT INTO userrecord  (UserId,Email,Name,Password,Flag) VALUES('{0}','{1}','{2}','{3}',0)", userId, email, name, password);
                int result = DBHelper.ExcuteNoQuerySql(sbSIOne.ToString());
                if (result == 1)
                {
                    if (!SendActivationEmail(userId, email))
                    {
                        result = 2;
                    }
                }
                return result;
            }
            else
            {
                return -1;
            }
        }


        public bool UpdateActivationFlag(string userId)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("update userrecord set flag=1 where UserId='{0}'", userId);
            int result = DBHelper.ExcuteNoQuerySql(sbSI.ToString());
            if (result == 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }


        public DataTable UserLogin(string password, string email)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from userrecord  where email='{0}' and password='{1}' and flag=1", email, password);
            DataTable dt = DBHelper.SearchSql(sbSI.ToString());
            return dt;
        }











        private bool SendActivationEmail(string userId, string email)
        {
            string emailSte = ConfigurationManager.AppSettings["emailSte"];
            string emailCode = ConfigurationManager.AppSettings["emailCode"];
            string SiteBaseUrl = ConfigurationManager.AppSettings["SiteBaseUrl"];
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(emailSte, "启源科技", System.Text.Encoding.UTF8); //发件人地址（可以随便写），发件人姓名，编码*/ 
            msg.Subject = "启源科技论文网站激活信息";//邮件标题 
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
            msg.Body = "<h2>感谢您注册启源科技</h2><p>请点击以下链接来完成网站激活,激活后方可登录</p><a href='" + SiteBaseUrl + "'>" + SiteBaseUrl + "</a>";//邮件内容 
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
            msg.IsBodyHtml = true;//是否是HTML邮件 
            msg.Priority = MailPriority.High;//邮件优先级 
            SmtpClient client = new SmtpClient();
            //发件邮箱和密码 ,注意这里要与发件人地址的邮箱一致
            client.Credentials = new System.Net.NetworkCredential(emailSte, emailCode);
            //client.Port = 25;//Gmail使用的端口587 
            //client.EnableSsl = true;//经过ssl加密
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "smtp.163.com";
            object userState = msg;
            //这里可以随便写什么，只要是规范的email地址
            msg.To.Add(email);
            try
            {
                /***************************/
                /*请注意SmtpClient类还有两个用于发送Email的方法：
                 * Send()和SendAsync()。Send()方法在它的发送操作完成前阻塞其它的程序执行；
                 * SendAsync()方法异步地发送Email。
                 * 不像Send()方法，SendAsync()方法不必等待检测发送操作是否成功。*/
                /***************************/
                //client.SendAsync(msg, userState);
                ////client.Send(msg);
                //Console.WriteLine("success!");
                System.Threading.Thread.Sleep(100);
                client.Send(msg);
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return false;
            }

        }
    }
}
