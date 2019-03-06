using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;
using System.Data;

namespace EzzeChargeServices.Bussiness
{
    public class GetNotification:SqlHelper
    {
        public List<DataClass.DataNotification.NotificationALL> GetNotificationCall(int Userlevel)
        {
            List<DataClass.DataNotification.NotificationALL> ListObjSA = new List<DataClass.DataNotification.NotificationALL>();
            DataTable dt = SqlHelper.GetDataUsingQuery("select noti,notidt,title from notifications where (scope=" + Userlevel + " or scope=-1) AND status=1 order by notiID desc");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListObjSA.Add(new DataClass.DataNotification.NotificationALL(dt.Rows[i]["notidt"].ToString(), dt.Rows[i]["noti"].ToString(), dt.Rows[i]["title"].ToString()));
                }
                
            }
            return ListObjSA;
        }
    }
}