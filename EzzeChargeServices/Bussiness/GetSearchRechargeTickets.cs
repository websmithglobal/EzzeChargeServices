using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetSearchRechargeTickets : SqlHelper
    {
        public Bussiness.AuthResponse SearchRechargeTickets(int Rechargeid, int Mainid)
        {
            DataTable dt = SqlHelper.GetDataUsingQuery("select top 1 req_id,req_status,Ticket_Reason from Add_Ticket_to_Recharge where req_id=" + Rechargeid + " and Mainid=" + Mainid + "");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["req_status"].ToString() == "0") { return new Bussiness.AuthResponse(0, "Wait Replay No. [" + Rechargeid + "]"); }
                else
                { return new Bussiness.AuthResponse(1, dt.Rows[0]["Ticket_Reason"].ToString()); }
            }
            else { return new Bussiness.AuthResponse(0, "Tickets Not Found.[" + Rechargeid + "]"); }
        }
    }
}