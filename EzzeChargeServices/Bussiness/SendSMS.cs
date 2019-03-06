using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class SendSMS
    {
        
        public static string SendSMSFire(string Mobile,string Msg)
        {
            string URL = string.Empty;
            DataTable dt = SqlHelper.GetDataUsingQuery("select smsapi from sms_api where isdefault='1'");
            if (dt.Rows.Count >= 1)
            {
                URL = dt.Rows[0]["smsapi"].ToString();
            }
            try
            {
                string SendMsg = URL.Replace("#mesg#", Msg).Replace("#mob#", Mobile);
                WebClient webC = new WebClient();
                System.IO.Stream str = webC.OpenRead(new Uri(SendMsg.ToLower().Contains("http://") ? SendMsg : "http://" + SendMsg));
                System.IO.StreamReader strReader = new System.IO.StreamReader(str);
                string responseString = strReader.ReadToEnd();
                strReader.Close();
                str.Close();
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }

        }

    }
}