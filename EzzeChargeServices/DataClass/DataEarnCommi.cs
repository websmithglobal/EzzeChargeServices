using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataEarnCommi
    {
        public class GetEarn
        {
            public float Commi { get; set; }

            public GetEarn(float _Commi)
            {
                this.Commi = _Commi;
            }
        }
    }
}