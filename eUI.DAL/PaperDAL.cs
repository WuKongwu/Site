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
                sbSI.Append("SELECT A.*, B.Path as imgPath,C.ReadCount,D.BuyCount FROM((SELECT * FROM paper WHERE Type = " + type + ") AS A LEFT JOIN (SELECT PaperId,Path FROM paperimg LIMIT 1 ) AS B ON A.Id = B.PaperId )");
                sbSI.Append("LEFT JOIN (SELECT Num AS ReadCount, PaperId FROM count WHERE Type = 1 ) AS C ON A.id = C.PaperId LEFT JOIN ( SELECT Num AS BuyCount, PaperId FROM count WHERE Type = 2 ) AS D ON A.id = D.PaperId");
                dtTypeList = DBHelper.SearchSql(sbSI.ToString());
            }

            return dtTypeList;
        }

        public DataTable SearchPaperHotListBytype(string type)
        {

            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select A.*,B.Num from paper as A LEFT JOIN (SELECT Num,PaperId from count where type=2)as B ON A.id=B.PaperId where 1=1 ");
            if (!string.IsNullOrEmpty(type))
            {
                sbSI.Append("and type=" + type + " order by Num desc limit 10");
            }
            else
            {
                sbSI.Append("order by Num desc limit 10");
            }
            DataTable dtTypeList = DBHelper.SearchSql(sbSI.ToString());

            return dtTypeList;
        }

        public DataTable SearchPaperList(string key)
        {

            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("SELECT A.*, B.Path as imgPath,C.ReadCount,D.BuyCount FROM((SELECT * FROM paper WHERE title  LIKE '%" + key + "%') AS A LEFT JOIN (SELECT PaperId,Path FROM paperimg LIMIT 1 ) AS B ON A.Id = B.PaperId )");
            sbSI.Append("LEFT JOIN (SELECT Num AS ReadCount, PaperId FROM count WHERE Type = 1 ) AS C ON A.id = C.PaperId LEFT JOIN ( SELECT Num AS BuyCount, PaperId FROM count WHERE Type = 2 ) AS D ON A.id = D.PaperId");
            DataTable dtTypeList = DBHelper.SearchSql(sbSI.ToString());

            return dtTypeList;
        }




        public DataTable SearchPaperDetailById(string Id)
        {

            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("SELECT A.*,C.ReadCount,D.BuyCount FROM (SELECT * FROM paper WHERE id = " + Id + ") AS A   LEFT JOIN (SELECT Num AS ReadCount, PaperId FROM count WHERE Type = 1 ) AS C ON A.id = C.PaperId LEFT JOIN ( SELECT Num AS BuyCount, PaperId FROM count WHERE Type = 2 ) AS D ON A.id = D.PaperId");

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
            sbSI.Append("select num from count where type=1 ");
            sbSI.AppendFormat("and  PaperId={0}", id);
            DataTable dt = DBHelper.SearchSql(sbSI.ToString());
            if (dt.Rows.Count == 0)
            {
                StringBuilder sbSIOne = new StringBuilder();
                sbSIOne.AppendFormat("INSERT INTO count (paperId,type,num) VALUES({0},1,1)", id);
                DBHelper.ExcuteNoQuerySql(sbSIOne.ToString());
            }
            else
            {
                int num = Convert.ToInt32(dt.Rows[0]["num"].ToString());
                StringBuilder sbSITwo = new StringBuilder();
                sbSITwo.AppendFormat("update count set num={0} where paperId={1} and type=1", num + 1, id);
                DBHelper.ExcuteNoQuerySql(sbSITwo.ToString());
            }
        }

    }
}
