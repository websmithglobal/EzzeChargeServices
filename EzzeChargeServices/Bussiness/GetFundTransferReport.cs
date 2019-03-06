using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Data;
using System.Data.SqlClient;

namespace EzzeChargeServices.Bussiness
{
    public class GetFundTransferReport : SqlHelper
    {
        DataTable dtTemp;
        public List<DataClass.GetFundReport> GetFundReport(DataClass.DataFundTransferReport obj)
        {
            List<DataClass.GetFundReport> rptList = new List<DataClass.GetFundReport>();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@ISTODAY", obj.IStoday.ToString());
            param[1] = new SqlParameter("@FROMDATE", obj.FromDate + " 00:00:01");
            param[2] = new SqlParameter("@TODATE", obj.ToDate + " 23:59:59");
            param[3] = new SqlParameter("@USERLEVEL", obj.UserLevel.ToString());
            param[4] = new SqlParameter("@SEARCH", obj.Search);
            param[5] = new SqlParameter("@MAINID", obj.mainid.ToString());
            param[6] = new SqlParameter("@USERTYPE", obj.UserType.ToString());
            dtTemp = ExecuteProcedure("GET_FUNDREPORT", param);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    rptList.Add(new DataClass.GetFundReport(dtTemp.Rows[i]["FundDate"].ToString(), dtTemp.Rows[i]["Transby"].ToString(), dtTemp.Rows[i]["UserName"].ToString(), dtTemp.Rows[i]["Mobile"].ToString(), dtTemp.Rows[i]["Credit"].ToString(), dtTemp.Rows[i]["Debit"].ToString(), dtTemp.Rows[i]["ToOldBal"].ToString(), dtTemp.Rows[i]["ToCloseBal"].ToString(), (dtTemp.Rows[i]["UserLevel"].ToString() == "1" ? "MD" : dtTemp.Rows[i]["UserLevel"].ToString() == "2" ? "SD" : dtTemp.Rows[i]["UserLevel"].ToString() == "3" ? "R" : "NA"), dtTemp.Rows[i]["ReqVia"].ToString().Trim(), dtTemp.Rows[i]["Remarks"].ToString().Trim()));
                }

            }
            return rptList;
        }

    }
}