using PhanMemQuanLyThiCong.Model;
using System.ComponentModel.DataAnnotations;

namespace PhanMemQuanLyThiCong.Model
{
    public class AppUserGroupViewModel
    {
        [StringLength(128)]
        public string UserId { set; get; }

        public int GroupId { set; get; }
        public virtual AppUserViewModel AppUser { set; get; }

        public virtual AppGroupViewModel AppGroup { set; get; }
    }
}