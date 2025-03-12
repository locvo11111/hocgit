using Newtonsoft.Json;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class CacheHelper
    {
        public static bool UseCache => MSETTING.Default.UseCache;// ConfigHelper.GetByKey("UseCache").ToLower() == "true";

        public static string ConvertObjectToString(object p)
        {
            string key = "";
            if (p == null) return key;
            return JsonConvert.SerializeObject(p);
        }
    }
}
