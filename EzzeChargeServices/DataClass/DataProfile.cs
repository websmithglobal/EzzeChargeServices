using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataProfile
    {
        public class GetProfileData
        {
            public int Mainid { get; set; }
            public string UserName { get; set; }
            public string EmailID { get; set; }
            public string Mobile { get; set; }

            public string Address { get; set; }

            public GetProfileData(int _Mainid, string _UserName, string _EmailID, string _Mobile, string _Address)
            {
                this.Mainid = _Mainid; this.UserName = _UserName; this.EmailID = _EmailID; this.Mobile = _Mobile;
                this.Address = _Address;
            }
        }
    }
}