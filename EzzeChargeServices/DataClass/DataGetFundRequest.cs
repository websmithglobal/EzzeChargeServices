using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataGetFundRequest
    {
        public string RequestDate { get; set; }
        public int Amount { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string Chqeue_DD_Online { get; set; }
        public string DateofDeposite { get; set; }
        public string TimeofDeposite { get; set; }
        public string Remarks { get; set; }

        public DataGetFundRequest(string _RequestDate, int _Amount, string _PaymentMode, string _BankName, string _Branch, string _Chqeue_DD_Online, string _DateofDeposite, string _TimeofDeposite, string _Remarks)
        {
            this.RequestDate = _RequestDate;
            this.Amount = _Amount; this.PaymentMode = _PaymentMode;
            this.BankName = _BankName; this.Branch = _BankName;
            this.Chqeue_DD_Online = _Chqeue_DD_Online; this.DateofDeposite = _DateofDeposite; this.TimeofDeposite = _TimeofDeposite;
            this.Remarks = _Remarks;
        }
    }
}