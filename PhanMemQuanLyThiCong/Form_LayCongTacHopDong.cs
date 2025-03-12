using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.HopDong;
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
    public partial class Form_LayCongTacHopDong : DevExpress.XtraEditors.XtraForm
    {
        public static int m_ps;
        List<string> database = new List<string>();
        public static Dictionary<string, string> m_dic_DOBOC = new Dictionary<string, string>();
        public static bool m_check=false;
        bool m_Vl;
        Dictionary<string, string> m_DVTH = new Dictionary<string, string>();
        public delegate void DE__TRUYENDATA(List<LayCongTacHopDong> Lst, int ps, bool KeHoach,bool IsDonGiaKeHoach);
        public DE__TRUYENDATA m_TruyenData;
        public Form_LayCongTacHopDong()
        {
            InitializeComponent();
            database = new List<string>()
            {
                "STT",  
                "ID",
                "MaHieu",
                "TenCongViec",
                "DonVi",
                "NgayKT",
                "NgayBD"
            };
        }

        private void Form_LayCongTacHopDong_Load(object sender, EventArgs e)
        {
            ctrl_DonViThucHienDuAn.DVTHChanged -= ctrl_DonViThucHienDuAn_DVTHChanged;
            DuAnHelper.GetDonViThucHiens(ctrl_DonViThucHienDuAn);
            ctrl_DonViThucHienDuAn.DVTHChanged += ctrl_DonViThucHienDuAn_DVTHChanged;
            fcn_checkDVTH();
        }
        private void fcn_checkDVTH()
        {
            ctrl_DonViThucHienDuAn.DVTHChanged -= ctrl_DonViThucHienDuAn_DVTHChanged;
            List<DonViThucHien> DVTH = ctrl_DonViThucHienDuAn.DataSource as List<DonViThucHien>;
            foreach (DonViThucHien item in DVTH)
            {
                if (m_DVTH.Keys.Contains(item.Table) && m_DVTH.Values.Contains(item.Code)/*item.LoaiDVTH!="Tự thực hiện"*/)
                {
                    if (item.IsGiaoThau)
                        rg_DonGia.SelectedIndex = 0;
                    ctrl_DonViThucHienDuAn.FcnAcceptchecked();
                    ctrl_DonViThucHienDuAn.EditValue = item.CodeFk;
                    fcn_updatedata();
                    m_Vl = false;
                }
            }
            ctrl_DonViThucHienDuAn.DVTHChanged += ctrl_DonViThucHienDuAn_DVTHChanged;
            //Fcn_capnhapsolieu();
            Fcn_CapNhapSoLieuNhomTuyen();
        }
        private void fcn_updatedata()
        {
            DonViThucHien DVTH = (ctrl_DonViThucHienDuAn.DataSource as List<DonViThucHien>).FindAll(x => m_DVTH.Keys.Contains(x.Table) && x.LoaiDVTH != "Tự thực hiện").FirstOrDefault();
            if (m_ps != 0)
                ce_PhatSinh.Checked = true;
        }
        private void Fcn_CapNhapSoLieuNhomTuyen()
        {

            string codeps = "";
            if (m_ps != 0)
            {
                string queryStr = $"SELECT \"Code\"  FROM {TDKH.TBL_SoLanPhatSinh} WHERE \"Ten\"='Phát sinh lần {m_ps}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                if (dt.Rows.Count == 0)
                {
                    MessageShower.ShowError($"Công trình mới chỉ có công tác thực hiện, chưa có khối lượng phát sinh lần {m_ps}. Bạn sang bảng Kế hoạch tiến độ chọn Thêm phát sinh để có khối lượng tạo phụ lục");
                    this.Close();
                    return;
                }
                codeps = dt.Rows[0][0].ToString();
            }
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            string dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_PhuLuc}";
            DataTable dtHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lstCodeHD = MyFunction.fcn_Array2listQueryCondition(dtHD.AsEnumerable().Select(x => x["CodeCongTacTheoGiaiDoan"].ToString()).ToArray());
            string CodeNhomPL = MyFunction.fcn_Array2listQueryCondition(dtHD.AsEnumerable().Select(x => x["CodeNhom"].ToString()).ToArray());
            dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeNhom\" IN ({CodeNhomPL})";
            dtHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lstCodecon = MyFunction.fcn_Array2listQueryCondition(dtHD.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            if (lstCodecon != "")
                lstCodeHD = lstCodeHD == "" ? lstCodecon : $"{lstCodeHD},{lstCodecon}";
            string condition = DVTH.IsGiaoThau ? "AND dmct.HasHopDongAB='1'" : "";
            List<LayCongTacHopDong> CTHD = new List<LayCongTacHopDong>();
            dbString = $"SELECT nct.Ten as TenNhom,nct.DonVi as DonViNhom," +
                  $"COALESCE(cttk.CodePhanTuyen, Tuyen.Code) AS CodePhanTuyen,COALESCE(TuyenCT.Ten, Tuyen.Ten) AS TenTuyen,nct.DonGia as DonGiaNhom,nct.DonGiaThiCong as DonGiaNhomThiCong,nct.KhoiLuongKeHoach as KhoiLuongNhom," +
                  $"nct.NgayBatDau as NgayBatDauNhom,nct.NgayKetThuc as NgayKetThucNhom,nct.KhoiLuongHopDongChiTiet as KhoiLuongHopDongChiTietNhom," +
                  $" COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
                  $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
                  $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi," +
                  $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
                   $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                   $"COALESCE(cttk.KyHieuBanVe, dmct.KyHieuBanVe) AS KyHieuBanVe, " +
                   $"COALESCE(cttk.KhoiLuongHopDongToanDuAn, dmct.KhoiLuongHopDongToanDuAn) AS KhoiLuongHopDongToanDuAn, " +
                   $"COALESCE(cttk.PhatSinh, dmct.PhatSinh) AS PhatSinh, " +
                   $"COALESCE(cttk.HasHopDongAB, dmct.HasHopDongAB) AS HasHopDongAB, " +
                   $"COALESCE(cttk.PhanTichVatTu, dmct.PhanTichVatTu) AS PhanTichVatTu,cttk.*, " +
                  $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn \r\n" +
                  $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                  $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                  $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                  $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                  $"ON cttk.CodeNhom = nct.Code \r\n" +
                  $"LEFT JOIN {TDKH.Tbl_PhanTuyen} Tuyen\r\n" +
                  $"ON dmct.CodePhanTuyen = Tuyen.Code " +
                  $"LEFT JOIN Tbl_TDKH_PhanTuyen TuyenCT  " +
                  $"ON cttk.CodePhanTuyen = TuyenCT.Code  \r\n" +
                  $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                  $"ON dmct.CodeHangMuc = hm.Code \r\n" +
                  $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                  $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
                  $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                  $"ON ctrinh.CodeDuAn = da.Code " +
                  $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
                  $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code" +
                  $" LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code \r\n" +
                  $"WHERE da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' \r\n" +
                  $"AND cttk.{DVTH.ColCodeFK}='{DVTH.Code}' AND cttk.Code NOT IN ({lstCodeHD}) {condition} " +
                  $"ORDER BY ctrinh.SortId ASC, hm.SortId ASC, cttk.SortId ASC\r\n";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dtCongTacTheoKy.AddIndPhanTuyenNhom();
            var grCongTrinh = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodeCongTrinh"].ToString());
            double KLKH = 0, TT = 0;
            if (dtCongTacTheoKy.Rows.Count != 0 && !m_check)
            {
                DateTime Max = dtCongTacTheoKy.AsEnumerable().Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                DateTime Min = dtCongTacTheoKy.AsEnumerable().Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                de_begin.EditValueChanged -= de_begin_EditValueChanged;
                de_end.EditValueChanged -= de_end_EditValueChanged;
                de_begin.EditValue = Min;
                de_end.EditValue = Max;
                de_begin.EditValueChanged += de_begin_EditValueChanged;
                de_end.EditValueChanged += de_end_EditValueChanged;
                m_check = true;
            }
            int stt = 1;
            string ngaybatdau = de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string ngayketthuc = de_end.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            List<KLHN> dtKLHN = new List<KLHN>();
            if (rg_Select.SelectedIndex == 1)
            {
                string[] lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                dtKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac, dateBD: de_begin.DateTime, dateKT: de_end.DateTime);
            }
            foreach (var Ctrinh in grCongTrinh)
            {
                string crCodeCT = Ctrinh.Key;
                CTHD.Add(new LayCongTacHopDong()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaHieu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenCongViec = Ctrinh.FirstOrDefault()["TenCongTrinh"].ToString(),
                });
                var grHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"].ToString());
                foreach (var HM in grHangMuc)
                {
                    string crCodeHM = HM.Key;
                    CTHD.Add(new LayCongTacHopDong()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaHieu = MyConstant.CONST_TYPE_HANGMUC,
                        TenCongViec = HM.FirstOrDefault()["TenHangMuc"].ToString(),
                    });
                    var grPhanTuyen = HM.GroupBy(x => (int)x["IndPT"])
                    .OrderBy(x => x.Key);
                    foreach (var Tuyen in grPhanTuyen)
                    {
                        var fstTuyen = Tuyen.First();
                        string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : $"{fstTuyen["CodePhanTuyen"]}_CodeTuyen";
                        if (fstTuyen["CodePhanTuyen"] != DBNull.Value)
                        {
                            CTHD.Add(new LayCongTacHopDong()
                            {
                                ParentID = crCodeHM,
                                ID = crCodeTuyen,
                                MaHieu = MyConstant.CONST_TYPE_PhanTuyen,
                                TenCongViec = fstTuyen["TenTuyen"].ToString(),
                            });
                        }
                        var grTuyenNhom = Tuyen.GroupBy(x => (int)x["IndNhom"])
                                .OrderBy(x => x.Key);
                        foreach (var NhomTuyen in grTuyenNhom)
                        {
                            var fstNhom = NhomTuyen.First();

                            string crCodeNhom = (fstNhom["CodeNhom"] == DBNull.Value) ? null : fstNhom["CodeNhom"].ToString();
                            if (rg_Select.SelectedIndex == 1)
                            {
                                var KLHNNhom = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.Nhom, new string[] { crCodeNhom }, dateBD: de_begin.DateTime, dateKT: de_end.DateTime);
                                KLKH = KLHNNhom.Any() ? (double)KLHNNhom.Sum(x => x.KhoiLuongKeHoach) : 0;

                            }
                            if (fstNhom["CodeNhom"] != DBNull.Value)
                            {
                                if (CodeNhomPL.Contains(crCodeNhom))
                                    continue;
                                CTHD.Add(new LayCongTacHopDong()
                                {
                                    ParentID = crCodeTuyen ?? crCodeHM,
                                    ID = crCodeNhom,
                                    CodeCT = crCodeCT,
                                    MaHieu = MyConstant.CONST_TYPE_NHOM,
                                    TenCongViec = fstNhom["TenNhom"].ToString(),
                                    DonVi = fstNhom["DonViNhom"].ToString(),
                                    KhoiLuongHopDong = fstNhom["KhoiLuongNhom"] == DBNull.Value ? 0 : double.Parse(fstNhom["KhoiLuongNhom"].ToString()),
                                    KhoiLuongKeHoach = Math.Round(KLKH, 4),
                                    DonGiaKeHoach = fstNhom["DonGiaNhom"] == DBNull.Value ? 0 : double.Parse(fstNhom["DonGiaNhom"].ToString()),
                                    DonGiaThiCong = fstNhom["DonGiaNhomThiCong"] == DBNull.Value ? 0 : double.Parse(fstNhom["DonGiaNhomThiCong"].ToString()),
                                    NgayBD = de_begin.DateTime,
                                    NgayKT = de_end.DateTime,
                                });
                            }
                            var grCongTacTuyen = NhomTuyen.GroupBy(x => x["Code"].ToString());

                            foreach (var CongTac in grCongTacTuyen)
                            {
                                var CTac = CongTac.FirstOrDefault();
                                if (rg_Select.SelectedIndex == 1)
                                {
                                    KLKH = dtKLHN.Where(x => x.ParentCode == CongTac.Key).Any() ? (double)dtKLHN.Where(x => x.ParentCode == CongTac.Key).Sum(x => x.KhoiLuongKeHoach) : 0;
                                }
                                if (m_ps == 0)
                                {
                                    CTHD.Add(new LayCongTacHopDong
                                    {
                                        STT = stt++,
                                        ParentID = crCodeNhom ?? crCodeTuyen ?? crCodeHM,
                                        CodeCT = crCodeCT,
                                        ID = CongTac.Key,
                                        MaHieu = CTac["MaHieuCongTac"].ToString(),
                                        TenCongViec = CTac["TenCongTac"].ToString(),
                                        DonVi = CTac["DonVi"].ToString(),
                                        KhoiLuongHopDong = CTac["KhoiLuongHopDongChiTiet"] == DBNull.Value ? 0 : double.Parse(CTac["KhoiLuongHopDongChiTiet"].ToString()),
                                        KhoiLuongKeHoach = Math.Round(KLKH, 4),
                                        DonGiaKeHoach = CTac["DonGia"] == DBNull.Value ? 0 : double.Parse(CTac["DonGia"].ToString()),
                                        DonGiaThiCong = CTac["DonGiaThiCong"] == DBNull.Value ? 0 : double.Parse(CTac["DonGiaThiCong"].ToString()),
                                        NgayBD = de_begin.DateTime,
                                        NgayKT = de_end.DateTime,
                                    });
                                }
                                //else
                                //{
                                //    CTHD.Add(new LayCongTacHopDong
                                //    {
                                //        STT = stt++,
                                //        ParentID = crCodeHM,
                                //        CodeCT = crCodeCT,
                                //        ID = rowct["Code"].ToString(),
                                //        MaHieu = rowct["MaHieuCongTac"].ToString(),
                                //        TenCongViec = rowct["TenCongTac"].ToString(),
                                //        DonVi = rowct["DonVi"].ToString(),
                                //        KhoiLuongHopDong = rowct["KhoiLuongToanBo"] == DBNull.Value ? 0 : double.Parse(rowct["KhoiLuongToanBo"].ToString()),
                                //        KhoiLuongKeHoach = Math.Round(KLKH, 4),
                                //        DonGiaKeHoach = rowct["DonGia"] == DBNull.Value ? 0 : long.Parse(rowct["DonGia"].ToString()),
                                //        DonGiaThiCong = rowct["DonGiaThiCong"] == DBNull.Value ? 0 : long.Parse(rowct["DonGiaThiCong"].ToString()),
                                //        NgayBD = de_begin.DateTime,
                                //        NgayKT = de_end.DateTime,
                                //    });
                                //}


                            }


                        }

                    }


                }

            }
            TL_HopDong.DataSource = CTHD;
            TL_HopDong.Refresh();
            TL_HopDong.RefreshDataSource();
            TL_HopDong.ExpandAll();
            WaitFormHelper.CloseWaitForm();
            if (dtCongTacTheoKy.Rows.Count == 0)
            {
                MessageShower.ShowWarning("Đơn vị thực hiện chưa có công tác!", "Cảnh báo");
            }
        }
        private void Fcn_capnhapsolieu()
        {
            string codeps = "";
            if (m_ps != 0)
            {
                string queryStr = $"SELECT \"Code\"  FROM {TDKH.TBL_SoLanPhatSinh} WHERE \"Ten\"='Phát sinh lần {m_ps}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                if (dt.Rows.Count == 0)
                {
                    MessageShower.ShowError($"Công trình mới chỉ có công tác thực hiện, chưa có khối lượng phát sinh lần {m_ps}. Bạn sang bảng Kế hoạch tiến độ chọn Thêm phát sinh để có khối lượng tạo phụ lục");
                    this.Close();
                    return;
                }
                codeps = dt.Rows[0][0].ToString();
            }

            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            DataTable dtCT, dtHM, dtCongTacPS;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            
            List<LayCongTacHopDong> CTHD = new List<LayCongTacHopDong>();
            string dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +          
                $"LEFT JOIN {TDKH.TBL_NhomCongTac} " +
                $"ON {TDKH.TBL_NhomCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhom " +
                $" WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'" +
                $" AND {TDKH.TBL_ChiTietCongTacTheoKy}.{DVTH.ColCodeFK}='{DVTH.Code}' ORDER BY \"SortId\" ASC";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"SELECT * FROM {MyConstant.TBL_HopDong_PhuLuc}";
            DataTable dtHD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lstCodeHD = dtHD.AsEnumerable().Select(x => x["CodeCongTacTheoGiaiDoan"].ToString()).ToArray();
            if (dtCongTacTheoKy.Rows.Count != 0&&!m_check)
            {
                DateTime Max = dtCongTacTheoKy.AsEnumerable().Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                DateTime Min = dtCongTacTheoKy.AsEnumerable().Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                de_begin.EditValueChanged -= de_begin_EditValueChanged;
                de_end.EditValueChanged -= de_end_EditValueChanged;
                de_begin.EditValue = Min;
                de_end.EditValue = Max;
                de_begin.EditValueChanged += de_begin_EditValueChanged;
                de_end.EditValueChanged += de_end_EditValueChanged;
                m_check = true;
            }
            int stt = 1;
            string ngaybatdau = de_begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string ngayketthuc = de_end.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            if (m_ps != 0)
            {
                dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
    $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
    $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
    $" WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodePhatSinh='{codeps}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.{DVTH.ColCodeFK}='{DVTH.Code}' ORDER BY \"Row\" ASC";

            }
            dtCongTacPS = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
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
                    DataRow[] drs_ct;
                    if (m_ps == 0)
                        drs_ct = dtCongTacTheoKy.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM&&!lstCodeHD.Contains(x["Code"].ToString())).ToArray();
                    else
                        drs_ct = dtCongTacPS.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM&&!lstCodeHD.Contains(x["Code"].ToString())).ToArray();
                    if (drs_ct.Count() == 0)
                        continue;
                    string [] lsCodeCongTac = drs_ct.AsEnumerable().Select(x =>x["Code"].ToString()).ToArray();
                    var dtKLHN = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);
                    foreach (var rowct in drs_ct)
                    {
                        double KLKH = 0, TT = 0;
                        var crRow = dtKLHN.Where(x => x.CodeCongTacTheoGiaiDoan == rowct["Code"].ToString() && x.Ngay >= DateTime.Parse(ngaybatdau) && x.Ngay <= DateTime.Parse(ngayketthuc)).ToArray();
                        foreach (var item in crRow)
                        {
                            KLKH += item.KhoiLuongKeHoach ?? 0;
                        }
                        if (m_ps == 0)
                        {
                            CTHD.Add(new LayCongTacHopDong
                            {
                                STT = stt++,
                                ParentID = crCodeHM,
                                CodeCT = crCodeCT,
                                ID = rowct["Code"].ToString(),
                                MaHieu = rowct["MaHieuCongTac"].ToString(),
                                TenCongViec = rowct["TenCongTac"].ToString(),
                                DonVi = rowct["DonVi"].ToString(),
                                KhoiLuongHopDong = rowct["KhoiLuongHopDongChiTiet"] == DBNull.Value ? 0 : double.Parse(rowct["KhoiLuongHopDongChiTiet"].ToString()),
                                KhoiLuongKeHoach = Math.Round(KLKH, 4),
                                DonGiaKeHoach = rowct["DonGia"] == DBNull.Value ? 0 : long.Parse(rowct["DonGia"].ToString()),
                                DonGiaThiCong = rowct["DonGiaThiCong"] == DBNull.Value ? 0 : long.Parse(rowct["DonGiaThiCong"].ToString()),
                                NgayBD = de_begin.DateTime,
                                NgayKT = de_end.DateTime,
                            });
                        }
                        else
                        {
                            CTHD.Add(new LayCongTacHopDong
                            {
                                STT = stt++,
                                ParentID = crCodeHM,
                                CodeCT = crCodeCT,
                                ID = rowct["Code"].ToString(),
                                MaHieu = rowct["MaHieuCongTac"].ToString(),
                                TenCongViec = rowct["TenCongTac"].ToString(),
                                DonVi = rowct["DonVi"].ToString(),
                                KhoiLuongHopDong = rowct["KhoiLuongToanBo"] == DBNull.Value ? 0 : double.Parse(rowct["KhoiLuongToanBo"].ToString()),
                                KhoiLuongKeHoach = Math.Round(KLKH, 4),
                                DonGiaKeHoach = rowct["DonGia"] == DBNull.Value ? 0 : long.Parse(rowct["DonGia"].ToString()),
                                DonGiaThiCong = rowct["DonGiaThiCong"] == DBNull.Value ? 0 : long.Parse(rowct["DonGiaThiCong"].ToString()),
                                NgayBD = de_begin.DateTime,
                                NgayKT = de_end.DateTime,
                            });
                        }
                    }
                }
            }
            TL_HopDong.DataSource = CTHD;
            TL_HopDong.Refresh();
            TL_HopDong.RefreshDataSource(); 
            TL_HopDong.ExpandAll();
            WaitFormHelper.CloseWaitForm();
            if (dtCongTacTheoKy.Rows.Count == 0)
            {
                MessageShower.ShowWarning("Đơn vị thực hiện chưa có công tác!", "Cảnh báo");
                this.Close();
            }

        }
        public void Fcn_LoadData(int ps, Dictionary<string, string> DVTH)
        {
            //m_dic_DOBOC = dic_DOBOC;
            m_DVTH = DVTH;
            m_Vl = true;
            m_ps = ps;
            m_check = false;
        }
        string check_kl, dg;
        static string kl;

        private void sb_All_Click(object sender, EventArgs e)
        {
            //TreeListNode[] lst = TL_HopDong.FindNodes(x => x.Level == 2);
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", true);
            TL_HopDong.CheckAll();
            TL_HopDong.Refresh();
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            //TreeListNode[] lst = TL_HopDong.FindNodes(x => x.Level == 2);
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", false);
            TL_HopDong.UncheckAll();
            TL_HopDong.Refresh();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            List<LayCongTacHopDong> lst = TL_HopDong.DataSource as List<LayCongTacHopDong>;
            if (lst.Count == 0)
                return;
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu", "Vui Lòng chờ!");
            if (ce_LayNhom.Checked)
            {
                List<LayCongTacHopDong> Nhom = lst.Where(x => x.MaHieu == "*" && x.Chon == true).ToList();
                if (Nhom.Count() != 0)
                {
                    string[] CodeNhom = Nhom.Select(x => x.ID).ToArray();
                    lst.RemoveAll(x => CodeNhom.Contains(x.ParentID));
                }
                lst.RemoveAll(x => x.Chon == false || x.MaHieu == "*T" || x.MaHieu == MyConstant.CONST_TYPE_HANGMUC || x.MaHieu == MyConstant.CONST_TYPE_CONGTRINH);
                bool KeHoach = rg_Select.SelectedIndex == 0 ? false : true;
                bool IsDonGiaKeHoach = rg_DonGia.SelectedIndex == 0 ? true : false;
                m_TruyenData(lst, m_ps, KeHoach, IsDonGiaKeHoach);
                this.Close();
            }
            else
            {
                lst.RemoveAll(x => x.Chon == false || x.MaHieu == "*T" || x.MaHieu == "*" || x.MaHieu == MyConstant.CONST_TYPE_HANGMUC || x.MaHieu == MyConstant.CONST_TYPE_CONGTRINH);
                bool KeHoach = rg_Select.SelectedIndex == 0 ? false : true;
                bool IsDonGiaKeHoach = rg_DonGia.SelectedIndex == 0 ? true : false;
                m_TruyenData(lst, m_ps, KeHoach, IsDonGiaKeHoach);
                this.Close();
            }
            WaitFormHelper.CloseWaitForm();
        }

        private void rg_Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            if (edit.SelectedIndex == 1)
            {
                de_begin.Enabled = true;
                de_end.Enabled = true;
                Fcn_CapNhapSoLieuNhomTuyen();
            }
            else
            {
                de_begin.Enabled = false;
                de_end.Enabled = false;
            }
        }

        private void TL_HopDong_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
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

        private void TL_HopDong_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
            }
            else
            {
                if (e.Node.GetValue("MaHieu") == null)
                    return;
                if (e.Node.GetValue("MaHieu").ToString() == MyConstant.CONST_TYPE_PhanTuyen)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (e.Node.GetValue("MaHieu").ToString() == MyConstant.CONST_TYPE_NHOM)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = MyConstant.color_Row_NhomCongTac;
                }
            }
        }

        private void TL_HopDong_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level < 2)
                e.CanFocus = false;
        }

        private void de_begin_EditValueChanged(object sender, EventArgs e)
        {
            //Fcn_capnhapsolieu();
            Fcn_CapNhapSoLieuNhomTuyen();

        }

        private void de_end_EditValueChanged(object sender, EventArgs e)
        {
            //Fcn_capnhapsolieu();
            Fcn_CapNhapSoLieuNhomTuyen();
        }

        private void ce_LayTheoNhom_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ctrl_DonViThucHienDuAn_Load(object sender, EventArgs e)
        {
            //Fcn_capnhapsolieu();
        }

        private void ctrl_DonViThucHienDuAn_DVTHChanged(object sender, EventArgs e)
        {
            Fcn_CapNhapSoLieuNhomTuyen();

        }
    }
}
