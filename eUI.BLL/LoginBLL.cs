using eUI.DAL;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.Common;
using eUI.Model.ViewModel;

namespace eUI.BLL
{
  public  class LoginBLL
    {
      LoginDAL loginDAL = new LoginDAL();
      public int RegistUser(string password, string name, string email)
      {
        int result=  loginDAL.RegistUser( password,  name,  email);
        return result;
      }
      public bool Activation(string userId) {

          return loginDAL.UpdateActivationFlag(userId);
      }

      public UserRecordList UserLogin(string password, string email)
      {
          UserRecordList userRecordList = new UserRecordList();
          DataTable dt = loginDAL.UserLogin(password, email);
          userRecordList.rows = dt.toList<UserRecord>();
          return userRecordList;
      }
    }
}
