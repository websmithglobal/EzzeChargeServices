using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataGetDistributorUsers
    {
        public class GetDistUsers
        {
            public int Mainid { get; set; }
            public string UserName { get; set; }

            public GetDistUsers(int _Mainid, string _UserName)
            {
                this.Mainid = _Mainid; this.UserName = _UserName;
            }
        }



    }
}