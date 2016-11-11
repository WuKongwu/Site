using eUI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
