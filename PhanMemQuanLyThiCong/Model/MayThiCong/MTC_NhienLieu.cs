namespace PhanMemQuanLyThiCong.Model.MayThiCong
{
    public class MTC_NhienLieu
    {
        public int? STT { get; set; }
        public string Code { get; set; }
        public string Ten { get; set; }
        public string DonVi { get; set; }
        public double? KhoiLuongDaNhap { get; set; } = 0;
        public double? KhoiLuongDaDung { get; set; } = 0;
        public bool? IsHienThi { get; set; } = false;
        public bool Modified { get; set; }
        public int SortId { get; set; }
        public string LastChange { get; set; }
        public string CreatedOn { get; set; }
    }
}