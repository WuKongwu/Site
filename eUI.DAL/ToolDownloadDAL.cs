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
   public class ToolDownloadDAL
    {

        public DataTable getToolList(ToolDownloadInfo toolDownloadInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from  tooldownload  Where 1=1 ");
            if (!string.IsNullOrEmpty(toolDownloadInfo.title))
            {
                sbSI.AppendFormat("AND TITLE LIKE '%{0}%'", toolDownloadInfo.title);
            }

            sbSI.AppendFormat(" limit {0},{1};", (toolDownloadInfo.page - 1) * toolDownloadInfo.rows, toolDownloadInfo.rows);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }
        public int getToolDownloadCount(ToolDownloadInfo toolDownloadInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from tooldownload  Where 1=1 ");
            if (!string.IsNullOrEmpty(toolDownloadInfo.title))
            {
                sbSI.AppendFormat("AND TITLE LIKE '%{0}%'", toolDownloadInfo.title);
            }

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }

        public bool Input(ToolDownloadInfo toolDownloadInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();

            string GetSessionWithDsmId = string.Format(@"INSERT INTO tooldownload 
                (title,url,content,picture,downloadNum) 
                VALUES('{0}','{1}','{2}','{3}','{4}')",
                toolDownloadInfo.title, toolDownloadInfo.url, toolDownloadInfo.content, toolDownloadInfo.picture,
                toolDownloadInfo.downloadNum);


            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);

            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool DeteleTool(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from tooldownload where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool UpdateTool(ToolDownloadInfo toolDownloadInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update tooldownload set Title='{0}',url ='{1}',
                content ='{2}',picture='{3}',downloadNum ='{4}' where id='{5}'" ,
                toolDownloadInfo.title, toolDownloadInfo.url, toolDownloadInfo.content, toolDownloadInfo.picture,
                toolDownloadInfo.downloadNum,toolDownloadInfo.Id);


            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetToolById(int id)
        {
            string str = string.Format("select * from  tooldownload  where id ={0}", id);

            return DBHelper.SearchSql(str);
        }


    }
}
