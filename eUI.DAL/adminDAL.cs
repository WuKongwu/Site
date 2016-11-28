using eUI.DAL.DBUtility;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class adminDAL
    {

        public DataTable getPaperList(PaperList paperList)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from paper Where 1=1 ");

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }


        public int GetId()
        {
            string getId = "SELECT LAST_INSERT_ID()";
            DataTable dtUserInfo = DBHelper.SearchSql(getId);
            return 0;
        }

        public bool Input(PaperInfo paperInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"INSERT INTO PAPER (Title,Info,DetailInfo,Price,CreateDate,Type) 
                VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", paperInfo.Title, paperInfo.Info, paperInfo.DetailInfo, paperInfo.Price, DateTime.Now, paperInfo.Type);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool DetelePaper(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from PAPER where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool UpdatePaper(PaperInfo paperInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update PAPER set Title='{0}',Info ='{1}',DetailInfo ='{2}',Price='{3}',Type ='{4}' where Id ='{5}'
               ", paperInfo.Title, paperInfo.Info, paperInfo.DetailInfo, paperInfo.Price,paperInfo.Type,paperInfo.Id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetPaperId(int id)
        {
            string str = string.Format("select * from PAPER where Id ={0}", id);

            return DBHelper.SearchSql(str);
        }
    }
}
