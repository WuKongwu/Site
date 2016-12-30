using eUI.DAL.DBUtility;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class TemplateDownloadDAL
    {
        public DataTable getTemplateList(TemplateDownloadInfo templateDownloadInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select A.*,B.TemplateType as Type from  templatedownload as A LEFT JOIN templatetype as B ON A.TypeId=B.Id  Where 1=1 ");
            if (!string.IsNullOrEmpty(templateDownloadInfo.Title))
            {
                sbSI.AppendFormat("AND TITLE LIKE '%{0}%'", templateDownloadInfo.Title);
            }
            else if (!string.IsNullOrEmpty(templateDownloadInfo.Type)) {
                sbSI.AppendFormat("AND TYPE='{0}'", templateDownloadInfo.Type);
            }
            sbSI.Append(" ORDER BY CreateDate DESC");
            sbSI.AppendFormat(" limit {0},{1};", (templateDownloadInfo.page - 1) * templateDownloadInfo.rows, templateDownloadInfo.rows);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }


        public DataTable getTmpTypeList(TemplateTypeInfo templateTypeInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from  templatetype  Where 1=1 ");
            if (!string.IsNullOrEmpty(templateTypeInfo.TemplateType))
            {
                sbSI.AppendFormat("AND TemplateType LIKE '%{0}%'", templateTypeInfo.TemplateType);
            }

            sbSI.AppendFormat(" limit {0},{1};", (templateTypeInfo.page - 1) * templateTypeInfo.rows, templateTypeInfo.rows);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }

        public DataTable getTmpTypeData()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from  templatetype");
          
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }


        public int getTemplateDownloadCount(TemplateDownloadInfo templateDownloadInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select A.*,B.TemplateType as Type from  templatedownload as A LEFT JOIN templatetype as B ON A.TypeId=B.Id  Where 1=1 ");
            if (!string.IsNullOrEmpty(templateDownloadInfo.Title))
            {
                sbSI.AppendFormat("AND TITLE LIKE '%{0}%'", templateDownloadInfo.Title);
            }
            else if (!string.IsNullOrEmpty(templateDownloadInfo.Type))
            {
                sbSI.AppendFormat("AND TYPE='{0}'", templateDownloadInfo.Type);
            }

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }

        public int getTmpTypeCount(TemplateTypeInfo templateTypeInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from templatetype  Where 1=1 ");
            if (!string.IsNullOrEmpty(templateTypeInfo.TemplateType))
            {
                sbSI.AppendFormat("AND TemplateType LIKE '%{0}%'", templateTypeInfo.TemplateType);
            }

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }

        public bool InputTemplateDownload(TemplateDownloadInfo templateDownloadInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();

            string GetSessionWithDsmId = string.Format(@"INSERT INTO templatedownload 
                (title,TemplateFile,content,picture,downloadNum,TypeId,CreateDate) 
                VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                templateDownloadInfo.Title, templateDownloadInfo.TemplateFile, templateDownloadInfo.Content, templateDownloadInfo.Picture,
                templateDownloadInfo.DownloadNum,templateDownloadInfo.TypeId,templateDownloadInfo.CreateDate);


            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);

            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool InputTemplateType(TemplateTypeInfo templateTypeInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();

            string GetSessionWithDsmId = string.Format(@"INSERT INTO templatetype 
                (templatetype) VALUES('{0}')",templateTypeInfo.TemplateType);

            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);

            if (iResult == 1)
                return true;
            else
                return false;
        }



        public bool DeteleTemplate(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from tooldownload where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool DeteleTemplateType(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from templatetype where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool UpdateTemplateDownload(TemplateDownloadInfo templateDownloadInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update templatedownload set Title='{0}',TemplateFile ='{1}',TypeId ='{2}',
                Content ='{3}',Picture='{4}',DownloadNum ='{5}',CreateDate ='{6}' where id='{7}'",
                templateDownloadInfo.Title, templateDownloadInfo.TemplateFile, templateDownloadInfo.TypeId, templateDownloadInfo.Content,
                templateDownloadInfo.Picture, templateDownloadInfo.DownloadNum,templateDownloadInfo.CreateDate,templateDownloadInfo.Id);

            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool UpdateTemplateType(TemplateTypeInfo templateTypeInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update templatetype set TemplateType='{0}' where id='{1}'",
                templateTypeInfo.TemplateType, templateTypeInfo.Id);

            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetTemplateById(int id)
        {
            string str = string.Format("select * from  templatedownload  where id ={0}", id);

            return DBHelper.SearchSql(str);
        }
        public DataTable GetTemplateTypeById(int id)
        {
            string str = string.Format("select * from  templatetype  where id ={0}", id);

            return DBHelper.SearchSql(str);
        }

        public int AddDownloadNum(int id)
        {
            int result = 0;
            try
            {
                StringBuilder sbSI = new StringBuilder();
                sbSI.Append("select downloadNum from templatedownload where 1=1 ");
                sbSI.AppendFormat("and  id={0}", id);
                DataTable dt = DBHelper.SearchSql(sbSI.ToString());
                int num = 0;
                if (dt.Rows[0]["downloadNum"] != null && !string.IsNullOrEmpty(dt.Rows[0]["downloadNum"].ToString()))
                {
                    num = int.Parse(dt.Rows[0]["downloadNum"].ToString());
                }
                result = num;
                StringBuilder sbSITwo = new StringBuilder();
                sbSITwo.AppendFormat("update templatedownload set downloadNum={0} where id={1}", num + 1, id);
                DBHelper.ExcuteNoQuerySql(sbSITwo.ToString());
                return result + 1;
            }
            catch
            {
                return result + 1;
            }
        }
    }
}
