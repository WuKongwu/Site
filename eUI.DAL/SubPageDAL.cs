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
    public class SubPageDAL
    {
        public DataTable SearchSubPageDAL()
        {
            string str = string.Format("select * from subpageone");

            return DBHelper.SearchSql(str);
        }

        public DataTable SearchSubPageByIdDAL(string id)
        {
            string str = string.Format("select * from subpageone");

            return DBHelper.SearchSql(str);
        }


        public bool UpdateTemplate(PaperSubPage paperSubPage)
        {
            StringBuilder sbAddUser = new StringBuilder();
            sbAddUser =sbAddUser.Append("update subpageone set ");
            if (!string.IsNullOrEmpty(paperSubPage.payguide)) {
                sbAddUser = sbAddUser.Append("payguide='" + paperSubPage.payguide + "'");
            }
            else if (!string.IsNullOrEmpty(paperSubPage.creditguarantee)) {
                sbAddUser = sbAddUser.Append("creditguarantee='" + paperSubPage.creditguarantee + "'");
            }
            else if (!string.IsNullOrEmpty(paperSubPage.aboutus))
            {
                sbAddUser = sbAddUser.Append("aboutus='" + paperSubPage.aboutus + "'");
            }
            sbAddUser = sbAddUser.Append(" where id ="+paperSubPage.id);

            int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            if (iResult == 1)
                return true;
            else
                return false;
        }

        
    }
}
