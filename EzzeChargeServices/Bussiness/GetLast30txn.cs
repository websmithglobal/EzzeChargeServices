using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetLast30txn
    {
        public List<DataClass.DataLast30txn.DataLast30> GetLast30Txn(int Mainid)
        {
            List<DataClass.DataLast30txn.DataLast30> ListUser = new List<DataClass.DataLast30txn.DataLast30>();
            DataTable dt = SqlHelper.GetDataUsingQuery("select top 30 R.REQDATE,O.OperatorName,R.NumbertoRecharge,R.Amount,R.TXID,R.BALNOW,R.STATUS from Recharge as R inner join Operators as O ON(R.OperatorCode=O.OperatorCode) where R.Mainid=" + Mainid + " Order By R.id desc");
            if (dt.Rows.Count > 0)
            {
                List<DataClass.DataLast30txn.DataLast30> ListObjSA = new List<DataClass.DataLast30txn.DataLast30>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListUser.Add(new DataClass.DataLast30txn.DataLast30(dt.Rows[i]["REQDATE"].ToString(), dt.Rows[i]["OperatorName"].ToString(), dt.Rows[i]["NumbertoRecharge"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["TXID"].ToString(), dt.Rows[i]["BALNOW"].ToString(), (dt.Rows[i]["STATUS"].ToString().ToUpper() == "S" ? "Success" : dt.Rows[i]["STATUS"].ToString().ToUpper() == "F" ? "Fail" : "Pending")));
                }
            }
            return ListUser;

        }
    }
}