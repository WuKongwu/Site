using eUI.DAL;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using eUI.Common;

namespace eUI.BLL
{
    public class OtherPageBLL
    {
        OtherPageDAL otherPageDAL = new OtherPageDAL();
        public OtherPageList getList(OtherPageInfo model)
        {
            OtherPageList otherPageList = new OtherPageList();
            DataTable dt = otherPageDAL.getList(model);
            otherPageList.rows = dt.toList<OtherPageInfo>();
            otherPageList.total = otherPageDAL.getCount(model);
            return otherPageList;
        }
        public bool DeteleOtherPage(int id)
        {
            return otherPageDAL.DeteleOtherPage(id);
        }
        public bool SaveOtherPage(OtherPageInfo mode)
        {
            bool b = false;
            if (mode.Id > 0)
            {
                b = otherPageDAL.UpdateOtherPage(mode);
            }
            else
            {
                b = otherPageDAL.InsertOtherPage(mode);
            }
            return b;
        }
        public OtherPageInfo GetOtherPageById(int id)
        {
            return otherPageDAL.GetOtherPageById(id).toList<OtherPageInfo>().FirstOrDefault();
        }
    }
}
