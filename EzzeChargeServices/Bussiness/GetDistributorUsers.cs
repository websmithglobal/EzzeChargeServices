using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Data;
namespace EzzeChargeServices.Bussiness
{
    public class GetDistributorUsers
    {
        public List<DataClass.DataGetDistributorUsers.GetDistUsers> GetDistUsers(int Mainid, int Userlevel, string UserName, string Password)
        {
            List<DataClass.DataGetDistributorUsers.GetDistUsers> GetDl = new List<DataClass.DataGetDistributorUsers.GetDistUsers>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT MAINID,USERNAME FROM USERS WHERE UPPERID=" + Mainid + " AND Userlevel=2");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    GetDl.Add(new DataClass.DataGetDistributorUsers.GetDistUsers(int.Parse(row["MAINID"].ToString()), row["USERNAME"].ToString()));
                }
            }
            return GetDl;
        }
    }
}