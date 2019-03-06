using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataDoRecharge
    {
        public int OperatorCode { get; set; }
        public int RecType { get; set; }
        public string NumberToRecharge { get; set; }
        public int Amount { get; set; }
        public int Mainid { get; set; }
        public string Via { get; set; }
    }
}