using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataDthBookingReport
    {
        public int IStoday { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int UserLevel { get; set; }
        public int Mainid { get; set; }
    }
    public class DataDthRpt
    {
        public string ReqDate { get; set; }
        public string OperatorName { get; set; }
        public string ConnectionType { get; set; }
        public int Qty { get; set; }
        public string Flname { get; set; }
        public string Llname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber1 { get; set; }
        public string MobileNumber2 { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Setupboxcharge { get; set; }
        public string Discount{ get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }

        public DataDthRpt() { }

        public DataDthRpt(string _ReqDate, string _OperatorName, string _ConnectionType, int _Qty, string _Flname, string _Llname, string _FirstName, string _LastName, string _Email, string _MobileNumber1,string _MobileNumber2,string _StateName,string _CityName,string _Address,string _Setupboxcharge,string _Discount,string _Remarks,string _Status)
        {
            this.ReqDate = _ReqDate; this.OperatorName = _OperatorName;
            this.ConnectionType = _ConnectionType; this.Qty = _Qty;
            this.Flname = _Flname; this.Llname = _Llname;
            this.FirstName = _FirstName; this.LastName = _LastName;
            this.Email = _Email; this.MobileNumber1 = _MobileNumber1;
            this.MobileNumber2 = _MobileNumber2; this.StateName = _StateName;
            this.CityName = _CityName; this.Address = _Address;
            this.Setupboxcharge = _Setupboxcharge; this.Discount = _Discount;
            this.Remarks = _Remarks; this.Status = _Status;
        }
    }
}


