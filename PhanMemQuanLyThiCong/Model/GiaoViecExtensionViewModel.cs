using VChatCore.ViewModels.SyncSqlite;
using System;
using System.Collections.Generic;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Common.ViewModel;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Enums;

namespace PhanMemQuanLyThiCong.Model
{
    public class GiaoViecExtensionViewModel : Tbl_GiaoViec_CongViecChaViewModel
    {
        public bool Chon { get; set; }
        //public string CodeCongViecCha { get; set; }
        public string CodeCongViecCon { get; set; }

        public string Id { get; set; }
        public string ParentId { get; set; }

        public string IdTreelist
        {
            get { return Id ?? CodeCongViecCon ?? CodeCongViecCha; }
        }

        public string ParentIdTreelist
        {
            get { return ParentId ?? ((CodeCongViecCon is null) ? null : CodeCongViecCha); }
        }
        
        public string CodeDauViecLon { get; set; }
        public string CodeCongTrinh { get; set; }
        public string CodeDuAn { get; set; }
        public string CodeThda { get; set; }
        public Guid UserId { get; set; }
        public string CommandId { get; set; }
        public Guid TaskId { get; set; }
        public string CongViecTheoGiaiDoanCode { get; set; }
        public string TenHangMuc { get; set; }
        public string TenCongTrinh { get; set; }
        public string TenDauViecNho { get; set; }
        public string TenDauViecLon { get; set; }
        public string TenDuAn { get; set; }
        public int TotalFile { get; set; }
        public int TotalApprove { get; set; }
        public double TyLe
        {
            get
            {
                if (TotalFile == 0 || TotalApprove == 0) return 0;
                return (double)Math.Ceiling((double)TotalApprove / TotalFile * 100.0);
            }
        }

        [JsonIgnore]
        public TypeCongTacEnum typeCongTac
        {
            get { return (CodeCongViecCon is null) ? TypeCongTacEnum.CONGTACCHA : TypeCongTacEnum.CONGTACCON; }
        }

        public DateTime? Ngay { get; set; }
        public double? KLKHNgay { get; set; }
        public double? KLTCNgay { get; set; }

        public string CodeFstLevel
        {
            get
            {
                return CodeCongTrinh ?? CodeDauViecLon;
            }
        }

        public string TenFstLevel
        {
            get
            {
                return TenCongTrinh ?? TenDauViecLon;
            }
        }

        public string CodeSndLevel
        {
            get
            {
                return CodeHangMuc ?? CodeDauMuc;
            }
        }

        public string TenSndLevel
        {
            get
            {
                return TenHangMuc ?? TenDauViecNho;
            }
        }

        public List<string> Permissions { get; set; }
        public List<Guid> ListUserViews { get; set; } = new List<Guid>();
        public List<Guid> ListUserDuyets { get; set; } = new List<Guid>();
        public List<Guid> ListUserThiCongs { get; set; } = new List<Guid>();
        public List<GiaoViec_FileDinhKemExtensionViewModel> ListFiles { get; set; } = new List<GiaoViec_FileDinhKemExtensionViewModel>();
        public List<GiaoViec_FileDinhKemExtensionViewModel> ListFilesCon { get; set; } = new List<GiaoViec_FileDinhKemExtensionViewModel>();
    }
}
