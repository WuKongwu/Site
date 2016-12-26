using eUI.DAL.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
   public class FootLinkDAL
    {
        public System.Data.DataTable getList(Model.ViewModel.FootLinkViewModel model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from  FootLink  Where 1=1 ");
            sbSI.AppendFormat(" limit {0},{1};", (model.page - 1) * model.rows, model.rows);
            DataTable dtFootLink = DBHelper.SearchSql(sbSI.ToString());
            return dtFootLink;
        }
        public int getCount(Model.ViewModel.FootLinkViewModel model)
        {
            return 0;
        }
    }
}
