using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using PhanMemQuanLyThiCong.Common.ViewModel;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_TDKH_CaiDatNgayNghi : Form
    {
        DataTable _dtCTTK, _dtNgayNghi, _dt_DayOfWeek;
        string queryCondition ="";
        public Form_TDKH_CaiDatNgayNghi(DataTable dtCongTacTheoKy)
        {
            InitializeComponent();
            _dtCTTK = dtCongTacTheoKy;
        }
        private void Form_TDKH_CaiDatNgayNghi_Load(object sender, EventArgs e)
        {

            var DA = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;

            if (DA is null)
            {
                MessageShower.ShowError("Chưa chọn dự án");
                this.Close();
                return;
            }
            this.Text = $"Cài đặt ngày nghỉ dự án \"{DA.TenDuAn}\"";
            lb_DuAn.Text = $"Dự án: {DA.TenDuAn}";
            string dbString = $"SELECT \"Code\", \"Ten\" FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\" = '{MSETTING.Default.DuAnHienTai}'";
            DataTable dtCongtrinh = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            cbb_CongTrinh.DataSource = dtCongtrinh.AsEnumerable().ToDictionary(x => x["Code"].ToString(), x => x["Ten"].ToString()).ToList();
            rg_ChonDoiTuong.SelectedIndex = 0;
        }

        private void rg_ChonDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rg_ChonDoiTuong.SelectedIndex == -1)
                return;
            string tenDaChon = rg_ChonDoiTuong.GetAccessibleName();

            switch (tenDaChon)
            {
                case "DuAn":
                    item_CongTrinh.Visibility = LayoutVisibility.Never;
                    item_HangMuc.Visibility = LayoutVisibility.Never;
                    item_CongTac.Visibility = LayoutVisibility.Never;
                    item_LoaiKeHoach.Visibility = LayoutVisibility.Never;
                    break;
                case "CongTrinh":
                    item_CongTrinh.Visibility = LayoutVisibility.Always;
                    item_HangMuc.Visibility = LayoutVisibility.Never;
                    item_CongTac.Visibility = LayoutVisibility.Never;
                    item_LoaiKeHoach.Visibility = LayoutVisibility.Never;
                    break;
                case "HangMuc":
                    item_CongTrinh.Visibility = LayoutVisibility.Always;
                    item_HangMuc.Visibility = LayoutVisibility.Always;
                    item_CongTac.Visibility = LayoutVisibility.Never;                   
                    item_LoaiKeHoach.Visibility = LayoutVisibility.Never;
                    break;
                case "CongTac":

                    if ((cbb_CongTac.DataSource as List<KeyValuePair<string, string>>).Count == 0)
                    {
                        MessageShower.ShowInformation("Vui lòng chọn hạng mục có chứa công tác!");
                        rg_ChonDoiTuong.SelectedIndex -= 1;
                        return;
                    }
                    item_CongTrinh.Visibility = LayoutVisibility.Always;
                    item_HangMuc.Visibility = LayoutVisibility.Always;
                    item_CongTac.Visibility = LayoutVisibility.Always;
                    item_LoaiKeHoach.Visibility = LayoutVisibility.Always;
                    break;
                default:
                    break;
            }
            LoadNgayNghiCongTac();
        }

        private void cbb_CongTrinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_CongTrinh.SelectedIndex < 0)
                return;
            if (!cbb_HangMuc.Visible)
                LoadNgayNghiCongTac();

            string dbString = $"SELECT \"Code\", \"Ten\" FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" = '{cbb_CongTrinh.SelectedValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            cbb_HangMuc.DataSource = dt.AsEnumerable().ToDictionary(x => x["Code"].ToString(), x => x["Ten"].ToString()).ToList();
        }

        private void bt_ThemNgayNghi_Click(object sender, EventArgs e)
        {
            DateTime dateSelected = cld_ChonNgayNghi.SelectionStart;

            var Dt = gc_NgayNghi.DataSource as BindingList<NgayNghiExtension>;

            //DataGridViewRow dgvr = dgv_NgayNgh;
            //dgvr.Cells["Ten"].Value = dateSelected.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);

            //dgv_NgayNghi.Rows.Add("123", dateSelected.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET));
            if (Dt.Where(x => x.Ngay == dateSelected.Date).FirstOrDefault() != null)
            {
                MessageShower.ShowInformation("Ngày này đã tồn tại, vui lòng chọn ngày khác");
                return;
            }

            var newrow = new NgayNghiExtension();
            newrow.Code = Guid.NewGuid().ToString();
            newrow.Ngay = dateSelected;

            string tenDaChon = rg_ChonDoiTuong.GetDescription();

            switch (tenDaChon)
            {
                case "Dự án":
                    newrow.CodeDuAn = MSETTING.Default.DuAnHienTai;
                    break;
                case "Công trình":
                    newrow.CodeCongTrinh = cbb_CongTrinh.SelectedValue.ToString();
                    break;
                case "Hạng mục":
                    newrow.CodeHangMuc = cbb_HangMuc.SelectedValue.ToString();
                    break;
                case "Công tác":
                    newrow.CodeCongTac = cbb_CongTac.SelectedValue.ToString();
                    newrow.LoaiCongTac = cbb_LoaiCongTac.Text;
                    break;
                default:
                    return;
            }
            Dt.Add(newrow);
            string dbString = $"INSERT INTO {TDKH.Tbl_Ngaynghi} " +
                $"(Code, Ngay, CodeDuAn, CodeCongTrinh, CodeHangMuc, CodeCongTac, LoaiCongTac) " +
                $"VALUES (@Code, @Ngay, @CodeDuAn, @CodeCongTrinh, @CodeHangMuc, @CodeCongTac, @LoaiCongTac)";

            object[] mParams =
            {
                newrow.Code,
                newrow.Ngay,
                newrow.CodeDuAn,
                newrow.CodeCongTrinh,
                newrow.CodeHangMuc,
                newrow.CodeCongTac,
                newrow.LoaiCongTac,
            };

            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: mParams);
        }

        private void cbb_CongTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNgayNghiCongTac();
        }

        private void cccb_NgayNghiTrongTuan_EditValueChanged(object sender, EventArgs e)
        {
            if (_dt_DayOfWeek.Rows.Count == 1)
                _dt_DayOfWeek.Rows[0]["NgayTrongTuan"] = cccb_NgayNghiTrongTuan.Text;
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(_dt_DayOfWeek, TDKH.Tbl_Ngaynghi);
        }

        private void cbb_LoaiCongTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNgayNghiCongTac();
        }

        //private void dgv_NgayNghi_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (gc_NgayNghi.Columns[e.ColumnIndex].HeaderText == "Xoa")
        //    {
        //        dgv_NgayNghi.Rows.RemoveAt(e.RowIndex);
        //    }    
        //}

