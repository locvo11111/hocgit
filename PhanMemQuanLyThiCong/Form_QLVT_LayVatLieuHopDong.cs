using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.HopDong;
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

namespace PhanMemQuanLyThiCong
{
    public partial class Form_QLVT_LayVatLieuHopDong : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DE__TRUYENDATA(List<LayCongTacHopDong> CTHD,string CodeHD,string CodeNCC);
        public DE__TRUYENDATA m__TRUYENDATA;
        public Form_QLVT_LayVatLieuHopDong()
        {
            InitializeComponent();
        }
        public void LoadDanhSachHopDong()
        {
            //List<DonViThucHien> DVTH = DuAnHelper.GetDonViThucHiens();
            //DVTH.Remove(DVTH.FindAll(x => x.Code == x.CodeDuAn).FirstOrDefault());
            //ctrl_DonViThucHienDuAn.DataSource = DVTH;
            dE_Begin.DateTime = dE_End.DateTime = DateTime.Now;
            string dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINNHACUNGCAP} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable NCC = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor> lstNCC= DuAnHelper.ConvertToList<Infor>(NCC);
            lUE_NhaCungCap.Properties.DataSource = lstNCC;
            if (lstNCC.Count != 0)
                lUE_NhaCungCap.EditValue = lstNCC.FirstOrDefault().Code;
            //dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            //DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //List<Infor_HopDong> HD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_hd);
            //lUE_TenHopDong.Properties.DataSource = HD;

        }
        private void lUE_NhaCungCap_EditValueChanged(object sender, EventArgs e)
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor_HopDong> HD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_hd).FindAll(x => x.CodeDonViThucHien == lUE_NhaCungCap.EditValue.ToString());
            lUE_TenHopDong.Properties.DataSource = HD;
            if (HD.Count != 0)
                lUE_TenHopDong.EditValue = HD.FirstOrDefault().Code;
        }

        private void lUE_TenHopDong_EditValueChanged(object sender, EventArgs e)
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"CodeHd\"='{lUE_TenHopDong.EditValue}'";
            DataTable dt_Dot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor> Dot = DuAnHelper.ConvertToList<Infor>(dt_Dot);
            lUE_Dot.Properties.DataSource = Dot;
            lUE_Dot.EditValue = Dot.Count == 0 ? null : Dot.FirstOrDefault().Code;
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu hợp đồng", "Vui Lòng chờ!");
            dbString = $"SELECT {MyConstant.TBL_hopdongNCC_TT}.Code,{MyConstant.TBL_hopdongNCC_TT}.KhoiLuongThuCong,{MyConstant.TBL_HopDong_PhuLuc}.DonGia AS DonGiaTheoHopDong,{MyConstant.TBL_HopDong_PhuLuc}.KhoiLuong,{MyConstant.TBL_HopDong_PhuLuc}.Code as CodeVatLieu,{TDKH.TBL_KHVT_VatTu}.MaVatLieu,{TDKH.TBL_KHVT_VatTu}.VatTu,{TDKH.TBL_KHVT_VatTu}.DonVi, " +
