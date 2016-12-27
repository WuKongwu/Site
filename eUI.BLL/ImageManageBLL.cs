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
    public class ImageManageBLL
    {
        ImageManageDAL dal = new ImageManageDAL();
        public ImageManageList getList(ImageManageViewModel model)
        {
            ImageManageList list = new ImageManageList();
            DataTable dt = dal.getList(model);
            list.rows = dt.toList<ImageManageModel>();
            list.total = dal.getCount(model);
            return list;
        }
        public bool Detele(int id)
        {
            return dal.Detele(id);
        }
        public bool Save(ImageManageViewModel mode)
        {
            bool b = false;
            if (mode.Id > 0)
            {
                b = dal.Update(mode);
            }
            else
            {
                b = dal.Insert(mode);
            }
            return b;
        }
        public ImageManageViewModel GetById(int id)
        {
            return dal.GetId(id).toList<ImageManageViewModel>().FirstOrDefault();
        }
    }
}
