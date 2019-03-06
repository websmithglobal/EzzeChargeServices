using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataNotification
    {
        public class NotificationALL
        {
            public string Date{ get; set; }
            public string Notification { get; set; }
            public string title { get; set; }

            public NotificationALL(string _Date, string _Notification,string _title)
            {
                this.Date = _Date; this.Notification = _Notification;this.title = _title;
            }
        }
    }
}