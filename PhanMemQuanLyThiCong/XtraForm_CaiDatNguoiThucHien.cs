using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Constant.Enum;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using PM360.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Dto;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_CaiDatNguoiThucHien : DevExpress.XtraEditors.XtraForm
    {
        string _dbTbl, _colForeignKey, _codeCT;
        DataRow _dr;
        bool _isFromTDKH;


        public XtraForm_CaiDatNguoiThucHien(string typeRow, string codeCT, string codeDA)
        {
            InitializeComponent();
            //_codeDA = codeDA;
            _dbTbl = (typeRow == MyConstant.TYPEROW_CVCha) ? GiaoViec.TBL_CONGVIECCHA : GiaoViec.TBL_CONGVIECCON;
            _codeCT = codeCT;
            _colForeignKey = (typeRow == MyConstant.TYPEROW_CVCha) ? "CodeCongViecCha" : "CodeCongViecCon";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {_dbTbl} WHERE \"{_colForeignKey}\" = '{_codeCT}'");
            _dr = dt.AsEnumerable().FirstOrDefault();
            if (_dr is null)
            {
                MessageShower.ShowInformation("Không thể tải công tác");
            }

            if (_dbTbl == GiaoViec.TBL_CONGVIECCHA)
            {
                DataTable dtDanhMucCongTac = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT \"Code\" FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"Code\" = '{_dr["CodeCongTacTheoGiaiDoan"]}'");

                _isFromTDKH = dtDanhMucCongTac.Rows.Count > 0;
            }

            var allPermission = BaseFrom.allPermission;

            if (!allPermission.HaveInitProjectPermission
                && !allPermission.AllContractorThatUserIsAdmin.Contains(SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH?.Code)
                && !allPermission.AllContractorThatUserIsAdmin.Contains(SharedControls.slke_ThongTinDuAn.EditValue as string)
                && !allPermission.TasksInEdit.Contains(codeCT))
            {
                //lke_NguoiPhuTrach.Enabled = false;
                //lke_NguoiTheoDoi.Enabled = false;
                //ccbbe_NguoiThamGia.Enabled = false;
                //lke_NguoiGiao.Enabled = false;

                return;
            }
        }

        private async void XtraForm_CaiDatNguoiThucHien_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tải người thực hiện từ server");
            var result = await CusHttpClient.InstanceCustomer.MGetAsync<List<GiaoViecExtensionViewModel>>($"{RouteAPI.TongDuAn_GetUserWithRoleInGiaoViec}/{_codeCT}");
            
            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError("Tải dữ liệu thất bại! Vui lòng kiểm tra kết nối internet!");
                AlertShower.ShowInfo(result.MESSAGE_CONTENT, "Lỗi tải người thực hiện");
                this.Close();
                WaitFormHelper.CloseWaitForm();
                return;
            }

            var lsUser = result.Dto;

            var usersInAdmin = lsUser.Where(x => x.CommandId == nameof(CommandCode.Add));
            var usersInView = lsUser.Where(x => x.CommandId == nameof(CommandCode.View));
            var usersInEdit = lsUser.Where(x => x.CommandId == nameof(CommandCode.Edit));
            var usersInApprove = lsUser.Where(x => x.CommandId == nameof(CommandCode.Approve));

            lbc_Admin.DataSource = usersInAdmin;
            lbc_view.DataSource = usersInView;
            lbc_Edit.DataSource = usersInEdit;
            lbc_Approve.DataSource = usersInApprove;
            WaitFormHelper.CloseWaitForm();

            /*            lci_DVTH1.Enabled = lci_DVTH2.Enabled = lci_DVTH3.Enabled = !_isFromTDKH;
                        lb_ChuY.Visible = _isFromTDKH;*/

            /*            //try
                        //{
                        //    Dictionary<string, string> dicDoiTuong = new Dictionary<string, string>()
                        //    {
                        //        {MyConstant.TBL_THONGTINNHATHAU, "Nhà Thầu" },
                        //        {MyConstant.TBL_THONGTINNHATHAUPHU, "Nhà Thấu Phụ" },
                        //        {MyConstant.TBL_THONGTINNHACUNGCAP, "Nhà Cung Cấp" },
                        //        {MyConstant.TBL_THONGTINTODOITHICONG, "Tổ Đội" },
                        //    };

                        //    cbb_DoiTuong.DataSource = dicDoiTuong.ToList();
                        //    if (_dr["CodeNhaThau"] != DBNull.Value)
                        //        cbb_DoiTuong.SelectedIndex = 0;
                        //    else if (_dr["CodeNhaThauPhu"] != DBNull.Value)
                        //        cbb_DoiTuong.SelectedIndex = 1;

                        //    else if (_dr["CodeNhaCungCap"] != DBNull.Value)
                        //        cbb_DoiTuong.SelectedIndex = 2;

                        //    else if (_dr["CodeToDoi"] != DBNull.Value)
                        //        cbb_DoiTuong.SelectedIndex = 3;

                        //    var response = await CusHttpClient.InstanceTBT.GetAsync($@"{MyConstant.SERVER_TYPE_MODEL_Users}\allusersdto");
                        //    if (response.StatusCode == HttpStatusCode.OK)
                        //    {
                        //        var content = response.Content.ReadAsStringAsync().Result;
                        //        //var Users = JObject.Parse(JObject.Parse(content.ToString())["data"].ToString());
                        //        var lsUser = JsonConvert.DeserializeObject<List<UserDto>>(JObject.Parse(content.ToString())["data"].ToString()).ToList();

                        //        //lke_NguoiGiao.Properties.DataSource
                        //        //    = lke_NguoiPhuTrach.Properties.DataSource
                        //        //    = lke_NguoiTheoDoi.Properties.DataSource
                        //        //    = ccbbe_NguoiThamGia.Properties.DataSource = lsUser;

                        //        //lke_NguoiGiao.EditValue = _dr["NguoiGiao"];
                        //        //lke_NguoiPhuTrach.EditValue = _dr["NguoiPhuTrach"];
                        //        //lke_NguoiTheoDoi.EditValue = _dr["NguoiTheoDoi"];

                        //        DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {GiaoViec.TBL_KEHOACH_NGUOITHAMGIA} WHERE \"{_colForeignKey}\" = '{_codeCT}'");

                        //        ccbbe_NguoiThamGia.EditValue = string.Join(";", dt.AsEnumerable().Select(x => x["CodeNguoiDung"]).ToArray());
                        //    }
                        //    else
                        //    {
                        //        MessageShower.ShowInformation("Không thể lấy thông tin người dùng");
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageShower.ShowInformation("Không thể lấy thông tin người dùng");
                        //}*/
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bt_HoanThanh_Click(object sender, EventArgs e)
        {
            /*List<string> setValue = new List<string>();

                _dr["NguoiGiao"] = (string.IsNullOrEmpty(lke_NguoiGiao.Text))?null:lke_NguoiGiao.EditValue;
                _dr["NguoiPhuTrach"] = (string.IsNullOrEmpty(lke_NguoiPhuTrach.Text))?null:lke_NguoiPhuTrach.EditValue;
                _dr["NguoiTheoDoi"] = (string.IsNullOrEmpty(lke_NguoiTheoDoi.Text))?null:lke_NguoiTheoDoi.EditValue;
            //setValue.Add($"\"NguoiGiao\" = '{lke_NguoiGiao.EditValue}'");
            _dr["CodeNhaThau"] = _dr["CodeNhaThauPhu"] = _dr["CodeNhaCungCap"] = _dr["CodeToDoi"] = DBNull.Value;

            if (!_isFromTDKH)
                _dr[$"Code{MyFunction.fcn_RemoveAccents(cbb_DoiTuong.Text).Replace(" ", "")}"] = cbb_DonViThucHien.SelectedValue;
            
            
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(_dr.Table, _dbTbl);

            //string[] allNguoiThamGia = ccbbe_NguoiThamGia.Properties.Items.Where(x => x.CheckState == CheckState.Checked).Select(x => x.Value as string).ToArray();
            
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {GiaoViec.TBL_KEHOACH_NGUOITHAMGIA} WHERE \"{_colForeignKey}\" = '{_codeCT}'");
            dt.Rows.Clear();
*//*            foreach (string nguoiThamGia in allNguoiThamGia)
            {
                DataRow newRow = dt.NewRow();
                newRow["Code"] = Guid.NewGuid().ToString();
                newRow[_colForeignKey] = _codeCT;
                newRow["CodeNguoiDung"] = nguoiThamGia;
                dt.Rows.Add(newRow);
            }*//*

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, GiaoViec.TBL_KEHOACH_NGUOITHAMGIA, true, queryStringCondition: $"WHERE \"{_colForeignKey}\" = '{_codeCT}'");
            MessageShower.ShowInformation("Đã cập nhật thông tin người tham gia");
            this.Close();
            //List<string>
            //string dbString1 = $"UPDATE {_dbTbl} SET";*/
        }

        private void cbb_DoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbb_DoiTuong.SelectedIndex < 0)
            //    return;

            //string dbString = $"SELECT \"Code\", \"Ten\" FROM {cbb_DoiTuong.SelectedValue} WHERE \"CodeDuAn\" = '{_codeDA}'";
            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //cbb_DonViThucHien.DataSource = dt;
            //cbb_DonViThucHien.SelectedValue = _dr[$"Code{MyFunction.fcn_RemoveAccents(cbb_DoiTuong.Text).Replace(" ","")}"];
        }
    }
}