//$"{TDKH.Tbl_HaoPhiVatTu}.DonVi,{TDKH.Tbl_HaoPhiVatTu}.DinhMucNguoiDung,{TDKH.Tbl_HaoPhiVatTu}.HeSoNguoiDung,{TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongHopDongChiTiet,{TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac, " +
$"{MyConstant.TBL_hopdongNCC_TT}.CodeDot,{MyConstant.TBL_HopDong_DotHopDong}.NgayBatDau,{MyConstant.TBL_HopDong_DotHopDong}.NgayKetThuc,{TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
$"FROM {MyConstant.TBL_hopdongNCC_TT} " +
$"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
$"ON {MyConstant.TBL_hopdongNCC_TT}.CodePhuLuc = {MyConstant.TBL_HopDong_PhuLuc}.Code " +
$"INNER JOIN {MyConstant.TBL_HopDong_DotHopDong} " +
$"ON {MyConstant.TBL_HopDong_DotHopDong}.Code = {MyConstant.TBL_hopdongNCC_TT}.CodeDot " +
$"INNER JOIN {TDKH.TBL_KHVT_VatTu} " +
$"ON {MyConstant.TBL_HopDong_PhuLuc}.CodeKHVT = {TDKH.TBL_KHVT_VatTu}.Code " +
$"WHERE  {MyConstant.TBL_hopdongNCC_TT}.CodeDot='{lUE_Dot.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            List<LayCongTacHopDong> CTHD = new List<LayCongTacHopDong>();
            double stt = 1;
            List<string> checkMaVatLieu = new List<string>();
            foreach (DataRow CT in dtCT.Rows)
            {
                string crCodeCT = CT["Code"].ToString();
                CTHD.Add(new LayCongTacHopDong()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaHieu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenCongViec = CT["Ten"].ToString(),
                });
                foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                {
                    string crCodeHM = HM["Code"].ToString();
                    CTHD.Add(new LayCongTacHopDong()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaHieu = MyConstant.CONST_TYPE_HANGMUC,
                        TenCongViec = HM["Ten"].ToString(),
                    });
                    DataRow[] crRows_VL = dt.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    foreach (var item in crRows_VL)
                    {
                     
                        CTHD.Add(new LayCongTacHopDong
                        {
                            STT = stt++,
                            ParentID = crCodeHM,
                            CodeCT = crCodeCT,
                            ID = item["CodeVatLieu"].ToString(),
                            MaHieu = item["MaVatLieu"].ToString(),
                            TenCongViec = item["VatTu"].ToString(),
                            DonVi = item["DonVi"].ToString(),
                            KhoiLuongHopDong = item["KhoiLuong"] == DBNull.Value ? 0 : Math.Round(double.Parse(item["KhoiLuong"].ToString()), 2),
                            DonGiaHopDong = item["DonGiaTheoHopDong"] ==DBNull.Value?0:Math.Round(double.Parse(item["DonGiaTheoHopDong"].ToString()),2)
                        });

                    }
                }
            }
            tL_KHVT.DataSource = CTHD;
            tL_KHVT.ExpandAll();
            WaitFormHelper.CloseWaitForm();
        }

        private void tL_KHVT_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue,(double)0) || (object.Equals(e.CellValue,(bool)false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tL_KHVT_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
            }
        }

        private void sB_Cancel_Click(object sender, EventArgs e)
        {
            tL_KHVT.UncheckAll();
            //TreeListNode[] lst = tL_KHVT.FindNodes(x => x.Level == 2);
            //if (lst.Count() == 0)
            //    return;
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", false);
        }

        private void sB_ok_Click(object sender, EventArgs e)
        {

            if (tL_KHVT.DataSource == null)
                return;
            List<LayCongTacHopDong> cthd = (tL_KHVT.DataSource as List<LayCongTacHopDong>).FindAll(x => x.Chon && x.MaHieu != "HM" && x.MaHieu != "CTR");
            //tL_KHVT.FindNodeByKeyID()
            //string tenghep = "";
            //string TenNoiDungUng = XtraInputBox.Show("Nhập tên nội dung ứng: ", "", "Dùng tên mặc định");
            //if (TenNoiDungUng == "" || TenNoiDungUng == "Dùng tên mặc định")
            //{
            //    foreach (LayCongTacHopDong item in cthd)
            //        tenghep += $", {item.TenCongViec}";
            //    TenNoiDungUng = tenghep.Remove(0, 1);
            //}
            //string ColCode = cBE_LoaiCP.Text == "Công tác" ? "CodeCongTac" : "CodeVatTu";
            m__TRUYENDATA(cthd, lUE_TenHopDong.EditValue.ToString(), lUE_NhaCungCap.EditValue.ToString());

            this.Close();
        }

        private void sB_All_Click(object sender, EventArgs e)
        {
            tL_KHVT.CheckAll();
            //TreeListNode[] lst = tL_KHVT.FindNodes(x => x.Level == 2 && x.Visible == true);
            //if (lst.Count() == 0)
            //    return;
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", true);
        }

        private void tL_KHVT_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level <= 1)
                e.CanFocus = false;
        }


        private void lUE_Dot_EditValueChanged(object sender, EventArgs e)
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"Code\"='{lUE_Dot.EditValue}'";
            DataTable dt_Dot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dE_Begin.DateTime = DateTime.Parse(dt_Dot.Rows[0]["NgayBatDau"].ToString());
            dE_End.DateTime = DateTime.Parse(dt_Dot.Rows[0]["NgayKetThuc"].ToString());
        }
    }
}
