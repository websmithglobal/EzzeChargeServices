using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetOperators
    {
        public List<DataClass.DataOperators.OperatorList> GetOperatorList()
        {
            List<DataClass.DataOperators.OperatorList> ListUser = new List<DataClass.DataOperators.OperatorList>();
            DataTable dt = SqlHelper.GetDataUsingQuery("select OperatorName,operatorCode,SERVICETYPE,OperatorImages from Operators where Status=1 order by operatorid asc");
            if (dt.Rows.Count > 0)
            {
                List<DataClass.DataOperators.OperatorList> ListObjSA = new List<DataClass.DataOperators.OperatorList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListUser.Add(new DataClass.DataOperators.OperatorList(dt.Rows[i]["OperatorName"].ToString(), int.Parse(dt.Rows[i]["operatorCode"].ToString()), int.Parse(dt.Rows[i]["ServiceType"].ToString()), "http://paytomoney.com/Operator/" + dt.Rows[i]["OperatorImages"].ToString()));
                }
            }
            return ListUser;

        }
    }
}