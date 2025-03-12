using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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
    public partial class XtraForm_LayCongTacMTC : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_LayCongTacMTC()
        {
            InitializeComponent();
        }

        public delegate void DE_TransDanhSachCongTac(List<LayCongTacHopDong> DanhSachCT, string CodeMayDuAn);
        public DE_TransDanhSachCongTac de_TranDanhSachCT;
        private string _CodeMayDuAn { get; set; }
        public void Fcn_UpdateData(string CodeMayDuAn, DateTime NgayNhap)
        {
            _CodeMayDuAn = CodeMayDuAn;
            WaitFormHelper.ShowWaitForm("Đang tải Dữ liệu công tác", "Vui Lòng chờ!");
            DonViThucHien DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;
            List<LayCongTacHopDong> CTHD = new List<LayCongTacHopDong>();
            string condition = $"AND cttk.NgayBatDau<='{NgayNhap.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' AND " +
            $"cttk.NgayKetThuc>='{NgayNhap.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
            string dbString =
                  $"SELECT " +
                  $" NULL AS CodeHangMuc,cttk.TenHangMuc," +
                  $"NULL AS CodeCongTrinh,NULL AS TenCongTrinh," +
                  $"cttk.DonVi," +
                  $"cttk.MaHieuCongTac," +
                   $"cttk.TenCongTac," +
                   $"NULL AS KhoiLuongHopDongToanDuAn, " +
                  $"NULL AS CodeDuAn,cttk.Code \r\n" +
                  $"FROM {MyConstant.TBL_MTC_NHATTRINHCONGTAC} cttk WHERE cttk.{DVTH.ColCodeFK}='{DVTH.Code}' \r\n" +
                  $" UNION ALL " +
            $"SELECT " +
            $" COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
            $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
            $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi," +
            $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
             $"COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
             $"COALESCE(cttk.KhoiLuongHopDongToanDuAn, dmct.KhoiLuongHopDongToanDuAn) AS KhoiLuongHopDongToanDuAn, " +
            $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn,cttk.Code \r\n" +
            $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
            $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
            $"ON cttk.CodeCongTac = dmct.Code \r\n" +
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
            $"AND cttk.{DVTH.ColCodeFK}='{DVTH.Code}' {condition} ";

            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            var grCongTrinh = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodeCongTrinh"].ToString());
            int stt = 1;
            CTHD.Add(new LayCongTacHopDong
            {
                ParentID = "0",
                ID = "1",
                MaHieu = "KH",
                TenCongViec = "CÔNG TÁC THEO KẾ HOẠCH"

            });
            CTHD.Add(new LayCongTacHopDong
            {
                ParentID = "0",
                ID = "2",
                MaHieu = "TC",
                TenCongViec = "CÔNG TÁC THỦ CÔNG"

            });

            foreach (var Ctrinh in grCongTrinh)
            {
                if (string.IsNullOrEmpty(Ctrinh.Key))
                {
                    var grCongTacTuyen = Ctrinh.GroupBy(x => x["Code"].ToString());
                    stt = 1;
                    foreach (var CongTac in grCongTacTuyen)
                    {
                        var CTac = CongTac.FirstOrDefault();
                        CTHD.Add(new LayCongTacHopDong
                        {
                            STT = stt++,
                            ParentID = "2",
                            ID = CongTac.Key,
                            MaHieu = CTac["MaHieuCongTac"].ToString(),
                            TenCongViec = CTac["TenCongTac"].ToString(),
                            DonVi = CTac["DonVi"].ToString(),
                        });
                    }
                }
                else
                {
                    string crCodeCT = Ctrinh.Key;
                    CTHD.Add(new LayCongTacHopDong()
                    {
                        ParentID = "1",
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
                        var grCongTacTuyen = HM.GroupBy(x => x["Code"].ToString());
                        stt = 1;
                        foreach (var CongTac in grCongTacTuyen)
                        {
                            var CTac = CongTac.FirstOrDefault();
                            CTHD.Add(new LayCongTacHopDong
                            {
                                STT = stt++,
                                ParentID = crCodeHM,
                                CodeCT = crCodeCT,
                                ID = CongTac.Key,
                                MaHieu = CTac["MaHieuCongTac"].ToString(),
                                TenCongViec = CTac["TenCongTac"].ToString(),
                                DonVi = CTac["DonVi"].ToString(),
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
        }

        private void TL_HopDong_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {

        }

        private void TL_HopDong_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Red;
            }
            else if (e.Node.Level == 1)
            {
                LayCongTacHopDong HD = TL_HopDong.GetRow(e.Node.Id) as LayCongTacHopDong;
                if (HD.ParentID == "1")
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                }
            }
            else if (e.Node.Level == 2)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
            }
            else
            {
                //if (e.Node.GetValue("MaHieu") == null)
                //    return;
                //if (e.Node.GetValue("MaHieu").ToString() == MyConstant.CONST_TYPE_PhanTuyen)
                //{
                //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //    e.Appearance.ForeColor = Color.Red;
                //}
                //else if (e.Node.GetValue("MaHieu").ToString() == MyConstant.CONST_TYPE_NHOM)
                //{
                //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //    e.Appearance.ForeColor = MyConstant.color_Row_NhomCongTac;
                //}
            }
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ChonAll_Click(object sender, EventArgs e)
        {
            TL_HopDong.CheckAll();
        }

        private void sb_Save_Click(object sender, EventArgs e)
        {
            List<LayCongTacHopDong> HD = TL_HopDong.DataSource as List<LayCongTacHopDong>;
            List<LayCongTacHopDong> HDSelect = HD.Where(x => x.Chon).ToList();
            de_TranDanhSachCT(HDSelect,_CodeMayDuAn);
            this.Close();
        }
    }
}