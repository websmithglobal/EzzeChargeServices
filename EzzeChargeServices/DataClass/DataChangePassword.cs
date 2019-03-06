using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataChangePassword
    {
        public int Mainid { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public int PwdType { get; set; }
    }
}