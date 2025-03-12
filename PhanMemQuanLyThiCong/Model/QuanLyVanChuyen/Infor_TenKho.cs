using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.QuanLyVanChuyen
{
    public class Infor_TenKho
    {
        public int SortId { get; set; }
        public string ID { get; set; }
        public string TenKho { get; set; }
        public string CodeDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string CodeHangMuc { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeKhoChung { get; set; }
        public bool ModifiedFromServer { get; set; } = false;
        public bool? Modified { get; set; } = false;
        public DateTime? LastChange { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ColCode
        {
            get
            {
                if (!string.IsNullOrEmpty(CodeDuAn))
                    return "CodeDuAn";
                else if (!string.IsNullOrEmpty(CodeHangMuc))
                    return "CodeHangMuc";
                else if (!string.IsNullOrEmpty(CodeCongTrinh))
                    return "CodeCongTrinh";
                else
                    return "CodeKhoChung";
            }
        }    
        public string NguonPhatSinh
        {
            get
            {
                if (!string.IsNullOrEmpty(TenDuAn))
                    return TenDuAn;
                else
                    return "Nguồn kho chung";
            }
        }      
        public string TenGhep
        {
            get
            {
                if (NguonPhatSinh==TenDuAn)
                {
                    if (!string.IsNullOrEmpty(CodeDuAn))
                        return $"Dự án:{TenKho}";
                    else 
                        return $"{TenKho} thuộc {TenDuAn}";
                }    
                else
                    return $"Nguồn kho chung:{TenKho}";
            }
        }


    }
}
