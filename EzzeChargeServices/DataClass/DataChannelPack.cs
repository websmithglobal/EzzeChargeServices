﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataChannelPack
    {
        public class ChannelPack
        {
            public int packchanel_sort { get; set; }
            public string chanelname { get; set; }
            public decimal price { get; set; }
            public int chanelno { get; set; }
            public int categoryid { get; set; }
            public string categoryname { get; set; }

            public ChannelPack(int _packchanel_sort, string _chanelname, decimal _price, int _chanelno, int _categoryid, string _categoryname)
            {
                this.packchanel_sort = _packchanel_sort;
                this.chanelname = _chanelname;
                this.price = _price;
                this.chanelno = _chanelno;
                this.categoryid = _categoryid;
                this.categoryname = _categoryname;
            }
        }
    }

    public class ChannelPackageList
    {
        public Int64 categoryid { get; set; }
        public string categoryname { get; set; }
        public List<ChannelList> lstChannel { get; set; }
    }

    public class ChannelList
    {
        public Int64 packchanel_sort { get; set; }
        public string chanelname { get; set; }
        public decimal price { get; set; }
        public Int64 chanelno { get; set; }
        public string boxtype { get; set; }
    }

    public class ChannelViewerList
    {
        public Int64 chanelid { get; set; }
        public Int64 operatorid { get; set; }
        public Int64 boxtypeid { get; set; }
        public string boxtype { get; set; }
        public string chanelname { get; set; }
        public decimal price { get; set; }
        public Int64 chanelno { get; set; }
        public Int64 categoryid { get; set; }
        public string categoryname { get; set; }
        public string detail { get; set; }
        public bool chanelstatus { get; set; }
    }

    public class ChannelApiParam
    {
        public string channelname { get; set; }
    }
}