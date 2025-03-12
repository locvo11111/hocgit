using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class GiaoViecWithRolesInGiaoViecViewModel
    {
        public List<GiaoViecExtensionViewModel> giaoviecs {  get; set; } = new List<GiaoViecExtensionViewModel>();
        public List<UserWithRoleInGiaoViec> roles { get; set; } = new List<UserWithRoleInGiaoViec> { };
    }
}
