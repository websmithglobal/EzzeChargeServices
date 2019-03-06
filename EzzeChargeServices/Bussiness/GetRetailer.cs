using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Data;

namespace EzzeChargeServices.Bussiness
{
    public class GetRetailer : SqlHelper
    {
        public Bussiness.AuthResponse SAVE_RETAILER(DataClass.DataRetailer obj, string UserName, string Password)
        {
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT TOP 1 COMM_Id,Slabid FROM USERS WHERE MainId=" + obj.Mainid + " and (MAILID='" + UserName + "' OR MOBILE='" + UserName + "') AND LOGINPASS='" + Password + "'");
            if (dt.Rows.Count > 0)
            {
                string[,] param = {{"UNM",obj.Name},
                           {"ADDRESS",obj.Address},
                           {"MOBILE",obj.Mobile},
                           {"MAIL",obj.EmailID},
                           {"COMMTYPE",dt.Rows[0]["COMM_Id"].ToString()},
                           {"SLABID",dt.Rows[0]["Slabid"].ToString()},
                           {"VIA","2"},
                           {"UPPERID",obj.UpperID.ToString()},
                           {"PancardNo",obj.PancardNo.ToString()},
                           {"GstnNo",obj.GstNo.ToString()},
                           {"createbyid",obj.Mainid.ToString()}};
                MEMBERS.SQLReturnValue mOVal = ExecuteProcWithMessageValue("SAVE_RETAILER", param, true);
                if (mOVal.ValueFromSQL > 0)
                {
                    SendSMS.SendSMSFire(obj.Mobile, "DEAR " + obj.Name + " YOUR ACCOUNT HAS BEEN CREATED AS RETAILER.USERNAME IS : " + obj.Mobile + ",PASSWORD IS : " + mOVal.MessageFromSQL + "");
                    return new Bussiness.AuthResponse(1, "DEAR " + obj.Name + " YOUR ACCOUNT HAS BEEN CREATED AS RETAILER.USERNAME IS : " + obj.Mobile + ",PASSWORD IS : " + mOVal.MessageFromSQL + "");
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