using DevExpress.XtraEditors;
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
    public partial class FormLayVatLieuHopDong : DevExpress.XtraEditors.XtraForm
    {
        public static int m_ps;
        List<string> database = new List<string>();
        public static Dictionary<string, string> m_dic_DOBOC = new Dictionary<string, string>();
        public static bool m_check=false;
        bool m_Vl;
        Dictionary<string, string> m_DVTH = new Dictionary<string, string>();
        public delegate void DE__TRUYENDATA(List<LayCongTacHopDong> HD, bool KeHoach, bool IsDonGiaKeHoach);
        public DE__TRUYENDATA m_TruyenData;
        public string LoaiVatLieu { get; set; }
        public bool CheckHaoPhi { get { return ce_haophi.Checked; } }
        public FormLayVatLieuHopDong()
        {
            InitializeComponent();
        }

        private void FormLayVatLieuHopDong_Load(object sender, EventArgs e)
        {
            ctrl_DonViThucHienDuAn.DVTHChanged -= ctrl_DonViThucHienDuAn_DVTHChanged;
            DuAnHelper.GetDonViThucHiens(ctrl_DonViThucHienDuAn);
            ctrl_DonViThucHienDuAn.DVTHChanged += ctrl_DonViThucHienDuAn_DVTHChanged;
            fcn_checkDVTH();

        }
        public void Fcn_LoadData(int ps, Dictionary<string, string> DVTH)
        {
            //m_dic_DOBOC = dic_DOBOC;
            database = new List<string>()
            {
                "ID",
                "MaHieu",
                "TenCongViec",
                "DonGiaKeHoach",
                "DonVi",
                "NgayKT",
                "NgayBD"
            };
            m_DVTH = DVTH;
            m_Vl = true;
            m_ps = ps;
            m_check = false;
        }
        private void fcn_checkDVTH()
        {
            ctrl_DonViThucHienDuAn.DVTHChanged -= ctrl_DonViThucHienDuAn_DVTHChanged;
            List<DonViThucHien> DVTH = ctrl_DonViThucHienDuAn.DataSource as List<DonViThucHien>;
            foreach (DonViThucHien item in DVTH)
            {
                if (m_DVTH.Keys.Contains(item.Table) && m_DVTH.Values.Contains(item.Code)/*item.LoaiDVTH!="Tự thực hiện"*/)
                {
                    ctrl_DonViThucHienDuAn.FcnAcceptchecked();
                    ctrl_DonViThucHienDuAn.EditValue = item.CodeFk;
                    //fcn_updatedata();
                    m_Vl = false;
                }
            }
            ctrl_DonViThucHienDuAn.DVTHChanged += ctrl_DonViThucHienDuAn_DVTHChanged;
            //Fcn_capnhapsolieu();
            Fcn_CapNhapVatTu();
        }

        private void Fcn_capnhapsolieu()
        {
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            if (DVTH is null)
                return;
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            DataTable dtCT, dtHM, dtCongTacPS;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            List<LayCongTacHopDong> CTHD = new List<LayCongTacHopDong>();
            string dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                $" WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.{DVTH.ColCodeFK}='{DVTH.Code}' ORDER BY \"Row\" ASC";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lstCode = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            dbString = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu} WHERE \"CodeCongTac\" IN ({lstCode}) AND \"LoaiVatTu\"='{LoaiVatLieu}'";
            DataTable dtHPVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);      
            dbString = $"SELECT * FROM {TDKH.TBL_KHVT_VatTu} WHERE {DVTH.ColCodeFK}='{DVTH.Code}' AND \"LoaiVatTu\"='{LoaiVatLieu}'";
            DataTable dtDanhSachVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lstCodeVL = dtDanhSachVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();

            if (dtDanhSachVT.Rows.Count != 0&& !m_check)
            {
                DateTime Max = dtDanhSachVT.AsEnumerable().Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                DateTime Min = dtDanhSachVT.AsEnumerable().Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                de_begin.EditValue = Min;
                de_end.EditValue = Max;
                m_check = true;
            }
            int stt = 1;
            
            string[] lst = dtHPVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            var dtKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.VatLieu, lstCodeVL, de_begin.DateTime, de_end.DateTime);
            var HPVTHN =MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.HaoPhiVatTu, lst, de_begin.DateTime, de_end.DateTime);
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
                    DataRow[] drs_VT= dtDanhSachVT.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    foreach(var item in drs_VT)
                    {
                        DataRow [] drs_VTCon= dtHPVT.AsEnumerable().Where(x => x["MaVatLieu"].ToString() == item["MaVatLieu"].ToString()&& x["VatTu"].ToString() == item["VatTu"].ToString()).ToArray();
                        string[] lstCT = drs_VTCon.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray();
                        DataRow[] drs_CT = dtCongTacTheoKy.AsEnumerable().Where(x => lstCT.Contains(x["Code"].ToString())).ToArray();
                        string CodeVT = item["Code"].ToString();
                        List<KLHN> crRow = dtKLHN.Where(x => x.ParentCode == CodeVT).ToList();
                        double KLKH = crRow.Any() ? (double)crRow.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach) : 0;
                        CTHD.Add(new LayCongTacHopDong
                        {
                            STT = stt++,
                            ParentID = crCodeHM,
                            ID = CodeVT,
                            MaHieu = item["MaVatLieu"].ToString(),
                            TenCongViec = item["VatTu"].ToString(),
                            DonVi = item["DonVi"].ToString(),
                            KhoiLuongHopDong = item["KhoiLuongHopDong"] == DBNull.Value ? 0 :Math.Round(double.Parse(item["KhoiLuongHopDong"].ToString()),2),
                            KhoiLuongKeHoach=Math.Round(KLKH, 4),
                            DonGiaKeHoach = item["DonGiaThiCong"] == DBNull.Value ? 0 : double.Parse(item["DonGiaThiCong"].ToString()),
                            NgayBD = de_begin.DateTime,
                            NgayKT = de_end.DateTime,
                        });
                        foreach (var rowct in drs_CT)
                        {
                            string CodeHPVT = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).FirstOrDefault()["Code"].ToString();
                            var cRowHN = HPVTHN.Where(x => x.ParentCode == CodeHPVT).ToList();
                            double KLKHHN = (double)cRowHN.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach);
                            LayCongTacHopDong New = new LayCongTacHopDong();
                            New.ID = Guid.NewGuid().ToString();
                            New.ParentID = CodeVT;
                            New.CodeCT = rowct["Code"].ToString();
                            New.MaHieu = rowct["MaHieuCongTac"].ToString();
                            New.TenCongViec = rowct["TenCongTac"].ToString();
                            DataRow dataRow = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).ToArray().FirstOrDefault();
                            double KL = double.Parse(dataRow[TDKH.COL_KHVT_DinhMucNguoiDung].ToString()) * double.Parse(dataRow[TDKH.COL_KHVT_HeSoNguoiDung].ToString()) * double.Parse(rowct[TDKH.COL_KhoiLuongHopDongChiTiet].ToString());
                            New.KhoiLuongHopDong = Math.Round(KL, 4);
                            New.KhoiLuongKeHoach = Math.Round(KLKHHN, 4);
                            CTHD.Add(New);
                        }

                    }
                }
            }
            TL_HopDong.DataSource = CTHD;
            TL_HopDong.Refresh();
            TL_HopDong.RefreshDataSource();
            TL_HopDong.ExpandAll();
            WaitFormHelper.CloseWaitForm();
        }    
        private void Fcn_CapNhapVatTu()
        {
            DonViThucHien DVTH = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
            if (DVTH is null)
                return;
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            DataTable dtCT, dtHM, dtCongTacPS;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            List<LayCongTacHopDong> CTHD = new List<LayCongTacHopDong>();
            string dbString = $"SELECT COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
                $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi," +
                $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                $"cttk.* FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
                $"ON dmct.Code = cttk.CodeCongTac " +
                $" WHERE cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                $"AND cttk.{DVTH.ColCodeFK}='{DVTH.Code}' ORDER BY \"Row\" ASC";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //string lstCode = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());   
            dbString = $"SELECT * FROM {TDKH.TBL_KHVT_VatTu} WHERE {DVTH.ColCodeFK}='{DVTH.Code}' AND \"LoaiVatTu\"='{LoaiVatLieu}'";
            DataTable dtDanhSachVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lstCodeVL = dtDanhSachVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            dbString = $"SELECT cttk.Code,\r\n" +
    $"cttk.CodeHangMuc,\r\n" +
    $"cttk.LoaiVatTu,\r\n" +
    $"cttk.DonVi,\r\n" +
    $"cttk.DonGia,\r\n" +
    $"cttk.NgayBatDau,\r\n" +
    $"cttk.NgayKetThuc,\r\n" +
    $"cttk.CodeNhaThau,\r\n" +
    $"cttk.CodeNhaThauPhu,\r\n" +
    $"cttk.CodeToDoi,\r\n" +
    $"cttk.DonGia,\r\n" +
    $"cttk.DonGiaThiCong,\r\n" +
    $"cttk.KhoiLuongHopDong,\r\n" +
    $"cttk.VatTu as TenCongTac,\r\n" +
    $"cttk.MaVatLieu as MaHieuCongTac,\r\n" +
    $"hm.CodeCongTrinh,\r\n" +
    $"hm.Ten as TenHangMuc,\r\n" +
    $"ctrinh.Ten as TenCongTrinh,\r\n" +
    $"ctrinh.CodeDuAn,\r\n" +
    $"Tuyen.Ten as TenTuyen,Tuyen.Code as CodePhanTuyen, null AS CodeNhom\r\n" +
    $"FROM {TDKH.TBL_KHVT_VatTu} cttk\r\n" +
    $"LEFT JOIN {TDKH.Tbl_PhanTuyen} Tuyen\r\n" +
    $"ON cttk.CodePhanTuyen = Tuyen.Code \r\n" +
    $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
    $"ON cttk.CodeHangMuc = hm.Code\r\n" +
    $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
    $"ON hm.CodeCongTrinh = ctrinh.Code\r\n" +
    $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
    $"ON ctrinh.CodeDuAn = da.Code\r\n" +
    $"WHERE \"LoaiVatTu\" = '{LoaiVatLieu}' \r\n" +
    $"AND da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}'  AND cttk.{DVTH.ColCodeFK}='{DVTH.Code}'";
            dtDanhSachVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtDanhSachVT.Rows.Count != 0&& !m_check)
            {
                de_begin.EditValueChanged -= de_begin_EditValueChanged;
                de_end.EditValueChanged -= de_end_EditValueChanged;
                DateTime Max = dtDanhSachVT.AsEnumerable().Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                DateTime Min = dtDanhSachVT.AsEnumerable().Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                de_begin.EditValue = Min;
                de_end.EditValue = Max;
                m_check = true;
                de_begin.EditValueChanged += de_begin_EditValueChanged;
                de_end.EditValueChanged += de_end_EditValueChanged;
            }
            int stt = 1;
            dbString = $"SELECT * FROM view_HaoPhiVatTu WHERE \"CodeVatTu\" IN ({ MyFunction.fcn_Array2listQueryCondition(lstCodeVL)}) AND \"LoaiVatTu\"='{LoaiVatLieu}'";
            DataTable dtHPVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lst = dtHPVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            var dtKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.VatLieu, lstCodeVL, de_begin.DateTime, de_end.DateTime);
            var HPVTHN =MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.HaoPhiVatTu, lst, de_begin.DateTime, de_end.DateTime);
            dtDanhSachVT.AddIndPhanTuyenNhom();
            var grCongTrinh = dtDanhSachVT.AsEnumerable().GroupBy(x => x["CodeCongTrinh"].ToString());
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
                    var grPhanTuyen = HM.GroupBy(x => (int)x["IndPT"]).OrderBy(x => x.Key);
                    foreach (var Tuyen in grPhanTuyen)
                    {
                        var fstTuyen = Tuyen.First();
                        string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : fstTuyen["CodePhanTuyen"].ToString();
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
                        var grTuyenNhom = Tuyen.GroupBy(x => (int)x["IndNhom"]).OrderBy(x => x.Key);
                        foreach (var NhomTuyen in grTuyenNhom)
                        {
                            var grCongTacTuyen = NhomTuyen.GroupBy(x => x["Code"].ToString());
                            foreach (var CongTac in grCongTacTuyen)
                            {
                                var First = CongTac.FirstOrDefault();
                                DataRow[] drs_VTCon = dtHPVT.AsEnumerable().Where(x => x["CodeVatTu"].ToString() ==CongTac.Key).ToArray();
                                string[] lstCT = drs_VTCon.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray();
                                DataRow[] drs_CT = dtCongTacTheoKy.AsEnumerable().Where(x => lstCT.Contains(x["Code"].ToString())).ToArray();
                                List<KLHN> crRow = dtKLHN.Where(x => x.ParentCode == CongTac.Key).ToList();
                                double KLKH = crRow.Any() ? (double)crRow.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach) : 0;
                                CTHD.Add(new LayCongTacHopDong
                                {
                                    STT = stt++,
                                    ParentID = crCodeHM,
                                    ID = CongTac.Key,
                                    MaHieu = First["MaHieuCongTac"].ToString(),
                                    TenCongViec = First["TenCongTac"].ToString(),
                                    DonVi = First["DonVi"].ToString(),
                                    KhoiLuongHopDong = First["KhoiLuongHopDong"] == DBNull.Value ? 0 : Math.Round(double.Parse(First["KhoiLuongHopDong"].ToString()), 2),
                                    KhoiLuongKeHoach = Math.Round(KLKH, 4),
                                    DonGiaKeHoach = First["DonGia"] == DBNull.Value ? 0 : double.Parse(First["DonGia"].ToString()),
                                    DonGiaThiCong = First["DonGiaThiCong"] == DBNull.Value ? 0 : double.Parse(First["DonGiaThiCong"].ToString()),
                                    NgayBD = de_begin.DateTime,
                                    NgayKT = de_end.DateTime,
                                });
                                foreach (var rowct in drs_CT)
                                {
                                    string CodeHPVT = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).FirstOrDefault()["Code"].ToString();
                                    var cRowHN = HPVTHN.Where(x => x.ParentCode == CodeHPVT).ToList();
                                    double KLKHHN = (double)cRowHN.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach);
                                    LayCongTacHopDong New = new LayCongTacHopDong();
                                    New.ID = Guid.NewGuid().ToString();
                                    New.ParentID = CongTac.Key;
                                    New.CodeCT = rowct["Code"].ToString();
                                    New.MaHieu = rowct["MaHieuCongTac"].ToString();
                                    New.TenCongViec = rowct["TenCongTac"].ToString();
                                    DataRow dataRow = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).ToArray().FirstOrDefault();
                                    double KL = double.Parse(dataRow[TDKH.COL_KHVT_DinhMucNguoiDung].ToString()) * double.Parse(dataRow[TDKH.COL_KHVT_HeSoNguoiDung].ToString()) * double.Parse(rowct[TDKH.COL_KhoiLuongHopDongChiTiet].ToString());
                                    New.KhoiLuongHopDong = Math.Round(KL, 4);
                                    New.KhoiLuongKeHoach = Math.Round(KLKHHN, 4);
                                    New.DonGiaKeHoach = rowct["DonGia"] == DBNull.Value ? 0 : double.Parse(rowct["DonGia"].ToString());
                                    New.DonGiaThiCong = rowct["DonGiaThiCong"] == DBNull.Value ? 0 : double.Parse(rowct["DonGiaThiCong"].ToString());
                                    CTHD.Add(New);
                                }
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
        }
        private void TL_HopDong_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level < 2)
                e.CanFocus = false;
        }

        private void TL_HopDong_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
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

        private void TL_HopDong_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
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
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Purple;
            }           
            else
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void rg_Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            if (edit.SelectedIndex == 1)
            {
                de_begin.Enabled = true;
                de_end.Enabled = true;
            }
            else if (edit.SelectedIndex == 0)
            {
                de_begin.Enabled = false;
                de_end.Enabled = false;
            }
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            TL_HopDong.UncheckAll();
        }

        private void sb_All_Click(object sender, EventArgs e)
        {
            TL_HopDong.CheckAll();
            //TreeListNode[] lst = TL_HopDong.FindNodes(x =>x.Level == 3);
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", true);
            //TL_HopDong.Refresh();
        }

        private void de_begin_EditValueChanged(object sender, EventArgs e)
        {
            //Fcn_capnhapsolieu();
            Fcn_CapNhapVatTu();
        }

        private void de_end_EditValueChanged(object sender, EventArgs e)
        {
            //Fcn_capnhapsolieu();
            Fcn_CapNhapVatTu();

        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            List<LayCongTacHopDong> lst = TL_HopDong.DataSource as List<LayCongTacHopDong>;
            List<LayCongTacHopDong> lstAdd = new List<LayCongTacHopDong>();
            if (lst.Count == 0)
                return;
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu", "Vui Lòng chờ!");
            TreeListNode[] LstTenVL = TL_HopDong.FindNodes(x => x.Level == 2&&x.Visible);
            string[] lstcode = LstTenVL.AsEnumerable().Where(x=> x.GetValue("MaHieu").ToString()!="*T").Select(x => x.GetValue("ID").ToString()).ToArray();
            foreach (string item in lstcode)
            {
                List<LayCongTacHopDong> HD = lst.FindAll(x => x.Chon == true && x.ParentID == item);
                LayCongTacHopDong TenVatTu = lst.FindAll(x => x.ID == item).FirstOrDefault();
                if (HD.Count == 0)
                    continue;
                double KLHD = HD.Sum(x => x.KhoiLuongHopDong);
                double KLKH = HD.Sum(x => x.KhoiLuongKeHoach);
                TenVatTu.KhoiLuongHopDong = KLHD;
                TenVatTu.KhoiLuongKeHoach = KLKH;
                lstAdd.Add(TenVatTu);
                lstAdd.AddRange(HD);
            }
            if (lstAdd.Count == 0)
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }
            bool KeHoach = rg_Select.SelectedIndex == 0?false:true;
            bool IsDonGiaKeHoach = rg_DonGia.SelectedIndex == 0 ? true : false;
            m_TruyenData(lstAdd,KeHoach, IsDonGiaKeHoach);
            WaitFormHelper.CloseWaitForm();
            this.Close();
        }

        private void ctrl_DonViThucHienDuAn_DVTHChanged(object sender, EventArgs e)
        {
            //Fcn_capnhapsolieu();
            Fcn_CapNhapVatTu();
        }

        private void ce_Loc_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_Loc.Checked)
                TL_HopDong.ActiveFilterString = "[KhoiLuongKeHoach]>0";
            else
                TL_HopDong.ClearColumnFilter(TL_HopDong.Columns["KhoiLuongKeHoach"]);
        }
        //private void Fcn_UpdateHaoPhi()
        //{
        //    WaitFormHelper.ShowWaitForm("Đang phân tích hao phí", "Vui Lòng chờ!");
        //    List<LayCongTacHopDong> ThuChiTDKH = (TL_HopDong.DataSource as List<LayCongTacHopDong>);
        //    TreeListNode[] LstTenVL = TL_HopDong.FindNodes(x => x.Level == 2);
        //    List<DonViThucHien> Infor = DuAnHelper.GetDonViThucHiens();
        //    DonViThucHien Detail = ctrl_DonViThucHienDuAn.SelectedDVTH as DonViThucHien;
        //    string[] lstcode = LstTenVL.AsEnumerable().Select(x => x.GetValue("ID").ToString()).ToArray();

        //    string dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
        //      $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
        //      $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
        //      $" WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.{Detail.ColCodeFK}='{Detail.Code}' ORDER BY \"Row\" ASC";
        //    DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        //    string lstCode = MyFunction.fcn_Array2listQueryCondition(dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
        //    dbString = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu} WHERE \"CodeCongTac\" IN ({lstCode}) AND \"LoaiVatTu\"='{LoaiVatLieu}'";
        //    DataTable dtHPVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        //    string[] lst = dtHPVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
        //    var HPVTHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.HaoPhiVatTu, lst, de_begin.DateTime, de_end.DateTime);
        //    foreach (TreeListNode item in LstTenVL)
        //    {
        //        string MaHieu = item.GetValue("MaHieu").ToString();
        //        string VatTu = item.GetValue("TenCongViec").ToString();
        //        string ID = item.GetValue("ID").ToString();
        //        DataRow[] drs_VTCon = dtHPVT.AsEnumerable().Where(x => x["MaVatLieu"].ToString() == MaHieu && x["VatTu"].ToString() == VatTu).ToArray();
        //        string[] lstCT = drs_VTCon.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray();
        //        DataRow[] drs_CT = dtCongTacTheoKy.AsEnumerable().Where(x => lstCT.Contains(x["Code"].ToString())).ToArray();
        //        foreach (var rowct in drs_CT)
        //        {
        //            string CodeHPVT = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).FirstOrDefault()["Code"].ToString();
        //            var cRowHN = HPVTHN.Where(x => x.CodeCha == CodeHPVT).ToList();
        //            //DataRow[] cRowHN = HPVTHN.AsEnumerable().Where(x => x["CodeHaoPhiVatTu"].ToString() == CodeHPVT && DateTime.Parse(x["Ngay"].ToString()) >= DateTime.Parse(ngaybatdau) && DateTime.Parse(x["Ngay"].ToString()) <= DateTime.Parse(ngayketthuc)).ToArray();
        //            double KLKHHN = (double)cRowHN.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach);
        //            LayCongTacHopDong New = new LayCongTacHopDong();
        //            New.ID = Guid.NewGuid().ToString();
        //            New.ParentID = ID;
        //            New.CodeCT = rowct["Code"].ToString();
        //            New.MaHieu = rowct["MaHieuCongTac"].ToString();
        //            New.TenCongViec = rowct["TenCongTac"].ToString();
        //            DataRow dataRow = drs_VTCon.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == rowct["Code"].ToString()).ToArray().FirstOrDefault();
        //            double KL = double.Parse(dataRow[TDKH.COL_KHVT_DinhMucNguoiDung].ToString()) * double.Parse(dataRow[TDKH.COL_KHVT_HeSoNguoiDung].ToString()) * double.Parse(rowct[TDKH.COL_KhoiLuongHopDongChiTiet].ToString());
        //            New.KhoiLuongHopDong = Math.Round(KL, 4);
        //            New.KhoiLuongKeHoach = Math.Round(KLKHHN, 4);
        //            ThuChiTDKH.Add(New);
        //        }
        //    }
        //    TL_HopDong.Refresh();
        //    TL_HopDong.RefreshDataSource();
        //    TL_HopDong.ExpandAll();
        //    WaitFormHelper.CloseWaitForm();
        //}
        private void ce_haophi_CheckedChanged(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang ẩn hiện dữ liệu, Vui lòng chờ!!!!!!");
            TreeListNode[] lst=TL_HopDong.FindNodes(x => x.Level == 3);
            foreach (var item in lst)
                item.Visible = ce_haophi.Checked;
            WaitFormHelper.CloseWaitForm();
            //if (ce_haophi.Checked)
            //    TL_HopDong.FindNodes(x => x.Level == 3) = false;     
            //else
            //    TL_HopDong.FindNodes(x => x.Level == 3).Visible = true;
        }
    }
}