/*        private void bt_LuuLai_Click(object sender, EventArgs e)
        {
            if (_dt_DayOfWeek.Rows.Count == 1)
            _dt_DayOfWeek.Rows[0]["NgayTrongTuan"] = cccb_NgayNghiTrongTuan.Text;
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(_dt_DayOfWeek, TDKH.Tbl_Ngaynghi);

            DataTable dtNgay = gc_NgayNghi.DataSource as DataTable;
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtNgay, TDKH.Tbl_Ngaynghi);

            MessageShower.ShowInformation("Đã lưu thành công ngày nghỉ");
            this.Close();
        }*/

        private void repoBt_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            NgayNghiExtension nn = gridView1.FocusedRowObject as NgayNghiExtension;

            if (MessageShower.ShowYesNoQuestion($"Bạn có muốn xóa ngày nghỉ ngày \"{nn.Ngay}\"") != DialogResult.Yes)
            {
                return;
            }    

            string dbString = $"DELETE FROM {TDKH.Tbl_Ngaynghi} WHERE Code = '{nn.Code}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            gridView1.DeleteSelectedRows();
        }

        private void LoadNgayNghiCongTac()
        {
            //Lây condition
            string cond = "";
            KeyValuePair<string, string>[] lsColVal;
            string tenDaChon = rg_ChonDoiTuong.GetAccessibleName();// Properties.Items[rg_ChonDoiTuong.SelectedIndex].Description;

            switch (tenDaChon)
            {
                case "DuAn":
                    cond = $"\"CodeDuAn\" = '{MSETTING.Default.DuAnHienTai}'";
                    lsColVal = new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("CodeDuAn", MSETTING.Default.DuAnHienTai) };
                    break;
                case "CongTrinh":
                    cond = $"\"CodeCongTrinh\" = '{cbb_CongTrinh.SelectedValue}'";
                    lsColVal = new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("CodeCongTrinh", cbb_CongTrinh.SelectedValue.ToString()) };
                    break;
                case "HangMuc":
                    cond = $"\"CodeHangMuc\" = '{cbb_HangMuc.SelectedValue}'";
                    lsColVal = new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("CodeHangMuc", cbb_HangMuc.SelectedValue.ToString()) };

                    break;
                case "CongTac":
                    cond = $"\"CodeCongTac\" = '{cbb_CongTac.SelectedValue}' AND \"LoaiCongTac\" = '{cbb_LoaiCongTac.Text}'";

                    lsColVal = new KeyValuePair<string, string>[] 
                    { 
                        new KeyValuePair<string, string>("CodeCongTac", cbb_CongTac.SelectedValue.ToString()), 
                        new KeyValuePair<string, string>("LoaiCongTac", cbb_LoaiCongTac.Text) 
                    };

                    break;
                default:
                    return;
            }

            string dbString = $"SELECT * FROM {TDKH.Tbl_Ngaynghi} WHERE \"NgayTrongTuan\" IS NOT NULL AND {cond}";
            
            _dt_DayOfWeek = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (_dt_DayOfWeek.Rows.Count >= 1)
            {
                if (_dt_DayOfWeek.Rows.Count > 1)
                {
                    MessageShower.ShowInformation("Xung đột dữ liệu!");
                    for (int i = 1; i < _dt_DayOfWeek.Rows.Count; i++)
                        _dt_DayOfWeek.Rows[i].Delete();
                }


                string[] lsDayofWeek = _dt_DayOfWeek.Rows[0]["NgayTrongTuan"].ToString().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                //string[ lsDayofWeek = _dt_DayOfWeek.Rows[0]["NgayTrongTuan"].ToString().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (CheckedListBoxItem item in cccb_NgayNghiTrongTuan.Properties.Items)
                {
                        item.CheckState = (lsDayofWeek.Contains(item.ToString()))?CheckState.Checked:CheckState.Unchecked;
                }
            }
            else if (_dt_DayOfWeek.Rows.Count == 0)
            {
                foreach (CheckedListBoxItem item in cccb_NgayNghiTrongTuan.Properties.Items)
                {
                    item.CheckState = CheckState.Unchecked;
                }
                DataRow newrow = _dt_DayOfWeek.NewRow();
                newrow["Code"] = Guid.NewGuid().ToString();
                foreach (var item in lsColVal)
                {
                    newrow[item.Key] = item.Value;

                }
                //newrow["CodeCongTac"] = cbb_CongTac.SelectedValue.ToString();
                //newrow["LoaiCongTac"] = cbb_LoaiCongTac.Text;
                _dt_DayOfWeek.Rows.Add(newrow);
            }

            //Lấy ngày cụ thể
            dbString = $"SELECT * FROM {TDKH.Tbl_Ngaynghi} WHERE \"Ngay\" IS NOT NULL AND {cond} ORDER BY \"Ngay\" ASC";
            var dtNgay = DataProvider.InstanceTHDA.ExecuteQueryModel<NgayNghiExtension>(dbString);
            gc_NgayNghi.DataSource = new BindingList<NgayNghiExtension>(dtNgay);

        }



        private void cbb_HangMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_CongTrinh.SelectedIndex < 0)
                return;

            if (!cbb_CongTac.Visible)
                LoadNgayNghiCongTac();


            var dicCT = _dtCTTK.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == cbb_HangMuc.SelectedValue.ToString()).ToArray()
                .ToDictionary(x => x["Code"].ToString(), x => x["TenCongTac"].ToString()) ;
            Dictionary<string, string> lsCT = dicCT;

            cbb_CongTac.DataSource = lsCT.ToList();
            
        }
    }
}
