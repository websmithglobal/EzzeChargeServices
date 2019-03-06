using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;

namespace EzzeChargeServices.Bussiness
{
    public class GetDthBookingReport : SqlHelper
    {
        DataTable dtTemp;
        public List<DataClass.DataDthRpt> GetDTHReport(DataClass.DataDthBookingReport obj)
        {
            List<DataClass.DataDthRpt> rptList = new List<DataClass.DataDthRpt>();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@ISTODAY", obj.IStoday.ToString());
            param[1] = new SqlParameter("@FROMDATE", obj.FromDate.ToString() + " 00:00:01");
            param[2] = new SqlParameter("@TODATE", obj.ToDate.ToString() + " 23:59:59");
            param[3] = new SqlParameter("@USERLEVEL", obj.UserLevel.ToString());
            param[4] = new SqlParameter("@MAINID", obj.Mainid.ToString());
            dtTemp = ExecuteProcedure("GET_DTHBOOKINGREPORT", param);
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                rptList.Add(new DataClass.DataDthRpt(dtTemp.Rows[i]["EntryDate"].ToString(), dtTemp.Rows[i]["OperatorName"].ToString(), dtTemp.Rows[i]["ConnectionType"].ToString(), int.Parse(dtTemp.Rows[i]["Qty"].ToString()), dtTemp.Rows[i]["Flname"].ToString(), dtTemp.Rows[i]["Llname"].ToString(), dtTemp.Rows[i]["FirstName"].ToString(), dtTemp.Rows[i]["LastName"].ToString(), dtTemp.Rows[i]["EmailID"].ToString(), dtTemp.Rows[i]["MobileNumber1"].ToString(), dtTemp.Rows[i]["MobileNumber2"].ToString(), dtTemp.Rows[i]["StateName"].ToString(), dtTemp.Rows[i]["CityName"].ToString(), dtTemp.Rows[i]["Address"].ToString(), dtTemp.Rows[i]["Setupboxcharge"].ToString(), dtTemp.Rows[i]["Discount"].ToString(), dtTemp.Rows[i]["Remarks"].ToString(), dtTemp.Rows[i]["Status3"].ToString()));
            }
            return rptList;

        }
    }
}