using eUI.DAL.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.Model.ViewModel;
using eUI.Model.Enums;

namespace eUI.DAL
{
    public class ImageManageDAL
    {
        public System.Data.DataTable getList(Model.ViewModel.ImageManageViewModel model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from  ImageManage  Where 1=1 ");
            if (!string.IsNullOrEmpty(model.ImageName))
            {
                sbSI.AppendFormat(" and ImageName like '%{0}%'", model.ImageName);
            }
            sbSI.AppendFormat(" limit {0},{1};", (model.page - 1) * model.rows, model.rows);
            DataTable dtFootLink = DBHelper.SearchSql(sbSI.ToString());
            return dtFootLink;
        }
        public int getCount(Model.ViewModel.ImageManageViewModel model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from ImageManage  Where 1=1 ");
            if (!string.IsNullOrEmpty(model.ImageName))
            {
                sbSI.AppendFormat(" and ImageName like '%{0}%'", model.ImageName);
            }
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());
            return dtBusiness.Rows.Count;
        }
        public bool Detele(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from ImageManage where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }
        public bool Update(ImageManageViewModel mode)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update ImageManage set ImageName='{0}',ImagePosition ='{1}',
                ImageURL ='{2}' ,CreateDate ='{3}' where Id ='{4}'
               ", mode.ImageName, mode.ImagePosition, mode.ImageURL, mode.CreateDate, mode.Id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetId(int id)
        {
            string str = string.Format("select * from  ImageManage  where Id ={0}", id);
            return DBHelper.SearchSql(str);
        }

        public bool Insert(ImageManageViewModel mode)
        {
            StringBuilder sbAddUser = new StringBuilder();

            string GetSessionWithDsmId = string.Format(@"INSERT INTO ImageManage 
                (ImageName,ImagePosition,ImageURL,CreateDate) 
                VALUES('{0}','{1}','{2}','{3}')",
                 mode.ImageName, mode.ImagePosition, mode.ImageURL, DateTime.Now);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);

            if (iResult == 1)
                return true;
            else
                return false;
        }
        public bool IsVaildtion(string imagePostion)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select count(1) from ImageManage where 1=1");
            switch (imagePostion)
            {
                case "main":
                    strSQL.AppendFormat(" and ImagePosition ='{0}'", imagePostion);
                   DataTable dt =  DBHelper.SearchSql(strSQL.ToString());
                   if (dt!=null)
                   {
                       int num = int.Parse(dt.Rows[0][0].ToString());
                       if (num>3)
                       {
                           return false;
                       }
                   }
                    break;
                case "news":
                    break;
                case "hots":
                    break;
                case "child":
                    break;
                case "logo":
                    break;
                default:
                    break;
            }
            return true;
        }


    }
}
