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
            sbSI.Append("select * from userrecord where AdminState='0' ");
            if (!string.IsNullOrEmpty(userRecord.Email))
            {
                sbSI.AppendFormat("AND Email like '%{0}%' ",userRecord.Email);
            }
            else if (!string.IsNullOrEmpty(userRecord.Name))
            {
                sbSI.AppendFormat("AND NAME like '%{0}%' ", userRecord.Name);
            }
            sbSI.AppendFormat(" limit {0},{1};", (userRecord.page - 1) * userRecord.rows, userRecord.rows);
            DataTable dtUserInfo = DBHelper.SearchSql(sbSI.ToString());

            return dtUserInfo;

        }
        public int getCount(UserRecord userRecord)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from userrecord where AdminState='0' ");
            if (!string.IsNullOrEmpty(userRecord.Email))
            {
                sbSI.AppendFormat("  where Email like '%{0}%'", userRecord.Email);
            }

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }

        public bool DeteleUserRecord(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from userrecord where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool UpdateUserRecord(UserRecord userRecord)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update userrecord set Email ='{0}',Password='{1}'
                 where AdminState='0' AND Id ='{2}'", userRecord.Email, userRecord.Password, userRecord.Id);

            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }
        public DataTable GetUserRecordById(int id)
        {
            string str = string.Format("select * from  userrecord  where AdminState='0' AND  Id ={0}", id);
            return DBHelper.SearchSql(str);
        }
    }
}
