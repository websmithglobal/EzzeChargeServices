using EzzeChargeServices._MyConnection;
using EzzeChargeServices.DataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace EzzeChargeServices.DataClass
{
    public class UserLogin : SqlHelper
    {
        public MEMBERS.SQLReturnValue CheckUserLogin(UserAuthntication Obj)
        {
            string[,] param = { { "UserName", Obj.UserName.ToString()}, 
                              { "Password", Obj.Password.ToString()}};
            return ExecuteProcWithMessageValue("CheckUserLogin", param, true);
        }
    }
}