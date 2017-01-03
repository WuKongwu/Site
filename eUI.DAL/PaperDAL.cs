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
    public class PaperDAL
    {
        public DataTable SearchAllTypeList()
        {

            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from paper order by CreateDate desc");

            DataTable dtAllTypeList = DBHelper.SearchSql(sbSI.ToString());

            return dtAllTypeList;
        }
        public DataTable SearchNewList()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from paper order by CreateDate desc limit 10");

            DataTable dtSearchNewList = DBHelper.SearchSql(sbSI.ToString());

            return dtSearchNewList;
        }
        public DataTable SearchRandomList(string type)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from paper where 1=1 ");
            if (!string.IsNullOrEmpty(type))
            {
                sbSI.Append("and type=" + type);
            }
            sbSI.Append(" order by RAND() limit 10");


            DataTable dtSearchRandomList = DBHelper.SearchSql(sbSI.ToString());

            return dtSearchRandomList;
        }

        public DataTable SearchPaperListByType(string type)
        {

            StringBuilder sbSI = new StringBuilder();
            DataTable dtTypeList = new DataTable();
            if (!string.IsNullOrEmpty(type))
            {
                sbSI.Append("SELECT * FROM  paper WHERE Type = " + type);

                dtTypeList = DBHelper.SearchSql(sbSI.ToString());
            }

            return dtTypeList;
        }

        public DataTable SearchPaperHotListBytype(string type)
        {

            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from paper where 1=1 ");
            if (!string.IsNullOrEmpty(type))
            {
                sbSI.Append("and type=" + type + " order by ReadNum desc limit 10");
            }
            else
            {
                sbSI.Append("order by ReadNum desc limit 10");
            }
            DataTable dtTypeList = DBHelper.SearchSql(sbSI.ToString());

            return dtTypeList;
        }

        public DataTable SearchPaperToolPage()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from tooldownload");
            sbSI.Append(" ORDER BY CreateDate DESC");
            DataTable dtTypeList = DBHelper.SearchSql(sbSI.ToString());
            return dtTypeList;
        }

        public DataTable SearchPaperTmpPage(string flag)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select A.*,B.TemplateType as Type from  templatedownload as A LEFT JOIN templatetype as B ON A.TypeId=B.Id WHERE 1=1 ");
            if (!string.IsNullOrEmpty(flag) && flag != "0")
            {
                sbSI.Append("AND A.TypeId=" + flag);
            }
            sbSI.Append(" ORDER BY CreateDate DESC");
            DataTable dtTypeList = DBHelper.SearchSql(sbSI.ToString());
            return dtTypeList;
        }


        public DataTable SearchFootLink()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from footlink");
            sbSI.Append(" ORDER BY CreateDate DESC");
            DataTable dtTypeList = DBHelper.SearchSql(sbSI.ToString());
            return dtTypeList;
        }

        public DataTable SearchOtherPage()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from otherpage");
            sbSI.Append(" ORDER BY CreateDate DESC");
            DataTable dtTypeList = DBHelper.SearchSql(sbSI.ToString());
            return dtTypeList;
        }

        public DataTable SearchPaperList(string key, string type)
        {

            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("SELECT * FROM paper WHERE title  LIKE '%" + key + "%' ");
            if (!string.IsNullOrEmpty(type))
            {
                sbSI.Append("AND type=" + type);
            }
            DataTable dtTypeList = DBHelper.SearchSql(sbSI.ToString());

            return dtTypeList;
        }




        public DataTable SearchPaperDetailById(string Id)
        {

            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("SELECT * FROM  paper WHERE id = " + Id);

            DataTable dtPaperDetail = DBHelper.SearchSql(sbSI.ToString());

            return dtPaperDetail;
        }

        public DataTable SearchTmpDetailById(string Id)
        {

            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("SELECT * FROM  templatedownload WHERE id = " + Id);

            DataTable dtPaperDetail = DBHelper.SearchSql(sbSI.ToString());

            return dtPaperDetail;
        }

        public DataTable SearchPaperImgListById(string Id)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from paperimg where 1=1 ");
            sbSI.Append("and PaperId =" + Id);
            DataTable dtImgList = DBHelper.SearchSql(sbSI.ToString());
            return dtImgList;
        }
        public void AddReadCount(string id)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select ReadNum from paper where 1=1 ");
            sbSI.AppendFormat("and  id={0}", id);
            DataTable dt = DBHelper.SearchSql(sbSI.ToString());
            int num = 0;
            if (dt.Rows[0]["ReadNum"] != null && !string.IsNullOrEmpty(dt.Rows[0]["ReadNum"].ToString()))
            {
                num = int.Parse(dt.Rows[0]["ReadNum"].ToString());
            }

            StringBuilder sbSITwo = new StringBuilder();
            sbSITwo.AppendFormat("update paper set ReadNum={0} where id={1}", num + 1, id);
            DBHelper.ExcuteNoQuerySql(sbSITwo.ToString());

        }
        public DataTable SearchImgManage()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from imagemanage order by CreateDate desc");
            DataTable dtImgList = DBHelper.SearchSql(sbSI.ToString());
            return dtImgList;
        }

        public bool CreateBusiness(Business business)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("INSERT INTO business (OrderNumber,Name,Price,PaperId,UserId,CreateTime,PayState) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", business.OrderNumber, business.Name, business.Price, business.PaperId, business.UserId, business.CreateTime, business.PayState);
            int result = DBHelper.ExcuteNoQuerySql(sbSI.ToString());
            if (result == 1)
            {
                return true;
            }
            else { return false; }
        }


        public DataTable SearchSubPage()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from subpageone");
            DataTable dtSearchSubPage = DBHelper.SearchSql(sbSI.ToString());
            return dtSearchSubPage;
        }


    }
}
