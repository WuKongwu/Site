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
            sbSI.Append("select * from paper Where 1=1 ");

            DataTable dtBusiness = DBHelper.SearchSql(sbSI.ToString());

            return dtBusiness;
        }


        public int GetId()
        {
            string getId = "SELECT LAST_INSERT_ID()";
            DataTable dtUserInfo = DBHelper.SearchSql(getId);
            return 0;
        }

        public bool Input(PaperInfo paperInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"INSERT INTO PAPER 
                (Title,Info,DetailInfo,Price,CreateDate,Type,ImgA,ImgB,ImgC,Video,FileUrl,Code) 
                VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", paperInfo.Title, paperInfo.Info, paperInfo.DetailInfo, paperInfo.Price,
                DateTime.Now, paperInfo.Type,paperInfo.ImgA,paperInfo.ImgB,
                paperInfo.ImgC,paperInfo.Video,paperInfo.FileUrl,paperInfo.Code);
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

        public bool UpdatePaper(PaperInfo paperInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            string GetSessionWithDsmId = string.Format(@"update PAPER set Title='{0}',Info ='{1}',
                DetailInfo ='{2}',Price='{3}',Type ='{4}',ImgA='{5}',ImgB='{6}',ImgC='{7}',Video='{8}',FileUrl='{9}',Code='{10}' where Id ='{11}'
               ", paperInfo.Title, paperInfo.Info, paperInfo.DetailInfo, paperInfo.Price,paperInfo.Type
               ,paperInfo.ImgA,paperInfo.ImgB,paperInfo.ImgC,paperInfo.Video,paperInfo.FileUrl,paperInfo.Code,paperInfo.Id);
            int iResult = DBHelper.ExcuteNoQuerySql(GetSessionWithDsmId);
            if (iResult == 1)
                return true;
            else
                return false;
        }

        public DataTable GetPaperId(int id)
        {
            string str = string.Format("select * from PAPER where Id ={0}", id);

            return DBHelper.SearchSql(str);
        }
    }
}
