using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetDTHBoxTypeMaster
    {
        public List<DataClass.DataDTHBoxType.BoxTypeMaster> GetDTHBoxType()
        {
            List<DataClass.DataDTHBoxType.BoxTypeMaster> BalList = new List<DataClass.DataDTHBoxType.BoxTypeMaster>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT boxtypeid, boxtype, boxtypestatus FROM BoxTypeMaster WHERE boxtypestatus=1");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BalList.Add(new DataClass.DataDTHBoxType.BoxTypeMaster(Int64.Parse(row["boxtypeid"].ToString()), row["boxtype"].ToString(), bool.Parse(row["boxtypestatus"].ToString())));
                }
            }
            return BalList;
        }
    }
}