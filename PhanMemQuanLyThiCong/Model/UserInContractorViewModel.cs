using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class UserInContractorViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string CodeNhaThau { get; set; }
        public string CodeToDoi { get; set; }
        public string CodeNhaThauPhu { get; set; }

        public bool IsAdmin { get; set; } = false;

        public string ContractorName { get; set; } = "";
        public FieldUpdateEnum FieldUpdate { get; set; } = FieldUpdateEnum.None;

        [JsonIgnore]
        public string CodeAutoGen
        {
            get
            {
                return CodeNhaThau ?? CodeToDoi ?? CodeNhaThauPhu;
            }
        }
    }
}
