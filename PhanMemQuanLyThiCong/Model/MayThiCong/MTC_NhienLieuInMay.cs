namespace PhanMemQuanLyThiCong.Model.MayThiCong
{
    public class MTC_NhienLieuInMay: MTC_NhienLieu
    {
        //public string Code { get; set; }
        public string CodeMay { get; set; }
        public string CodeChiTiet { get; set; }//=Code Code này là Code tham chiếu đến bảng nhiên liệu in máy
        public string CodeMayTheoDuAn { get; set; }
        public string CodeNhienLieu { get; set; }
        public int LoaiNhienLieu { get; set; }
        public double? MucTieuThu { get; set; }
        //public bool Modified { get; set; }
        //public int SortId { get; set; }
        //public string LastChange { get; set; }
        //public string CreatedOn { get; set; }
    }
}