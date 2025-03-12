using VChatCore.ViewModels.SyncSqlite;
using System.ComponentModel.DataAnnotations;
using VChatCore.Dto;

namespace PhanMemQuanLyThiCong.Model
{
    public class AppDuAnGoupViewModel
    {
        public int GroupId { set; get; }

        [StringLength(128)]
        public string TongHopDuAnId { get; set; }

        public virtual AppGroupViewModel AppGroup { set; get; }

    }
}