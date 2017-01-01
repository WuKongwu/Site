using eUI.DAL;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using eUI.Common;
using eUI.Model.Enums;
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
        public bool Save(ImageManageViewModel mode, out string msg)
        {
            bool b = false;
            msg = string.Empty;
            bool isvail = dal.IsVaildtion(mode.ImagePosition);
            if (mode.Id > 0)
            {
                if (isvail)
                {
                    b = dal.Update(mode);
                }
                else
                {
                    switch (mode.ImagePosition)
                    {
                        case "0":
                            msg = "轮播已存在3张图片!";
                            break;
                        case "1":
                            msg = "最新已存在1张图片!";
                            break;
                        case "2":
                            msg = "热点已存在1张图片!";
                            break;
                        case "3":
                            msg = "已存在1张图片!";
                            break;
                        case "4":
                            msg = "Logo已存在1张图片!";
                            break;
                        default:
                            msg = string.Empty;
                            break;
                    }
                }
            }
            else
            {
                if (isvail)
                {
                    b = dal.Insert(mode);
                }
                else
                {
                    switch (mode.ImagePosition)
                    {
                        case "0":
                            msg = "轮播已存在3张图片,只能进行修改";
                            break;
                        case "1":
                            msg =  "最新已存在1张图片,只能进行修改";
                            break;
                        case "2":
                            msg = "热点已存在1张图片,只能进行修改";
                            break;
                        case "3":
                            msg =  "已存在1张图片,只能进行修改";
                            break;
                        case "4":
                            msg =  "Logo已存在1张图片,只能进行修改";
                            break;
                        default:
                            msg = string.Empty;
                            break;
                    }
                }
            }
            return b;
        }
        public ImageManageViewModel GetById(int id)
        {
            return dal.GetId(id).toList<ImageManageViewModel>().FirstOrDefault();
        }
    }
}
