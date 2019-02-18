using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;

namespace SMS.Data.Services
{
    public static class MvcExtension
    {
        public static ActionResult ToMvcJson(this object obj)
        {
            //return newtonsoft.Json.JsonConvert.SerializeObject(obj);
            ContentResult con=new  ContentResult();
            con.Content= JsonConvert.SerializeObject(obj);
            return con;
        }
        public static ActionResult ToMvcJson(this object obj, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings settings)
        {
            ContentResult con = new ContentResult();
            con.Content = JsonConvert.SerializeObject(obj,formatting,settings);
            return con;
        }
        public static ActionResult ToMvcJson(this object obj, string DateTimeFormats = "yyyy-MM-dd HH:mm:ss")
        {
            ContentResult con = new ContentResult();
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = DateTimeFormats };
            con.Content = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, timeConverter);
            return con;
        }
    }
}
