using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ThuChiTamUng
{
    public class ChiPhiGiaiChiChiTiet
    {
        public string Code { get; set; }
        public bool ChiTietCoDonGia { get; set; } = true;
        public double KhoiLuong { get; set; }
        public double DonGia { get; set; }
        public string DonVi { get; set; }
        public int STT { get; set; }
        public string NoiDungUng { get; set; }
        public string Ten { get; set; }
        public string File { get; set; } = "File đính kèm";
        public string GhiChu { get; set; }
        public string CodeKC { get; set; }
        public string CodeKCNew { get; set; }
        public long ThanhTienChiTiet { get; set; }
        public long ThanhTien
        {
            get
            {
                if (ChiTietCoDonGia)
                    return (long)(KhoiLuong * DonGia);
                else
                    return ThanhTienChiTiet;
            }
        }

        public string CodeCha { get
            {
                if (CodeKC == null)
                {
                    return CodeKCNew;
                }

                else
                {
                    return CodeKC;
                }

            }}       
        public string colcode
        { get
            {
                if (CodeKC == null)
                {
                    //colcode = "CodeKCNew";
                    return "CodeKCNew";
                }

                else
                {
                    //colcode = "CodeKC";
                    return "CodeKC";
                }

            }}
    }
}
