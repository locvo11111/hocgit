using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class CongTac : ICloneable
    {
        public CongTac()
        {
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        [Key]
        public string Code { get; set; }
        public string CodeNhom { get; set; }
        public double? KhoiLuongNhom { get; set; }
        public string IdCongTacGd { get; set; }
        public bool Chon { get; set; } = false;
        public string STT { get; set; }
        public string MaHieuCongTac { get; set; }
        public string TenCongTac { get; set; }
        public string TenCongTacCompare { get; set; }
        public string keysMatch { get; set; }
        public int keyCount { get; set; }
        public int dinhMucLenght { get; set; }
        public string LyTrinhCaoDo { get; set; }
        public int? LanThiCong { get; set; }
        public string DonVi { get; set; }
        public double? KhoiLuongTT { get; set; }
        public double? KhoiLuongPS { get; set; }
        public double? DonGia { get; set; }
        public int? TongNhanCong { get; set; }

        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public DateTime? NBDThiCong { get; set; }
        public DateTime? NKTThiCong { get; set; }
        public int? soNgayThicong
        {
            get
            {
                if (NKTThiCong.HasValue && NBDThiCong.HasValue)
                    return (NKTThiCong.Value - NBDThiCong.Value).Days + 1;
                else
                    return null;
            }
            set
            {
            }
        }

        public int? TongNgayThucHien { get; set; }
        public int? SoNgayNghiGiuaThiCong { get; set; }
        public int? SoNgayNghiNghiemThu { get; set; }
        public DateTime? NgayNTNB { get; set; }
        public string GioNTNB { get; set; }
        public DateTime? NgayPYC { get; set; }
        public string GioPYC { get; set; }
        public DateTime? NgayNTXD { get; set; }
        public string GioNTXD { get; set; }
        public DateTime? NgayKTraTruocThiCong { get; set; }
        public string GioKTraTruocThiCong { get; set; }
        public DateTime? NgayKTraDangThiCong { get; set; }
        public string GioKTraDangThiCong { get; set; }
        public DateTime? NgaySauThiCongHoanThien { get; set; }
        public string GioSauThiCongHoanThien { get; set; }
        public DateTime? CaoDoKTHHNgay { get; set; }
        public DateTime? NgayLayMauThiNghiem { get; set; }
        public string GioLayMauThiNghiem { get; set; }
        public string KetQua { get; set; }
        public int? NhanCongTrongNgay { get; set; }
        public string MayThiCong { get; set; }
        public string MayPhuThiCong { get; set; }
        public string TenNKThiCongThucTe { get; set; }
        public string KyHieuBanVe { get; set; }
        public DateTime? NgayBienBanHienTruong { get; set; }
        public string GioBienBanHienTruong { get; set; }
        public string KyHieuPYC { get; set; }
        public string KyHieuNTXD { get; set; }
        public double? KLLKKyTruoc { get; set; }
        public double KhoiLuongToanBo { get; set; } = 0;
        public double? KhoiLuongHD { get; set; }
        public double? KLHKN { get; set; }
        public string DuongDanAnh { get; set; }
        public string NhomThamGia { get; set; }
        public string NhomKy { get; set; }
        public string BBNguoiDung { get; set; }
        public int LoaiTT { get; set; } = 2021;
        public string CTDinhMuc { get; set; }

        //private string _DinhMuc { get; set; }
        public int UuTien { get; set; }
        public double ChenhLechfi { get; set; } = double.MaxValue;
        public double ChenhLechcao { get; set; } = double.MaxValue;
        public double ChenhLechrong { get; set; } = double.MaxValue;
        public double ChenhLechk { get; set; } = double.MaxValue;
        public double ChenhLechdamnen { get; set; } = double.MaxValue;
        public double ChenhLechsau { get; set; } = double.MaxValue;
        public double ChenhLechday { get; set; } = double.MaxValue;
        public string TenBBVatLieu { get; set; }
        public int KNT { get; set; } = 1;
        public int? BBSauNT { get; set; }
        public int? BBTruocNT { get; set; }
        public int IdHangMuc { get; set; }
        public int IdGiaiDoan { get; set; }
        public string GhiChu { get; set; }
        public string HuongDan { get; set; }
        public string KyHieuBB { get; set; }
        public virtual List<int> TGNghiemThuTruocs { get; set; }
        public virtual List<int> TGNghiemThuSaus { get; set; }
        public string MaHangMuc { get; set; }
        public string MaGiaiDoan { get; set; }
        public int TypeId { get; set; }
        public int LoaiDinhMuc { get; set; }
        public string ParentId { get; set; } = Guid.Empty.ToString();
        public string MaGop { get; set; }
        public List<string> ListGops { get; set; } = new List<string>();
        public int? SoLanGop { get; set; }
        public int SortDefault { set; get; }
        public int? TrinhTuThiCong { set; get; }
        public int SortId { set; get; }

        /// <summary>
        ///Nhân công ngày tạm tính được sửa thủ công 
        /// </summary>
        public int? NhanCongNgayTamTinh { set; get; }
        public int? MayTamTinh { set; get; }
        public int? NhanCongMayTamTinh { set; get; } = null;
        public int? NhanCongMayTamTinhGoc { set; get; } = null;

        public string ListLoaiTrus { get; set; }
        //public int? TongNgayTamTinh { set; get; }
        public int? SoNgayTiepTheo { set; get; }
        public string RootId { get; set; }
        public string NguoiDung1 { get; set; }
        public string NguoiDung2 { get; set; }
        public string NguoiDung3 { get; set; }
        public string NguoiDung4 { get; set; }
        public string NguoiDung5 { get; set; }
        public string NguoiDung6 { get; set; }
        public string NguoiDung7 { get; set; }
        public string NguoiDung8 { get; set; }
        public string NguoiDung9 { get; set; }
        public string NguoiDung10 { get; set; }
        public string NguoiDung11 { get; set; }
        public string NguoiDung12 { get; set; }
        public string NguoiDung13 { get; set; }
        public string NguoiDung14 { get; set; }
        public string NguoiDung15 { get; set; }
        public string NguoiDung16 { get; set; }
        public string NguoiDung17 { get; set; }
        public string NguoiDung18 { get; set; }
        public string NguoiDung19 { get; set; }
        public string NguoiDung20 { get; set; }
        public string NguoiDung21 { get; set; }
        public string NguoiDung22 { get; set; }
        public string NguoiDung23 { get; set; }
        public string NguoiDung24 { get; set; }
        public string NguoiDung25 { get; set; }
        public string NguoiDung26 { get; set; }
        public string NguoiDung27 { get; set; }
        public string NguoiDung28 { get; set; }
        public string NguoiDung29 { get; set; }
        public string NguoiDung30 { get; set; }
        public int? TGNTR1 { get; set; }
        public int? TGNTR3 { get; set; }
        public int? TGNTR7 { get; set; }
        public int? TGNTR14 { get; set; }
        public int? TGNTR21 { get; set; }
        public int? TGNTR28 { get; set; }
        public bool IsNguoiDung { get; set; } = false;
        public string NhomMauBienBan { get; set; }
        public string TenHangMuc { get; set; }
        public string SheetName { get; set; }
        public int RowInd { get; set; }
        public string Hide { set; get; }
        public int? TongNhanCongConLai { get; set; }
        public string IsNghiemThu { get; set; } = "Đang thi công";

        public List<CongTac> ListCongTacGops { get; set; }

        public DateTime? MaxNBD { get; set; } = null;

        public virtual ChiTietCongTac ChiTietCongTac { get; set; } = new ChiTietCongTac();
    }
}
