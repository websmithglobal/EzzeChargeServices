using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;

namespace EzzeChargeServices.Bussiness
{
    public class GetRechargeReport : SqlHelper
    {
        DataTable dtTemp;
        public List<DataClass.DataRpt> GetSearchRechargeReport(DataClass.RechargeReport obj)
        {
            List<DataClass.DataRpt> rptList = new List<DataClass.DataRpt>();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ISTODAY", obj.IStoday.ToString());
            param[1] = new SqlParameter("@FROMDATE", obj.FromDate.ToString() + " 00:00:01");
            param[2] = new SqlParameter("@TODATE", obj.ToDate.ToString() + " 23:59:59");
            param[3] = new SqlParameter("@USERLEVEL", obj.UserLevel.ToString());
            param[4] = new SqlParameter("@SEARCH", obj.Search.ToString());
            param[5] = new SqlParameter("@MAINID", obj.mainid.ToString());
            param[6] = new SqlParameter("@OPCODE", obj.Opcode.ToString());
            param[7] = new SqlParameter("@STATUS", obj.Status.ToString());
            param[8] = new SqlParameter("@AMT", obj.Amount.ToString());
            param[9] = new SqlParameter("@MOBILE", obj.Mobile.ToString());
            param[10] = new SqlParameter("@USERTYPE", obj.UserType.ToString());
            if (obj.LiveData == 0)
            { dtTemp = ExecuteProcedure("GET_RECHRGREREPORT", param); }
            else
            { dtTemp = ExecuteProcedure("GET_RECHRGREREPORT_ARCHIVE", param); }

            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                rptList.Add(new DataClass.DataRpt(int.Parse(dtTemp.Rows[i]["TicketStatus"].ToString()),int.Parse(dtTemp.Rows[i]["ID"].ToString()), dtTemp.Rows[i]["REQDATE"].ToString(), dtTemp.Rows[i]["operatorname"].ToString(), dtTemp.Rows[i]["NUMBERTORECHARGE"].ToString(), dtTemp.Rows[i]["AMOUNT"].ToString(), dtTemp.Rows[i]["TXID"].ToString(), dtTemp.Rows[i]["BALNOW"].ToString(), (dtTemp.Rows[i]["STATUS"].ToString().ToUpper() == "S" ? "Success" : dtTemp.Rows[i]["STATUS"].ToString().ToUpper() == "F" ? "Fail" : "Pending"), dtTemp.Rows[i]["Reqtyp"].ToString(), dtTemp.Rows[i]["REQVIA"].ToString().Trim(),int.Parse(dtTemp.Rows[i]["isTicketShow"].ToString())));
            }
            return rptList;

        }
    }
}