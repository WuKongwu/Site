using eUI.DAL;
using eUI.Common;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.BLL
{
    public class AdminRecordBLL
    {
        AdminRecordDAL adminRecordDAL = new AdminRecordDAL();
        public UserRecordList getAdminList(UserRecord userRecord)
        {
            UserRecordList userRecordList = new UserRecordList();
            DataTable dt = adminRecordDAL.getAdminList(userRecord);
            userRecordList.rows = dt.toList<UserRecord>();
            userRecordList.total = adminRecordDAL.getAdminCount(userRecord);
            return userRecordList;
        }

        public bool SaveAdmin(UserRecord userRecord)
        {
            bool b = false;
            if (userRecord.Id > 0)
            {
                b = adminRecordDAL.UpdateAdmin(userRecord);
            }
            else
            {
                b = adminRecordDAL.InputAdmin(userRecord);
            }
            return b;
        }
        public UserRecord GetAdminById(int id)
        {
            return adminRecordDAL.GetAdminId(id).toList<UserRecord>().FirstOrDefault();
        }

        public bool DeteleAdmin(int id)
        {
            return adminRecordDAL.DeteleAdmin(id);
        }
    }
}
