using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
namespace EzzeChargeServices.Bussiness
{
    public class GetRechargeTickets : SqlHelper
    {
        public Bussiness.AuthResponse SaveRechargeTickets(DataClass.DataRechargeTickets obj)
        {
            string[,] param = {{"mainid",obj.Mainid.ToString()},
                           {"req_id",obj.RechargeId.ToString()},
                           {"tran_type",obj.TicketsId.ToString()},
                           {"userlevel",obj.Userlevel.ToString()},
                           {"remark",obj.Remarks.ToString()},
                           {"archiveid",obj.Archiveid.ToString()}};
            MEMBERS.SQLReturnValue M = ExecuteProcWithMessageValue("ADD_TICKET", param, true);
            if (M.ValueFromSQL == 1) { return new Bussiness.AuthResponse(1, "Your Ticket has be Successfully Recieved.your Ticket No." + obj.TicketsId.ToString() + " Ticket Date is:" + DateTime.Now.ToString() + ""); }
            else { return new Bussiness.AuthResponse(0, M.MessageFromSQL); }

        }
    }
}