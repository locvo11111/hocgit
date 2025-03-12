using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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
using PhanMemQuanLyThiCong.Model.HopDong;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Ctrl_KiemSoatChung : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_KiemSoatChung()
        {
            InitializeComponent();
        }

        private void ctrl_KiemSoatChung_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tải bảng biểu đồ kiểm soát dự án");
            SharedControls.ctrl_KiemSoatTienDoDuAn = ctrl_KiemSoatTienDoDuAn;
            WaitFormHelper.ShowWaitForm("Đang tải bảng thông báo hợp đồng");
            Fcn_CapNhapHopDong();
            WaitFormHelper.ShowWaitForm("Đang tải bảng kiểm soát tiến độ");
            kiemSoatTienDo1.Fcn_UpdateKiemSoat();
            WaitFormHelper.ShowWaitForm("Đang tải biểu đồ dòng tiền");
            ctrl_BaoCaoTaiChinhDuAn1.LoadDongTien();
            WaitFormHelper.CloseWaitForm();
        }
        
        public void RefreshTongHopTienDo()
        {
            //CVHN_TongHopTienDo.DataSource = TongHopHelper.GetAllKeHoachTienDoCongTrinhTheoDuAn(!ce_AnCongTac.Checked);

        }


        private void Fcn_CapNhapHopDong()
        {
            string dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG}";
            DataTable DtHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KiemSoatHopDong> KS = new List<KiemSoatHopDong>();
            string IDSPH = Guid.NewGuid().ToString();
            string IDGPH = Guid.NewGuid().ToString();
            string IDThuChi = Guid.NewGuid().ToString();
            string HopDong = Guid.NewGuid().ToString();
            KS.Add(new KiemSoatHopDong
            {
                ID= HopDong,
                ParentID="0",
                NoiDung="Hợp đồng"
            });   
            KS.Add(new KiemSoatHopDong
            {
                ID= IDThuChi,
                ParentID="0",
                NoiDung="Thu chi tạm ứng"
            });     
            KS.Add(new KiemSoatHopDong
            {
                ID= IDSPH,
                ParentID=HopDong,
                NoiDung="Hợp đồng sắp phát hành"
            });     
            KS.Add(new KiemSoatHopDong
            {
                ID= IDGPH,
                ParentID=HopDong,
                NoiDung="Hợp đồng sắp đến hạn"
            });
            DataRow[] RowSapHH = DtHD.AsEnumerable().Where(x => DateTime.Parse(x["NgayBatDau"].ToString()).Date >= DateTime.Now.Date).ToArray();
            DataRow[] RowHH = DtHD.AsEnumerable().Where(x => DateTime.Parse(x["NgayKetThuc"].ToString()).AddDays(-10).Date <= DateTime.Now.Date).ToArray();
            string tenduan = SharedControls.slke_ThongTinDuAn.Text;
            foreach(var row in RowSapHH)
            {
                KS.Add(new KiemSoatHopDong
                {
                    ID = Guid.NewGuid().ToString(),
                    ParentID = IDSPH,
                    NoiDung = row["TenHopDong"].ToString(),
                    DuAn = tenduan,
                    Date = DateTime.Parse(row["NgayBatDau"].ToString())
                }); ;
            }           
            foreach(var row in RowHH)
            {
                KS.Add(new KiemSoatHopDong
                {
                    ID = Guid.NewGuid().ToString(),
                    ParentID = IDGPH,
                    NoiDung = row["TenHopDong"].ToString(),
                    DuAn = tenduan,
                    Date = DateTime.Parse(row["NgayKetThuc"].ToString())
                });
            }
            Tl_ThongBaoHopDong.DataSource = KS;
            Tl_ThongBaoHopDong.ExpandAll();
        }
        
        private void cbb_ChonCuaSo_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cbb_ChonCuaSo.SelectedIndex)
            {
                case 0:
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Both;
                    spcc_KiemSoatChung_Row1.PanelVisibility = SplitPanelVisibility.Both;
                    spcc_KiemSoatChung_Row2.PanelVisibility = SplitPanelVisibility.Both;
                    break;
                case 1:
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Panel1;
                    spcc_KiemSoatChung_Row1.PanelVisibility = SplitPanelVisibility.Panel1;
                    break;
                case 2:
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Panel1;
                    spcc_KiemSoatChung_Row1.PanelVisibility = SplitPanelVisibility.Panel2;

                    break;
                case 3:
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Panel2;
                    spcc_KiemSoatChung_Row2.PanelVisibility = SplitPanelVisibility.Panel1;

                    break;
                case 4:
                    spcc_KiemSoatChung_Main.PanelVisibility = SplitPanelVisibility.Panel2;
                    spcc_KiemSoatChung_Row2.PanelVisibility = SplitPanelVisibility.Panel2;

                    break;
                default:
                    break;
            }

            
        }

        private void spcc_KiemSoatChung_Row1_SplitterMoved(object sender, EventArgs e)
        {
            spcc_KiemSoatChung_Row2.SplitterPosition = spcc_KiemSoatChung_Row1.SplitterPosition;
        }

        private void spcc_KiemSoatChung_Row2_SplitterMoved(object sender, EventArgs e)
        {
            spcc_KiemSoatChung_Row1.SplitterPosition = spcc_KiemSoatChung_Row2.SplitterPosition;
        }

        private void spcc_KiemSoatChung_Main_SizeChanged(object sender, EventArgs e)
        {
            spcc_KiemSoatChung_Main.SplitterPosition = spcc_KiemSoatChung_Main.Height / 2;
            spcc_KiemSoatChung_Row1.SplitterPosition = spcc_KiemSoatChung_Row1.Width / 2;
            spcc_KiemSoatChung_Row2.SplitterPosition = spcc_KiemSoatChung_Row2.Width / 2;
        }

        private void rg_CheDoXem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CVHN_TongHopTienDo.CollapseAll();
            //if (rg_CheDoXem.SelectedIndex == 0)
            //    return;

            //CVHN_TongHopTienDo.ExpandToLevel(rg_CheDoXem.SelectedIndex - 1);
        }

        private void tl_CanhBaoQuanTrong_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
                return;
            }
        }

        private void Tl_ThongBaoHopDong_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
                return;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
                return;
            }
        }

        private void Tl_ThongBaoHopDong_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (e.Node.Level == 2)
                return;
            if (object.Equals(e.CellValue, (double)0) || object.Equals(e.CellValue, false) || e.Column.FieldName == "Date")
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void ce_AnCongTac_CheckedChanged(object sender, EventArgs e)
        {
            //foreach (var node in CVHN_TongHopTienDo.Nod)
            WaitFormHelper.ShowWaitForm("Đang tải tiến độ");
            RefreshTongHopTienDo();
            WaitFormHelper.CloseWaitForm();

        }

        private void ctrl_BaoCaoTaiChinhDuAn1_sb_capNhap_Click(object sender, EventArgs e)
        {
            ctrl_BaoCaoTaiChinhDuAn1.LoadDongTien();
        }
        public event EventHandler ctrl_KiemSoatTienDoDuAn_Sb_XemChiTiet
        {
            add
            {
                ctrl_KiemSoatTienDoDuAn .Sb_XemChiTiet_Click+= value;
            }
            remove
            {
                ctrl_KiemSoatTienDoDuAn.Sb_XemChiTiet_Click -= value;
            }
        }
        private void ctrl_KiemSoatTienDoDuAn_Sb_XemChiTiet_Click(object sender, EventArgs e)
        {
            //if (!ctrl_KiemSoatTienDoDuAn.IsSelect)
            //    return;
            //xtraTab_KiemSoat.SelectedPageChanged -= xtraTab_KiemSoat_SelectedPageChanged;
            //xtraTab_KiemSoat.SelectedTabPage = xtraTab_KiemSoatTienDo;
            //string Condition = ctrl_KiemSoatTienDoDuAn.Condition;
            //string Condition1 = ctrl_KiemSoatTienDoDuAn.Condition1;
            //if (Condition == "All")
            //    kiemSoatTienDo.Fcn_UpdateKiemSoat(true);
            //else
            //    kiemSoatTienDo.Fcn_UpdateKiemSoat(true, Condition: Condition, Condition1: Condition1);
            //xtraTab_KiemSoat.SelectedPageChanged += xtraTab_KiemSoat_SelectedPageChanged;
        }

        private void ctrl_KiemSoatTienDoDuAn_Load(object sender, EventArgs e)
        {
            ctrl_KiemSoatTienDoDuAn.Fcn_UpdateBieuDoTron();
            ctrl_KiemSoatTienDoDuAn.Fcn_UpdateBarChart();
        }
    }
}
