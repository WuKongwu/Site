using eUI.Common;
using eUI.DAL;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.BLL
{
    public class BusinessBLL
    {
        BusinessDAL businessDAL = new BusinessDAL();
        public BusinessList getBusinessList(Business business)
        {
            BusinessList businessList = new BusinessList();
            DataTable dt = businessDAL.getList(business);
            businessList.rows = dt.toList<Business>();
            businessList.total = businessDAL.getCount(business);
            return businessList;
        }
    }
}
