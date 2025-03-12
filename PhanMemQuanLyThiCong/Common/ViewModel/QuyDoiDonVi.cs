using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class QuyDoiDonVi
    {
        public bool Chon { get; set; } = false;
        public string Code { get; set; }
        public string MaHieuCongTac { get; set; }
        public string TenCongTac { get; set; }
        public string DonVi { get; set; }
        public double KhoiLuongToanBo { get; set; }
        public double KhoiLuongGoc { get; set; }
        public string DonViQuyDoi { get; set; }
        public double HeSoQuyDoiDonVi { get; set; } 
        public string DonViNew { get; set; }
        public double HeSoNew { get; set; }
        public double? KhoiLuongNewCustom { get; set; } = null;
        public double KhoiLuongNew
        {
            get => KhoiLuongNewCustom ?? KhoiLuongGoc * HeSoNew;
        }
    }
}
