using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Data;
namespace EzzeChargeServices.Bussiness
{
    public class GetDistributor : SqlHelper
    {
        public Bussiness.AuthResponse SAVE_DISTRIBUTOR(DataClass.DataDistributor obj, string UserName, string Password)
        {
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT TOP 1 COMM_Id,Slabid FROM USERS WHERE MainId=" + obj.mainid + " and (MAILID='" + UserName + "' OR MOBILE='" + UserName + "') AND LOGINPASS='" + Password + "' AND Userlevel=1");
            if (dt.Rows.Count > 0)
            {
                string[,] param = {{"UNM",obj.Name},
                           {"ADDRESS",obj.Address},
                           {"MOBILE",obj.Mobile},
                           {"MAIL",obj.EmailID},
                           {"COMMTYPE",dt.Rows[0]["COMM_Id"].ToString()},
                           {"SLABID",dt.Rows[0]["Slabid"].ToString()},
                           {"VIA","2"},
                           {"UPPERID",obj.mainid.ToString()},
                            {"PancardNo",obj.PancardNo.ToString()},
                           {"GstnNo",obj.GstNo.ToString()},
                           {"createbyid",obj.mainid.ToString()}};
                MEMBERS.SQLReturnValue mOVal = ExecuteProcWithMessageValue("SAVE_DIST", param, true);
                if (mOVal.ValueFromSQL > 0)
                {
                    return new Bussiness.AuthResponse(1, "DEAR " + obj.Name + " YOUR ACCOUNT HAS BEEN CREATED AS DISTRIBUTOR.USERNAME IS : " + obj.Mobile + ",PASSWORD IS : " + mOVal.MessageFromSQL + "");
                    SendSMS.SendSMSFire(obj.Mobile, "DEAR " + obj.Name + " YOUR ACCOUNT HAS BEEN CREATED AS DISTRIBUTOR.USERNAME IS : " + obj.Mobile + ",PASSWORD IS : " + mOVal.MessageFromSQL + "");
                }
                else
                { return new Bussiness.AuthResponse(0, mOVal.MessageFromSQL); }
            }
            else
            {
                return new Bussiness.AuthResponse(0, "server internal error.");
            }

        }
    }
}