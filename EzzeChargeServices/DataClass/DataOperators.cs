using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataOperators
    {
        public class OperatorList
        {
            public string OperatorName { get; set; }

            public int OperatorCode { get; set; }

            public int ServiceType { get; set; }

            public string Images { get; set; }
            public OperatorList() { }
            public OperatorList(string _OperatorName, int _OperatorCode, int _ServiceType, string _Images)
            {
                this.OperatorName = _OperatorName; this.OperatorCode = _OperatorCode; this.ServiceType = _ServiceType;
                this.Images = _Images;
            }
        }
    }
}