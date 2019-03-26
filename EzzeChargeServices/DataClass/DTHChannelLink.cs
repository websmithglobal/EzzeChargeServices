using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DTHChannelLinkList
    {
        public Int64 link_id { get; set; }
        public Int64 link_operatorid { get; set; }
        public string OperatorName { get; set; }
        public string link_name { get; set; }
        public string link_weburl { get; set; }
        public string link_videolink { get; set; }
        public string link_filename { get; set; }
        public string link_filepath { get; set; }
    }
}