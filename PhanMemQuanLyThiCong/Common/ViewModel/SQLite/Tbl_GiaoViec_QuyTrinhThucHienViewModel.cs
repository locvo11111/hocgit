using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class Tbl_GiaoViec_QuyTrinhThucHienViewModel : ICloneable
    {
		        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //Column
		public string Code { get; set; } = "";
		public string CodeDauViec { get; set; }
		public string CodeCongViecChaHienTai { get; set; }
		public string CodeCongViecConHienTai { get; set; }
		public string CodeCongViecConTiepTheo { get; set; }
		public int LoaiTuongQuan { get; set; } = 0;
		public int SoNgay { get; set; } = 0;
		public string TrangThai { get; set; } = "";
		public string CodeCongViecChaTiepTheo { get; set; }
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }

    }
}
