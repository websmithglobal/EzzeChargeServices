using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataLedgerReport
    {
        public class GetLedgerList
        {
            public string Date { get; set; }
            public float BeforeBal { get; set; }
            public float Credit { get; set; }
            public float Debit { get; set; }
            public float AfterBal { get; set; }
            public string Remarks { get; set; }
            public string LedgerType { get; set; }
            public GetLedgerList(string _Date, float _BeforeBal, float _Credit, float _Debit, float _AfterBal, string _Remarks, string _LedgerType)
            {
                this.Date = _Date; this.BeforeBal = _BeforeBal;
                this.Credit = _Credit; this.Debit = _Debit;
                this.AfterBal = _AfterBal; this.Remarks = _Remarks;
                this.LedgerType = _LedgerType;
                
            }
        }
    }
}