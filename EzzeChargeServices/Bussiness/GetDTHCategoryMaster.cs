using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetDTHCategoryMaster
    {
        public List<DataClass.DataCategoryMaster.CategoryMaster> GetDTHCategory()
        {
            List<DataClass.DataCategoryMaster.CategoryMaster> BalList = new List<DataClass.DataCategoryMaster.CategoryMaster>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT categoryid, categoryname, categorystatus FROM CategoryMaster WHERE categorystatus=1");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BalList.Add(new DataClass.DataCategoryMaster.CategoryMaster(Int64.Parse(row["categoryid"].ToString()), row["categoryname"].ToString(), bool.Parse(row["categorystatus"].ToString())));
                }
            }
            return BalList;
        }
    }
}