namespace PhanMemQuanLyThiCong.Model.MayThiCong
{
    public class MTC_ChiTietDinhMuc
    {
        public string Code { get; set; }
        public string CodeMay { get; set; }
        public string TenMay { get; set; }
        public string CodeDinhMuc { get; set; }
        public string DinhMucCongViec { get; set; }
        public string DonVi { get; set; }
        public double MucTieuThu { get; set; }
        public string GhiChu { get; set; }
        public bool Chon { get; set; } = false;
    }
}