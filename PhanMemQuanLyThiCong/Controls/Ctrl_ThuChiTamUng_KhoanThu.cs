using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
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

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Ctrl_ThuChiTamUng_KhoanThu : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_ThuChiTamUng_KhoanThu()
        {
            InitializeComponent();

        }
        public TreeList Fcn_LoadTreeList()
        {
            TreeList New = new TreeList();
            New = tL_KhoanThu;
            return New;
        }
        public void Fcn_AddData(List<KhoanThu> KhoanThu)
        {
            List<KhoanThu> KT = tL_KhoanThu.DataSource as List<KhoanThu>;
            if (KT == null)
            {
                //List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource);
                //rILUE_CongTrinhHM.DataSource = Infor;
                //rILUE_CongTrinhHM.DropDownRows = Infor.Count;

                //string dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI}";
                //List<Infor> LoaiKP = new List<Infor>();

                //foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                //    LoaiKP.Add(new Infor
                //    {
                //        Code = item["Code"].ToString(),
                //        Ten = item["Ten"].ToString()
                //    });
                string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}";
                List<Infor> TenNV = new List<Infor>();
                foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                    TenNV.Add(new Infor
                    {
                        Code = item["Code"].ToString(),
                        Ten = item["TenNhanVien"].ToString(),
                        Decription = item["MaNhanVien"].ToString()
                    });
                //LoaiKP.Add(new Infor
                //{
                //    Code = "Add",
                //    Ten = "Thêm"
                //});

                //rILUE_LoaiKinhPhi.DataSource = LoaiKP;
                //lUE_LoaiKinhPhi.Properties.DataSource = LoaiKP;
                rILUE_TenNguoi.DataSource = TenNV;
                //lUE_TenNguoiLap.Properties.DataSource = TenNV;
                rILUE_ToChucCaNhan.DataSource = DuAnHelper.GetCaNhanToChuc();
                //lUE_ToChucCaNhan.Properties.DataSource = DuAnHelper.GetCaNhanToChuc();
                tL_KhoanThu.DataSource = KhoanThu;
                return;
            }
            foreach (KhoanThu item in KhoanThu)
            {
                string ParentId = KT.FindAll(x => x.ParentID == item.CongTrinh).FirstOrDefault().ID;
                item.ParentID = ParentId;
                KT.Add(item);
            }
            //tL_KhoanThu.DataSource = KT;
            tL_KhoanThu.RefreshDataSource();
            tL_KhoanThu.Refresh();
            tL_KhoanThu.ExpandAll();
        }
        public void Fcn_Refresh()
        {
            tL_KhoanThu.RefreshDataSource();
            tL_KhoanThu.Refresh();
            tL_KhoanThu.ExpandAll();
        }
        public void Fcn_Export()
        {
            tL_KhoanThu.ShowRibbonPrintPreview();
        }
        public void Fcn_Update()
        {
            string dbstring = $"SELECT * " +
  $"FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} " +
  $"INNER JOIN {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} " +
  $"ON {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.CodeDeXuat = {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.Code " +
  $"WHERE {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT}.CodeDuAn ='{SharedControls.slke_ThongTinDuAn.EditValue}' AND {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU}.IsNguonThu='True' ";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
            dbstring = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} WHERE \"IsNguonThu\"='False'";
            DataTable dtTC = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
            dtTC.Columns["Code"].ColumnName = "ID";
            //if (dt.Rows.Count == 0)
            //    return;
            dt.Columns["Code"].ColumnName = "ID";
            dbstring = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable dtct = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
            List<KhoanThu> KT = new List<KhoanThu>();
            foreach (DataRow row in dtct.Rows)
            {
                DataRow[] crow = dt.AsEnumerable().Where(x => x["CongTrinh"].ToString() == row["Code"].ToString()).ToArray();
                DataRow[] crowtc = dtTC.AsEnumerable().Where(x => x["CongTrinh"].ToString() == row["Code"].ToString()).ToArray();
                KT.Add(new KhoanThu
                {
                    ID = row["Code"].ToString(),
                    ParentID = "0",
                    NoiDungThu = row["Ten"].ToString()
                });
                string codeNT = Guid.NewGuid().ToString();
                string codeKNT = Guid.NewGuid().ToString();
                KT.Add(new KhoanThu
                {
                    ID = codeNT,
                    ParentID = row["Code"].ToString(),
                    NoiDungThu = "THU THEO NGUỒN THU"
                });
                KT.Add(new KhoanThu
                {
                    ID = codeKNT,
                    ParentID = row["Code"].ToString(),
                    NgayThangThucHien = DateTime.Now.Date,
                    NoiDungThu = "THU TỰ DO KHÔNG THUỘC NGUỒN THU"
                });       
                KT.Add(new KhoanThu
                {
                    ID =Guid.NewGuid().ToString(),
                    ParentID = codeKNT,
                    NgayThangThucHien = DateTime.Now.Date,
                    TrangThai=3,
                    CheckDaThu=false,
                    NoiDungThu = "Nhập nguồn thu (Nếu có)"
                });
                if (crow.Any())
                {
                    List<KhoanThu> KTrow = DuAnHelper.ConvertToList<KhoanThu>(crow.CopyToDataTable());
                    KTrow.ForEach(x => x.ParentID = codeNT);
                    KTrow.ForEach(x => x.FileDinhKem = "Xem File");
                    KT.AddRange(KTrow);
                }      
                if (crowtc.Any())
                {
                    List<KhoanThu> KTrowtc = DuAnHelper.ConvertToList<KhoanThu>(crowtc.CopyToDataTable());
                    KTrowtc.ForEach(x => x.ParentID = codeKNT);
                    KTrowtc.ForEach(x => x.FileDinhKem = "Xem File");
                    KT.AddRange(KTrowtc);
                }

            }
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}";
            List<Infor> TenNV = new List<Infor>();
            foreach (DataRow item in DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows)
                TenNV.Add(new Infor
                {
                    Code = item["Code"].ToString(),
                    Ten = item["TenNhanVien"].ToString(),
                    Decription = item["MaNhanVien"].ToString()
                });
            //LoaiKP.Add(new Infor
            //{
            //    Code = "Add",
            //    Ten = "Thêm"
            //});

            //rILUE_LoaiKinhPhi.DataSource = LoaiKP;
            //lUE_LoaiKinhPhi.Properties.DataSource = LoaiKP;
            rILUE_TenNguoi.DataSource = TenNV;
            //lUE_TenNguoiLap.Properties.DataSource = TenNV;
            rILUE_ToChucCaNhan.DataSource = DuAnHelper.GetCaNhanToChuc();
            tL_KhoanThu.DataSource = KT;
            tL_KhoanThu.RefreshDataSource();
            tL_KhoanThu.Refresh();
            tL_KhoanThu.ExpandAll();
        }

        private void tL_KhoanThu_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.OrangeRed;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                KhoanThu task = tL_KhoanThu.GetRow(e.Node.Id) as KhoanThu;
                if (task.NoiDungThu == "THU THEO NGUỒN THU")
                    e.Appearance.ForeColor = Color.Blue;
                else
                    e.Appearance.ForeColor = Color.Green;
                e.Appearance.FontStyleDelta = (FontStyle.Italic);
                //e.Appearance.ForeColor = Color.LightSeaGreen;
            }
        }

        private void tL_KhoanThu_ShowingEditor(object sender, CancelEventArgs e)
        {
            KhoanThu KT = tL_KhoanThu.GetFocusedRow() as KhoanThu;
            if (KT == null)
                return;
            if (KT.NoiDungThu == "Nhập nguồn thu (Nếu có)" && tL_KhoanThu.FocusedColumn.FieldName != "NoiDungThu")
                e.Cancel = true;
            else if (KT.CheckDaThu&& tL_KhoanThu.FocusedColumn.FieldName!= "FileDinhKem")
                e.Cancel=true;
        }

        private void tL_KhoanThu_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level == 0 || e.Node.Level == 1)
                e.CanFocus = false;
            //else
            //{
            //    KhoanThu KT = tL_KhoanThu.GetFocusedRow() as KhoanThu;
            //    if (KT == null)
            //        return;
            //    if (KT.ID == "" && tL_KhoanThu.FocusedColumn.FieldName != "NoiDungThu")
            //        e.CanFocus = false;
            //}
        }

        private void tL_KhoanThu_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            List<KhoanThu> Data = tL_KhoanThu.DataSource as List<KhoanThu>;
            KhoanThu KT = tL_KhoanThu.GetFocusedRow() as KhoanThu;
            KhoanThu KTCha = tL_KhoanThu.GetDataRecordByNode(tL_KhoanThu.FocusedNode.ParentNode) as KhoanThu;
            string dbString = "";
            if (e.Node.Level == 2)
            {
                if(KT.NoiDungThu!= "Nhập nguồn thu (Nếu có)"&& tL_KhoanThu.ActiveEditor.OldEditValue is null)
                {
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} SET '{e.Column.FieldName}'=@Value,\"TrangThai\"='{3}' WHERE \"Code\"='{KT.ID}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                }
                if (tL_KhoanThu.ActiveEditor.OldEditValue == null)
                    return;
                if (tL_KhoanThu.ActiveEditor.OldEditValue.ToString() == "Nhập nguồn thu (Nếu có)")
                {
                    KT.ID = Guid.NewGuid().ToString();
                    KT.FileDinhKem = "Xem File";
                    KT.TrangThai = KT.TrangThai == 3 ? 3 : 3;
                    dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} (\"TrangThai\",\"CongTrinh\",\"Code\",'{e.Column.FieldName}',\"IsNguonThu\") VALUES ('{3}','{KTCha.ParentID}','{KT.ID}',@Value,'{false}')";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                    Data.Add(new KhoanThu
                    {
                        ID = Guid.NewGuid().ToString(),
                        ParentID = KT.ParentID,
                        NgayThangThucHien=DateTime.Now,
                        FileDinhKem = "Xem File",
                        NoiDungThu = "Nhập nguồn thu (Nếu có)"
                    });
                }
                else
                {
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} SET '{e.Column.FieldName}'=@Value,\"TrangThai\"='{3}' WHERE \"Code\"='{KT.ID}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                }
            }
            tL_KhoanThu.RefreshDataSource();
        }

        private void tL_KhoanThu_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (e.Node.Level == 2)
                return;
            if (object.Equals(e.CellValue, (double)0) || object.Equals(e.CellValue, false)||e.Column.FieldName== "NgayThangThucHien")
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }

        }

        private void tL_KhoanThu_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Node.Level <= 1)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnActivate = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                btnActivate.Buttons.Clear();
                e.RepositoryItem = btnActivate;
            }
        }
        private void rICE_CheckNgay_CheckedChanged(object sender, EventArgs e)
        {
            KhoanThu KT = tL_KhoanThu.GetFocusedRow() as KhoanThu;
            string dbstring = "";
            if (tL_KhoanThu.FocusedColumn.FieldName == "CheckDaThu")
            {
                if (KT.CheckDaThu)
                {
                    KT.CheckDaThu = true;
                    return;
                }
                DateTime NgayChi = KT.NgayThangThucHien;
                DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xác nhận đã thu không?");
                if (rs == DialogResult.No)
                {
                    KT.CheckDaThu = false;
                    return;
                }
                dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANTHU} SET \"CheckDaThu\"='{true}',\"NgayThangThucHien\"='{NgayChi.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' WHERE \"Code\"='{KT.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
                KT.CheckDaThu = true;
                MessageShower.ShowInformation("Xác nhận thành công!");

            }
            tL_KhoanThu.RefreshDataSource();
            tL_KhoanThu.Refresh();
        }

        private void rHpLE_File_Click(object sender, EventArgs e)
        {
            KhoanThu KT = tL_KhoanThu.GetFocusedRow() as KhoanThu;
            if (KT.ParentID == "0")
                return;
            FormLuaChon luachon = new FormLuaChon(KT.ID, FileManageTypeEnum.THUCHITAMUNG_KT, KT.NoiDungThu);
            luachon.ShowDialog();
        }
    }
}
