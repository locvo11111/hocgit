using System.ComponentModel;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public enum CommandCode
    {
        [Description("Thêm")] //Chỉ admin mới có quyền add
        Add,
        
        [Description("Sửa/Thực hiện")]
        Edit,
        
        [Description("Xóa")]
        Delete,
        
        [Description("Xem/Theo dõi")]
        View,
        
        [Description("Duyệt")]
        Approve
    }
}
