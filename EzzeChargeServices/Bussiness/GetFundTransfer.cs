using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Data;
namespace EzzeChargeServices.Bussiness
{
    public class GetFundTransfer : SqlHelper
    {
        public Bussiness.AuthResponse GPRSTOWEB(int Mainid, string CustomerMobile, float Amount, string TranPwd)
        {
            DataTable dtMob = SqlHelper.GetDataUsingQuery("SELECT TOP 1 MOBILE FROM USERS WHERE MAINID=" + Mainid + "");
            if (dtMob.Rows.Count > 0)
            {
                string[,] param = {{"KEYWORD","GBAL"},                           
                           {"ACTION","102"},
                           {"AMOUNT",Amount.ToString()},                           
                           {"PROCMOBILE",CustomerMobile.ToString().Trim()},
                           {"MOBILE",dtMob.Rows[0]["MOBILE"].ToString()},
                           {"TRANSPASS",TranPwd.ToString().Trim()},
                       
                          };
                MEMBERS.SQLReturnValue mOVal = ExecuteProcWithMessageValue2("PROCESS_WITH_KEYWORDS1", param, true);
                if (mOVal.ValueFromSQL > 0) { return new Bussiness.AuthResponse(1, mOVal.MessageFromSQL1); }
                else
                { return new Bussiness.AuthResponse(0, mOVal.MessageFromSQL); }

            }
            else { return new Bussiness.AuthResponse(0, "INVALID USERS"); }

        }
    }
}