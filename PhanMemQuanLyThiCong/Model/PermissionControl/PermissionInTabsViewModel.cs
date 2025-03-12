using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.PermissionControl
{
    public class PermissionInTabsViewModel
    {
        public string FunctionName { get; set; }
        public string CmdCode { get; set; }

        [JsonIgnore]
        public string TabName 
        { 
            get 
            {

                    return ((FunctionCode)Enum.Parse(typeof(FunctionCode), BaseFrom.dicFcnCode[FunctionName])).GetEnumDescription();

            }
        }
    }
}
