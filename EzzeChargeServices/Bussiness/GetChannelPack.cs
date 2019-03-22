using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EzzeChargeServices._MyConnection;
using EzzeChargeServices.DataClass;

namespace EzzeChargeServices.Bussiness
{
    public class GetChannelPack
    {
        public List<ChannelPackageList> GetChannelListByPack(int packid)
        {
            List<ChannelPackageList> lstCategory = new List<ChannelPackageList>();
            try
            {
                DataTable dt = SqlHelper.GetDataUsingQuery("SELECT PackageMasterDetail.packchanel_sort, ChanelMaster.chanelname, ChanelMaster.price, ChanelMaster.chanelno, ChanelMaster.categoryid, CategoryMaster.categoryname FROM PackageMasterDetail INNER JOIN ChanelMaster ON PackageMasterDetail.packchanel_chanelid = ChanelMaster.chanelid INNER JOIN CategoryMaster ON ChanelMaster.categoryid = CategoryMaster.categoryid WHERE packchanel_packid = " + packid + " ORDER BY PackageMasterDetail.packchanel_sort");

                var val1 = (from dr in dt.AsEnumerable()
                            select new {
                                categoryid = dr.Field<Int64>("categoryid"),
                                categoryname = dr.Field<string>("categoryname")
                            }).Distinct();

                lstCategory = (from dr1 in val1
                               select new ChannelPackageList()
                               {
                                   categoryid = dr1.categoryid,
                                   categoryname = dr1.categoryname,
                                   lstChannel = (from dr in dt.AsEnumerable()
                                                 where dr.Field<Int64>("categoryid").Equals(dr1.categoryid)
                                                 select new ChannelList()
                                                 {
                                                     packchanel_sort = dr.Field<Int64>("packchanel_sort"),
                                                     chanelname = dr.Field<string>("chanelname"),
                                                     price = dr.Field<decimal>("price"),
                                                     chanelno = dr.Field<Int64>("chanelno")
                                                 }).ToList()
                               }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstCategory;
        }
    }
}