using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Uc_ThongTinBanQuyen : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_ThongTinBanQuyen()
        {
            InitializeComponent();
            if(BaseFrom.IsFullAccess || BaseFrom.IsLimitDate)
            {
                txt_FullName.Text = BaseFrom.BanQuyenKeyInfo.FullName;
                txt_DiaChi.Text = BaseFrom.BanQuyenKeyInfo.Address;
                txt_Email.Text = BaseFrom.BanQuyenKeyInfo.Email;
                txt_Phone.Text = BaseFrom.BanQuyenKeyInfo.PhoneNumber;
                txt_KieuKhoa.Text = BaseFrom.BanQuyenKeyInfo.TypeCode== TypeStatus.KHOACUNG? "Khóa cứng":"Khóa mềm";

                if (BaseFrom.BanQuyenKeyInfo.StartDate.HasValue)
                    txt_NgayKichHoat.Text = BaseFrom.BanQuyenKeyInfo.StartDate.Value.ToString("dd/MM/yyyy");
                if (BaseFrom.BanQuyenKeyInfo.TypeCode == TypeStatus.KHOAMEM)
                {
                    txt_ThoiHanBanQuyen.Text = $"Thời hạn bản quyền còn lại {BaseFrom.BanQuyenKeyInfo.LimitDate} ngày.";
                    txt_MaKhoa.Text = BaseFrom.BanQuyenKeyInfo.SerialNo;

                }
                else
                {
                    txt_MaKhoa.Text = BaseFrom.BanQuyenKeyInfo.KeyCode;
                }
            }    
            
        }
    }
}
