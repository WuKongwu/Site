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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction_id">订单号</param>
        /// <param name="out_trade_no">商品ID</param>
        /// <returns></returns>
        public int UpdateBusiness(string transaction_id, string out_trade_no)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("Update  business set PayState='1',PayDate='{0}',OrderNumber='{1}'  Where OutTradeNo='{2}'", DateTime.Now, transaction_id, out_trade_no);
            int result = DBHelper.ExcuteNoQuerySql(sbSI.ToString());
            return result;
        }

        public DataTable SearchPaperPath(string out_trade_no, string UserGuid)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("SELECT A.* FROM paper AS A LEFT JOIN business AS B ON A.`Code`=B.PaperCode WHERE B.OutTradeNo='{0}' AND B.UserId='{1}' AND B.PayState=1", out_trade_no, UserGuid);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());
            return dtBusiness;
        }

        public int AddPayCountNum(string transaction_id, string out_trade_no)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("SELECT A.* FROM paper AS A LEFT JOIN business AS B ON A.`Code`=B.PaperCode WHERE B.OutTradeNo='{0}' ", out_trade_no);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());
            int PayNum = 0;
            int Id = 0;
            if (dtBusiness.Rows[0]["PayNum"] != null && !string.IsNullOrEmpty(dtBusiness.Rows[0]["PayNum"].ToString()))
            {
                PayNum = int.Parse(dtBusiness.Rows[0]["PayNum"].ToString());
                Id = int.Parse(dtBusiness.Rows[0]["Id"].ToString());
            }
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("Update paper SET PayNum='{0}' WHERE ID='{1}'", PayNum + 1, Id);
            int result = DBHelper.ExcuteNoQuerySql(sbStr.ToString());
            return result;
        }
        public int DeleteTimeOutNoPayData()
        {
            DateTime dte = DateTime.Now.AddHours(2);

            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("DELETE from  business WHERE PayState=0 AND  CreateTime < '{0}'", dte);
            int result = DBHelper.ExcuteNoQuerySql(sbSI.ToString());
            return result;
        }

    }
}
