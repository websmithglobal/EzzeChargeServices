using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataGetRechargeTickets
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Search { get; set; }
        public int Mainid { get; set; }
        public int LiveData { get; set; }
    }
    public class DataGRT
    {
        public string EntryDate { get; set; }
        public string OperatorName { get; set; }
        public string Number { get; set; }
        public string Amount { get; set; }
        public string Remakrs { get; set; }
        public string TicketNo { get; set; }
        public string Status { get; set; }
        public string TransType { get; set; }

        public string Reason { get; set; }
        public string Closedate { get; set; }
        public DataGRT() { }

        public DataGRT(string _EntryDate, string _OperatorName, string _Number, string _Amount, string _Remakrs, string _TicketNo, string _Status, string _TransType, string _Reason, string _Closedate)
        {
            this.EntryDate = _EntryDate;
            this.OperatorName = _OperatorName; this.Number = _Number; this.Amount = _Amount;
            this.Remakrs = _Remakrs; this.TicketNo = _TicketNo; this.Status = _Status; this.TransType = _TransType;
            this.Reason = _Reason; this.Closedate = _Closedate;
        }

    }
}