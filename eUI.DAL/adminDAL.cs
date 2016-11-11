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
            string GetSessionWithDsmId = "INSERT INTO PAPER (ID,Title,Info,DetailInfo,Price,CreateDate,Type) VALUES(SEQ_CG_MR_MOD_DET.NEXTVAL,:CG_SESN_ID,:TAB_ID,:ATBT_ID,:ATBT_VALUE)";


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
