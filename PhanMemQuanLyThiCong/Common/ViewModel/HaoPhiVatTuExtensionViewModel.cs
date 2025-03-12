using DevExpress.XtraRichEdit.Fields;
using Newtonsoft.Json;
using System;

namespace VChatCore.ViewModels.SyncSqlite
{
    public class HaoPhiVatTuExtensionViewModel : Tbl_TDKH_HaoPhiVatTuViewModel, ICloneable
    {
		public object Clone()
        {
            return this.MemberwiseClone();
        }
		//Column
		public bool? Chon { get; set; } = false;
        public string ParentCode { get; set; }
		public string CodeHangMuc { get; set; }
		public string CodePhanTuyen { get; set; }
		
		public double?  KhoiLuongKeHoach { get; set; }
		public double?  KhoiLuongKeHoachVatTu { get; set; }
		public double KhoiLuongDinhMucChuan { get; set; }

		public long DonGiaDinhMucGoc
		{
			get { return DonGiaDinhMucGocCustom ?? (long)Math.Round(DonGia * HeSo * DinhMuc); }
		}

        public long DonGiaDinhMucGiaoThau
        {
            get { return DonGiaDinhMucGiaoThauCustom??(long)Math.Round(DonGiaThiCong * HeSoNguoiDung * DinhMucNguoiDung); }
        }

		public long? DonGiaDinhMucGocCustom { get; set; }
		public long? DonGiaDinhMucGiaoThauCustom { get; set; }

		public long? DonGiaVatLieuDocVao { get; set; }
		public long? DonGiaNhanCongDocVao { get; set; }
		public long? DonGiaMayDocVao { get; set; }

		public long? DonGiaDocVao { get; set; }
		public long? ChenhLech
		{
			get
			{
				if (!DonGiaDocVao.HasValue)
					return null;

				return DonGiaDinhMucGiaoThau - DonGiaDocVao.Value;
			}
		}

        

    }
}
