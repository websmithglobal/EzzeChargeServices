using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataFundTransferReport
    {
        public int IStoday { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Search { get; set; }
        public int UserLevel { get; set; }
        public int mainid { get; set; }
        public int UserType { get; set; }

    }
    public class GetFundReport
    {
        public string FundDate { get; set; }
        public string FundFrom { get; set; }
        public string FundTo { get; set; }
        public string Mobile { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string ToOldBal { get; set; }
        public string ToCloseBal { get; set; }
        public string UserLevel { get; set; }
        public string ReqVia { get; set; }
        public string Remarks { get; set; }

        public GetFundReport() { }

        public GetFundReport(string _FundDate, string _FundFrom, string _FundTo, string _Mobile, string _Credit, string _Debit, string _ToOldBal, string _ToCloseBal, string _UserLevel, string _ReqVia, string _Remarks)
        {
            this.FundDate = _FundDate;
            this.FundFrom = _FundFrom;
            this.FundTo = _FundTo;
            this.Mobile = _Mobile;
            this.Credit = _Credit;
            this.Debit = _Debit;
            this.ToOldBal = _ToOldBal;
            this.ToCloseBal = _ToCloseBal;
            this.UserLevel = _UserLevel;
            this.ReqVia = _ReqVia;
            this.Remarks = _Remarks;
        }
    }
}