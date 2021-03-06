﻿using eUI.DAL.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.Model.ViewModel;

namespace eUI.DAL
{
   public class FootLinkDAL
    {
        public System.Data.DataTable getList(Model.ViewModel.FootLinkViewModel model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from  FootLink  Where 1=1 ");
            if (!string.IsNullOrEmpty(model.Name))
            {
                sbSI.AppendFormat(" and Name like '%{0}%'", model.Name);
            }
            sbSI.Append(" ORDER BY CreateDate DESC");
            sbSI.AppendFormat(" limit {0},{1};", (model.page - 1) * model.rows, model.rows);
            DataTable dtFootLink = DBHelper.SearchSql(sbSI.ToString());
            return dtFootLink;
        }
        public int getCount(Model.ViewModel.FootLinkViewModel model)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select * from FootLink  Where 1=1 ");
            if (!string.IsNullOrEmpty(model.Name))
            {
                sbSI.AppendFormat(" and Name like '%{0}%'", model.Name);
            }
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());
            return dtBusiness.Rows.Count;
        }
        public bool DeteleFootLink(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from FootLink where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }
        public bool UpdateFootLink(FootLinkViewModel mode)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update FootLink set ImageURL='{0}',SiteURL ='{1}',CreateDate ='{2}',
                Name ='{3}'  where Id ='{4}'
               ", mode.ImageURL,mode.SiteURL,mode.CreateDate,mode.Name, mode.Id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetFootLinkId(int id)
        {
            string str = string.Format("select * from  FootLink  where Id ={0}", id);
            return DBHelper.SearchSql(str);
        }

        public bool InsertFootLink(FootLinkViewModel mode)
        {
            StringBuilder sbAddUser = new StringBuilder();
          
            string GetSessionWithDsmId = string.Format(@"INSERT INTO FootLink 
                (ImageURL,SiteURL,Name,CreateDate) 
                VALUES('{0}','{1}','{2}','{3}')",
                 mode.ImageURL,mode.SiteURL,mode.Name,mode.CreateDate);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);

            if (iResult == 1)
                return true;
            else
                return false;
        }
    }
}
