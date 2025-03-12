using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Constant;
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
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Menu;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Ctrl_ThuChiTamUng_KhoanChi : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_ThuChiTamUng_KhoanChi()
        {
            InitializeComponent();
        }
        public  TreeList Fcn_LoadTreeList()
        {
            TreeList New = new TreeList();
            New = tL_KhoanChi;
            return New;
        }
        public void Fcn_Updata(List<KhoanChi> KhoanChi)
        {
            List<KhoanChi> KC = tL_KhoanChi.DataSource as List<KhoanChi>;
            int STT = 1;
            if (KC == null)
            {
                List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource,false);
                rILUE_CongTrinhHM.DataSource = Infor;
                rILUE_CongTrinhHM.DropDownRows = Infor.Count;

                KhoanChi.ForEach(x => x.STT = (STT++).ToString());
                tL_KhoanChi.DataSource = KhoanChi;
            }
            else
            {
                STT = KC.FindAll(x => x.ParentID == "0").Count;
                foreach (KhoanChi item in KhoanChi)
                {
                    if (KC.Select(x => x.ID).Contains(item.ID))
                        continue;
                    item.STT = (++STT).ToString();
                    KC.Add(item);
                }

            }

            tL_KhoanChi.RefreshDataSource();
            tL_KhoanChi.ExpandAll();
        }
        public void Fcn_Export()
        {
            tL_KhoanChi.ShowRibbonPrintPreview();
        }
        public void Fcn_Refresh()
        {
            tL_KhoanChi.RefreshDataSource();
            tL_KhoanChi.Refresh();
            tL_KhoanChi.ExpandAll();
        }
        private void tL_KhoanChi_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Node.Level >= 1)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnActivate = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                btnActivate.Buttons.Clear();
                e.RepositoryItem = btnActivate;
            }
        }

        private void rIBE_ThemCP_Click(object sender, EventArgs e)
        {
            string ParentId = tL_KhoanChi.GetFocusedRowCellValue("ID").ToString();
            string Stt_Parent = tL_KhoanChi.GetFocusedRowCellValue("STT").ToString();
            //string CodeKC = tL_KhoanChi.GetFocusedRowCellValue("i").ToString();
            string code = Guid.NewGuid().ToString();
            List<KhoanChi> KC = tL_KhoanChi.DataSource as List<KhoanChi>;
            KhoanChi cRKC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
            int STT = KC.FindAll(x => x.ParentID == ParentId).Count;
            string Noidungung = "", dbString = "";
            Noidungung = $"Tạm ứng thi công lần {++STT}";
            KhoanChi newKC = new KhoanChi();
            newKC.STT = $"{Stt_Parent}.{STT}";
            newKC.ID = code;
            newKC.ParentID = ParentId;
            newKC.NoiDungUng = Noidungung;
            dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_NEWKHOANCHI} (\"Code\",\"CodeKC\",\"NoiDungUng\") VALUES " +
                $"('{code}','{ParentId}',@NoiDungUng)";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { Noidungung });
            if (STT == 1)
            {
                string query = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} WHERE \"CodeKC\"='{cRKC.ID}'";
                DataTable dtct = DataProvider.InstanceTHDA.ExecuteQuery(query);
                if (dtct.Rows.Count != 0)
                {
                    DialogResult rs = MessageShower.ShowYesNoQuestion("Chi phí đã có giải chi, bạn có chắc thêm mới chi phí?", "Cảnh Báo!");
                    if (rs == DialogResult.Yes)
                    {
                        newKC.GiaTriGiaiChi = cRKC.GiaTriGiaiChi;
                        foreach (DataRow row in dtct.Rows)
                        {
                            dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} SET \"CodeKC\"=NULL,\"CodeKCNew\"='{code}' WHERE \"Code\"='{row["Code"]}' ";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_NEWKHOANCHI} SET \"GiaTriGiaiChi\"='{newKC.GiaTriGiaiChi}' WHERE \"Code\"='{code}'";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                        }
                    }
                }
            }
            KC.Add(newKC);
            tL_KhoanChi.RefreshDataSource();
            tL_KhoanChi.ExpandAll();

            tL_KhoanChi.SetFocusedNode(tL_KhoanChi.FindNodeByKeyID(code));

        }

        private void tL_KhoanChi_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, 0) || (object.Equals(e.CellValue, false)&& (e.Node.Level >= 1)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
            if((e.Column.FieldName== "DateThucNhanUng"||e.Column.FieldName== "DateXacNhanDaUng" || e.Column.FieldName== "DateXacNhanDaChi") && e.Node.Level == 1)
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tL_KhoanChi_ShowingEditor(object sender, CancelEventArgs e)
        {
            KhoanChi KC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
            if (KC.TrangThai == 1)
            {
                e.Cancel = true;
                return;
            }            
            string colum = tL_KhoanChi.FocusedColumn.FieldName;
            if (((colum == "DateThucNhanUng" || colum == "DateXacNhanDaUng") && KC.CheckDaUng)|| colum == "DateXacNhanDaChi" && KC.CheckDaChi)
                e.Cancel = true;
            else if ((colum == "CheckDaChi" && KC.CheckDaChi)|| (colum == "CheckDaUng" && KC.CheckDaUng))
                e.Cancel = true;
            string[] FieldName = { "Chon", "TrangThai", "AddCP", "CheckDaChi" };
            if (KC.ParentID != "0" && FieldName.Contains(colum))
                e.Cancel = true;
            else if (KC.ParentID == "0" && colum == "NoiDungUng")
                e.Cancel = true;
        }
        public void Fcn_UpdateTrangThai(string CodeDX)
        {
            KhoanChi kc = (tL_KhoanChi.DataSource as List<KhoanChi>).FindAll(x => x.CodeDeXuat == CodeDX).FirstOrDefault();
            kc.TrangThai = 2;
            tL_KhoanChi.RefreshDataSource();

        }

        private void tL_KhoanChi_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            KhoanChi KC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
            BindingList<ChiPhiGiaiChiChiTiet> lst_KC = new BindingList<ChiPhiGiaiChiChiTiet>();
            DataTable dtct = null;
            int STT = 1;
            if (e.Node.Level == 0)
            {
                string dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} WHERE \"CodeKC\"='{KC.ID}'";
                dtct = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dtct.Rows.Count == 0)
                {
                    dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_NEWKHOANCHI} WHERE \"CodeKC\"='{KC.ID}'";
                    DataTable dtnew = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (dtnew.Rows.Count == 0)
                        goto nextcode;
                    else
                    {
                        string lstcode = MyFunction.fcn_Array2listQueryCondition(dtnew.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                        dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} WHERE \"CodeKCNew\" IN  ({lstcode})";
                        DataTable crDt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        foreach (DataRow row in dtnew.Rows)
                        {
                            DataRow[] crow = crDt.AsEnumerable().Where(x => x["CodeKCNew"].ToString() == row["Code"].ToString()).ToArray();
                            if (crow.Count() == 0)
                            {
                                lst_KC.Add(new ChiPhiGiaiChiChiTiet
                                {
                                    NoiDungUng = $"Chi tiết giải chi cho Chi phí: {row["NoiDungUng"].ToString()}",
                                    STT = 0,
                                    File = "",
                                });
                                continue;
                            }

                            BindingList<ChiPhiGiaiChiChiTiet> lst = new BindingList<ChiPhiGiaiChiChiTiet>(DuAnHelper.ConvertToList<ChiPhiGiaiChiChiTiet>(crow.CopyToDataTable()));
                            STT = 1;
                            foreach (ChiPhiGiaiChiChiTiet item in lst)
                            {
                                item.NoiDungUng = $"Chi tiết giải chi cho Chi phí: {row["NoiDungUng"].ToString()}";
                                item.STT = STT++;
                                item.File = "File đính kèm";
                                lst_KC.Add(item);
                            }

                        }
                        goto secondcode;
                    }
                }
            }
            else
            {
                string dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} WHERE \"CodeKCNew\"='{KC.ID}'";
                dtct = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            }
            STT = 1;
            if (dtct.Rows.Count != 0)
            {
                lst_KC = new BindingList<ChiPhiGiaiChiChiTiet>(DuAnHelper.ConvertToList<ChiPhiGiaiChiChiTiet>(dtct));

                foreach (ChiPhiGiaiChiChiTiet item in lst_KC)
                {
                    item.NoiDungUng = $"Chi tiết giải chi cho Chi phí: {KC.NoiDungUng}";
                    item.STT = STT++;
                    item.File = "File đính kèm";
                }
            }
            goto nextcode;

            nextcode:

            lst_KC.Add(new ChiPhiGiaiChiChiTiet
            {
                NoiDungUng = $"Chi tiết giải chi cho Chi phí: {KC.NoiDungUng}",
                STT = dtct.Rows.Count + 1,
                File = "File đính kèm",
                CodeKC = e.Node.Level == 0 ? KC.ID : null,
                CodeKCNew = e.Node.Level == 1 ? KC.ID : null,
            });
            goto secondcode;
            secondcode:
            gc_KhoanChiChiTiet.DataSource = lst_KC;
            gc_KhoanChiChiTiet.RefreshDataSource();
        }

        private void cb_ThuChiTamUng_KhoanChiChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int indOfTableLayout = 0;
            bool isMucLon = int.TryParse(cb.Name.Substring(cb.Name.Length - 1), out indOfTableLayout); //Có phải mục lớn không: Ví dụ ĐỀ XUẤT
            string TienTo = "Hiện";/*(isMucLon) ? "Thêm " : "Thêm quy trình ";*/
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

        static double Tong = 0;
        private void gv_KhoanChiChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            BindingList<ChiPhiGiaiChiChiTiet> lst = gv_KhoanChiChiTiet.DataSource as BindingList<ChiPhiGiaiChiChiTiet>;
            ChiPhiGiaiChiChiTiet CP = gv_KhoanChiChiTiet.GetRow(e.RowHandle) as ChiPhiGiaiChiChiTiet;
            string dbString = "";
            
            gv_KhoanChiChiTiet.UpdateSummary();
            double TT = double.Parse(gv_KhoanChiChiTiet.Columns["ThanhTien"].SummaryItem.SummaryValue.ToString());
            if (CP.Code == null)
            {
                CP.Code = Guid.NewGuid().ToString();
                dbString = $"INSERT INTO {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} (\"Code\",'{CP.colcode}','{e.Column.FieldName}') VALUES ('{CP.Code}','{CP.CodeCha}','{e.Value}')";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                lst.Add(new ChiPhiGiaiChiChiTiet
                {
                    NoiDungUng = $"{CP.NoiDungUng}",
                    STT = gv_KhoanChiChiTiet.RowCount,
                    File = "File đính kèm",
                    CodeKC = CP.colcode == "CodeKC" ? CP.CodeCha : null,
                    CodeKCNew = CP.colcode == "CodeKCNew" ? CP.CodeCha : null,
                });
                gc_KhoanChiChiTiet.RefreshDataSource();
            }
            else
            {
                ChiPhiGiaiChiChiTiet Cp = gv_KhoanChiChiTiet.GetFocusedRow() as ChiPhiGiaiChiChiTiet;
                if (Cp.ChiTietCoDonGia)
                {
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} SET '{e.Column.FieldName}'=@Value WHERE \"Code\"='{CP.Code}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                }
                else if (e.Column.FieldName == "DonGia" || e.Column.FieldName == "KhoiLuong")
                {
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} SET '{e.Column.FieldName}'=@Value,\"ChiTietCoDonGia\"='{true}' WHERE \"Code\"='{CP.Code}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                    Cp.ChiTietCoDonGia = true;
                    gc_KhoanChiChiTiet.RefreshDataSource();
                }
            }
            if (Tong != TT)
            {
                bool IsLayChiTiet =(bool)tL_KhoanChi.GetRowCellValue(tL_KhoanChi.FocusedNode, "IsLayChiTiet");
                if (IsLayChiTiet)
                {
                    tL_KhoanChi.SetRowCellValue(tL_KhoanChi.FocusedNode, "GiaTriGiaiChi", TT);
                    if (CP.CodeKC == null)
                    {
                        dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"GiaTriGiaiChi\"='{TT}' WHERE \"Code\"='{CP.CodeCha}' ";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                    else
                    {
                        dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_NEWKHOANCHI} SET \"GiaTriGiaiChi\"='{TT}' WHERE \"Code\"='{CP.CodeCha}' ";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                }
               
            }
        }

        private void gv_KhoanChiChiTiet_KeyUp(object sender, KeyEventArgs e)
        {
            double TT = 0;
            gv_KhoanChiChiTiet.UpdateSummary();
            if (gv_KhoanChiChiTiet.Columns["ThanhTien"].SummaryItem.SummaryValue != null)
                TT = double.Parse(gv_KhoanChiChiTiet.Columns["ThanhTien"].SummaryItem.SummaryValue.ToString());
            if (TT != Tong)
            {
                ChiPhiGiaiChiChiTiet CP = gv_KhoanChiChiTiet.GetFocusedRow() as ChiPhiGiaiChiChiTiet;
                bool IsLayChiTiet = (bool)tL_KhoanChi.GetRowCellValue(tL_KhoanChi.FocusedNode, "IsLayChiTiet");
                if (IsLayChiTiet)
                {
                    tL_KhoanChi.SetRowCellValue(tL_KhoanChi.FocusedNode, "GiaTriGiaiChi", TT);
                    string dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"GiaTriGiaiChi\"='{TT}' WHERE \"Code\"='{CP.CodeCha}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (gv_KhoanChiChiTiet.RowCount == 1)
                    return;
                ChiPhiGiaiChiChiTiet CP = gv_KhoanChiChiTiet.GetFocusedRow() as ChiPhiGiaiChiChiTiet;
                e.SuppressKeyPress = true;
                gv_KhoanChiChiTiet.DeleteSelectedRows();
                string dbString = $"DELETE FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} WHERE \"Code\"='{CP.Code}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                bool IsLayChiTiet = (bool)tL_KhoanChi.GetRowCellValue(tL_KhoanChi.FocusedNode, "IsLayChiTiet");
                if (IsLayChiTiet)
                {
                    double TTGC = (double)tL_KhoanChi.GetRowCellValue(tL_KhoanChi.FocusedNode, "GiaTriGiaiChi");
                    tL_KhoanChi.SetRowCellValue(tL_KhoanChi.FocusedNode, "GiaTriGiaiChi", TTGC-CP.ThanhTien);
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"GiaTriGiaiChi\"='{TTGC - CP.ThanhTien}' WHERE \"Code\"='{CP.CodeCha}' ";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
            }
        }

        private void gv_KhoanChiChiTiet_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (object.Equals(e.CellValue, 0) || (object.Equals(e.CellValue, false)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tL_KhoanChi_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (tL_KhoanChi.FocusedNode.Level == 1 && (e.Column.FieldName == "GiaTriGiaiChi"|| e.Column.FieldName == "GiaTriTamUngThucTe"))
            {
                KhoanChi KC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
                string dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_NEWKHOANCHI} SET '{e.Column.FieldName}'=@Value WHERE \"Code\"='{KC.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
                TreeListNode Parent = tL_KhoanChi.FocusedNode.ParentNode;
                if (Parent is null)
                    return;
                string Code = Parent.GetValue("ID").ToString();
                long TT = 0;
                foreach (TreeListNode item in Parent.Nodes)
                {
                    if (tL_KhoanChi.GetRowCellValue(item, e.Column.FieldName) == null)
                        continue;
                    TT +=long.Parse(tL_KhoanChi.GetRowCellValue(item, e.Column.FieldName).ToString());
                }
                tL_KhoanChi.SetRowCellValue(Parent, e.Column.FieldName, TT);
                dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET '{e.Column.FieldName}'=@Value WHERE \"Code\"='{Code}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { TT });
            }
            //else if (tL_KhoanChi.FocusedNode.Level == 1 && e.Column.FieldName == "GiaTriTamUngThucTe")
            //{
            //    TreeListNode Parent = tL_KhoanChi.FocusedNode.ParentNode;
            //    long TT = 0;
            //    foreach (TreeListNode item in Parent.Nodes)
            //    {
            //        if (tL_KhoanChi.GetRowCellValue(item, "GiaTriGiaiChi") == null)
            //            continue;
            //        TT += long.Parse(tL_KhoanChi.GetRowCellValue(item, "GiaTriGiaiChi").ToString());
            //    }
            //    tL_KhoanChi.SetRowCellValue(Parent, "GiaTriGiaiChi", TT);

            //}
            else if (tL_KhoanChi.FocusedNode.Level == 0 && e.Column.FieldName != "CheckDaChi" && e.Column.FieldName != "CheckDaUng"&& e.Column.FieldName != "Chon")
            {
                KhoanChi KC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
                string dbString = "";
                if (e.Column.FieldName == "DateThucNhanUng" || e.Column.FieldName == "DateXaNhanDaChi" || e.Column.FieldName == "DateThucNhanChi")
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET '{e.Column.FieldName}'='{DateTime.Parse(e.Value.ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' WHERE \"Code\"='{KC.ID}' ";
                else
                    dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET '{e.Column.FieldName}'=@Value WHERE \"Code\"='{KC.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });
            }
        }

        private void gv_KhoanChiChiTiet_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tL_KhoanChi.FocusedNode.Level == 0)
            {
                TreeListNode Parent = tL_KhoanChi.FocusedNode.ParentNode;
                if (Parent == null)
                    return;
                if (Parent.Nodes == null && tL_KhoanChi.FocusedColumn.FieldName != "File")
                    e.Cancel = true;
            }
        }

        private void tL_KhoanChi_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "GiaTriGiaiChi")
            {
                e.Appearance.BackColor = e.Node.Level == 0 ? Color.LightSalmon : Color.LightGreen;
            }
        }

        private void gv_KhoanChiChiTiet_GroupLevelStyle(object sender, DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventArgs e)
        {
            if (e.Level == 0)
            {
                e.LevelAppearance.ForeColor = Color.WhiteSmoke;
                e.LevelAppearance.BackColor = Color.Salmon;
            }
        }

        private void gv_KhoanChiChiTiet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ChiPhiGiaiChiChiTiet CP = gv_KhoanChiChiTiet.GetFocusedRow() as ChiPhiGiaiChiChiTiet;
            if (e.CellValue != null && e.Column.FieldName == "ThanhTien")
            {
                if (CP.CodeKC != null)
                    e.Appearance.BackColor = Color.Salmon;
                else
                    e.Appearance.BackColor = Color.LightGreen;
            }
        }
        public void Fcn_Load()
        {
            string dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_DEXUAT} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt.Rows.Count == 0)
            {
                tL_KhoanChi.DataSource = null;
                return;
            }
            string lst = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} WHERE \"CodeDeXuat\" IN ({lst})";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_NEWKHOANCHI}";
            DataTable dt_newKC = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT}";
            DataTable dt_KCCT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dt.Columns["Code"].ColumnName = "ID";
            dt_newKC.Columns["Code"].ColumnName = "ID";
            //dt.Columns["Code"].ColumnName = "ID";
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false);
            rILUE_CongTrinhHM.DataSource = Infor;
            rILUE_CongTrinhHM.DropDownRows = Infor.Count;

            dbString = $"SELECT *  FROM {ThuChiTamUng.TBL_THUCHITAMUNG_LOAIKINHPHI}";
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
            List<KhoanChi> KC = new List<KhoanChi>();
            List<KhoanChi> KCNew = new List<KhoanChi>();
            KC = DuAnHelper.ConvertToList<KhoanChi>(dt);
            int stt = 1;
            foreach (KhoanChi item in KC)
            {
                item.ParentID = "0";
                item.STT = (stt++).ToString();
                item.File = "Xem File";
                DataRow[] crRow = dt_newKC.AsEnumerable().Where(x => x["CodeKC"].ToString() == item.ID).ToArray();
                if (crRow.Any())
                {
                    KCNew = DuAnHelper.ConvertToList<KhoanChi>(crRow.CopyToDataTable());
                    KCNew.ForEach(x => x.ParentID = item.ID);
                    //KCNew.ForEach(x => x.DateThucNhanUng =);
                }
                crRow = dt_KCCT.AsEnumerable().Where(x => x["CodeKC"].ToString() == item.ID).ToArray();
            }
            KC.AddRange(KCNew);
            tL_KhoanChi.DataSource = KC;
            tL_KhoanChi.ExpandAll();
        }

        private void rICE_DaChi_CheckedChanged(object sender, EventArgs e)
        {
            KhoanChi KC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
            string dbstring = "";
            if (tL_KhoanChi.FocusedColumn.FieldName == "CheckDaChi")
            {
                if (KC.CheckDaChi)
                {
                    KC.CheckDaChi = true;
                    return;
                }

                if (!KC.CheckDaUng)
                {
                    MessageShower.ShowWarning("Vui lòng xác nhận đã ứng!");
                    return;
                }
                DateTime NgayChi = KC.DateXacNhanDaChi;
                if (NgayChi.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) == "0001-01-01")
                {
                    MessageShower.ShowWarning("Vui lòng xác nhận ngày đã chi!");
                    return;

                }
                DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xác nhận đã chi không?");
                if(rs==DialogResult.No)
                {
                    KC.CheckDaChi = false;
                    return;
                }    
                dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"TrangThai\"='{4}',\"CheckDaChi\"='{true}',\"DateXacNhanDaChi\"='{NgayChi.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' WHERE \"Code\"='{KC.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
                KC.TrangThai = 4;
                KC.CheckDaUng = true;
                MessageShower.ShowInformation("Xác nhận thành công!");
            }
            else
            {
                if (KC.CheckDaUng)
                {
                    KC.CheckDaUng = true;
                    return;
                }

                DateTime DateThucNhanUng = KC.DateThucNhanUng;
                DateTime DateXacNhanDaUng = KC.DateXacNhanDaUng;
                if (DateThucNhanUng.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) == "0001-01-01" || DateXacNhanDaUng.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE) == "0001-01-01")
                {
                    MessageShower.ShowWarning("Vui lòng xác nhận ngày nhận ứng và ngày xác nhận!");
                    return;

                }
                DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xác nhận đã ứng không?");
                if (rs == DialogResult.No)
                {
                    KC.CheckDaUng = false;
                    return;
                }
                dbstring = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"TrangThai\"='{3}',\"CheckDaUng\"='{true}'," +
                    $"\"DateThucNhanUng\"='{DateThucNhanUng.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}',\"DateXacNhanDaUng\"='{DateXacNhanDaUng.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' WHERE \"Code\"='{KC.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbstring);
                KC.TrangThai = 3;
                KC.CheckDaUng = true;
                MessageShower.ShowInformation("Xác nhận thành công!");
            }
            tL_KhoanChi.RefreshDataSource();
            tL_KhoanChi.Refresh();
        }

        private void rIHP_File_Click(object sender, EventArgs e)
        {
            KhoanChi KC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
            if (KC.ParentID != "0")
                return;
            FormLuaChon luachon = new FormLuaChon(KC.ID, FileManageTypeEnum.THUCHITAMUNG_KC, KC.NoiDungUng);
            luachon.ShowDialog();
        }

        private void iHPLE_File_Click(object sender, EventArgs e)
        {
            ChiPhiGiaiChiChiTiet KC = gv_KhoanChiChiTiet.GetFocusedRow() as ChiPhiGiaiChiChiTiet;
            if (KC.Ten == null)
                return;
            FormLuaChon luachon = new FormLuaChon(KC.Code,FileManageTypeEnum.THUCHITAMUNG_KCCT, KC.Ten);
            luachon.ShowDialog();
        }
        private void fcn_Handle_Popup_QLVT_BoGiaTri(object sender, EventArgs e)
        {
            TreeListNode Tl = tL_KhoanChi.FocusedNode;
            if (Tl == null)
                return;
            KhoanChi KC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
            if (Tl.Level == 0)
            {
                string dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"GiaTriGiaiChi\"='{0}',\"IsLayChiTiet\"='{false}' WHERE \"Code\"='{KC.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            else
            {
                //TreeListNode Parent = Tl.ParentNode;
                ////double TTParent = (double)tL_KhoanChi.GetRowCellValue(Parent, "GiaTriGiaiChi");
                ////double TTChild = (double)tL_KhoanChi.GetRowCellValue(Tl, "GiaTriGiaiChi");
                ////tL_KhoanChi.SetRowCellValue(Parent, "GiaTriGiaiChi", TTParent-TTChild);
                string dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_NEWKHOANCHI} SET \"GiaTriGiaiChi\"='{0}',\"IsLayChiTiet\"='{false}' WHERE \"Code\"='{KC.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            tL_KhoanChi.SetRowCellValue(Tl, "GiaTriGiaiChi", 0);
            tL_KhoanChi.SetRowCellValue(Tl, "IsLayChiTiet", false);
        }   
        private void fcn_Handle_Popup_QLVT_LayGiaTri(object sender, EventArgs e)
        {
            TreeListNode Tl = tL_KhoanChi.FocusedNode;
            if (Tl == null)
                return;
            double TT = 0;
            gv_KhoanChiChiTiet.UpdateSummary();
            if (gv_KhoanChiChiTiet.Columns["ThanhTien"].SummaryItem.SummaryValue != null)
                TT = double.Parse(gv_KhoanChiChiTiet.Columns["ThanhTien"].SummaryItem.SummaryValue.ToString());
            KhoanChi KC = tL_KhoanChi.GetFocusedRow() as KhoanChi;
            if (Tl.Level == 0)
            {
                string dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"GiaTriGiaiChi\"='{TT}',\"IsLayChiTiet\"='{true}' WHERE \"Code\"='{KC.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            else
            {
                //TreeListNode Parent = Tl.ParentNode;
                //long TTParent = (long)tL_KhoanChi.GetRowCellValue(Parent, "GiaTriGiaiChi");
                //long TTChild = (long)tL_KhoanChi.GetRowCellValue(Tl, "GiaTriGiaiChi");
                //tL_KhoanChi.SetRowCellValue(Parent, "GiaTriGiaiChi", TTParent-TTChild);
                string dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_NEWKHOANCHI} SET \"GiaTriGiaiChi\"='{TT}',\"IsLayChiTiet\"='{true}' WHERE \"Code\"='{KC.ID}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            tL_KhoanChi.SetRowCellValue(Tl, "GiaTriGiaiChi", TT);
            tL_KhoanChi.SetRowCellValue(Tl, "IsLayChiTiet", true);
        }   
        private void fcn_Handle_Popup_QLVT_Xoa(object sender, EventArgs e)
        {
            if (gv_KhoanChiChiTiet.RowCount == 1)
                return;
            ChiPhiGiaiChiChiTiet CP = gv_KhoanChiChiTiet.GetFocusedRow() as ChiPhiGiaiChiChiTiet;
            gv_KhoanChiChiTiet.DeleteSelectedRows();
            string dbString = $"DELETE FROM {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHICT} WHERE \"Code\"='{CP.Code}' ";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            bool IsLayChiTiet = (bool)tL_KhoanChi.GetRowCellValue(tL_KhoanChi.FocusedNode, "IsLayChiTiet");
            if (IsLayChiTiet)
            {
                double TTGC = (double)tL_KhoanChi.GetRowCellValue(tL_KhoanChi.FocusedNode, "GiaTriGiaiChi");
                tL_KhoanChi.SetRowCellValue(tL_KhoanChi.FocusedNode, "GiaTriGiaiChi", TTGC - CP.ThanhTien);
                dbString = $"UPDATE {ThuChiTamUng.TBL_THUCHITAMUNG_KHOANCHI} SET \"GiaTriGiaiChi\"='{TTGC - CP.ThanhTien}' WHERE \"Code\"='{CP.CodeCha}' ";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
        }
        private void gv_KhoanChiChiTiet_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (e.MenuType == GridMenuType.Row)
            {
                DXMenuItem menuItem = new DXMenuItem("Bỏ lấy tổng chi tiết làm giá trị giải chi", this.fcn_Handle_Popup_QLVT_BoGiaTri);
                menuItem.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem);

                DXMenuItem menuItem_Chon = new DXMenuItem("Lấy tổng chi tiết làm giá trị giải chi ", this.fcn_Handle_Popup_QLVT_LayGiaTri);
                menuItem_Chon.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem_Chon);         
                
                DXMenuItem menuItem_Xoa = new DXMenuItem("Xóa nội dung chọn", this.fcn_Handle_Popup_QLVT_Xoa);
                menuItem_Xoa.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuItem_Xoa);
            }
        }
    }
}
