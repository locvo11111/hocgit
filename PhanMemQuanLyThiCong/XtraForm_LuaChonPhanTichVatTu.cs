using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using PhanMemQuanLyThiCong.Constant.Enum;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using StackExchange.Profiling.Internal;
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
    public partial class XtraForm_LuaChonPhanTichVatTu : DevExpress.XtraEditors.XtraForm
    {
        public DoBocVatTu _Type { get; set; }
        public XtraForm_LuaChonPhanTichVatTu(DoBocVatTu Type)
        {
            InitializeComponent();
            _Type = Type;
        }
        private void Fcn_LoadData(DoBocVatTu type,string condition, TreeList TL)
        {
            if (SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH is null)
            {
                this.Close();
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu vật tư");
            if (type==DoBocVatTu.AllVatTu)
            {
                type = DoBocVatTu.VatLieu;
                condition= TL==tl_PhanTich? "AND hp.PhanTichKeHoach=true": "AND hp.PhanTichKeHoach=false";
            }
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);
            string dbString =!ce_HienCongTac.Checked?TDKHHelper.GetDbStringFullInfoVatTuBrief(type, codeCT, codeHM, SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH,
                 ignoreKLKH: true, isGetNhapKho: false, IsPhanTich: condition): TDKHHelper.GetDbStringFullInfoVatTuWithHaoPhi(type, codeCT, codeHM, SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH,
                 ignoreKLKH: true, isGetNhapKho: false,isGetKLTCCongTac:false); 

            DataTable vatTus = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //var lsHaoPhi = TDKHHelper.GetFullInfoVatTuBriefDataTable(type, codeCT, codeHM, SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH, 
            //    out List<KLHN> klhnsVtu, ignoreKLKH: true, isGetNhapKho: false, IsPhanTich: "AND hp.PhanTichKeHoach=1");

            var codesHp = vatTus.AsEnumerable().Select(x => x["Code"].ToString()).Distinct();
            List<VatLieu> ModelVT = new List<VatLieu>();

            var grsCTrinh = vatTus.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);
            foreach (var grCTrinh in grsCTrinh)
            {
                DataRow fstCtr = grCTrinh.First();
                ModelVT.Add(new VatLieu
                {
                    ParentID = "0",
                    ID = grCTrinh.Key.ToString(),
                    TenVatTu = fstCtr["TenCongTrinh"].ToString(),
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH
                });

                var grsHM = grCTrinh.GroupBy(x => x["CodeHangMuc"]);
                foreach (var grHM in grsHM)
                {
                    DataRow fstHM = grHM.First();
                    ModelVT.Add(new VatLieu
                    {
                        ParentID = grCTrinh.Key.ToString(),
                        ID = grHM.Key.ToString(),
                        TenVatTu = fstHM["TenHangMuc"].ToString(),
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC
                    });
                    var grsPhanTuyen = grHM.GroupBy(x => x["CodePhanTuyen"].ToString())
                            .OrderByDescending(x => x.Key.HasValue());
                    foreach (var grPhanTuyen in grsPhanTuyen)
                    {
                        DataRow fstPT = grPhanTuyen.First();
                        if (grPhanTuyen.Key.HasValue())
                        {

                            ModelVT.Add(new VatLieu
                            {
                                ParentID = grHM.Key.ToString(),
                                ID = grPhanTuyen.Key.ToString(),
                                TenVatTu = fstPT["TenPhanTuyen"].ToString(),
                                MaVatTu = MyConstant.CONST_TYPE_PhanTuyen
                            });
                        }
                        if (ce_HienCongTac.Checked)
                        {
                            var grsVatTu = grPhanTuyen.GroupBy(x => x["CodeVatTu"]);
                            foreach (var grVatTu in grsVatTu)
                            {
                                var fstVT = grVatTu.First();
                                if (fstVT["Code"] == DBNull.Value && fstVT["CodeThiCong"] == DBNull.Value)
                                    continue;
                                WaitFormHelper.ShowWaitForm($"{ fstVT["MaVatLieu"]}_{ fstVT["VatTu"]}");
                                ModelVT.Add(new VatLieu
                                {
                                    ParentID = grPhanTuyen.Key.HasValue() ? grPhanTuyen.Key.ToString() : grHM.Key.ToString(),
                                    ID = fstVT["CodeVatTu"].ToString(),
                                    TenVatTu = fstVT["VatTu"].ToString(),
                                    MaVatTu = fstVT["MaVatLieu"].ToString(),
                                    DonVi = fstVT["DonVi"].ToString(),
                                    DonGiaHienTruong = fstVT["DonGiaThiCong"] == DBNull.Value ? 0 : double.Parse(fstVT["DonGiaThiCong"].ToString()),
                                    HopDongKl = fstVT["KhoiLuongHopDong"] == DBNull.Value ? 0 : double.Parse(fstVT["KhoiLuongHopDong"].ToString()),
                                });
                                foreach (var haoPhi in grVatTu)
                                {

                                    ModelVT.Add(new VatLieu
                                    {
                                        ParentID = fstVT["CodeVatTu"].ToString(),
                                        ID = haoPhi["Code"].ToString(),
                                        TenVatTu = haoPhi["TenCongTac"].ToString(),
                                        MaVatTu = haoPhi["MaHieuCongTac"].ToString(),
                                        DonVi = haoPhi["DonVi"].ToString(),
                                        DonGiaHienTruong = haoPhi["DonGia"] == DBNull.Value ? 0 : double.Parse(haoPhi["DonGia"].ToString()),
                                        HopDongKl = haoPhi["KhoiLuongHopDong"] == DBNull.Value ? 0 : double.Parse(haoPhi["KhoiLuongHopDong"].ToString()),
                                    });
                                }

                            }
                        }
                        else
                        {
                            foreach (var grVatTu in grPhanTuyen)
                            {
                                var fstVT = grVatTu;
                                if (fstVT["Code"] == DBNull.Value && fstVT["CodeThiCong"] == DBNull.Value)
                                    continue;
                                WaitFormHelper.ShowWaitForm($"{ fstVT["MaVatLieu"]}_{ fstVT["VatTu"]}");
                                ModelVT.Add(new VatLieu
                                {
                                    ParentID = grPhanTuyen.Key.HasValue() ? grPhanTuyen.Key.ToString() : grHM.Key.ToString(),
                                    ID = fstVT["Code"].ToString(),
                                    TenVatTu = fstVT["VatTu"].ToString(),
                                    MaVatTu = fstVT["MaVatLieu"].ToString(),
                                    DonVi = fstVT["DonVi"].ToString(),
                                    DonGiaHienTruong = fstVT["DonGiaThiCong"] == DBNull.Value ? 0 : double.Parse(fstVT["DonGiaThiCong"].ToString()),
                                    HopDongKl = fstVT["KhoiLuongHopDong"] == DBNull.Value ? 0 : double.Parse(fstVT["KhoiLuongHopDong"].ToString()),
                                });                             
                            }
                            if (grPhanTuyen.Key.HasValue())
                            {
                                ModelVT.Add(new VatLieu
                                {
                                    ParentID = grHM.Key.ToString(),
                                    ID = Guid.NewGuid().ToString(),
                                    TenVatTu = $"HT_{fstPT["TenPhanTuyen"]}",
                                    MaVatTu = MyConstant.CONST_TYPE_HoanThanhPhanTuyen
                                });
                            }
                        }
                     

                    }


                }


            }
            TL.DataSource = ModelVT;
            TL.RefreshDataSource();
            TL.Refresh();
            TL.ExpandAll();
            WaitFormHelper.CloseWaitForm();
        }
        private void tl_PhanTich_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (object.Equals(e.CellValue, (double)0))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tl_PhanTich_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
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
            else if (e.Node.Level == 2)
            {
                if (e.Node.GetValue("MaVatTu") is null)
                    return;
               string MVT= e.Node.GetValue("MaVatTu").ToString();
                if (MVT == MyConstant.CONST_TYPE_HoanThanhPhanTuyen || MVT == MyConstant.CONST_TYPE_PhanTuyen)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = MyConstant.color_Row_PhanTuyen;
                }
            }
        }

        private void cbe_LoaiVatTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cbe_LoaiVatTu.Text;
            DoBocVatTu Type = (DoBocVatTu)Array.IndexOf(TDKH.sheetsName, name);
            _Type = Type;
            Fcn_LoadData(Type, "AND hp.PhanTichKeHoach=true", tl_PhanTich);
            Fcn_LoadData(Type, "AND hp.PhanTichKeHoach=false", tl_KhongPhanTich);
            ce_HienCongTac.Checked = false;
        }

        private void XtraForm_LuaChonPhanTichVatTu_Load(object sender, EventArgs e)
        {
            Fcn_LoadData(_Type, "AND hp.PhanTichKeHoach=true", tl_PhanTich);
            Fcn_LoadData(_Type, "AND hp.PhanTichKeHoach=false", tl_KhongPhanTich);
        }

        private void ce_HienCongTac_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_HienCongTac.Checked)
            {

            }
            else
            {

            }
        }


        private void splitContainerControl1_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Height*2/ 3;
        }

        private void sb_ThempT_Click(object sender, EventArgs e)
        {

            List<VatLieu> VatLieu = tl_KhongPhanTich.DataSource as List<VatLieu>;
            List<VatLieu> VatLieuSelect = VatLieu.Where(x => x.Chon == true).ToList();
            if (!VatLieuSelect.Any())
                return;
            VatLieu.RemoveAll(x => x.Chon == true);
            string[] lstCode = VatLieuSelect.Select(x => x.ID).ToArray();
            string dbString = $"UPDATE  {TDKH.TBL_KHVT_VatTu} SET \"PhanTichKeHoach\"=true " +
                $"WHERE \"Code\" IN ({MyFunction.fcn_Array2listQueryCondition(lstCode)})";
            DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"UPDATE  {TDKH.Tbl_HaoPhiVatTu} SET \"PhanTichKeHoach\"=true " +
                $"WHERE \"CodeVatTu\" IN ({MyFunction.fcn_Array2listQueryCondition(lstCode)})";
            DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            Fcn_LoadData(_Type, "AND hp.PhanTichKeHoach=true", tl_PhanTich);
            Fcn_LoadData(_Type, "AND hp.PhanTichKeHoach=false", tl_KhongPhanTich);
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            tl_PhanTich.UncheckAll();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            tl_KhongPhanTich.UncheckAll();
        }

        private void XtraForm_LuaChonPhanTichVatTu_FormClosed(object sender, FormClosedEventArgs e)
        {
            int loc = 1;
        }

        private void sb_BoPT_Click(object sender, EventArgs e)
        {
            List<VatLieu> VatLieu = tl_PhanTich.DataSource as List<VatLieu>;
            List<VatLieu> VatLieuSelect = VatLieu.Where(x => x.Chon == true).ToList();
            if (!VatLieuSelect.Any())
                return;
            VatLieu.RemoveAll(x => x.Chon == true);
            string[] lstCode = VatLieuSelect.Select(x => x.ID).ToArray();
            string dbString = $"UPDATE  {TDKH.TBL_KHVT_VatTu} SET \"PhanTichKeHoach\"=false " +
                $"WHERE \"Code\" IN ({MyFunction.fcn_Array2listQueryCondition(lstCode)})";
            DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"UPDATE  {TDKH.Tbl_HaoPhiVatTu} SET \"PhanTichKeHoach\"=false " +
                $"WHERE \"CodeVatTu\" IN ({MyFunction.fcn_Array2listQueryCondition(lstCode)})";
            DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            Fcn_LoadData(_Type, "AND hp.PhanTichKeHoach=true", tl_PhanTich);
            Fcn_LoadData(_Type, "AND hp.PhanTichKeHoach=false", tl_KhongPhanTich);
        }

        private void sb_PT_Click(object sender, EventArgs e)
        {
            List<VatLieu> VatLieu = tl_KhongPhanTich.DataSource as List<VatLieu>;
            List<VatLieu> VatLieuSelect = VatLieu.Where(x => x.Chon == true).ToList();
            if (!VatLieuSelect.Any())
                return;
            VatLieu.RemoveAll(x => x.Chon == true);
            string[] lstCode = VatLieuSelect.Select(x => x.ID).ToArray();
            string dbString = $"UPDATE  {TDKH.TBL_KHVT_VatTu} SET \"PhanTichKeHoach\"=true " +
                $"WHERE \"Code\" IN ({MyFunction.fcn_Array2listQueryCondition(lstCode)})";
            DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dbString = $"UPDATE  {TDKH.Tbl_HaoPhiVatTu} SET \"PhanTichKeHoach\"=true " +
                $"WHERE \"CodeVatTu\" IN ({MyFunction.fcn_Array2listQueryCondition(lstCode)})";
            DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            Fcn_LoadData(_Type, "AND hp.PhanTichKeHoach=true", tl_PhanTich);
            Fcn_LoadData(_Type, "AND hp.PhanTichKeHoach=false", tl_KhongPhanTich);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            tl_KhongPhanTich.CheckAll();
        }

        private void sb_ChonAllPhanTich_Click(object sender, EventArgs e)
        {
            tl_PhanTich.CheckAll();
        }
    }
}