using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    public class LayCongTac : Tbl_TDKH_ChiTietCongTacTheoGiaiDoanViewModel
    {
        public bool? Chon { get; set; } = false;
        public bool DaLay { get; set; }

        public string Id { get; set; }
        public string ParentId { get; set; }
        public double KhoiLuongHopDong { get; set; }
        public string MoTa { get; set; }
        public string CodeCongViecCha { get; set; }
        //public string CodeCongTacGiaoThau { get; set; }
        //public string TenCongTac { get; set; }
        public string TenHangMuc { get; set; }
        //public string CodeHangMuc { get; set; }
        //public string CodePhanTuyen { get; set; }
        public string TenPhanTuyen { get; set; }
        public string TenCongTrinh { get; set; }
        public string CodeCongTrinh { get; set; }
        //public double KhoiLuongHopDongToanDuAn { get; set; }
        //public string DonVi { get; set; }
        //public string MaHieuCongTac { get; set; }

        public double KhoiLuongDaGiao { get; set; }
        public double KhoiLuongConLai 
        { 
            get
            {
                return KhoiLuongToanBo - KhoiLuongDaGiao;
            }
        }

        public double KhoiLuongCanLay { get; set; }

        public string DispHangMuc
        {
            get
            {
                return $"{TenHangMuc}_{CodeHangMuc}";
            }
        }

        public string DispCongTrinh
        {
            get
            {
                return $"{TenCongTrinh}_{CodeCongTrinh}";
            }
        }
    }
}
