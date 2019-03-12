using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EzzeChargeServices._MyConnection;

namespace EzzeChargeServices.Bussiness
{
    public class GetChannelPack
    {
        public List<DataClass.DataChannelPack.ChannelPack> GetChannelListByPack(int packid)
        {
            List<DataClass.DataChannelPack.ChannelPack> lstPack = new List<DataClass.DataChannelPack.ChannelPack>();
            DataTable dt = SqlHelper.GetDataUsingQuery("SELECT PackageMasterDetail.packchanel_sort, ChanelMaster.chanelname, ChanelMaster.price, ChanelMaster.chanelno, ChanelMaster.categoryid, CategoryMaster.categoryname FROM PackageMasterDetail INNER JOIN ChanelMaster ON PackageMasterDetail.packchanel_chanelid = ChanelMaster.chanelid INNER JOIN CategoryMaster ON ChanelMaster.categoryid = CategoryMaster.categoryid WHERE packchanel_packid = " + packid + " ORDER BY PackageMasterDetail.packchanel_sort");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lstPack.Add(new DataClass.DataChannelPack.ChannelPack(int.Parse(row["packchanel_sort"].ToString()), row["chanelname"].ToString(), decimal.Parse(row["packchanel_sort"].ToString()), int.Parse(row["chanelno"].ToString()), int.Parse(row["categoryid"].ToString()), row["categoryname"].ToString()));
                }
            }
            return lstPack;
        }
    }
}