using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_BangNhanVienViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string MaNhanVien { get; set; }
		public string TenNhanVien { get; set; }
		public string HinhAnh { get; set; }
		public string ChucVu { get; set; }
		public DateTime? NgayKyHopDong { get; set; }
		public string MaSoThue { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string SoDienThoai { get; set; }
		public long? SoNguoiPhuThuoc { get; set; }
		public string PhongBan { get; set; }
		public string DiaChi { get; set; }
		public int? SortId { get; set; }
		public string CCCD { get; set; }
		public string GioiTinh { get; set; } = "Nam";
		public string Email { get; set; }
		public string DanToc { get; set; }
		public string TrangThaiLamViec { get; set; } = "Đang làm việc";
		public bool? IsNhanVienCty { get; set; } = false;
		public string TenNhanVienKhongDau { get; set; }
		public string NhanVienVanPhongCongTruong { get; set; } = "Văn phòng";
		public string MauThe { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
