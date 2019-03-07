using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataCategoryMaster
    {
        public class CategoryMaster
        {
            public Int64 categoryid { get; set; }
            public string categoryname { get; set; }
            public bool categorystatus { get; set; }

            public CategoryMaster(Int64 _categoryid, string _categoryname, bool _categorystatus)
            {
                this.categoryid = _categoryid;
                this.categoryname = _categoryname;
                this.categorystatus = _categorystatus;
            }
        }
    }
}