using eUI.DAL.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.Model.ViewModel;

namespace eUI.DAL
{
   public class MenuDAL
    {
        public System.Data.DataTable getList(Model.ViewModel.MenuViewModel model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from  Menu  Where 1=1 ");
            if (!string.IsNullOrEmpty(model.MenuName))
            {
                sbSI.AppendFormat(" and Name like '%{0}%'", model.MenuName);
            }
            sbSI.AppendFormat(" limit {0},{1};", (model.page - 1) * model.rows, model.rows);
            DataTable dtMenu = DBHelper.SearchSql(sbSI.ToString());
            return dtMenu;
        }
        public int getCount(Model.ViewModel.MenuViewModel model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from Menu  Where 1=1 ");
            if (!string.IsNullOrEmpty(model.MenuName))
            {
                sbSI.AppendFormat(" and MenuName like '%{0}%'", model.MenuName);
            }
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());
            return dtBusiness.Rows.Count;
        }
        public bool DeteleMenu(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from Menu where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }
        public bool UpdateMenu(MenuViewModel mode)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update Menu set MenuName='{0}',Type ='{1}',ChildImageUrl ='{2}',
                OrderIndex ='{3}'  where Id ='{4}'
               ", mode.MenuName, mode.Type, mode.ChildImageURL, mode.OrderIndex, mode.Id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetMenuId(int id)
        {
            string str = string.Format("select * from  Menu  where Id ={0}", id);
            return DBHelper.SearchSql(str);
        }

        public bool InsertMenu(MenuViewModel mode)
        {
            StringBuilder sbAddUser = new StringBuilder();
          
            string GetSessionWithDsmId = string.Format(@"INSERT INTO Menu 
                (MenuName,Type,ChildImageURL,OrderIndex) 
                VALUES('{0}','{1}','{2}','{3}')",
                 mode.MenuName, mode.Type, mode.ChildImageURL, mode.OrderIndex);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);

            if (iResult == 1)
                return true;
            else
                return false;
        }
    }
}
