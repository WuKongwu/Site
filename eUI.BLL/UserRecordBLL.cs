using eUI.DAL;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.Common;
namespace eUI.BLL
{
   public class UserRecordBLL
    {
        UserRecordDAL userInfoDAL = new UserRecordDAL();
        public UserRecordList getUserList(UserRecord userInfo)
        {
            UserRecordList user = new UserRecordList();
            DataTable dt = userInfoDAL.getList(userInfo);
            user.rows = dt.toList<UserRecord>();
            user.total = dt.Rows.Count;
            return user;
        }
    }
}
