using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;

namespace EzzeChargeServices.Bussiness
{
    public class GetPackageMaster
    {
        public List<DataClass.DataPackageMaster.DDLPackageMaster> GetPackageListByOperator(int operatorCode)
        {
            List<DataClass.DataPackageMaster.DDLPackageMaster> lstPack = new List<DataClass.DataPackageMaster.DDLPackageMaster>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT pack_id, pack_name FROM PackageMaster WHERE operator_id = "+ operatorCode + " ORDER BY pack_name");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lstPack.Add(new DataClass.DataPackageMaster.DDLPackageMaster(int.Parse(row["pack_id"].ToString()), row["pack_name"].ToString()));
                }
            }
            return lstPack;
        }
    }
}