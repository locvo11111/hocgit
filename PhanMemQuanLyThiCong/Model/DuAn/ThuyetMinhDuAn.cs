using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.DuAn
{
    public class ThuyetMinhDuAn
    {
		public string STT { get; set; } = "";
		public string NoiDung { get; set; } 
		public string Code { get; set; }
		public string Xoa { get; set; } 
		public string CodeParent { get; set; }
		public string CodeDuAn { get; set; }
		public string FileDinhKem { get; set; } = "";
		public string HinhAnhDiKem { get; set; }
		public DateTime? Ngay { get; set; }
		public int State { get; set; } = 0;
		public int? SortId { get; set; }
		public bool ModifiedFromServer { get; set; } = false;
		public bool? Modified { get; set; } = false;
		public DateTime? LastChange { get; set; }
		public DateTime? CreatedOn { get; set; }
	}
}
