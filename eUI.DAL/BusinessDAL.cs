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
    public class BusinessDAL
    {
        public DataTable getList(Business business)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from business where 1=1");
            //if (!string.IsNullOrEmpty(business.Email))
            //{
            //    sbSI.AppendFormat("  where Email like '%{0}%'", userRecord.Email);
            //}
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }
    }
}
