using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataRechargeTickets
    {
        public int RechargeId { get; set; }
        public int TicketsId { get; set; }
        public int Userlevel { get; set; }
        public string Remarks { get; set; }
        public int Mainid { get; set; }
        public int Archiveid { get; set; }
    }
}