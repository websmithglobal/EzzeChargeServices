using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public sealed class AuthResponse
    {
        public int Code;
        public string Message;
        public AuthResponse() { }
        public AuthResponse(int _code, string _msg)
        {
            this.Code = _code; this.Message = _msg;
        }
    }
}