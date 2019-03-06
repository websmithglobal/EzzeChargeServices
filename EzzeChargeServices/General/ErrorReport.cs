using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace EzzeChargeServices.General
{
    public static class ErrorReport
    {
        public static void LogError(Exception ex, string url)
        {
            String URL = "http://wsservice.appsmith.co.in/api/Error/StoreError";
            WebRequest request = WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";

            var values = new Dictionary<string, string>
            {
               { "ProjectName", "EzeeCharge" },
               { "ErrorText", ex.Message.ToString() },
               { "ErrorParameters", "world" },
               { "ErrorURL", url },
               { "ErrorStacktrace", ex.StackTrace.ToString() },
            };

            string jsonFormat = Newtonsoft.Json.JsonConvert.SerializeObject(values);

            Byte[] data = Encoding.UTF8.GetBytes(jsonFormat);

            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string str = reader.ReadLine();
        }
    }
}