using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetDTHCharges
    {
        public List<DataClass.DataDTHCharges.GetDcharges> GetChargesList(int Mainid, int OperatorCode, int ConnectionType)
        {
            List<DataClass.DataDTHCharges.GetDcharges> BalList = new List<DataClass.DataDTHCharges.GetDcharges>();
            DataTable dt = SqlHelper.GetDataUsingQuery("select top 1 D.operatorcode,D.connectiontype,D.setupboxcharge,D.yourcost,D.discount,(select Bal from Users where mainid=" + Mainid + ") as YourBalance from DTHSaleSetup as D inner join Operators as O on(D.operatorcode=O.operatorCode) inner join Users as U on(D.mainid=U.MainId) where D.operatorcode =" + OperatorCode + " and D.connectiontype =" + ConnectionType + "");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BalList.Add(new DataClass.DataDTHCharges.GetDcharges(int.Parse(row["operatorcode"].ToString()), int.Parse(row["connectiontype"].ToString()), float.Parse(row["setupboxcharge"].ToString()), float.Parse(row["yourcost"].ToString()), float.Parse(row["discount"].ToString()), float.Parse(row["YourBalance"].ToString())));
                }
            }
            return BalList;
        }
    }
}