using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    [JsonObject]
    [Serializable]
    public class KeyRequestInfoViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string SerialNo { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
        public string PCName { get; set; } = Environment.MachineName;
        public string SerialHDD { get; set; } = string.Empty;
    }
}
