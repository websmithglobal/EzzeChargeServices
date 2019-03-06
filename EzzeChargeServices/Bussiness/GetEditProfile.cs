using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;

namespace EzzeChargeServices.Bussiness
{
    public class GetEditProfile : SqlHelper
    {
        public Bussiness.AuthResponse EditMyProfile(DataClass.DataEditProfile obj)
        {
            string[,] param = {{"MAINID",obj.Mainid.ToString()},
                           {"UserName",obj.UserName.ToString()},
                           {"EmailID",obj.EmailID.ToString()},
                           {"Address",obj.Address.ToString()}};
            MEMBERS.SQLReturnValue mOVal = ExecuteProcWithMessageValue("EDIT_PROFILE", param, true);
            if (mOVal.ValueFromSQL > 0) { return new Bussiness.AuthResponse(1, mOVal.MessageFromSQL); }
            else
            { return new Bussiness.AuthResponse(0, mOVal.MessageFromSQL); }

        }
    }
}