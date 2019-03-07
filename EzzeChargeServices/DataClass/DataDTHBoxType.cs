using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataDTHBoxType
    {
        public class BoxTypeMaster
        {
            public Int64 boxtypeid { get; set; }
            public string boxtype { get; set; }
            public bool boxtypestatus { get; set; }

            public BoxTypeMaster(Int64 _boxtypeid, string _boxtype, bool _boxtypestatus)
            {
                this.boxtypeid = _boxtypeid;
                this.boxtype = _boxtype;
                this.boxtypestatus = _boxtypestatus;
            }
        }
    }
}