using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class UserWithRoleInGiaoViec
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string CommandId { get; set; }


    }
}
