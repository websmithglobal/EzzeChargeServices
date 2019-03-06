using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace EzzeChargeServices.DataClass
{
    public class UserAuthntication : SoapHeader
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}