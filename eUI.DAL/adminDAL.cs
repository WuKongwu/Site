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
    public class adminDAL
    {

        public DataTable getPaperList(PaperList paperList)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select paper.* ,menu.MenuName from  paper  LEFT JOIN menu ON paper.Type=menu.Id  Where 1=1 ");
            if (!string.IsNullOrEmpty(paperList.Number))
            {
                sbSI.AppendFormat("AND CODE LIKE '%{0}%'", paperList.Number);
            }
            else if (!string.IsNullOrEmpty(paperList.Title))
            {
                sbSI.AppendFormat("AND TITLE LIKE '%{0}%'", paperList.Title);
            }
            else if (paperList.Type != 0)
            {
                sbSI.AppendFormat("AND TYPE = '{0}'", paperList.Type);
            }
            else if (paperList.StTime > DateTime.MinValue && paperList.EdTime > DateTime.MinValue)
            {
                sbSI.AppendFormat("AND CreateDate BETWEEN '{0}' AND '{1}'", paperList.StTime, paperList.EdTime);
            }
            sbSI.Append(" ORDER BY CreateDate DESC");
            sbSI.AppendFormat(" limit {0},{1};", (paperList.page - 1) * paperList.rows, paperList.rows);
            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }
        public int getPaperCount(PaperList paperList)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendFormat("select paper.* ,menu.MenuName from  paper  LEFT JOIN menu ON paper.Type=menu.Id  Where 1=1 ");
            if (!string.IsNullOrEmpty(paperList.Number))
            {
                sbSI.AppendFormat("AND CODE LIKE '%{0}%'", paperList.Number);
            }
            else if (!string.IsNullOrEmpty(paperList.Title))
            {
                sbSI.AppendFormat("AND TITLE LIKE '%{0}%'", paperList.Title);
            }
            else if (paperList.Type != 0)
            {
                sbSI.AppendFormat("AND TYPE = '{0}'", paperList.Type);
            }
            else if (paperList.StTime > DateTime.MinValue && paperList.EdTime > DateTime.MinValue)
            {
                sbSI.AppendFormat("AND CreateDate BETWEEN '{0}' AND '{1}'", paperList.StTime, paperList.EdTime);
            }

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness.Rows.Count;
        }


        public int GetId()
        {
            string getId = "SELECT LAST_INSERT_ID()";
            DataTable dtUserInfo = DBHelper.SearchSql(getId);
            return int.Parse(dtUserInfo.Rows[0][0].ToString());
        }

        public bool Input(PaperInfo paperInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            if (string.IsNullOrEmpty(paperInfo.Code))
            {
                paperInfo.Code = Guid.NewGuid().ToString("D");
            }
            string GetSessionWithDsmId = string.Format(@"INSERT INTO PAPER 
                (Title,Info,DetailInfo,Price,CreateDate,Type,ImgA,ImgB,ImgC,ImgD,ImgE,
                Video,VideoZip,Version,FileUrl,Code,ReadNum,PayNum) 
                VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',
                '{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                paperInfo.Title, paperInfo.Info, paperInfo.DetailInfo, paperInfo.Price,
                paperInfo.CreateDate, paperInfo.Type, paperInfo.ImgA, paperInfo.ImgB,
                paperInfo.ImgC, paperInfo.ImgD, paperInfo.ImgE, paperInfo.Video, 
                paperInfo.VideoZip, paperInfo.Version, paperInfo.FileUrl, paperInfo.Code,
                paperInfo.ReadNum,paperInfo.PayNum);
          
           
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
           
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public bool DetelePaper(int id)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"delete from PAPER where Id='{0}' ", id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }


        public DataTable getMenuData()
        {
            string menuStr = "SELECT * from menu order by OrderIndex  limit 0, 8";
            DataTable dtMenuInfo = DBHelper.SearchSql(menuStr);
            return dtMenuInfo;
        }
        public bool UpdatePaper(PaperInfo paperInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update PAPER set Title='{0}',Info ='{1}',
                DetailInfo ='{2}',Price='{3}',Type ='{4}',ImgA='{5}',ImgB='{6}',ImgC='{7}',ImgD='{8}',
                ImgE='{9}',Video='{10}',VideoZip='{11}',Version='{12}',FileUrl='{13}',Code='{14}',
                ReadNum='{15}',PayNum='{16}',CreateDate='{17}' where Id ='{18}'
               ", paperInfo.Title, paperInfo.Info, paperInfo.DetailInfo, paperInfo.Price, paperInfo.Type
               , paperInfo.ImgA, paperInfo.ImgB, paperInfo.ImgC, paperInfo.ImgD, paperInfo.ImgE, paperInfo.Video,
               paperInfo.VideoZip, paperInfo.Version, paperInfo.FileUrl, paperInfo.Code, paperInfo.ReadNum,
               paperInfo.PayNum,paperInfo.CreateDate,paperInfo.Id);
            
         
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetPaperId(int id)
        {
            string str = string.Format("select * from  paper  where paper.Id ={0}", id);

            return DBHelper.SearchSql(str);
        }
    }
}
