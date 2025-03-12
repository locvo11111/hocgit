using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class PhuLucThuCong
    {
        public string Code { get; set; }
        public int STT { get; set; }
        public int SortId { get; set; }
        public string TenCongViec { get; set; }
        public bool Modified { get; set; }
        public string MaHieu { get; set; }
        public string DonVi { get; set; }
        public double DonGia { get; set; }
        public double KhoiLuong { get; set; }
        public long ThanhTien { get { return (long)Math.Round(DonGia * KhoiLuong); } }
        public string TenNoiDungUng { get; set; }
        public string CodeCha { get; set; }
        public string LastChange { get; set; }
        public bool ModifiedFromServer { get; set; }
        public string CreatedOn { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }

    }
}
