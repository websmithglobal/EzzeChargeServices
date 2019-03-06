using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class GetChildUsers
    {
        public int Mainid { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public decimal Balance { get; set; }

        public GetChildUsers(int _Mainid, string _UserName , decimal _Bal, string _MobileNumber)
        {
            this.Mainid = _Mainid; this.UserName = _UserName;this.Balance = _Bal;this.MobileNumber = _MobileNumber;
        }
    }
}