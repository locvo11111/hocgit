using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel.KLHN
{
    public class KLHNRequest
    {
        public TypeKLHN type { get; set; }
        public bool IsGetPrjHaveOwner { get; set; } = true;
        public bool IsGetMuiThiCong { get; set; } = false;
        public List<string> lsCodeMain { get; set; } = null;
        public DateTime? dateCalcLuyKe { get; set; } = null;
        public DateTime? dateBD { get; set; } = null;
        public DateTime? dateKT { get; set; } = null;
        public bool ignoreKLKH { get; set; } = false;
        public bool ignoreKLNhanThau { get; set; } = false;
        public bool isLapLaiKeHoach { get; set; } = false;
        public bool isCalcKLNhapKho { get; set; } = false;
        public int typeLayKhoiLuong { get; set; } = 0;//0: Chỉ lấy giao thầu, 1: Chỉ lấy nhận thầu, 2: Lấy cả giao + nhận
        public string CodeDuAn { get; set; }
        public string ConditionDMCT { get; set; }
        public string WhereCondition { get; set; }
        public List<DateTime> dates { get; set; }

    }
}
