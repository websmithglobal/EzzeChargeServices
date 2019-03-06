using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
namespace EzzeChargeServices.Bussiness
{
    public class GetIMEIClass : SqlHelper
    {
        public Bussiness.AuthResponse Save_IMEI(DataClass.DataIMEIClass obj)
        {
            int OutS = SqlHelper.DML("UPDATE USERS SET IMEI='" + obj.IMEI + "' WHERE MAINID=" + obj.Mainid + "");
            return new Bussiness.AuthResponse(OutS == 1 ? 1 : 0, OutS == 1 ? "IMEI UPDATED" : "IMEI UPDATE FAILED");
        }
    }
}