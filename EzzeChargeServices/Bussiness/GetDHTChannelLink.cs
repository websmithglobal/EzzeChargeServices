using EzzeChargeServices._MyConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.Bussiness
{
    public class GetDHTChannelLink
    {
        public List<DataClass.DTHChannelLinkList> GetDTHChannelLink()
        {
            List<DataClass.DTHChannelLinkList> lstLinkList = new List<DataClass.DTHChannelLinkList>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT DTHChannelLink.link_id, DTHChannelLink.link_operatorid, Operators.OperatorName, DTHChannelLink.link_name, DTHChannelLink.link_weburl, DTHChannelLink.link_videolink, DTHChannelLink.link_filename, DTHChannelLink.link_filepath FROM DTHChannelLink INNER JOIN Operators ON DTHChannelLink.link_operatorid = Operators.operatorCode ORDER BY DTHChannelLink.link_name");

            lstLinkList = (from dr in dt.AsEnumerable()
                           select new DataClass.DTHChannelLinkList()
                           {
                               link_id = dr.Field<Int64>("link_id"),
                               link_operatorid = dr.Field<Int64>("link_operatorid"),
                               OperatorName = dr.Field<string>("OperatorName"),
                               link_name = dr.Field<string>("link_name"),
                               link_weburl = dr.Field<string>("link_weburl"),
                               link_videolink = dr.Field<string>("link_videolink"),
                               link_filename = dr.Field<string>("link_filename"),
                               link_filepath = dr.Field<string>("link_filepath")
                           }).ToList();
            return lstLinkList;
        }
    }
}