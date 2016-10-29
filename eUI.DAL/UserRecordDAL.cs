using eUI.DAL.DBUtility;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class UserRecordDAL
    {
        public DataTable getList(UserRecord userRecord)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.Append("select * from userrecord");
            if (!string.IsNullOrEmpty(userRecord.Email))
            {
                sbSI.AppendFormat("  where Email like '%{0}%'",userRecord.Email);
            }
            DataTable dtUserInfo = DBHelper.SearchSql(sbSI.ToString());

            return dtUserInfo;

            //    //SqlServer 2012及以上分页方式
            //    //如果是导出Excel，则不需要分页
            //    if (!userInfo.isExport)
            //    {
            //        sbSI.AppendLine("Declare @PageSize int, @PageIndex int  ");
            //        sbSI.AppendLine("Set @PageSize = " + userInfo.rows);
            //        sbSI.AppendLine("Set @PageIndex = " + userInfo.page);
            //    }
            //    sbSI.AppendLine("Select Id,Name,Tel,Address,Pwd,Email, Cast(Gender as char(1)) Gender,");
            //    sbSI.AppendLine("Convert(varchar(10),CreateTime,120) CreateTime,PicUrl");
            //    if (!userInfo.isExport)
            //    {
            //        sbSI.AppendLine(",COUNT(*) OVER(PARTITION BY '') AS Total ");
            //    }
            //    sbSI.AppendLine("From dbo.UserInfo With(Nolock)");
            //    sbSI.AppendLine("Where Name like '%" + userInfo.name + "%'");
            //    //添加创建日期查询
            //    if (userInfo.stTime.Year != 1 && userInfo.edTime.Year != 1)
            //    {
            //        sbSI.AppendLine("And CreateTime Between '" + userInfo.stTime + "' And '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "'");
            //    }
            //    else if (userInfo.stTime.Year != 1)
            //    {
            //        sbSI.AppendLine("And CreateTime >= '" + userInfo.stTime + "'");
            //    }
            //    else if (userInfo.edTime.Year != 1)
            //    {
            //        sbSI.AppendLine("And CreateTime <= '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "' ");
            //    }

            //    sbSI.AppendLine("Order by Id Desc");
            //    if (!userInfo.isExport)
            //    {
            //        sbSI.AppendLine("OFFSET (@PageIndex-1)*@PageSize Rows  ");
            //        sbSI.AppendLine("FETCH NEXT @PageSize ROWS ONLY; ");
            //    }

            //    return sbSI.ToString();
        }
    }
}
