using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using PhanMemQuanLyThiCong.Model;
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

namespace PhanMemQuanLyThiCong
{
    public partial class Form_QLVT_LayTuTDKH : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DE__TRUYENDATA(List<TCTU_TDKH> ThuChiTDKH, DonViThucHien DVTH);
        public DE__TRUYENDATA m__TRUYENDATA;
        DataTable M_YeuCau = new DataTable();
        bool m_check { get; set; }= false;
        public Form_QLVT_LayTuTDKH()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            string dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND \"CodeKHVT\" IS NOT NULL";
            DataTable dt_yeucau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            M_YeuCau = dt_yeucau;
            m_check = false;
            dE_Begin.EditValueChanged -= dE_Begin_EditValueChanged;
            dE_End.EditValueChanged -= dE_End_EditValueChanged;
            dE_Begin.DateTime = dE_End.DateTime = DateTime.Now;
            dE_Begin.EditValueChanged += dE_Begin_EditValueChanged;
            dE_End.EditValueChanged += dE_End_EditValueChanged;
            slue_CongTrinhHangMuc.EditValueChanged -= slue_CongTrinhHangMuc_EditValueChanged;
            List<InforCT_HM> InforHM = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, true,true);
            slue_CongTrinhHangMuc.Properties.DataSource = InforHM;
            slue_CongTrinhHangMuc.EditValue = InforHM.FirstOrDefault().ID;
            slue_CongTrinhHangMuc.EditValueChanged += slue_CongTrinhHangMuc_EditValueChanged;
            cBE_LoaiCP.Text = "Vật liệu";
            List<DonViThucHien> Infor = DuAnHelper.GetDonViThucHiens();
            lUE_ToChucCaNhan.Properties.DataSource = Infor;
            lUE_ToChucCaNhan.EditValue = Infor.FirstOrDefault().Code;
        }
        public bool CheckHaoPhi { get { return ce_HaoPhi.Checked;} }
        private void Test()
        {
            if (lUE_ToChucCaNhan.EditValue == null)
            {
                MessageShower.ShowWarning("Vui lòng chọn đơn vị thực hiện!");
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu Kế hoạch", "Vui Lòng chờ!");
            //DataTable dtCT, dtHM;
            //DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);

            string dbString = "";
            List<DonViThucHien> Infor = DuAnHelper.GetDonViThucHiens();
            DonViThucHien Detail = Infor.FindAll(x => x.Code == (string)lUE_ToChucCaNhan.EditValue).FirstOrDefault();
            List<TCTU_TDKH> ThuChiTDKH = new List<TCTU_TDKH>();

            //dbString = $"SELECT COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac" +
            //    $",COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi," +
            //    $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
            //    $"cttk.* FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
            //    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
            //    $"ON dmct.Code =cttk.CodeCongTac " +
            //    $" WHERE cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
            //    $"AND cttk.{Detail.ColCodeFK}='{Detail.Code}' ORDER BY \"Row\" ASC";
            dbString = $"SELECT hp.* FROM view_HaoPhiVatTu hp WHERE " +               
                $"hp.CodeHangMuc='{slue_CongTrinhHangMuc.EditValue}' AND hp.LoaiVatTu='{cBE_LoaiCP.Text}'";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            //string lstCode = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            //dbString = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu} WHERE \"CodeCongTac\" IN ({lstCode}) AND \"LoaiVatTu\"='{cBE_LoaiCP.Text}'";
            //DataTable dtHPVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT hp.*,HM.Ten as TenHangMuc,CT.Ten as TenCongTrinh,CT.Code as CodeCongTrinh FROM {TDKH.TBL_KHVT_VatTu} hp " +
                 $"LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} HM ON HM.Code=hp.CodeHangMuc" +
                $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CT ON CT.Code=HM.CodeCongTrinh" +
                $" WHERE hp.{Detail.ColCodeFK}='{Detail.Code}' AND hp.CodeHangMuc='{slue_CongTrinhHangMuc.EditValue}' AND hp.LoaiVatTu='{cBE_LoaiCP.Text}'";
            DataTable dtDanhSachVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lstCodeVL = dtDanhSachVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();

            if (dtDanhSachVT.Rows.Count != 0 && !m_check)
            {
                m_check = true;
                DateTime Max = dtDanhSachVT.AsEnumerable().Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                DateTime Min = dtDanhSachVT.AsEnumerable().Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                dE_Begin.EditValueChanged -= dE_Begin_EditValueChanged;
                dE_End.EditValueChanged -= dE_End_EditValueChanged;
                dE_Begin.DateTime = Min;
                dE_End.DateTime = Max;
                dE_Begin.EditValueChanged += dE_Begin_EditValueChanged;
                dE_End.EditValueChanged += dE_End_EditValueChanged;
            }
            int stt = 1;
            string[] lst =dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            var dtKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.VatLieu, lstCodeVL, dE_Begin.DateTime, dE_End.DateTime);
            var HPVTHN = CheckHaoPhi ? MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.HaoPhiVatTu, lst, dE_Begin.DateTime, dE_End.DateTime) : null;
            double KLLKYC = 0, KLLKXK = 0, ConLai = 100, KLKH = 0, KLHD = 0;
            bool CheckVatLieu = false;
            var GrCtrinh = dtDanhSachVT.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);
            foreach(var Ctrinh in GrCtrinh)
            {
                ThuChiTDKH.Add(new TCTU_TDKH()
                {
                    ParentID = "0",
                    ID = Ctrinh.Key.ToString(),
                    MaHieu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenCongViec = Ctrinh.FirstOrDefault()["TenCongTrinh"].ToString(),
                });
                var grHM = Ctrinh.GroupBy(x => x["CodeHangMuc"]);
                foreach(var Hm in grHM)
                {
                    ThuChiTDKH.Add(new TCTU_TDKH()
                    {
                        ParentID = Ctrinh.Key.ToString(),
                        ID = Hm.Key.ToString(),
                        MaHieu = MyConstant.CONST_TYPE_HANGMUC,
                        TenCongViec = Hm.FirstOrDefault()["TenHangMuc"].ToString(),
                    });
                    stt = 1;
                    var grVatLieu = Hm.GroupBy(x => x["Code"]);
                    foreach(var VL in grVatLieu)
                    {
                        var FirstItem = VL.FirstOrDefault();
                        List<KLHN> crRow = dtKLHN.Where(x => x.ParentCode == VL.Key.ToString()).ToList();
                        KLKH = crRow.Any() ? (double)crRow.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach) : 0;
                        KLHD = FirstItem["KhoiLuongHopDong"] == DBNull.Value ? 0 : Math.Round(double.Parse(FirstItem["KhoiLuongHopDong"].ToString()), 2);
                        KLLKYC = 0; KLLKXK = 0; ConLai = 100;
                        CheckVatLieu = false;
                        if (M_YeuCau.Rows.Count != 0)
                        {
                            DataRow[] crowKHVT = M_YeuCau.AsEnumerable().Where(x => x["CodeKHVT"].ToString() == VL.Key.ToString()).ToArray();
                            if (crowKHVT.Count() != 0)
                            {
                                CheckVatLieu = true;
                                KLLKYC = crowKHVT.FirstOrDefault()["LuyKeYeuCau"] == DBNull.Value ? 0 : Math.Round(double.Parse(crowKHVT.FirstOrDefault()["LuyKeYeuCau"].ToString()), 2);
                                KLLKXK = crowKHVT.FirstOrDefault()["LuyKeXuatKho"] == DBNull.Value ? 0 : Math.Round(double.Parse(crowKHVT.FirstOrDefault()["LuyKeXuatKho"].ToString()), 2);
                            }
                        }
                        ThuChiTDKH.Add(new TCTU_TDKH()
                        {
                            STT = stt++,
                            ParentID = Hm.Key.ToString(),
                            ID = VL.Key.ToString(),
                            MaHieu = FirstItem["MaVatLieu"].ToString(),
                            TenCongViec = FirstItem["VatTu"].ToString(),
                            DonVi = FirstItem["DonVi"].ToString(),
                            KhoiLuongHopDong = KLHD,
                            KhoiLuongKeHoach = Math.Round(KLKH, 4),
                            LuyKeYeuCau = KLLKYC,
                            LuyKeXuatKho = KLLKXK,
                            IsVatLieu = CheckVatLieu,
                            DonGia = FirstItem["DonGia"] == DBNull.Value ? 0 : double.Parse(FirstItem["DonGia"].ToString()),
                            NgayBD = dE_Begin.DateTime,
                            NgayKT = dE_End.DateTime,
                        });
                        if (CheckHaoPhi)
                        {
                            DataRow[] drs_VTCon = dtCongTacTheoKy.AsEnumerable().Where(x => x["MaVatLieu"].ToString() == FirstItem["MaVatLieu"].ToString()
                            && x["VatTu"].ToString() == FirstItem["VatTu"].ToString() && x["DonVi"].ToString() == FirstItem["DonVi"].ToString()).ToArray();
                            foreach (var rowct in drs_VTCon)
                            {
                                var cRowHN = HPVTHN.Where(x => x.ParentCode == rowct["Code"].ToString()).ToList();
                                double KLKHHN = (double)cRowHN.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach);
                                TCTU_TDKH New = new TCTU_TDKH();
                                New.ID = Guid.NewGuid().ToString();
                                New.ParentID = rowct["Code"].ToString();
                                New.CodeCT = rowct["Code"].ToString();
                                New.MaHieu = rowct["MaHieuCongTac"].ToString();
                                New.TenCongViec = rowct["TenCongTac"].ToString();
                                DataRow dataRow = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).ToArray().FirstOrDefault();
                                double KL = rowct["KhoiLuongKeHoach"] == DBNull.Value ? 0 : double.Parse(rowct["KhoiLuongKeHoach"].ToString());
                                New.KhoiLuongHopDong = Math.Round(KL, 4);
                                New.KhoiLuongKeHoach = Math.Round(KLKHHN, 4);
                                ThuChiTDKH.Add(New);
                            }
                        }


                    }



                }

            }
            //foreach (DataRow CT in dtCT.Rows)
            //{
            //    string crCodeCT = CT["Code"].ToString();
            //    ThuChiTDKH.Add(new TCTU_TDKH()
            //    {
            //        ParentID = "0",
            //        ID = crCodeCT,
            //        MaHieu = MyConstant.CONST_TYPE_CONGTRINH,
            //        TenCongViec = CT["Ten"].ToString(),
            //    });
            //    foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
            //    {
            //        string crCodeHM = HM["Code"].ToString();
            //        ThuChiTDKH.Add(new TCTU_TDKH()
            //        {
            //            ParentID = crCodeCT,
            //            ID = crCodeHM,
            //            MaHieu = MyConstant.CONST_TYPE_HANGMUC,
            //            TenCongViec = HM["Ten"].ToString(),
            //        });
            //        DataRow[] drs_VT = dtDanhSachVT.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
            //        foreach (var item in drs_VT)
            //        {
            //            DataRow[] drs_VTCon = dtHPVT.AsEnumerable().Where(x => x["MaVatLieu"].ToString() == item["MaVatLieu"].ToString() && x["VatTu"].ToString() == item["VatTu"].ToString()).ToArray();
            //            string[] lstCT = drs_VTCon.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray();
            //            DataRow[] drs_CT = dtCongTacTheoKy.AsEnumerable().Where(x => lstCT.Contains(x["Code"].ToString())).ToArray();
            //            string CodeVT = item["Code"].ToString();
            //            List<KLHN> crRow = dtKLHN.Where(x => x.CodeCha == CodeVT).ToList();
            //            //DataRow[] crRow = dtKLHN.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == item["Code"].ToString() && DateTime.Parse(x["Ngay"].ToString()) >= DateTime.Parse(ngaybatdau) && DateTime.Parse(x["Ngay"].ToString()) <= DateTime.Parse(ngayketthuc)).ToArray();
            //            KLKH = crRow.Any() ? (double)crRow.Where(x=>x.KhoiLuongKeHoach.HasValue).Sum(x=>x.KhoiLuongKeHoach) : 0;
            //            KLHD = item["KhoiLuongHopDong"] == DBNull.Value ? 0 : Math.Round(double.Parse(item["KhoiLuongHopDong"].ToString()), 2);
            //            KLLKYC = 0; KLLKXK = 0; ConLai = 100;
            //            CheckVatLieu = false;
            //            if (M_YeuCau.Rows.Count != 0)
            //            {
            //                DataRow[] crowKHVT = M_YeuCau.AsEnumerable().Where(x => x["CodeKHVT"].ToString() == CodeVT).ToArray();
            //                if (crowKHVT.Count() != 0)
            //                {
            //                    CheckVatLieu = true;
            //                    KLLKYC = crowKHVT.FirstOrDefault()["LuyKeYeuCau"] == DBNull.Value ? 0 : Math.Round(double.Parse(crowKHVT.FirstOrDefault()["LuyKeYeuCau"].ToString()), 2);
            //                    KLLKXK = crowKHVT.FirstOrDefault()["LuyKeXuatKho"] == DBNull.Value ? 0 : Math.Round(double.Parse(crowKHVT.FirstOrDefault()["LuyKeXuatKho"].ToString()), 2);
            //                }
            //            }
            //            ThuChiTDKH.Add(new TCTU_TDKH()
            //            {
            //                STT = stt++,
            //                ParentID = crCodeHM,
            //                ID = CodeVT,
            //                MaHieu = item["MaVatLieu"].ToString(),
            //                TenCongViec = item["VatTu"].ToString(),
            //                DonVi = item["DonVi"].ToString(),
            //                KhoiLuongHopDong = KLHD,
            //                KhoiLuongKeHoach = Math.Round(KLKH, 4),
            //                LuyKeYeuCau = KLLKYC,
            //                LuyKeXuatKho = KLLKXK,
            //                IsVatLieu = CheckVatLieu,
            //                DonGia = item["DonGia"] == DBNull.Value ? 0 : double.Parse(item["DonGia"].ToString()),
            //                NgayBD = dE_Begin.DateTime,
            //                NgayKT = dE_End.DateTime,
            //            });
            //            if (CheckHaoPhi)
            //            {
            //                foreach (var rowct in drs_CT)
            //                {
            //                    string CodeHPVT = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).FirstOrDefault()["Code"].ToString();
            //                    var cRowHN = HPVTHN.Where(x => x.CodeCha == CodeHPVT).ToList();
            //                    double KLKHHN = (double)cRowHN.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach);
            //                    TCTU_TDKH New = new TCTU_TDKH();
            //                    New.ID = Guid.NewGuid().ToString();
            //                    New.ParentID = CodeVT;
            //                    New.CodeCT = rowct["Code"].ToString();
            //                    New.MaHieu = rowct["MaHieuCongTac"].ToString();
            //                    New.TenCongViec = rowct["TenCongTac"].ToString();
            //                    DataRow dataRow = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).ToArray().FirstOrDefault();
            //                    double KL = double.Parse(dataRow[TDKH.COL_KHVT_DinhMucNguoiDung].ToString()) * double.Parse(dataRow[TDKH.COL_KHVT_HeSoNguoiDung].ToString()) * double.Parse(rowct[TDKH.COL_KhoiLuongHopDongChiTiet].ToString());
            //                    New.KhoiLuongHopDong = Math.Round(KL, 4);
            //                    New.KhoiLuongKeHoach = Math.Round(KLKHHN, 4);
            //                    ThuChiTDKH.Add(New);
            //                }
            //            }
            //        }
            //    }
            //}
            tL_KHVT.DataSource = ThuChiTDKH;
            tL_KHVT.ExpandAll();
            tL_KHVT.Refresh();
            tL_KHVT.RefreshDataSource();
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_UpdateHaoPhi()
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích hao phí", "Vui Lòng chờ!");
            List<TCTU_TDKH> ThuChiTDKH = (tL_KHVT.DataSource as List<TCTU_TDKH>);
            TreeListNode[] LstTenVL = tL_KHVT.FindNodes(x => x.Level == 2);
            TreeListNode[] LstCongTac = tL_KHVT.FindNodes(x => x.Level == 3);
            foreach (var item in LstCongTac)
                tL_KHVT.Nodes.Remove(item);
            tL_KHVT.UncheckAll();
            List<DonViThucHien> Infor = DuAnHelper.GetDonViThucHiens();
            DonViThucHien Detail = Infor.FindAll(x => x.Code == (string)lUE_ToChucCaNhan.EditValue).FirstOrDefault();
            string[] lstcode = LstTenVL.AsEnumerable().Select(x => x.GetValue("ID").ToString()).ToArray();

            string dbString = $"SELECT COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac" +
                $",COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi," +
                $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                $"cttk.* FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
                $"ON dmct.Code =cttk.CodeCongTac " +
                $" WHERE cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                $"AND cttk.{Detail.ColCodeFK}='{Detail.Code}' ORDER BY \"Row\" ASC";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string lstCode = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            dbString = $"SELECT * FROM {MyConstant.view_HaoPhiVatTu} WHERE \"CodeCongTac\" IN ({lstCode}) AND \"LoaiVatTu\"='{cBE_LoaiCP.Text}'";
            DataTable dtHPVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lst = dtHPVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            var HPVTHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.HaoPhiVatTu, lst, dE_Begin.DateTime, dE_End.DateTime);
            foreach (TreeListNode item in LstTenVL)
            {
                string MaHieu = item.GetValue("MaHieu").ToString();
                string VatTu = item.GetValue("TenCongViec").ToString();
                string ID= item.GetValue("ID").ToString();
                DataRow[] drs_VTCon = dtHPVT.AsEnumerable().Where(x => x["MaVatLieu"].ToString() == MaHieu && x["VatTu"].ToString() == VatTu).ToArray();
                string[] lstCT = drs_VTCon.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray();
                DataRow[] drs_CT = dtCongTacTheoKy.AsEnumerable().Where(x => lstCT.Contains(x["Code"].ToString())).ToArray();
                foreach (var rowct in drs_CT)
                {
                    string CodeHPVT = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).FirstOrDefault()["Code"].ToString();
                    var cRowHN = HPVTHN.Where(x => x.ParentCode == CodeHPVT).ToList();
                    //DataRow[] cRowHN = HPVTHN.AsEnumerable().Where(x => x["CodeHaoPhiVatTu"].ToString() == CodeHPVT && DateTime.Parse(x["Ngay"].ToString()) >= DateTime.Parse(ngaybatdau) && DateTime.Parse(x["Ngay"].ToString()) <= DateTime.Parse(ngayketthuc)).ToArray();
                    double KLKHHN = (double)cRowHN.Where(x=>x.KhoiLuongKeHoach.HasValue).Sum(x=>x.KhoiLuongKeHoach);
                    TCTU_TDKH New = new TCTU_TDKH();
                    New.ID = Guid.NewGuid().ToString();
                    New.ParentID = ID;
                    New.CodeCT = rowct["Code"].ToString();
                    New.MaHieu = rowct["MaHieuCongTac"].ToString();
                    New.TenCongViec = rowct["TenCongTac"].ToString();
                    DataRow dataRow = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).ToArray().FirstOrDefault();
                    double KL = double.Parse(dataRow[TDKH.COL_KHVT_DinhMucNguoiDung].ToString()) * double.Parse(dataRow[TDKH.COL_KHVT_HeSoNguoiDung].ToString()) * double.Parse(rowct[TDKH.COL_KhoiLuongHopDongChiTiet].ToString());
                    New.KhoiLuongHopDong = Math.Round(KL, 4);
                    New.KhoiLuongKeHoach = Math.Round(KLKHHN, 4);
                    ThuChiTDKH.Add(New);
                }
            }
            tL_KHVT.Refresh();
            tL_KHVT.RefreshDataSource();
            tL_KHVT.ExpandAll();
            WaitFormHelper.CloseWaitForm();
        }
        private void sB_Ok_Click(object sender, EventArgs e)
        {
                if (tL_KHVT.DataSource == null)
                return;
            List<TCTU_TDKH> ThuChiTDKH = (tL_KHVT.DataSource as List<TCTU_TDKH>);
            List<TCTU_TDKH> lstAdd = new List<TCTU_TDKH>();
            TreeListNode[] LstTenVL = tL_KHVT.FindNodes(x => x.Level == 2&&x.Visible);
            string[] lstcode = LstTenVL.AsEnumerable().Select(x => x.GetValue("ID").ToString()).ToArray();
            if (CheckHaoPhi)
            {
                foreach (string item in lstcode)
                {
                    List<TCTU_TDKH> HD = ThuChiTDKH.FindAll(x => x.Chon == true && x.ParentID == item);
                    TCTU_TDKH TenVatTu = ThuChiTDKH.FindAll(x => x.ID == item).FirstOrDefault();
                    if (HD.Count == 0)
                        continue;
                    double KLHD = HD.Sum(x => x.KhoiLuongHopDong);
                    double KLKH = HD.Sum(x => x.KhoiLuongKeHoach);
                    TenVatTu.KhoiLuongHopDong = KLHD;
                    TenVatTu.KhoiLuongKeHoach = KLKH;
                    lstAdd.Add(TenVatTu);
                    //lstAdd.AddRange(HD);
                }
                if (lstAdd.Count == 0)
                {
                    MessageShower.ShowWarning("Vui lòng chọn ít nhất một vật liệu!");
                    return;
                }
            }
            else
            {
                lstAdd = ThuChiTDKH.FindAll(x => x.Chon == true && lstcode.Contains(x.ID)).ToList();
            }
            List<DonViThucHien> Infor = lUE_ToChucCaNhan.Properties.DataSource as List<DonViThucHien>;
            string CodeDVTH = lUE_ToChucCaNhan.EditValue.ToString();
            DonViThucHien DVTH = Infor.Find(x => x.Code == CodeDVTH);
            if(DVTH.IsGiaoThau)
                DVTH= Infor.Find(x => x.LoaiDVTH==MyConstant.LoaiDVTH_TuThucHien);
            m__TRUYENDATA(lstAdd, DVTH);
            this.Close();
        }

        private void sB_Update_Click(object sender, EventArgs e)
        {
        }

        private void sB_All_Click(object sender, EventArgs e)
        {
            tL_KHVT.CheckAll();
            //TreeListNode [] lst = tL_KHVT.FindNodes(x => x.Level == 3&&x.Visible==true);
            //if (lst.Count() == 0)
            //    return;
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", true);
        }

        private void sB_Cancel_Click(object sender, EventArgs e)
        {
            tL_KHVT.UncheckAll();
            //TreeListNode[] lst = tL_KHVT.FindNodes(x => x.Level == 3 && x.Visible == true);
            //if (lst.Count() == 0)
            //    return;
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", false);
        }

        private void lUE_ToChucCaNhan_EditValueChanged(object sender, EventArgs e)
        {
            //Fcn_LayData();
            Test();
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
            else if (e.Node.Level == 3)
            {
                //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Purple;
            }
            else
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Black;
                TCTU_TDKH task = tL_KHVT.GetRow(e.Node.Id) as TCTU_TDKH;
                if (task == null)
                    return;
                if (task.ConLai >= 90)
                {
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void tL_KHVT_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level <= 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tL_KHVT_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level < 2)
                e.CanFocus = false;
        }

        private void dE_Begin_EditValueChanged(object sender, EventArgs e)
        {
            //Fcn_LayData();
            Test();
        }

        private void dE_End_EditValueChanged(object sender, EventArgs e)
        {
            //Fcn_LayData();
            Test();
        }

        private void ce_Loc_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_Loc.Checked)
                tL_KHVT.ActiveFilterString = "[KhoiLuongKeHoach]>0";
            else
                tL_KHVT.ClearColumnFilter(tL_KHVT.Columns["KhoiLuongKeHoach"]);
        }

        private void ce_HaoPhi_CheckedChanged(object sender, EventArgs e)
        {
            if(ce_HaoPhi.Checked)
                Fcn_UpdateHaoPhi();
            else
            {
                WaitFormHelper.ShowWaitForm("Đang phân tích hao phí", "Vui Lòng chờ!");
                TreeListNode[] LstCongTac = tL_KHVT.FindNodes(x => x.Level == 3);
                foreach (var item in LstCongTac)
                    tL_KHVT.Nodes.Remove(item);
                tL_KHVT.UncheckAll();
                WaitFormHelper.CloseWaitForm();
            }
        }

        private void slue_CongTrinhHangMuc_EditValueChanged(object sender, EventArgs e)
        {
            Test();
        }
    }
}
