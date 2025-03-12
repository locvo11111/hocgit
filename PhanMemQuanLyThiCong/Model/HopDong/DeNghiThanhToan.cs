using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.HopDong
{
    public class DeNghiThanhToan
    {
        public double S1 = 0;
        public double S2 = 0;
        public double S3 = 0;
        public string Code{ get; set; }
        public string GhiChu{ get; set; }
        public string Type_Row { get; set; }
        public string TenCongTac{ get; set; }
        public string DonVi{ get; set; }
        public string LuyKeKyTruoc{ get; set; }
        public string LuyKeKyNay{ get; set; }
        public string KyNay { get; set; }
        public int LoaiCT { get; set; }
        public int IndexCT { get; set; }
        public int IndexCha { get; set; }
        public bool IsCoDinh { get; set; } =false;
        public bool SetLuyKeKyTruoc { get {if (double.TryParse(LuyKeKyTruoc, out S1))
                    return true;
                else return false;
            } }      
        public bool SetKyNay { get {if (double.TryParse(KyNay, out  S2))
                    return true;
                else return false;
            } }        
        public bool SetLuyKeKyNay{ get {if (double.TryParse(LuyKeKyNay, out S3))
                    return true;
                else return false;
            } }
    }
}
