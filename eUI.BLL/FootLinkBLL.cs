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
    public class FootLinkBLL
    {
        FootLinkDAL footLinkDAL = new FootLinkDAL();
        public FootLinkList getList(FootLinkViewModel model)
        {
            FootLinkList footLinkList = new FootLinkList();
            DataTable dt = footLinkDAL.getList(model);
            footLinkList.rows = dt.toList<FootLinkModel>();
            footLinkList.total = footLinkDAL.getCount(model);
            return footLinkList;
        }
    }
}
