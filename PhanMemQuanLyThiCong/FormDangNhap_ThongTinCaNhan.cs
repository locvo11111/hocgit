using DevExpress.XtraScheduler.Native;
using Newtonsoft.Json.Linq;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using VChatCore.Model;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model;

namespace PhanMemQuanLyThiCong
{
    public partial class FormDangNhap_ThongTinCaNhan : Form
    {
        private int _type;
        private byte[] imageArray;
        private AppUserViewModel AppUser;

        public FormDangNhap_ThongTinCaNhan(int type)
        {
            InitializeComponent();
            _type = type;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public FormDangNhap_ThongTinCaNhan(int type, AppUserViewModel appUser)
        {
            InitializeComponent();
            _type = type;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            AppUser = appUser;
        }

        private void btn_TTCN_Thoat_Click(object sender, EventArgs e)
        {
            // Thoát fom đăng ký hệ thống
            this.Close();
        }

        private void btn_TTCN_DangNhap_Click(object sender, EventArgs e)
        {
            // Đăng nhập hệ thống - Nút bên đăng ký link sang
            //FormDangNhap_HeThong_ChatRoom DangNhap = new FormDangNhap_HeThong_ChatRoom();
            //DangNhap.ShowDialog();
        }

        private async void FormDangNhap_ThongTinCaNhan_Load(object sender, EventArgs e)
        {
            switch (_type)
            {
                case MyConstant.CONST_TYPE_FORMTHONGTINCANHAN_THONGTIN:

                    gr_ThongTin.Text = "Thông tin đã đăng ký";
                    txt_TTCN_Pass.Enabled = txt_TTCN_NhaLaiPass.Enabled = false;
                    string url = "";
                    if (AppUser != null)
                    {
                        txt_CoQuan.Text = AppUser.Company;
                        txt_FullName.Text = AppUser.FullName;
                        txt_Phone.Text = AppUser.PhoneNumber;
                        txt_TTCN_TenPhong.Text = AppUser.Department;
                        txt_Email.Text = AppUser.Email;
                        txt_DiaChi.Text = AppUser.Address;
                        cbb_GioiTinh.Text = AppUser.Gender;
                        cbb_NoiLamViec.Text = AppUser.WorkAddress;
                        url = $"{CusHttpClient.InstanceTBT.BaseAddress}img?key={AppUser.Avatar}";
                    }
                    else
                    {
                        //string token = MSETTING.Default.TokenTBT;
                        try
                        {
                            var response = await CusHttpClient.InstanceTBT.GetAsync("users/profile");

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                //var content = response.Content.ReadAsStringAsync().Result;
                                var user = MyDataHelper.ReadDataFromJson<AppUserViewModel>(response);
                                txt_CoQuan.Text = user.Company.ToString();
                                txt_FullName.Text = user.FullName.ToString();
                                txt_Phone.Text = user.PhoneNumber.ToString();
                                //txt_TTCN_TenPhong.Text = user.ToString();
                                txt_Email.Text = user.Email.ToString();
                                txt_DiaChi.Text = user.Address.ToString();
                                cbb_GioiTinh.Text = user.Gender.ToString();
                                //cbb_NoiLamViec.Text = user["NoiLamViec"].ToString();
                                url = $"{CusHttpClient.InstanceTBT.BaseAddress}img?key={user.Avatar}";
                            }
                            else
                            {
                                var content = response.Content.ReadAsStringAsync().Result;
                                MessageShower.ShowInformation("Lỗi tải thông tin người dùng 1");
                            }
                        }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                        catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                        {
                            MessageShower.ShowInformation("Lỗi tải thông tin người dùng! Vui lòng kiemer tra kết nối internet của bạn!");

                            return;
                        }
                    }
                    //picture_TTCN_AnhDaiDien.Load(url);
                    break;

                case MyConstant.CONST_TYPE_FORMTHONGTINCANHAN_DANGKY:
                    gr_ThongTin.Text = "Đăng ký tài khoản mới";
                    btn_TTCN_SuaThongTin_DangKy.Text = "Đăng ký";

                    try
                    {
                        var response = await CusHttpClient.InstanceTBT.GetAsync("users/profile");

                        //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        //{
                        //    var content = response.Content.ReadAsStringAsync().Result;
                        //    var User = JObject.Parse(JObject.Parse(content.ToString())["data"].ToString());
                        //}
                        //else
                        //{
                        //    var content = response.Content.ReadAsStringAsync().Result;
                        //    MessageShower.ShowInformation("Lỗi tải thông tin người dùng 1");
                        //}
                    }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                    catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                    {
                        MessageShower.ShowInformation("Lỗi tải thông tin người dùng 2");
                    }
                    break;

                default:
                    break;
            }
        }

        private void btn_TTCN_DangXuat_Click(object sender, EventArgs e)
        {
            MSETTING.Default.TokenTBT = "";
            MSETTING.Default.Save();
            this.DialogResult = DialogResult.Retry;
            MessageShower.ShowInformation("Đăng xuất thành công");
            this.Close();
        }

        private async void btn_TTCN_SuaThongTin_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                CoQuan = txt_CoQuan.Text,
                Address = txt_DiaChi.Text,
                FullName = txt_FullName.Text,
                Password = txt_TTCN_Pass.Text,
                Email = txt_Email.Text,
                Phone = txt_Phone.Text,
                PhongLamViec = txt_TTCN_TenPhong.Text,
                NgaySinh = dateTime_TTCN_NgaySinh.Value.ToString(),
                Gender = cbb_GioiTinh.Text,
                NoiLamViec = cbb_NoiLamViec.Text,
                Avatar = (imageArray != null) ? $@"data:image/png;base64,{Convert.ToBase64String(imageArray)}" : null
            };

            switch (_type)
            {
                case MyConstant.CONST_TYPE_FORMTHONGTINCANHAN_DANGKY:
                    try
                    {
                        var response = await CusHttpClient.InstanceTBT.PostAsJsonAsync("auths/sign-up", user);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            MessageShower.ShowInformation("Đăng ký tài khoản mới thành công");
                        else
                            MessageShower.ShowInformation("Không thể đăng ký tài khoản");
                    }
                    catch (Exception ex)
                    {
                        MessageShower.ShowInformation("Không thể đăng ký tài khoản: " + ex.Message);
                    }

                    break;

                case MyConstant.CONST_TYPE_FORMTHONGTINCANHAN_THONGTIN:
                    try
                    {
                        var response = await CusHttpClient.InstanceTBT.PostAsJsonAsync("users/profile", user);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            MessageShower.ShowInformation("Chỉnh sửa thông tin thành công");
                        else
                            MessageShower.ShowInformation("Không thể chỉnh sửa thông tin");
                    }
                    catch (Exception ex)
                    {
                        MessageShower.ShowInformation("Không thể chỉnh sửa thông tin: " + ex.Message);
                    }
                    break;

                default:
                    break;
            }
            this.Close();
        }

        private void picture_TTCN_AnhDaiDien_Click(object sender, EventArgs e)
        {
            if (m_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picture_TTCN_AnhDaiDien.Image = Image.FromFile(m_openFileDialog.FileName);
                imageArray = File.ReadAllBytes(m_openFileDialog.FileName);
                //var base64 = Convert.ToBase64String(imageArray);
            }
        }
    }
}
