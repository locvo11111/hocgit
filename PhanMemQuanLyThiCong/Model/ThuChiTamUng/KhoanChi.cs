using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class KhoanChi
    {
        public string NoiDungUng { get; set; }
        public string AddCP { get; set; }
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string CodeDeXuat { get; set; }
        public double GiaTriTamUngDaDuyet { get; set; }
        public double GiaTriTamUngThucTe { get; set; }
        public string STT { get; set; }
        public double GiaTriGiaiChi { get; set; }
        public double ConLai { get { return GiaTriTamUngThucTe - GiaTriGiaiChi; } }
        public int TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string ChonChiTiet { get; set; }
        public string File { get; set; }
        public string LoaiKinhPhi { get; set; }
        public string CongTrinh { get; set; }
        public string NguoiLapTamUng { get; set; }
        public string CodeHd { get; set; }
        //public string CodeHd { get; set; }
        public string ToChucCaNhanNhanChiPhiTamUng { get; set; }
        public bool Chon { get; set; }
        public bool IsLayChiTiet { get; set; } = true;
        public bool CheckDaChi { get; set; }
        public bool CheckDaUng { get; set; }
        public DateTime DateThucNhanUng { get; set; }
        public DateTime DateXacNhanDaChi { get; set; }
        public DateTime DateXacNhanDaUng { get; set; }
    }
}
