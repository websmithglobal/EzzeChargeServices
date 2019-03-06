using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataFundRequest
    {
        public int FAmount { get; set; }
        public int PaymentMode { get; set; }
        public string TxnPassword { get; set; }
        public int BankNameID { get; set; }
        public string Branch { get; set; }
        public string ChqeueDDOnlineType { get; set; }
        public string DateofDeposite { get; set; }
        public string TimeofDeposite { get; set; }
        public string Remarks { get; set; }
        public int MainId { get; set; }
    }
}