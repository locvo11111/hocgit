using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public class FormNoti
    {
        public const string NoLicense = "Bạn đang ở chế độ dùng bản quyền nhưng không có đăng ký bản quyền!\r\n" +
            "Vui lòng ĐĂNG KÝ BẢN QUYỀN để tiếp tục sử dụng hoặc chuyển sang chế độ TÀI KHOẢN và đăng nhập tài khoản đã được phân quyền!";

        public const string InvalidAccount = "Tài khoản của bạn không hợp lệ!\r\n" +
            "Vui lòng đăng nhập lại và chọn khóa đã được phân quyền để sử dụng phần mềm";

        public const string InvalidKeyCode = "Bạn chưa chọn khóa hoạt động!\r\n" +
            "Vui lòng truy cập thông tin cá nhân ở GÓC TRÊN BÊN PHẢI để xem danh sách và chọn khóa hoạt động";

        public const string ValidKey = "Vui lòng CHỌN/TẠO dự án mới hoặc TẢI VỀ từ server để tiếp tục sử dụng các tính năng của Phần Mềm";
        public const string ValidProject = "Vui lòng CHỌN Các tab tính năng để sử dụng phần mềm!";
    }
}
