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
            ;
            string GetSessionWithDsmId = string.Format(@"INSERT INTO PAPER (Title,Info,DetailInfo,Price,CreateDate,Type) 
                VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",paperInfo.Title,paperInfo.Info,paperInfo.DetailInfo,paperInfo.Price,DateTime.Now,paperInfo.Type);


            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
