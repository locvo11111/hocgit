using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class CongTacBriefWithRoleDetailViewModel
    {
        public List<CongTacBriefViewModel> CongTacBriefs { get; set; }
        public List<RoleDetailViewModel> RoleDetails { get; set; }
    }
}