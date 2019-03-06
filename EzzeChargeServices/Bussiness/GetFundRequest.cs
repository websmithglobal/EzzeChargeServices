using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;

namespace EzzeChargeServices.Bussiness
{
    public class GetFundRequest : SqlHelper
    {
        public Bussiness.AuthResponse SaveFundRequest(DataClass.DataFundRequest obj)
        {
            string[,] param = { {"FUNDREQUESTID","1"},
                        {"FAMOUNT",obj.FAmount.ToString()},
                        {"PAYMENTMODE",obj.PaymentMode.ToString()},
                        {"TXNPASSWORD",obj.TxnPassword},
                        {"BANKNAME",obj.BankNameID.ToString()},
                        {"BRANCH",obj.Branch},
                        {"CHQEUEDDONLINETYPE",obj.ChqeueDDOnlineType},
                        {"DATEOFDEPOSITE",obj.DateofDeposite},
                        {"TIMEOFDEPOSITE",obj.TimeofDeposite},
                        {"REMARKS",obj.Remarks.ToString()},
                        {"MAINID",obj.MainId.ToString()}};
            MEMBERS.SQLReturnValue M = ExecuteProcWithMessageValue("SAVE_FUNDREQUEST", param, true);
            if (M.ValueFromSQL == 1) { return new Bussiness.AuthResponse(1, M.MessageFromSQL); }
            else { return new Bussiness.AuthResponse(0, M.MessageFromSQL); }
        }
    }
}