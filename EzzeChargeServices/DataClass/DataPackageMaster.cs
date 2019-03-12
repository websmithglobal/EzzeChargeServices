using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataPackageMaster
    {
        public class DDLPackageMaster
        {
            public int pack_id { get; set; }
            public string pack_name { get; set; }

            public DDLPackageMaster(int _pack_id, string _pack_name)
            {
                this.pack_id = _pack_id;
                this.pack_name = _pack_name;
            }
        }
    }
}