using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;

namespace EzzeChargeServices.Bussiness
{
    public class GetChangePassword : SqlHelper
    {
        string editTranpwd = string.Empty;
        public Bussiness.AuthResponse ChangePassword(DataClass.DataChangePassword obj)
        {
            if (obj.PwdType == 1)
            { editTranpwd = "Editpwd"; }
            else if (obj.PwdType == 2) { editTranpwd = "EditTranspwd"; }

            string[,] param = {{"MAINID",obj.Mainid.ToString()},
                           {"OLDPASS",obj.OldPassword.ToString()},
                           {"NEWPASS",obj.NewPassword.ToString()},
                           {"EDITTRANSPWD",editTranpwd.ToString()}};
            MEMBERS.SQLReturnValue mOVal = ExecuteProcWithMessageValue("UPDATE_ADMIN_TRANSPASS", param, true);
            if (mOVal.ValueFromSQL > 0) { return new Bussiness.AuthResponse(1, mOVal.MessageFromSQL); }
            else
            { return new Bussiness.AuthResponse(0, mOVal.MessageFromSQL); }
        }
    }
}