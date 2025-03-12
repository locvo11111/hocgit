using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.ChamCong
{
    class ChiTietChamCong_Gio_Ngay
    {
        public string Code { get; set; }
        public string ChuThich { get; set; }
        public string GhiChu { get; set; }
        public string CodeNhanVien { get; set; }
        public bool? Modified { get; set; }
        public string CreatedOn { get; set; }
        public string LastChange { get; set; }
        public string Buoi { get; set; } = "Sáng";
        public string LoaiChamCong { get; set; } = "Vào";
        public DateTime? DateTime { get; set; }
        public DateTime? GioChamCongVao { get; set; }
        public DateTime? GioChamCongRa { get; set; }
        public int GioVao { get { return GioChamCongVao.HasValue?GioChamCongVao.Value.Hour:0; } }
        public int PhutVao { get { return GioChamCongVao.HasValue?GioChamCongVao.Value.Minute:0; } }       
        public int GioRa { get { return GioChamCongRa.HasValue?GioChamCongRa.Value.Hour:0; } }
        public int PhutRa { get { return GioChamCongRa.HasValue?GioChamCongRa.Value.Minute:0; } }
        public int GioVaoMacDinh { get; set; }
        public int PhutVaoMacDinh { get; set; }      
        public int GioRaMacDinh { get; set; }
        public int PhutRaMacDinh { get; set; }
        public int Label { get; set; }
        public string NghiLam { get; set; } = "";
        public string TangCa { get; set; } = "";
        public string VaoTre { get; set; } = "";
        public string RaSom { get; set; } = "";
        public double HeSoTangCa { get; set; }
        public void ChiTietChamCong()
        {
            if (NghiLam != "")
                Label = 3;
            else if (TangCa != "")
                Label = 4;
            else if (GioVao == GioVaoMacDinh && GioRa == GioRaMacDinh && PhutVao == PhutVaoMacDinh && PhutRa == PhutRaMacDinh)
            {
                Label = 0;//bình thường
            }
            else if (GioVao < GioVaoMacDinh && GioRa > GioRaMacDinh 
                || GioVao == GioVaoMacDinh && PhutVao <= PhutVaoMacDinh && GioRa == GioRaMacDinh && PhutRa >= PhutRaMacDinh
                ||GioVao<GioRaMacDinh&&GioRa==GioRaMacDinh&&PhutRa>PhutRaMacDinh)
            {
                Label = 1;//Sớm
            }
            else
            {
                Label = 2;//trễ
            }
        }     
        public  double TongCong()
        {
            return Math.Round((double)(GioVao * 60 + PhutVao - GioRa * 60 - GioRa) / 60, 3)*HeSoTangCa;
        }
        public void Fcn_CheckVaoTreRaSom()
        {
            if (GioVao>GioVaoMacDinh||GioVao==GioVaoMacDinh&&PhutVao>PhutVaoMacDinh)
            {
                if(DateTime.HasValue)
                    VaoTre =$"Ngày {DateTime.Value.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} Giờ vào {GioChamCongVao.Value.ToString("HH:mm")}" +
                        $" Giờ ra {GioChamCongRa.Value.ToString("HH:mm")} ";
            }
            else if (GioRa < GioRaMacDinh||GioRa==GioRaMacDinh&&PhutRa<PhutRaMacDinh)
            {
                if (DateTime.HasValue)
                    RaSom = $"Ngày {DateTime.Value.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} Giờ vào {GioChamCongVao.Value.ToString("HH:mm")}" +
                        $" Giờ ra {GioChamCongRa.Value.ToString("HH:mm")} ";
            }
        }


    }
}
