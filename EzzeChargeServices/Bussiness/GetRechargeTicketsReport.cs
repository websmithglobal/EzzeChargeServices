using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Data;
using System.Data.SqlClient;

namespace EzzeChargeServices.Bussiness
{
    public class GetRechargeTicketsReport : SqlHelper
    {
        DataTable dtTemp;
        public List<DataClass.DataGRT> GetRechargeTickets(DataClass.DataGetRechargeTickets obj)
        {
            List<DataClass.DataGRT> rptList = new List<DataClass.DataGRT>();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@MAINID", obj.Mainid.ToString());
            param[1] = new SqlParameter("@FROMDATE", obj.FromDate.ToString() + " 00:00:01");
            param[2] = new SqlParameter("@TODATE", obj.ToDate.ToString() + " 23:59:59");
            param[3] = new SqlParameter("@SEARCH", obj.Search.ToString());
            param[4] = new SqlParameter("@LIVE", obj.LiveData.ToString());
            dtTemp = ExecuteProcedure("GET_USERS_TICKETS", param);
            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    rptList.Add(new DataClass.DataGRT(dtTemp.Rows[i]["EntryDate"].ToString(), dtTemp.Rows[i]["OperatorName"].ToString(), dtTemp.Rows[i]["NUMTORECH"].ToString(), dtTemp.Rows[i]["Amount"].ToString(), dtTemp.Rows[i]["REMARK"].ToString(), dtTemp.Rows[i]["REQ_ID"].ToString(), dtTemp.Rows[i]["STATUS"].ToString(), dtTemp.Rows[i]["TRANS_TYPE"].ToString(), dtTemp.Rows[i]["TICKET_REASON"].ToString(), dtTemp.Rows[i]["CLOSEDATE"].ToString()));
                }

            }
            return rptList;

        }
    }
}