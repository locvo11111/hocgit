using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ChamCong
{
    public class DanhSachNhanVienModel
    {
        public string UrlImage { get; set; }
        public string Code { get; set; }
        public string DuAn { get; set; }
        public string CodePhuPhi { get; set; }
        public string CodeDuAn { get; set; }
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; } = "";
        public string TenGhep { get { return $"{TenNhanVien}({MaNhanVien})"; } }
        public string NhanVienVanPhongCongTruong { get; set; } = "Văn phòng";
        public string TenNhanVienKhongDau { get { return MyFunction.fcn_RemoveAccents(TenNhanVien); } }
        public string HinhAnh { get; set; }
        public string LogoSecond { get; set; }
        public string LogoFirst { get; set; }
        public string FilePath { get; set; }
        public string FilePathLoGo1 { get; set; }
        public string FilePathLoGo2 { get; set; }
        public string ChucVu { get; set; } = "20";
        public DateTime? NgayKyHopDong { get; set; }
        public string MaSoThue { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public int? SoNguoiPhuThuoc { get; set; }
        public long? LuongCoBan { get; set; }
        public long? PhuCapTrachNhiem { get; set; }
        public long? TienCom { get; set; }
        public long? XangXe { get; set; }
        public long? NhaOConNho { get; set; }
        public long? SoTienGiamTru { get; set; }
        public long? KhoanKhac { get; set; }
        public long? TamUng { get; set; }
        public long? NgayTinhLuong { get; set; } = 22;
        public string PhongBan { get; set; } = "3";
        public string DiaChi { get; set; }
        public bool? Modified { get; set; }
        public string CreatedOn { get; set; }
        public string LastChange { get; set; }
        public string Link { get; set; } = "Thêm ảnh đại diện";
        public string FileDinhKem { get; set; } = "Thêm File Nhân sự";
        public string CCCD { get; set; }
        public string Email { get; set; }
        public string GioiTinh { get; set; } = "Nam";
        public string DanToc { get; set; } = "Kinh";
        public string MauThe { get; set; }

        public string TrangThaiLamViec { get; set; } = "Đang làm việc";
        public bool IsNhanVienCty { get; set; } = true;

    }
}
