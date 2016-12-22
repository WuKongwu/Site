using eUI.DAL.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class PayPaperDAL
    {
        public int CheckPayInfoForBusiness(string Code, string UserGuid)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from business  Where OrderNumber='{0}' and userid='{1}'", Code, UserGuid);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }

        public string SearchPaperPath(string Code)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from paper  Where code='{0}'", Code);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());
            string fileName = string.Empty;
            if (dtBusiness.Rows.Count > 0) {
                fileName = dtBusiness.Rows[0]["FileUrl"].ToString();
            }
            return fileName;
        }
    }
}
