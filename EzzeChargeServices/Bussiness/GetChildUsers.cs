using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Data;

namespace EzzeChargeServices.Bussiness
{
    public class GetChildUsers : SqlHelper
    {
        public List<DataClass.GetChildUsers> GetChildUser(int Mainid)
        {
            List<DataClass.GetChildUsers> GetDl = new List<DataClass.GetChildUsers>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT MAINID,USERNAME,Bal,MOBILE FROM USERS WHERE UPPERID=" + Mainid);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    GetDl.Add(new DataClass.GetChildUsers(int.Parse(row["MAINID"].ToString()), row["USERNAME"].ToString(), decimal.Parse(row["Bal"].ToString()), row["MOBILE"].ToString()));
                }
            }
            return GetDl;
        }
    }
}