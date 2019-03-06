using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataLast30txn
    {
        public class DataLast30
        {
            public string ReqDate { get; set; }
            public string OperatorName { get; set; }
            public string NumberToRecharge { get; set; }
            public string Amount { get; set; }
            public string Txid { get; set; }
            public string NowBal { get; set; }
            public string Status { get; set; }

            public DataLast30() { }

            public DataLast30(string _ReqDate, string _OperatorName, string _NumberToRecharge, string _Amount, string _Txid, string _NowBal, string _Status)
            {
                this.ReqDate = _ReqDate; this.OperatorName = _OperatorName; this.NumberToRecharge = _NumberToRecharge; this.Amount = _Amount;
                this.Txid = _Txid; this.NowBal = _NowBal; this.Status = _Status;
            }
        }
      
    }
}