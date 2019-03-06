using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetEarnCommi
    {
        public List<DataClass.DataEarnCommi.GetEarn> GetEarnCommission(int Mainid)
        {
            List<DataClass.DataEarnCommi.GetEarn> BalList = new List<DataClass.DataEarnCommi.GetEarn>();
            DataTable dt = SqlHelper.GetDataUsingQuery("select ISNULL(SUM((CASE WHEN U.userlevel=3 THEN R.COMMIR WHEN U.userlevel=2 THEN R.COMMISD WHEN U.userlevel=1 THEN R.COMMIMD END)),0) AS COMMI from recharge as r inner join users as u on(R.mainid=U.mainid) where r.mainid=" + Mainid + " and r.reqdate >= dateadd(d,datediff(d, 0, getdate()), 0) and  r.reqdate < dateadd(d, datediff(d, 0, getdate())+1, 0)");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BalList.Add(new DataClass.DataEarnCommi.GetEarn(float.Parse(row["COMMI"].ToString())));
                }
            }
            return BalList;
        }
    }
}