using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ChamCong
{
    public class CaiDaGioLamChamCong
    {
        public string Code { get; set; }
        public string CodeDuAn { get; set; }
        public string BuoiSang_Chieu_Toi { get; set; } = "Chiều";
        public string MuaHe_Dong { get; set; } = "Mùa hè";
        public string CongTruong_VanPhong { get; set; } = "Công trường";
        public int GioVao { get; set; }
        public int PhutVao { get; set; }
        public int GioRa { get; set; }
        public int PhutRa { get; set; }
        public int VaoTre { get; set; }
        public int RaSom { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public bool? Modified { get; set; }
        public string CreatedOn { get; set; }
        public string LastChange { get; set; }
        public CaiDaGioLamChamCong()
        {
            if (VaoTre != 0)
            {
                if (VaoTre <= PhutVao)
                    PhutVao = PhutVao - VaoTre;
                else
                {
                    int Sophut = GioVao * 60 + PhutVao+VaoTre;
                    GioVao = Sophut / 60;
                    PhutVao = Sophut%60;
                }
            }
            if (RaSom != 0)
            {
                if (RaSom <= PhutRa)
                    PhutRa = PhutRa - RaSom;
                else
                {
                    int Sophut = GioRa * 60 + PhutRa-RaSom;
                    GioRa = Sophut / 60;
                    PhutRa = Sophut % 60;
                }
            }
        }
    }
}
