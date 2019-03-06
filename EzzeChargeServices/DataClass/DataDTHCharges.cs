using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataDTHCharges
    {
        public class GetDcharges
        {

            public int operatorcode { get; set; }
            public int connectiontype { get; set; }
            public float setupboxcharge { get; set; }
            public float yourcost { get; set; }
            public float discount { get; set; }
            public float YourBalance { get; set; }

            public GetDcharges(int _operatorcode,int _connectiontype,float _setupboxcharge,float _yourcost, float _discount,float _YourBalance)
            {
                this.operatorcode = _operatorcode;
                this.connectiontype = _connectiontype;
                this.setupboxcharge = _setupboxcharge;
                this.yourcost = _yourcost;
                this.discount = _discount;
                this.YourBalance = _YourBalance;
            }
        }
    }
}