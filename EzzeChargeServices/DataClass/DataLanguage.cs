using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzzeChargeServices.DataClass
{
    public class DataLanguage
    {
        public class GetLanguage
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public GetLanguage(string _Name,int _Id)
            {
                this.Id = _Id;
                this.Name = _Name;
            }
        }
    }
}