using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetLanguage
    {
        public List<DataClass.DataLanguage.GetLanguage> GetLanguageList()
        {
            List <DataClass.DataLanguage.GetLanguage > BalList = new List<DataClass.DataLanguage.GetLanguage> ();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT languageid,languageName from languageMaster order by languageid desc");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BalList.Add(new DataClass.DataLanguage.GetLanguage(row["languageName"].ToString(),int.Parse(row["languageid"].ToString())));
                }
            }
            return BalList;
        }
    }
}