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
            sbSI.Append("select * from business left join paper on business.PaperId=paper.Id Where 1=1 ");
            if (!string.IsNullOrEmpty(business.Name))
            {
                sbSI.AppendFormat("AND NAME LIKE '%{0}%'", business.Name);
            }
            else if (!string.IsNullOrEmpty(business.OrderNumber))
            {
                sbSI.AppendFormat("AND OrderNumber LIKE '%{0}%'", business.OrderNumber);
            }
            else if (!string.IsNullOrEmpty(business.Title))
            {
                sbSI.AppendFormat("AND Title LIKE '%{0}%'", business.Title);
            }
            else if (business.PayDateStart > DateTime.MinValue && business.PayDateEnd > DateTime.MinValue)
            {
                sbSI.AppendFormat("AND PayDate BETWEEN '{0}' AND '{1}'", business.PayDateStart, business.PayDateEnd);
            }
            sbSI.AppendFormat(" limit {0},{1};", (business.page - 1) * business.rows, business.rows);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }

        public int getCount(Business business)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from business left join paper on business.PaperId=paper.Id Where 1=1 ");
            if (!string.IsNullOrEmpty(business.Name))
            {
                sbSI.AppendFormat("AND NAME LIKE '%{0}%'", business.Name);
            }
            else if (!string.IsNullOrEmpty(business.OrderNumber))
            {
                sbSI.AppendFormat("AND OrderNumber LIKE '%{0}%'", business.OrderNumber);
            }
            else if (!string.IsNullOrEmpty(business.Title))
            {
                sbSI.AppendFormat("AND Title LIKE '%{0}%'", business.Title);
            }
            else if (business.PayDateStart > DateTime.MinValue && business.PayDateEnd > DateTime.MinValue)
            {
                sbSI.AppendFormat("AND PayDate BETWEEN '{0}' AND '{1}'", business.PayDateStart, business.PayDateEnd);
            }

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }
    }
}
