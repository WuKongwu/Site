using eUI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.BLL
{
    public class PayPaperBLL
    {
        public bool CheckPayInfoForBusiness(string Code, string UserGuid)
        {
            PayPaperDAL payPaperDAL = new PayPaperDAL();
            int result = payPaperDAL.CheckPayInfoForBusiness(Code, UserGuid);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string SearchPaperPath(string Code)
        {
            PayPaperDAL payPaperDAL = new PayPaperDAL();
            string fileName = payPaperDAL.SearchPaperPath(Code);
            return fileName;
        }
    }
}
