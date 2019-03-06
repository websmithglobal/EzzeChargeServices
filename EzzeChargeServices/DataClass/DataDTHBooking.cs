using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataDTHBooking
    {
        public int OperatorCode { get; set; }
        public int ConnectionType { get; set; }
        public int Qty { get; set; }
        public int FirstLanguageId { get; set; }
        public int SecoundLanguageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber1 { get; set; }
        public string MobileNumber2 { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }
        public string LandMark { get; set; }
        public int Mainid { get; set; }
        public int Userlevel { get; set; }
    }
}
