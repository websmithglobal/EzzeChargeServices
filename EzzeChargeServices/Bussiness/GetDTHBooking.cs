using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;

namespace EzzeChargeServices.Bussiness
{
    public class GetDTHBooking : SqlHelper
    {
        public Bussiness.AuthResponse SaveDTHBooking(DataClass.DataDTHBooking obj)
        {
            string[,] param = {{"OperatorCode",obj.OperatorCode.ToString()},
                                {"ConnectionType",obj.ConnectionType.ToString()},
                                {"Qty",obj.Qty.ToString()},
                                {"FirstLanguageId",obj.FirstLanguageId.ToString()},
                                {"SecoundLanguageId",obj.SecoundLanguageId.ToString()},
                                {"FirstName",obj.FirstName.ToString()},
                                {"LastName",obj.LastName.ToString()},
                                {"EmailID",obj.EmailID.ToString()},
                                {"MobileNumber1",obj.MobileNumber1.ToString()},
                                {"MobileNumber2",obj.MobileNumber2.ToString()},
                                {"StateName",obj.StateName.ToString()},
                                {"CityName",obj.CityName.ToString()},
                                {"Pincode",obj.Pincode.ToString()},
                                {"Address",obj.Address.ToString()},
                                {"LandMark",obj.LandMark.ToString()},
                                {"Userlevel",obj.Userlevel.ToString()},
                                {"Mainid",obj.Mainid.ToString()}};
            MEMBERS.SQLReturnValue M = ExecuteProcWithMessageValue("SAVE_DTHBOOKING", param, true);
            if (M.ValueFromSQL == 1) { return new Bussiness.AuthResponse(1, M.MessageFromSQL); }
            else { return new Bussiness.AuthResponse(0, M.MessageFromSQL); }
        }
    }
}