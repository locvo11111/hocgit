using DevExpress.DataAccess.Native;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.API;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model;
using PM360.Common;
using PM360.Common.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PhanMemQuanLyThiCong.Form_CaiDatDotHopDong;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Uc_DangKyPhanMem : DevExpress.XtraEditors.XtraUserControl
    {
        private List<Provinces> Provinces;
        private List<Department> Departments;
        public static string PCName { get; set; } = Environment.MachineName;
        public Uc_DangKyPhanMem()
        {
            InitializeComponent();
            #region Validate-------------------------------
            dxValidationProvider.SetValidationRule(txt_FullName, new CustomValidationTextEditRule { ErrorText = "Không được để trống trường này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_DiaChi, new CustomValidationTextEditRule { ErrorText = "Không được để trống trường này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_Email, new CustomValidationTextEditRule { ErrorText = "Không được để trống trường này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_Phone, new CustomValidationTextEditRule { ErrorText = "Không được để trống trường này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(cbTinhThanh, new CustomValidationLookupRule { ErrorText = "Không được để trống trường này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_Password, new CustomValidationTextEditRule { ErrorText = "Không được để trống trường này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_Password, new CustomValidationPasswordRule { ErrorText = "Mật khẩu quá ngắn", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProvider.SetValidationRule(txt_Confirm, new CustomValidationTextEditRule { ErrorText = "Không được để trống trường này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            dxValidationProviderKichHoat.SetValidationRule(txt_Key, new CustomValidationTextEditRule { ErrorText = "Không được để trống trường này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });
            #endregion-------------------------------------
            Provinces = BaseFrom.Provinces;
            Departments = BaseFrom.Departments;
            cbTinhThanh.Properties.DataSource = Provinces;
            cbPhongBan.Properties.DataSource = Departments;

        }

        /// <summary>
        /// Đăng ký dùng thử
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private async void btn_DangKy_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider.Validate())
            {
                MessageShower.ShowWarning("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            if (txt_Password.Text != txt_Confirm.Text)
            {
                MessageShower.ShowWarning("Xác nhận mật khẩu không trùng khớp");
                return;
            }

            if (ce_DungThu.Checked)
            {
                var model = new RegisterOldViewModel();
                model.name = txt_FullName.Text;
                model.email = txt_Email.Text;
                model.address = txt_DiaChi.Text;
                model.phone = txt_Phone.Text;
                model.province_id = cbTinhThanh.Text;
                model.department_id = cbPhongBan.Text;
                model.serial_hdd = ConfigHelper.GetSerialHDD();
                model.password = txt_Password.Text;
                //bool result = UtilAPI<bool>.RegisterKey(ConstantAPI.API_REGISTER_KEY_OLD, model);
                var result = await UtilAPI<ResultMessage<AppUserViewModel>>.RegisterKey(RouteAPI.ADD_ORDER, model);
                if (result.MESSAGE_TYPECODE)
                {
                    if (String.IsNullOrEmpty(result.Dto.SerialNo))
                    {
                        MessageShower.ShowInformation($"Đăng ký thành công. Vui lòng vào địa chỉ Email {model.email} để lấy khóa kích hoạt");
                        btn_DangKy.Enabled = false;
                    }
                    else
                    {
                        txt_Key.Text = result.Dto.SerialNo;
                    }
                }
                else MessageShower.ShowInformation("Đăng ký không thành công");
            }
            else
            {
                var user = new AppUserViewModel()
                {
                    FullName = txt_FullName.Text,
                    Email = txt_Email.Text,
                    Address = txt_DiaChi.Text,
                    PhoneNumber = txt_Phone.Text,
                    Province = cbTinhThanh.Text,
                    Department = cbPhongBan.Text,
                    Password = txt_Password.Text,
                    ConfirmPassword = txt_Confirm.Text
                };
                var result = await CusHttpClient.InstanceTBT.MPostAsJsonAsync<bool>(RouteAPI.USER_REGISTER, user);
                if (result.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowInformation("Đăng ký thành công");
                }
                else MessageShower.ShowInformation("Đăng ký không thành công\r\n"+ result.MESSAGE_CONTENT);

            }
        }

        /// <summary>
        /// Kích hoạt key phần mềm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_Send_Click(object sender, EventArgs e)
        {
            if (!dxValidationProviderKichHoat.Validate())
                return;
            var model = new RegisterOldViewModel();
            model.key_code = txt_Key.Text.Trim();
            var result = await UtilAPI<bool>.ActivatedCode(txt_Key.Text.Trim());
            if (result.MESSAGE_TYPECODE)
            {
                btn_DangKy.Enabled = false;
                btn_Send.Enabled = false;
                if (PermissionHelper.SetKeyToWindow(model.key_code)) ;
                {
                    if (!string.IsNullOrEmpty(result.MESSAGE_CONTENT))
                        MessageShower.ShowInformation(result.MESSAGE_CONTENT);
                    else MessageShower.ShowInformation("Kích hoạt khóa không thành công!");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(result.MESSAGE_CONTENT))
                    MessageShower.ShowInformation(result.MESSAGE_CONTENT);
                else MessageShower.ShowInformation("Kích hoạt khóa không thành công!");
            }
        }
    }
}
