using eUI.DAL.DBUtility;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class UserRecordDAL
    {
        public DataTable getList(UserRecord userRecord)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from userrecord");
            if (!string.IsNullOrEmpty(userRecord.Email))
            {
                sbSI.AppendFormat("  where Email like '%{0}%'",userRecord.Email);
            }
            sbSI.AppendFormat(" limit {0},{1};", (userRecord.page - 1) * userRecord.rows, userRecord.rows);
            DataTable dtUserInfo = DBHelper.SearchSql(sbSI.ToString());

            return dtUserInfo;

        }
        public int getCount(UserRecord userRecord)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from userrecord");
            if (!string.IsNullOrEmpty(userRecord.Email))
            {
                sbSI.AppendFormat("  where Email like '%{0}%'", userRecord.Email);
            }

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }
    }
}
