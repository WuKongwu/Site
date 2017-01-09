using eUI.DAL;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.Common;
using eUI.Model.ViewModel;
namespace eUI.BLL
{
    public class PayPaperBLL
    {
        public bool UpdateBusiness(string transaction_id, string out_trade_no)
        {
            PayPaperDAL payPaperDAL = new PayPaperDAL();
            int result = payPaperDAL.UpdateBusiness(transaction_id, out_trade_no);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PaperDetailViewModel GetPaperDownloadUrl(string orderNumber, string UserGuid)
        {
            PaperDetailViewModel paperDetailViewModel = new PaperDetailViewModel();
            PayPaperDAL payPaperDAL = new PayPaperDAL();
            DataTable paperDetail = payPaperDAL.SearchPaperPath(orderNumber, UserGuid);
            paperDetailViewModel.detail = paperDetail.toList<PaperInfo>();
            return paperDetailViewModel;
        }

        public bool AddPayCountNum(string transaction_id, string out_trade_no)
        {
            PayPaperDAL payPaperDAL = new PayPaperDAL();
            int result = payPaperDAL.AddPayCountNum(transaction_id, out_trade_no);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteTimeOutNoPayData()
        {
            PayPaperDAL payPaperDAL = new PayPaperDAL();
            int result = payPaperDAL.DeleteTimeOutNoPayData();
            if (result >= 1)
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
