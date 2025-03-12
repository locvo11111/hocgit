using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class MyDataHelper
    {
        public static T ReadDataFromJson<T>(HttpResponseMessage response)
        {
            string result = response.Content.ReadAsStringAsync().Result;
            //return JsonConvert.DeserializeObject<T>(JObject.Parse(result)["data"].ToString());
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
    }
}
