using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
namespace EzzeChargeServices.Bussiness
{
    public class GetLedgerReport : SqlHelper
    {
        public List<DataClass.DataLedgerReport.GetLedgerList> GetLedger(int Mainid, int UserLevel)
        {
            List<DataClass.DataLedgerReport.GetLedgerList> BalList = new List<DataClass.DataLedgerReport.GetLedgerList>();
            string[,] param = { { "USERID", Mainid.ToString() },
                              { "USERLEVEL", UserLevel.ToString() }};
            DataTable dtMR = ExecuteProcedureReturnDataTable("GET_LEDGER_INAPPS", param);
            foreach (DataRow row in dtMR.Rows)
            {
                BalList.Add(new DataClass.DataLedgerReport.GetLedgerList(row["Date"].ToString(), float.Parse(row["BeforeBal"].ToString()), float.Parse(row["Credit"].ToString()), float.Parse(row["Debit"].ToString()), float.Parse(row["AfterBal"].ToString()), row["Remarks"].ToString(), row["LedgerType"].ToString()));
            }
            return BalList;

        }
    }
}