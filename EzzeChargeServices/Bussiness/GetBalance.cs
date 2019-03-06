using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetBalance
    {
        public List<DataClass.DataAcBal.GetACBal> GetACBalanceList(int Mainid, string UserName, string Password)
        {
            List<DataClass.DataAcBal.GetACBal> BalList = new List<DataClass.DataAcBal.GetACBal>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT TOP 1 ISNULL(Bal,0) as Bal  FROM USERS WHERE MainId=" + Mainid + " and (MAILID='" + UserName + "' OR MOBILE='" + UserName + "') AND LOGINPASS='" + Password + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BalList.Add(new DataClass.DataAcBal.GetACBal(float.Parse(row["Bal"].ToString())));
                }
            }
            return BalList;
        }
    }
}