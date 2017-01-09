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
    public class MenuBLL
    {
        MenuDAL menuDAL = new MenuDAL();
        public MenuList getList(MenuViewModel model)
        {
            MenuList menuList = new MenuList();
            DataTable dt = menuDAL.getList(model);
            menuList.rows = dt.toList<MenuModel>();
            menuList.total = menuDAL.getCount(model);
            return menuList;
        }
        public bool DeteleMenu(int id)
        {
            return menuDAL.DeteleMenu(id);
        }
        public bool SaveMenu(MenuViewModel mode)
        {
            bool b = false;
            if (mode.Id > 0)
            {
                b = menuDAL.UpdateMenu(mode);
            }
            else
            {
                b = menuDAL.InsertMenu(mode);
            }
            return b;
        }
        public MenuViewModel GetMenuById(int id)
        {
            return menuDAL.GetMenuId(id).toList<MenuViewModel>().FirstOrDefault();
        }
    }
}
