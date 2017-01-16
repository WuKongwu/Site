using eUI.DAL.DBUtility;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class AdminRecordDAL
    {

        public DataTable getAdminList(UserRecord userRecord)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select *  from  userrecord  Where AdminState='1'");
            if (!string.IsNullOrEmpty(userRecord.Name))
            {
                sbSI.AppendFormat(" AND Name LIKE '%{0}%'", userRecord.Name);
            }
            else if (!string.IsNullOrEmpty(userRecord.Email))
            {
                sbSI.AppendFormat(" AND Email LIKE '%{0}%'", userRecord.Email);
            }
            sbSI.AppendFormat(" limit {0},{1};", (userRecord.page - 1) * userRecord.rows, userRecord.rows);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }
        public int getAdminCount(UserRecord userRecord)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select *  from  userrecord  Where AdminState='1'");
            if (!string.IsNullOrEmpty(userRecord.Name))
            {
                sbSI.AppendFormat(" AND Name LIKE '%{0}%'", userRecord.Name);
            }
            else if (!string.IsNullOrEmpty(userRecord.Email))
            {
                sbSI.AppendFormat(" AND Email LIKE '%{0}%'", userRecord.Email);
            }

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }

        public bool DeteleAdmin(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from userrecord where AdminState='1' AND Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }



        public bool UpdateAdmin(UserRecord userRecord)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update userrecord set Name='{0}',Email ='{1}',Password='{2}'
                 where AdminState='1' AND Id ='{3}'", userRecord.Name, userRecord.Email, userRecord.Password, userRecord.Id);

            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool InputAdmin(UserRecord userRecord)
        {
            StringBuilder sbAddUser = new StringBuilder();
            userRecord.UserId = Guid.NewGuid().ToString("D");
            string GetSessionWithDsmId = string.Format(@"INSERT INTO userrecord 
                (UserId,Email,Name,Password,Flag,AdminState) 
                VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",
                 userRecord.UserId, userRecord.Email, userRecord.Name, userRecord.Password,1,1);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);

            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetAdminId(int id)
        {
            string str = string.Format("select * from  userrecord  where AdminState='1' AND Id ={0}", id);

            return DBHelper.SearchSql(str);
        }
    }
}
