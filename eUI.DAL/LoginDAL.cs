using eUI.DAL.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class LoginDAL
    {

        public int RegistUser(string password, string name, string email)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from userrecord where Email={0}",email);
            DataTable dt = DBHelper.SearchSql(sbSI.ToString());
            if (dt.Rows.Count == 0)
            {
                string userId = Guid.NewGuid().ToString("D");
                StringBuilder sbSIOne = new StringBuilder();
                sbSIOne.AppendFormat("INSERT userrecord count (UserId,Email,Name,Password,Flag) VALUES({0},{1},{2},{3},0)", userId, email, name, password);
                return  DBHelper.ExcuteNoQuerySql(sbSIOne.ToString());
            }
            else
            {
                return -1;
            }
        }
    }
}
