using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.HopDong;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
using PhanMemQuanLyThiCong.Repositories;
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
    public partial class Form_TCTU_LayDauViecHopDong : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DE__TRUYENDATA(List<LayCongTacHopDong> CTHD, string TenNoiDung,string CodeHD,bool Chi,bool HD,string ToChucCaNhan);
        public static bool _mcheckclosed=false;
        public DE__TRUYENDATA m__TRUYENDATA;
        public Form_TCTU_LayDauViecHopDong()
        {
            InitializeComponent();
        }
        public void LoadDanhSachHopDong()
        {
            //lUE_CongTrinh.EditValueChanged -= lUE_CongTrinh_EditValueChanged;
            //lUE_TenHopDong.EditValueChanged -= lUE_TenHopDong_EditValueChanged;
            //lUE_Dot.EditValueChanged -= lUE_Dot_EditValueChanged;
            _mcheckclosed = false;
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu, Vui lòng chờ!");
            List<DonViThucHien> DVTH = DuAnHelper.GetDonViThucHiens(ctrl_DonViThucHienDuAn,true);
            DVTH.Remove(DVTH.FindAll(x => x.Code == x.CodeDuAn).FirstOrDefault());
            ctrl_DonViThucHienDuAn.DataSource = DVTH;
            ctrl_DonViThucHienDuAn.EditValue = DVTH.FirstOrDefault().CodeFk;
            ctrl_DonViThucHienDuAn.FcnAcceptchecked();
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false,true);
            lUE_CongTrinh.Properties.DataSource = Infor;
            lUE_CongTrinh.EditValue = Infor.FirstOrDefault().ID;
            WaitFormHelper.CloseWaitForm();
            //string dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            //DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //List<Infor_HopDong> HD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_hd);

            //lUE_TenHopDong.Properties.DataSource = HD;

            //lUE_CongTrinh.EditValueChanged -= lUE_CongTrinh_EditValueChanged;
            //lUE_TenHopDong.EditValueChanged -= lUE_TenHopDong_EditValueChanged;
            //lUE_Dot.EditValueChanged -= lUE_Dot_EditValueChanged;
        }

        private void ctrl_DonViThucHienDuAn_DVTHChanged(object sender, EventArgs e)
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor_HopDong> HD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_hd).FindAll(x => x.CodeDonViThucHien == ctrl_DonViThucHienDuAn.SelectedDVTH.Code);
            lUE_TenHopDong.Properties.DataSource = HD;
            lUE_TenHopDong.EditValue = HD.Count == 0 ? null : HD.FirstOrDefault().Code;
            //FcnUpdateCongTac();
        }

        private void lUE_TenHopDong_EditValueChanged(object sender, EventArgs e)
        {
            
            string dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"CodeHd\"='{lUE_TenHopDong.EditValue}'";
            DataTable dt_Dot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor> Dot = DuAnHelper.ConvertToList<Infor>(dt_Dot);
            lUE_Dot.Properties.DataSource = Dot;
            lUE_Dot.EditValue = Dot.Count == 0 ? null : Dot.FirstOrDefault().Code;
        }

        private void lUE_Dot_EditValueChanged(object sender, EventArgs e)
        {
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            string dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"Code\"='{lUE_Dot.EditValue}'";
            DataTable dt_Dot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dt_Dot.Rows.Count == 0)
            {
                tL_KHVT.DataSource = null;
                tL_KHVT.RefreshDataSource();
                return;
            }
            dE_Begin.DateTime = DateTime.Parse(dt_Dot.Rows[0]["NgayBatDau"].ToString());
            dE_End.DateTime = DateTime.Parse(dt_Dot.Rows[0]["NgayKetThuc"].ToString());
            if(DVTH.Table==MyConstant.TBL_THONGTINNHACUNGCAP)
                Fcn_UpdateNcc();
            else
                Fcn_UpdateCongTac();
        }
        private void Fcn_UpdateNcc()
        {
            if (lUE_CongTrinh.EditValue == null || lUE_Dot.EditValue == null || lUE_TenHopDong.EditValue == null)
                return;
            string dbstring = $"SELECT {MyConstant.TBL_hopdongNCC_TT}.Code,{MyConstant.TBL_hopdongNCC_TT}.LuyKeDenHetKyNay," +
                $"{MyConstant.TBL_HopDong_PhuLuc}.DonGia AS DonGiaTheoHopDong,{MyConstant.TBL_HopDong_PhuLuc}.KhoiLuong," +
                $"{MyConstant.TBL_HopDong_PhuLuc}.Code as CodeVatLieu,{TDKH.TBL_KHVT_VatTu}.MaVatLieu," +
                $"{TDKH.TBL_KHVT_VatTu}.VatTu,{TDKH.TBL_KHVT_VatTu}.DonVi, " +
$"{MyConstant.TBL_hopdongNCC_TT}.CodeDot,{MyConstant.TBL_HopDong_DotHopDong}.NgayBatDau,{MyConstant.TBL_HopDong_DotHopDong}.NgayKetThuc,{TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
$"FROM {MyConstant.TBL_hopdongNCC_TT} " +
$"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
$"ON {MyConstant.TBL_hopdongNCC_TT}.CodePhuLuc = {MyConstant.TBL_HopDong_PhuLuc}.Code " +
$"INNER JOIN {MyConstant.TBL_HopDong_DotHopDong} " +
$"ON {MyConstant.TBL_HopDong_DotHopDong}.Code = {MyConstant.TBL_hopdongNCC_TT}.CodeDot " +
$"INNER JOIN {TDKH.TBL_KHVT_VatTu} " +
$"ON {MyConstant.TBL_HopDong_PhuLuc}.CodeKHVT = {TDKH.TBL_KHVT_VatTu}.Code " +
$"WHERE {MyConstant.TBL_hopdongNCC_TT}.CodeDot='{lUE_Dot.EditValue}'";
            DataTable Dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC,false);
            string ngaybatdau = dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string ngayketthuc = dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            DataRow[] dstCT = dtCT.AsEnumerable().Where(x => x["Code"].ToString() == lUE_CongTrinh.EditValue.ToString()).ToArray();
            List<LayCongTacHopDong> CTHD = new List<LayCongTacHopDong>();
            foreach (DataRow CT in dstCT.CopyToDataTable().Rows)
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
                    DataRow[] drs_ct = Dt_HD.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    int stt = 1;
                    foreach (var rowct in drs_ct)
                    {
                        CTHD.Add(new LayCongTacHopDong
                        {
                            STT = stt++,
                            ParentID = crCodeHM,
                            CodeCT = crCodeCT,
                            ID = rowct["Code"].ToString(),
                            MaHieu = rowct["MaVatLieu"].ToString(),
                            TenCongViec = rowct["VatTu"].ToString(),
                            DonVi = rowct["DonVi"].ToString(),
                            KhoiLuongHopDong = rowct["KhoiLuong"]==DBNull.Value?0: double.Parse(rowct["KhoiLuong"].ToString()),
                            KhoiLuongThiCong = rowct["LuyKeDenHetKyNay"] == DBNull.Value ? 0 : double.Parse(rowct["LuyKeDenHetKyNay"].ToString()),
                            ThanhTienThiCong = rowct["LuyKeDenHetKyNay"] == DBNull.Value ? 0 : double.Parse(rowct["DonGia"].ToString())* double.Parse(rowct["LuyKeDenHetKyNay"].ToString()),
                            NgayBD = dE_Begin.DateTime,
                            NgayKT = dE_End.DateTime,
                        });
                    }
                }
            }
            tL_KHVT.DataSource = CTHD;
            tL_KHVT.RefreshDataSource();
            tL_KHVT.ExpandAll();
            tL_KHVT.Refresh();
        }
        private void Fcn_UpdateCongTac()
        {
            if (lUE_CongTrinh.EditValue == null || lUE_Dot.EditValue == null || lUE_TenHopDong.EditValue == null)
                return;
            string dbString = $"SELECT {MyConstant.TBL_hopdongAB_HT}.*,{MyConstant.TBL_HopDong_PhuLuc}.Code as CodePL,{TDKH.TBL_ChiTietCongTacTheoKy}.Code as CodeCongTacTheoGiaiDoan,{MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh," +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc," +
                $"{TDKH.TBL_DanhMucCongTac}.DonVi,{TDKH.TBL_DanhMucCongTac}.MaHieuCongTac,{TDKH.TBL_DanhMucCongTac}.TenCongTac" +
                $",{MyConstant.TBL_HopDong_PhuLuc}.DonGia,{MyConstant.TBL_HopDong_PhuLuc}.KhoiLuong FROM {MyConstant.TBL_hopdongAB_HT} " +
    $"INNER JOIN {MyConstant.TBL_HopDong_DotHopDong} " +
    $"ON {MyConstant.TBL_HopDong_DotHopDong}.Code = {MyConstant.TBL_hopdongAB_HT}.CodeDot " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_DoBoc} " +
                    $"ON {MyConstant.TBL_HopDong_DoBoc}.Code = {MyConstant.TBL_hopdongAB_HT}.CodeDB " +
                    $"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.Code = {MyConstant.TBL_HopDong_DoBoc}.CodePL " +
                    $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl = {MyConstant.TBL_ThongtinphulucHD}.Code " +
                    $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
                    $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd = {MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
                    $"INNER JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                    $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                    $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +        
                    $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                    $"ON {TDKH.TBL_DanhMucCongTac}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +    
                    $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                    $" WHERE {MyConstant.TBL_HopDong_DoBoc}.CodeDot='{lUE_Dot.EditValue}' AND {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong='{lUE_TenHopDong.EditValue}' " +
                    $"AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
            DataTable Dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            string ngaybatdau = dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string ngayketthuc = dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            List<LayCongTacHopDong> CTHD = new List<LayCongTacHopDong>();
            DataRow[] dstCT = dtCT.AsEnumerable().Where(x => x["Code"].ToString() == lUE_CongTrinh.EditValue.ToString()).ToArray();
            int stt = 1;
            //string[] lsCodeCongTac = Dt_HD.AsEnumerable().Select(x =>x["CodeCongTacTheoGiaiDoan"].ToString()).ToArray();
            ////dbString = $"SELECT *  FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" IN ({string.Join(", ", lsCodeCongTac)}) ";
            //DataTable dtKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);
            //DataTable dtKLHN= MyFunction.Fcn_CalKLKH(Dt_HD, "CodeCongTacTheoGiaiDoan");
            foreach (DataRow CT in dstCT.CopyToDataTable().Rows)
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
                    DataRow[] drs_ct = Dt_HD.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    foreach (var rowct in drs_ct)
                    {
                        //DataRow[] crRow = dtKLHN.AsEnumerable().Where(x => x["CodeCongTacTheoGiaiDoan"].ToString() == rowct["CodeCongTacTheoGiaiDoan"].ToString() && DateTime.Parse(x["Ngay"].ToString()) >= DateTime.Parse(ngaybatdau) && DateTime.Parse(x["Ngay"].ToString()) <= DateTime.Parse(ngayketthuc)).ToArray();
                        //double KLTC = 0,TT=0;
                        //foreach (var item in crRow)
                        //{
                        //    if (item["KhoiLuongThiCong"]==DBNull.Value)
                        //        continue;
                        //    KLTC += double.Parse(item["KhoiLuongThiCong"].ToString());
                        //    TT += double.Parse(item["ThanhTienThiCong"].ToString());
                        //}
                        CTHD.Add(new LayCongTacHopDong
                        {
                            STT = stt++,
                            ParentID = crCodeHM,
                            CodeCT=crCodeCT,
                            ID = rowct["CodePL"].ToString(),
                            MaHieu = rowct["MaHieuCongTac"].ToString(),
                            TenCongViec = rowct["TenCongTac"].ToString(),
                            DonVi = rowct["DonVi"].ToString(),
                            KhoiLuongHopDong = rowct["KhoiLuong"]!=DBNull.Value?double.Parse(rowct["KhoiLuong"].ToString()):0,
                            KhoiLuongThiCong = rowct["ThucHienKyNay"] != DBNull.Value ? double.Parse(rowct["ThucHienKyNay"].ToString()) : 0,
                            ThanhTienThiCong = rowct["ThucHienKyNay"] != DBNull.Value && rowct["DonGia"] != DBNull.Value ?Math.Round(double.Parse(rowct["ThucHienKyNay"].ToString()) * double.Parse(rowct["DonGia"].ToString())) : 0,
                            NgayBD = dE_Begin.DateTime,
                            NgayKT = dE_End.DateTime,
                        });

                    }
                    }
            }
            tL_KHVT.DataSource = CTHD;
            tL_KHVT.RefreshDataSource();
            tL_KHVT.ExpandAll();
            tL_KHVT.Refresh();
        }

        private void tL_KHVT_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            //if (e.Node.Level == 0)
            //{
            //    List<TreeListNode> lst = tL_KHVT.Nodes.AsEnumerable().Where(x => x.Level == 0).ToList();
            //    foreach (TreeListNode item in lst)
            //    {
            //        if (item.Expanded)
            //        {
            //            MessageShower.ShowInformation("Chỉ chọn được 1 công trình!", "", MessageBoxButtons.OK);
            //            TreeListNodes Child = item.Nodes;
            //            foreach (TreeListNode cr in Child)
            //            {
            //                foreach(TreeListNode crow in cr.Nodes)
            //                    crow.SetValue("Chon", false);
            //            }
            //            item.Collapse();
            //            //e.CanExpand = false;
            //        }
            //    }
            //}
        }

        private void tL_KHVT_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level < 2)
                e.CanFocus = false;
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

        private void tL_KHVT_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }


        private void sB_ok_Click(object sender, EventArgs e)
        {
            if (tL_KHVT.DataSource is null)
                return;
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            string tenghep = "";
            _mcheckclosed = false;
            //tL_KHVT.FindNodeByKeyID()
            if (ce_CT.Checked)
            {
                if (tL_KHVT.DataSource == null)
                    return;
                List<LayCongTacHopDong> cthd = (tL_KHVT.DataSource as List<LayCongTacHopDong>).FindAll(x => x.Chon && x.MaHieu != "HM" && x.MaHieu != "CTR");
                if (cthd.Count() == 0)
                    return;
                string TenNoiDungUng = XtraInputBox.Show("Nhập tên nội dung ứng: ", "", "Dùng tên mặc định");
                if (TenNoiDungUng == "" || TenNoiDungUng == "Dùng tên mặc định")
                {
                    foreach (LayCongTacHopDong item in cthd)
                        tenghep += $", {item.TenCongViec}";
                    TenNoiDungUng = tenghep.Remove(0, 1);
                }
                _mcheckclosed = true;
                if (DVTH.Table == MyConstant.TBL_THONGTINNHATHAU)
                    m__TRUYENDATA(cthd, TenNoiDungUng, lUE_TenHopDong.EditValue.ToString(), false,false, DVTH.Code);
                else
                    m__TRUYENDATA(cthd, TenNoiDungUng, lUE_TenHopDong.EditValue.ToString(), true,false, DVTH.Code);
            }
            else
            {
                _mcheckclosed = true;
                tenghep = lUE_TenHopDong.Text;
                List<LayCongTacHopDong> cthd = (tL_KHVT.DataSource as List<LayCongTacHopDong>).FindAll(x =>x.MaHieu != "HM" && x.MaHieu != "CTR");
                //if (cthd.Count() == 0)
                //    return;
                if (DVTH.Table == MyConstant.TBL_THONGTINNHATHAU)
                    m__TRUYENDATA(cthd, tenghep, lUE_TenHopDong.EditValue.ToString(), false, true, DVTH.Code);
                else
                    m__TRUYENDATA(cthd, tenghep, lUE_TenHopDong.EditValue.ToString(), true, true, DVTH.Code);
            }
            this.Close();
        }

        private void ce_CT_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_CT.Checked)
                ce_HD.Checked = false;
            else
                ce_HD.Checked = true;
        }

        private void ce_HD_CheckedChanged(object sender, EventArgs e)
        {            
            if (ce_HD.Checked)
                ce_CT.Checked = false;
            else
                ce_CT.Checked = true;
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

        private void lUE_CongTrinh_EditValueChanged(object sender, EventArgs e)
        {
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            if (DVTH.Table == MyConstant.TBL_THONGTINNHACUNGCAP)
                Fcn_UpdateNcc();
            else
                Fcn_UpdateCongTac();
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

        private void Form_TCTU_LayDauViecHopDong_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_mcheckclosed)
                return;
            m__TRUYENDATA(null, "", null, true, false, null);
        }
    }
}