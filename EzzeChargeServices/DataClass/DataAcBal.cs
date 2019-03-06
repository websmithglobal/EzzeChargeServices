using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataAcBal
    {
        public class GetACBal
        {

            public float Balance { get; set; }

            public GetACBal(float _Balance)
            {
                this.Balance = _Balance;
            }
        }
    }
}