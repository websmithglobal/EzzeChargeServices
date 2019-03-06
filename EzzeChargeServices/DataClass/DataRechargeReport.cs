using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{

    public class RechargeReport
    {
        public int IStoday { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Search { get; set; }
        public int UserLevel { get; set; }
        public int mainid { get; set; }
        public int Opcode { get; set; }
        public string Status { get; set; }
        public string Mobile { get; set; }
        public string Amount { get; set; }
        public int UserType { get; set; }
        public int LiveData { get; set; }

        
    }

    public class DataRpt
    {
        public int RechargeID { get; set; }
        public string ReqDate { get; set; }
        public string OperatorName { get; set; }
        public string NumberToRecharge { get; set; }
        public string Amount { get; set; }
        public string Txid { get; set; }
        public string NowBal { get; set; }
        public string Status { get; set; }
        public string ReqType { get; set; }
        public string Via { get; set; }

        public int TicketStatus { get; set; }
        public int isTicketShow { get; set; }
        
        public DataRpt() { }

        public DataRpt(int _TicketStatus, int _RechargeID, string _ReqDate, string _OperatorName, string _NumberToRecharge, string _Amount, string _Txid, string _NowBal, string _Status, string _ReqType, string _Via,int _isTicketShow)
        {
            this.RechargeID = _RechargeID;
            this.TicketStatus = _TicketStatus;
            this.ReqDate = _ReqDate; this.OperatorName = _OperatorName; this.NumberToRecharge = _NumberToRecharge; this.Amount = _Amount;
            this.Txid = _Txid; this.NowBal = _NowBal; this.Status = _Status; this.ReqType = _ReqType; this.Via = _Via;
            this.isTicketShow = _isTicketShow;
        }
    }

}