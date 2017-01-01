using eUI.DAL.DBUtility;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace eUI.DAL
{
    public class OtherPageDAL
    {
        public System.Data.DataTable getList(Model.ViewModel.OtherPageInfo model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from  otherpage  Where 1=1 ");
            if (!string.IsNullOrEmpty(model.Name))
            {
                sbSI.AppendFormat(" and Name like '%{0}%'", model.Name);
            }
            sbSI.Append(" ORDER BY CreateDate DESC");
            sbSI.AppendFormat(" limit {0},{1};", (model.page - 1) * model.rows, model.rows);
            DataTable dtOtherPage = DBHelper.SearchSql(sbSI.ToString());
            return dtOtherPage;
        }
        public int getCount(Model.ViewModel.OtherPageInfo model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from otherpage  Where 1=1 ");
            if (!string.IsNullOrEmpty(model.Name))
            {
                sbSI.AppendFormat(" and Name like '%{0}%'", model.Name);
            }
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());
            return dtBusiness.Rows.Count;
        }
        public bool DeteleOtherPage(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from otherpage where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }
        public bool UpdateOtherPage(OtherPageInfo mode)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update otherpage set ImgFile='{0}',Url ='{1}',CreateDate ='{2}',
                Name ='{3}'  where Id ='{4}'
               ", mode.ImgFile, mode.Url, mode.CreateDate, mode.Name, mode.Id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetOtherPageById(int id)
        {
            string str = string.Format("select * from  otherpage  where Id ={0}", id);
            return DBHelper.SearchSql(str);
        }

        public bool InsertOtherPage(OtherPageInfo mode)
        {
            StringBuilder sbAddUser = new StringBuilder();

            string GetSessionWithDsmId = string.Format(@"INSERT INTO otherpage 
                (ImgFile,Url,Name,CreateDate) 
                VALUES('{0}','{1}','{2}','{3}')",
                 mode.ImgFile, mode.Url, mode.Name, mode.CreateDate);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);

            if (iResult == 1)
                return true;
            else
                return false;
        }
    }
}
