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
            string str = string.Format("select * from subpage");

            return DBHelper.SearchSql(str);
        }

        public DataTable SearchSubPageByIdDAL(string id)
        {
            string str = string.Format("select * from subpage where id="+id);

            return DBHelper.SearchSql(str);
        }


        public bool UpdateTemplate(PaperSubPage paperSubPage)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update subpage set DevelopmentToolPage='{0}',TemplatePage ='{1}' where Id ='{2}'",
                paperSubPage.DevelopmentToolPage, paperSubPage.TemplatePage, paperSubPage.Id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool Input(PaperSubPage paperSubPage)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"INSERT INTO subpage 
                (DevelopmentToolPage,TemplatePage) 
                VALUES('{0}','{1}')", paperSubPage.DevelopmentToolPage, paperSubPage.TemplatePage);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }
    }
}
