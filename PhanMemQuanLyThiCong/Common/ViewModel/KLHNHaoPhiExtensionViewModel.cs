using VChatCore.ViewModels.SyncSqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class KLHNHaoPhiExtensionViewModel : Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgayViewModel
    {
        public string CodeVatTu { get; set; }
        public string CodeGiaiDoan { get; set; }
        public string CodeHangMuc { get; set; }
        public string MaVatLieu { get; set; }
        public string VatTu { get; set; }
        public string DonVi { get; set; }
        public string CodeNhaThau { get; set; }
        public string CodeNhaThauPhu { get; set; }
        public string CodeToDoi { get; set; }
        public string CodeDonViThucHien { get; set; }
        public long DonGia { get; set; }
        public string LoaiVatTu { get; set; }
        public double KhoiLuongToanBo { get; set; }
        public double HeSoNguoiDung { get; set; }
        public double DinhMucNguoiDung { get; set; }
        public double KhoiLuongKeHoachHaoPhi
        {
            get
            {
                return KhoiLuongToanBo * HeSoNguoiDung * DinhMucNguoiDung;
            }
        }
        public string SearchString 
        {
            get { return $"{MaVatLieu};{VatTu};{DonVi};{DonGia}"; }
        }
    }
}
