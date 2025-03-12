using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class RoleDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? TaskId { get; set; }
        public string CongViecChaCode { get; set; }
        public string CongViecTheoGiaiDoanCode { get; set; }
        public string CommandId { get; set; }
    }
}
