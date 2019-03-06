using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataDistributor
    {
        public int mainid { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Userlevel { get; set; }

        public string PancardNo { get; set; }
        public string GstNo { get; set; }
    }
}