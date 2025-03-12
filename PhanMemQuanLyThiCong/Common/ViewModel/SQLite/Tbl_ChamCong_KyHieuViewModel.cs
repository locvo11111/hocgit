using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_ChamCong_KyHieuViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; }
		public string NghiOm { get; set; }
		public double? PhanTramNghiOm { get; set; }
		public string HocTap { get; set; }
		public double? PhanTramHocTap { get; set; }
		public string NghiDe { get; set; }
		public double? PhanTramNghiDe { get; set; }
		public string NghiPhep { get; set; }
		public double? PhanTramNghiPhep { get; set; }
		public string ChamCong { get; set; }
		public double? PhanTramChamCong { get; set; }
		public string CongTac { get; set; }
		public double? PhanTramCongTac { get; set; }
		public string NghiTheoCheDo { get; set; }
		public string NghiThu7ChuNhat { get; set; }
		public string NghiKhongLyDo { get; set; }
		public double? PhanTramNghiTheoCheDo { get; set; }
		public double? PhanTramNghiThu7ChuNhat { get; set; }
		public double? PhanTramNghiKhongLyDo { get; set; }
		public double? BaoHiem { get; set; }
		public double? Thue { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
