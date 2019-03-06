using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetProfile
    {
        public List<DataClass.DataProfile.GetProfileData> GetMyProfile(int Mainid, string UserName, string Password)
        {
            List<DataClass.DataProfile.GetProfileData> GetP = new List<DataClass.DataProfile.GetProfileData>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT TOP 1 MainId,UserName,MAILID,MOBILE,ADDRESS FROM USERS WHERE MainId=" + Mainid + " and (MAILID='" + UserName + "' OR MOBILE='" + UserName + "') AND LOGINPASS='" + Password + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    GetP.Add(new DataClass.DataProfile.GetProfileData(int.Parse(row["MainId"].ToString()), row["UserName"].ToString(), row["MAILID"].ToString(), row["MOBILE"].ToString(), row["ADDRESS"].ToString()));
                }
            }
            return GetP;
        }
    }
}