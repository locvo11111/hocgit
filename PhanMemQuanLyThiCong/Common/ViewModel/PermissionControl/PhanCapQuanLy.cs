using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel.PermissionControl
{
    public class PhanCapQuanLy
    {
        public string Id { get; set; }
        public string ParentId { get; set; }

        public int? Level { get; set; }
        public string Ten {  get; set; }

        public string LevelCusString { get; set; }
        public string LevelString
        {
            get
            {
                if (LevelCusString != null)
                    return LevelCusString;

                if (Level is null)
                    return null;

                return $"Quản trị cấp {Level}";
            }
        }
    }
}
