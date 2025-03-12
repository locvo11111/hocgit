using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class EditFormDeXuat : EditFormUserControl
    {
        public EditFormDeXuat()
        {
            InitializeComponent();
            //Fcn_LoadComponent();
        }
        public void Fcn_LoadComponent()
        {
            //panelControl1.Hide();
            this.SetBoundPropertyName(txtE_Noidungung, "gridColumn2");
            this.SetBoundFieldName(txtE_Noidungung, "NoiDungUng");      
            
            this.SetBoundPropertyName(txtE_GiaTri, "gridColumn3");
            this.SetBoundFieldName(txtE_GiaTri, "GiaTriDotNay");       

            this.SetBoundPropertyName(lUE_CongTrinh, "gridColumn7");
            this.SetBoundFieldName(lUE_CongTrinh, "CongTrinh");   
            
            this.SetBoundPropertyName(lUE_LoaiKinhPhi, "gridColumn6");
            this.SetBoundFieldName(lUE_LoaiKinhPhi, "LoaiKinhPhi");       

            this.SetBoundPropertyName(lUE_TenNguoiung, "gridColumn4");
            this.SetBoundFieldName(lUE_TenNguoiung, "NguoiLapTamUng");   
            
            this.SetBoundPropertyName(lUE_ToChucCaNhan, "gridColumn5");
            this.SetBoundFieldName(lUE_ToChucCaNhan, "ToChucCaNhanNhanChiPhiTamUng");
            Group_Thongtinchitiet.Visible = false;
            //this.Height = this.Height - Group_Thongtinchitiet.Height;
            if (SharedControls.slke_ThongTinDuAn == null)
                return;
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource,false);
            lUE_CongTrinh.Properties.DataSource = Infor;
            lUE_CongTrinh.Properties.DropDownRows = Infor.Count;

            string dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI}";
            List<Infor> LoaiKP = new List<Infor>();

            foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                LoaiKP.Add(new Infor
                {
                    Code = item["Code"].ToString(),
                    Ten = item["Ten"].ToString()
                });
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}";
            List<Infor> TenNV = new List<Infor>();
            foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                TenNV.Add(new Infor
                {
                    Code = item["Code"].ToString(),
                    Ten = item["TenNhanVien"].ToString(),
                    Decription = item["MaNhanVien"].ToString()
                });
            LoaiKP.Add(new Infor
            {
                Code = "Add",
                Ten = "Thêm"
            });

            lUE_LoaiKinhPhi.Properties.DataSource = LoaiKP;
            //lUE_LoaiKinhPhi.EditValue = LoaiKP.FirstOrDefault().Code;
            lUE_TenNguoiung.Properties.DataSource = TenNV;
            //lUE_TenNguoiung.EditValue = TenNV.FirstOrDefault().Code;
            lUE_ToChucCaNhan.Properties.DataSource = DuAnHelper.GetCaNhanToChuc();
            //lUE_ToChucCaNhan.EditValue = DuAnHelper.GetCaNhanToChuc().FirstOrDefault().Code;
        }
        private void MyHandle_GV_QTMH_AnHienPanel_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            //int indOfTableLayout = 0;
            //bool isMucLon = int.TryParse(cb.Name.Substring(cb.Name.Length - 1), out indOfTableLayout); //Có phải mục lớn không: Ví dụ ĐỀ XUẤT
            string TienTo = "Hiện";/*(isMucLon) ? "Thêm " : "Thêm quy trình ";*/
            //Control[] lsPn = cb.Parent.Parent.Controls. (cb.Name.Replace("cb", ""), true); //Tìm panel tương ứng với combobox
            //if (lsPn.Length > 0)
            //{
            string nameSearch = cb.Name.Replace("cb", "");
            bool isHasControl = false;
            foreach (Control ctrl in cb.Parent.Controls)
            {
                if (ctrl.Name.EndsWith(nameSearch) && ctrl != cb)
                {
                    isHasControl = true;
                    ctrl.Visible = cb.Checked;


                    if (!cb.Checked)
                    {
                        cb.Text = $"Hiện {cb.Text.Replace("Ẩn ", "")}";
                        cb.ForeColor = Color.Red;
                    }
                    else
                    {
                        cb.Text = $"Ẩn {cb.Text.Replace("Hiện ", "")}";
                        cb.ForeColor = Color.Black;
                    }
                }
            }
        }
        private void sB_ChiTiet_Click(object sender, EventArgs e)
        {
            if (!Group_Thongtinchitiet.Visible == false)
                return;
            if (txtE_Noidungung.Text == "")
            {
                MessageShower.ShowWarning("Vui lòng nhập tên Nội dung ứng!", "Cảnh báo thiếu dữ liệu");
                return;
            }
            Group_Thongtinchitiet.Visible = true;
            //this.Height = this.Height + Group_Thongtinchitiet.Height;
            BindingList<PhuLucThuCong> PL = new BindingList<PhuLucThuCong>();
            PL.Add(new PhuLucThuCong
            {
                Code = Guid.NewGuid().ToString(),
                TenNoiDungUng = txtE_Noidungung.Text,
                STT = 1
            });
            gc_PhuLucThuCong.DataSource = PL;
            gv_PhuLucThuCong.Columns["TenNoiDungUng"].Group();
            gv_PhuLucThuCong.ExpandAllGroups();
        }
        public BindingList<PhuLucThuCong> Fcn_DataThuCong()
        {
            gv_PhuLucThuCong.UpdateGroupSummary();
            if (!gv_PhuLucThuCong.IsVisible)
                return null;
            if(gv_PhuLucThuCong.Columns["ThanhTien"].SummaryItem.SummaryValue!=null)
                txtE_GiaTri.Text = gv_PhuLucThuCong.Columns["ThanhTien"].SummaryItem.SummaryValue.ToString();
            BindingList<PhuLucThuCong> PL = gv_PhuLucThuCong.DataSource as BindingList<PhuLucThuCong>;
            return PL;
        }
        public List<Infor> Fcn_DataLoaiKP()
        {
            return lUE_LoaiKinhPhi.Properties.DataSource as List<Infor>;
        }
        private void gv_PhuLucThuCong_GroupRowCollapsing(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            e.Allow = false;
        }

        private void gv_PhuLucThuCong_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gv_PhuLucThuCong.SetRowCellValue(e.RowHandle, "Code", Guid.NewGuid().ToString());
            gv_PhuLucThuCong.SetRowCellValue(e.RowHandle, "TenNoiDungUng", txtE_Noidungung.Text);
            gv_PhuLucThuCong.SetRowCellValue(e.RowHandle, "STT", gv_PhuLucThuCong.RowCount-1);
        }

        private void gv_PhuLucThuCong_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                gv_PhuLucThuCong.CloseEditor();
                e.SuppressKeyPress = true;
                gv_PhuLucThuCong.AddNewRow();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                gv_PhuLucThuCong.CloseEditor();
                e.SuppressKeyPress = true;
                gv_PhuLucThuCong.FocusedRowHandle = gv_PhuLucThuCong.FocusedRowHandle + 1;
            }
            else if (e.KeyCode == Keys.Delete)
            {

            }
            gv_PhuLucThuCong.UpdateSummary();
            gv_PhuLucThuCong.UpdateGroupSummary();
            txtE_GiaTri.Text = gv_PhuLucThuCong.Columns["ThanhTien"].SummaryItem.SummaryValue.ToString();
        }

        private void gc_PhuLucThuCong_EditorKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //gv_PhuLucThuCong.CloseEditor();
                //e.SuppressKeyPress = true;
                //gv_PhuLucThuCong.FocusedRowHandle = gv_PhuLucThuCong.FocusedRowHandle + 1;
            }
        }

        private void lUE_LoaiKinhPhi_EditValueChanged(object sender, EventArgs e)
        {
            if (lUE_LoaiKinhPhi.EditValue == "Add")
            {
                string LoaiKP = XtraInputBox.Show("Tên kinh phí", "Nhập tên loại kinh phí", "");
                if (LoaiKP != null)
                {
                    string Code = Guid.NewGuid().ToString();

                    List<Infor> KP = lUE_LoaiKinhPhi.Properties.DataSource as List<Infor>;
                    KP.Add(new Infor
                    {
                        Code=Code,
                        Ten=LoaiKP
                    });

                    KP.RemoveAll(x => x.Code == "Add");
                    KP.Add(new Infor
                    {
                        Code = "Add",
                        Ten = "Thêm"
                    });

                    lUE_LoaiKinhPhi.Properties.DataSource = KP;
                    lUE_LoaiKinhPhi.EditValue = Code;

                }
                else
                {
                    lUE_LoaiKinhPhi.EditValue = null;
                }
            }
            //if (lUE_LoaiKinhPhi.Text == "")
            //    lUE_LoaiKinhPhi.ItemIndex = 0;
            //if (lUE_CongTrinh.Text == "")
            //    lUE_CongTrinh.ItemIndex = 0;
            //if (lUE_ToChucCaNhan.Text == "")
            //    lUE_ToChucCaNhan.ItemIndex = 0;
            //if (lUE_TenNguoiung.Text == "")
            //    lUE_TenNguoiung.ItemIndex = 0;
        }

        private void gv_PhuLucThuCong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gv_PhuLucThuCong.UpdateSummary();
            gv_PhuLucThuCong.UpdateGroupSummary();
            txtE_GiaTri.Text = gv_PhuLucThuCong.Columns["ThanhTien"].SummaryItem.SummaryValue.ToString();
        }

        private void gv_PhuLucThuCong_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (e.MenuType == GridMenuType.Row)
            {
                DXMenuItem menuItem = new DXMenuItem("Chèn dòng", this.fcn_Handle_Popup_ChenDong);
                menuItem.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem);     
                
                DXMenuItem Xoa = new DXMenuItem("Xóa dòng", this.fcn_Handle_Popup_Xoa);
                Xoa.Tag = hitInfo.Column;
                e.Menu.Items.Add(Xoa);

            }
        }
        private void fcn_Handle_Popup_ChenDong(object sender,EventArgs e)
        {
            gv_PhuLucThuCong.CloseEditor();
            gv_PhuLucThuCong.AddNewRow();
            gv_PhuLucThuCong.UpdateSummary();
            gv_PhuLucThuCong.UpdateGroupSummary();
        }   
        private void fcn_Handle_Popup_Xoa(object sender,EventArgs e)
        {
            gv_PhuLucThuCong.DeleteSelectedRows();
        }

        private void lUE_ToChucCaNhan_EditValueChanged(object sender, EventArgs e)
        {
            //if (lUE_LoaiKinhPhi.Text == "")
            //    lUE_LoaiKinhPhi.ItemIndex = 0;
            //if (lUE_CongTrinh.Text == "")
            //    lUE_CongTrinh.ItemIndex = 0;
            ////if (lUE_ToChucCaNhan.Text == "")
            ////    lUE_ToChucCaNhan.ItemIndex = 0;
            //if (lUE_TenNguoiung.Text == "")
            //    lUE_TenNguoiung.ItemIndex = 0;
        }

        private void lUE_TenNguoiung_EditValueChanged(object sender, EventArgs e)
        {
            //if (lUE_LoaiKinhPhi.Text == "")
            //    lUE_LoaiKinhPhi.ItemIndex = 0;
            //if (lUE_CongTrinh.Text == "")
            //    lUE_CongTrinh.ItemIndex = 0;
            //if (lUE_ToChucCaNhan.Text == "")
            //    lUE_ToChucCaNhan.ItemIndex = 0;
            //if (lUE_TenNguoiung.Text == "")
            //    lUE_TenNguoiung.ItemIndex = 0;
        }

        private void lUE_CongTrinh_EditValueChanged(object sender, EventArgs e)
        {
            //if (lUE_LoaiKinhPhi.Text == "")
            //    lUE_LoaiKinhPhi.ItemIndex = 0;
            ////if (lUE_CongTrinh.Text == "")
            ////    lUE_CongTrinh.ItemIndex = 0;
            //if (lUE_ToChucCaNhan.Text == "")
            //    lUE_ToChucCaNhan.ItemIndex = 0;
            //if (lUE_TenNguoiung.Text == "")
            //    lUE_TenNguoiung.ItemIndex = 0;
        }

        private void gc_PhuLucThuCong_Click(object sender, EventArgs e)
        {
            gv_PhuLucThuCong.UpdateGroupSummary();
            gv_PhuLucThuCong.UpdateSummary();
        }
    }
}
