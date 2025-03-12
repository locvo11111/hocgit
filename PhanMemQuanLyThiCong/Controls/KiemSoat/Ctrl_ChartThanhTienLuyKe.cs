using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.Menu;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.CustomEditor;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Ctrl_ChartThanhTienLuyKe : DevExpress.XtraEditors.XtraUserControl
    {
        static Dictionary<string, List<Chart_KhoiLuongThanhTien>> dicBieuDo = new Dictionary<string, List<Chart_KhoiLuongThanhTien>>();
        DataTable _dt;
        public Ctrl_ChartThanhTienLuyKe()
        {
            InitializeComponent();
            //Fcn_LoadData();
        }
        public void Fcn_LoadData(TypeDVTH typeDVTH)
        {
            RepositoryItemAnyControl item = new RepositoryItemAnyControl();
            item.Control = cc_VatTu;
            ColChart.View.GridControl.RepositoryItems.Add(item);

            ColChart.ColumnEdit = item;
            if(typeDVTH==TypeDVTH.NhaThau)
                gv_BieuDoTaiChinh.RowHeight = Screen.PrimaryScreen.Bounds.Height/2;
            else
                gv_BieuDoTaiChinh.RowHeight = Screen.PrimaryScreen.Bounds.Height / 3;
        }
        private List<KLHN> Fcn_Update(string CodeDVTH, string colCode, DateTime? NBD = null, DateTime? NKT = null)
        {
            string[] lsCodeCongTac;
            string dbString = "";
            DataTable dtTheoNgay = null;
            List<KLHN> Lst = new List<KLHN>();
            if (colCode == "TenNhaCungCap")
            {
                dbString = $"SELECT * FROM {QLVT.TBL_QLVT_NHAPVT} " +
                    $"INNER JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                    $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code={QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
                    $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {QLVT.TBL_QLVT_YEUCAUVT}.TenNhaCungCap='{CodeDVTH}'";
                DataTable dtVL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                lsCodeCongTac = dtVL.AsEnumerable().Select(x => $"'{x["Code"]}'").ToArray();

                dbString = $"SELECT Code, Ngay, " +
                    $"KhoiLuong as KhoiLuongThiCong,DonGia as DonGiaThiCong  " +
                    $"FROM {QLVT.TBL_QLVT_NHAPVTKLHN} " +
                    $"WHERE CodeCha IN ({string.Join(", ", lsCodeCongTac)}) " +
                    $"ORDER BY Ngay ASC";
                //dtTheoNgay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                //dtTheoNgay.Columns.Add("ThanhTienThiCong", typeof(long));
                //dtTheoNgay.Columns["ThanhTienThiCong"].Expression = "[DonGia]*[KhoiLuongThiCong]";
                List<KLHN> LstVC = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(dbString);
                //LstVC.ForEach(x => x.ThanhTienThiCong= (long )Math.Round((double)x.DonGiaThiCong * (double)x.KhoiLuongThiCong));
                dbString = $"SELECT {TDKH.TBL_KHVT_KhoiLuongHangNgay}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCongTac,{TDKH.TBL_KHVT_KhoiLuongHangNgay}.Code AS CodeHangNgay,{TDKH.TBL_KHVT_VatTu}.DonGia AS DonGiaKeHoach," +
                $" {TDKH.TBL_KHVT_VatTu}.DonGiaThiCong, {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,{TDKH.TBL_KHVT_VatTu}.DonVi,  " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
                $"FROM {TDKH.TBL_KHVT_KhoiLuongHangNgay} " +
                $"JOIN {TDKH.TBL_KHVT_VatTu} ON {TDKH.TBL_KHVT_VatTu}.Code={TDKH.TBL_KHVT_KhoiLuongHangNgay}.CodeVatTu " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
                var lsCon = new List<KLTTHangNgay>();
                foreach (KLTTHangNgay item in lsSource)
                {
                    List<Tbl_ThongTinNhaCungCapViewModel> _nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                    var NccsVM = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(item.NhaCungCap);
                    if (!NccsVM.Any())
                        continue;
                    Dictionary<string, string> _DicNccs = _nccs.ToDictionary(x => x.Code, x => x.Ten);
                    if (_DicNccs is null)
                    {
                        var nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                        _DicNccs = nccs.ToDictionary(x => x.Code, x => x.Ten);
                    }
                    foreach (var ncc in NccsVM.Where(x => x.Code == CodeDVTH))
                    {
                        Lst.Add(new KLHN
                        {
                            KhoiLuongThiCong = ncc.KhoiLuong,
                            DonGiaThiCong = ncc.DonGia,
                            ThanhTienThiCong= ncc.KhoiLuong!=0&& ncc.DonGia!=0? (long)Math.Round(ncc.DonGia* ncc.KhoiLuong):0,
                            Ngay = item.Ngay.Value
                        });
                        //DataRow Crow = dtTheoNgay.NewRow();
                        //Crow["DonGia"] = ncc.DonGia;
                        //Crow["KhoiLuongThiCong"] = ncc.KhoiLuong;
                        //Crow["Date"] = item.Ngay;
                        //dtTheoNgay.Rows.Add(Crow);
                    }
                }
                Lst.AddRange(LstVC);
                return Lst;
            }
            dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {colCode}='{CodeDVTH}'";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            Lst = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac, dateBD: NBD, dateKT: NKT);
            return Lst;
        }
        private List<Chart_KhoiLuongThanhTien> PushData(List<KLHN> dt, int Ngay,string ColCode)
        {
            if (dt.Count() == 0)
            {
                cc_VatTu.DataSource = null;

                return null;
            }

            List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();
            List<Chart_KhoiLuongThanhTien> lsOutCondition = new List<Chart_KhoiLuongThanhTien>();
            var grsDate = dt.GroupBy(x => x.Ngay).OrderBy(x => x.Key);
            long luyKeTTKeHoach = 0;
            long luyKeTTThiCong = 0;
            foreach (var item in grsDate)
            {
                long ThanhTienKH = (long)item.Sum(x => x.ThanhTienKeHoach);
                long ThanhTienTC = (long)Math.Round(item.Sum(x => x.ThanhTienThiCong)??0);
                luyKeTTKeHoach += ThanhTienKH;
                luyKeTTThiCong += ThanhTienTC;
                lsOut.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = item.Key,
                    LuyKeThanhTienKeHoach = luyKeTTKeHoach,
                    LuyKeThanhTienThiCong = luyKeTTThiCong,
                    ThanhTienKeHoach = ThanhTienKH,
                    ThanhTienThiCong = ThanhTienTC

                });
            }
            DateTime Min = lsOut.Min(x => x.Date);
            DateTime Max = lsOut.Max(x => x.Date);
            XYDiagram diagram = (XYDiagram)cc_VatTu.Diagram;

            if (Ngay >= 1)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 1;
                diagram.AxisX.WholeRange.SetMinMaxValues(Min, Max);
                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                diagram.AxisX.DateTimeScaleOptions.GridSpacing = Ngay;
            }

            if (Ngay == 1)
                lsOutCondition = lsOut;
            else if (Ngay > 1)
            {
                long LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == Min).FirstOrDefault().LuyKeThanhTienKeHoach;
                long LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == Min).FirstOrDefault().LuyKeThanhTienThiCong;
                long ThanhTienKeHoach = 0;
                long ThanhTienThiCong = 0;
                lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                {
                    Date = Min,
                    LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                    LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                    ThanhTienKeHoach = LuyKeThanhTienKeHoach,
                    ThanhTienThiCong = LuyKeThanhTienThiCong

                });
                for (DateTime i = Min.AddDays(Ngay); i <= Max; i = i.AddDays(Ngay))
                {
                    LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : lsOut.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThanhTienKeHoach;
                    LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == i).FirstOrDefault() == null ? LuyKeThanhTienThiCong : lsOut.FindAll(x => x.Date == i).FirstOrDefault().LuyKeThanhTienThiCong;
                    DateTime Pre = i.AddDays(-Ngay);
                    ThanhTienKeHoach = lsOut.FindAll(x => x.Date > Pre && x.Date <= i).Sum(x => x.ThanhTienKeHoach);
                    ThanhTienThiCong = lsOut.FindAll(x => x.Date > Pre && x.Date <= i).Sum(x => x.ThanhTienThiCong);
                    lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = i,
                        LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                        LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                        ThanhTienKeHoach = ThanhTienKeHoach,
                        ThanhTienThiCong = ThanhTienThiCong

                    });
                    if (i.AddDays(Ngay) >= Max)
                    {
                        LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : lsOut.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienKeHoach;
                        LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienThiCong : lsOut.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienThiCong;
                        ThanhTienKeHoach = lsOut.FindAll(x => x.Date > i && x.Date <= Max).Sum(x => x.ThanhTienKeHoach);
                        ThanhTienThiCong = lsOut.FindAll(x => x.Date > i && x.Date <= Max).Sum(x => x.ThanhTienThiCong);
                        lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = Max,
                            LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                            LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                            ThanhTienKeHoach = ThanhTienKeHoach,
                            ThanhTienThiCong = ThanhTienThiCong

                        });
                        return lsOutCondition;
                    }

                }
            }
            else
            {
                diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                diagram.AxisX.DateTimeScaleOptions.GridSpacing = 1;
                diagram.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Manual;
                diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
                List<DateTime> Month = new List<DateTime>();
                int tongThang = (Max.Year - Min.Year) * 12 + Max.Month - Min.Month;
                long LuyKeThanhTienKeHoach = 0;
                long LuyKeThanhTienThiCong = 0;
                long ThanhTienKeHoach = 0;
                long ThanhTienThiCong = 0;
                for (int i = 0; i <= tongThang; i++)
                {
                    DateTime Add = Min.AddMonths(i);
                    DateTime Begin = new DateTime(Add.Year, Add.Month, 1);
                    DateTime MaxDay = new DateTime(Add.Year, Add.Month, DateTime.DaysInMonth(Add.Year, Add.Month));
                    if (Add.Month == Max.Month && Add.Year == Max.Year)
                    {
                        LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : lsOut.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienKeHoach;
                        LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == Max).FirstOrDefault() == null ? LuyKeThanhTienThiCong : lsOut.FindAll(x => x.Date == Max).FirstOrDefault().LuyKeThanhTienThiCong;
                        ThanhTienKeHoach = lsOut.FindAll(x => x.Date >= Begin && x.Date <= Max).Sum(x => x.ThanhTienKeHoach);
                        ThanhTienThiCong = lsOut.FindAll(x => x.Date >= Begin && x.Date <= Max).Sum(x => x.ThanhTienThiCong);
                        lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                        {
                            Date = Begin,
                            LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                            LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                            ThanhTienKeHoach = ThanhTienKeHoach,
                            ThanhTienThiCong = ThanhTienThiCong

                        });
                        return lsOutCondition;
                    }
                    LuyKeThanhTienKeHoach = lsOut.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThanhTienKeHoach : lsOut.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThanhTienKeHoach;
                    LuyKeThanhTienThiCong = lsOut.FindAll(x => x.Date == MaxDay).FirstOrDefault() == null ? LuyKeThanhTienThiCong : lsOut.FindAll(x => x.Date == MaxDay).FirstOrDefault().LuyKeThanhTienThiCong;
                    ThanhTienKeHoach = lsOut.FindAll(x => x.Date >= Begin && x.Date <= MaxDay).Sum(x => x.ThanhTienKeHoach);
                    ThanhTienThiCong = lsOut.FindAll(x => x.Date >= Begin && x.Date <= MaxDay).Sum(x => x.ThanhTienThiCong);
                    lsOutCondition.Add(new Chart_KhoiLuongThanhTien
                    {
                        Date = Begin,
                        LuyKeThanhTienKeHoach = LuyKeThanhTienKeHoach,
                        LuyKeThanhTienThiCong = LuyKeThanhTienThiCong,
                        ThanhTienKeHoach = ThanhTienKeHoach,
                        ThanhTienThiCong = ThanhTienThiCong

                    });
                }
            }
            return lsOutCondition;
        }

        public void FcnLoadDataGrid(TypeDVTH typeDVTH, int SoNgay, DateTime? NBD = null, DateTime? NKT = null)
        {
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];
            string dbString = $"SELECT \"Code\",\"Ten\" FROM {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"CodeDuAn\"<>\"Code\"";
            DataTable dt_NhaThau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            foreach (DataRow item in dt_NhaThau.Rows)
            {
                List<KLHN> dtTheoNgay = Fcn_Update(item["Code"].ToString(), colFk, NBD, NKT);
                if (dtTheoNgay.Count() == 0)
                    goto Label;
                Label:
                List<Chart_KhoiLuongThanhTien> Source = PushData(dtTheoNgay, SoNgay,colFk);
                if (dicBieuDo.ContainsKey(item["Code"].ToString()))
                {
                    dicBieuDo.Remove(item["Code"].ToString());
                    dicBieuDo.Add(item["Code"].ToString(), Source);
                }
                else
                    dicBieuDo.Add(item["Code"].ToString(), Source);
            }
            gc_BieuDoTaiChinh.DataSource = dt_NhaThau;
            gc_BieuDoTaiChinh.RefreshDataSource();
            gc_BieuDoTaiChinh.Refresh();
            gv_BieuDoTaiChinh.ExpandAllGroups();
            Fcn_LoadData(typeDVTH);
        }
        private void gv_BieuDoTaiChinh_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column == ColChart)
            {
                DataRow dr = (e.Row as DataRowView).Row;
                string code = dr["Code"].ToString();
                e.Value = dicBieuDo[code];
            }
        }
        private void gv_BieuDoTaiChinh_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
        }
        [DisplayName("Visible")]
        [Category("SeriThanhTienKeHoach")]
        public bool SeriThanhTienKeHoach
        {
            get
            {
                return cc_VatTu.Series[0].Visible;
            }

            set
            {
                cc_VatTu.Series[0].Visible = value;
            }
        }     
        [DisplayName("Visible")]
        [Category("SeriLuyKeThanhTienKeHoach")]
        public bool SeriLuyKeThanhTienKeHoach
        {
            get
            {
                return cc_VatTu.Series[2].Visible;
            }

            set
            {
                cc_VatTu.Series[2].Visible = value;
            }
        }       
        public bool SeriXuHuong
        {
            get
            {
                return cc_VatTu.Series[4].Visible;
            }

            set
            {
                cc_VatTu.Series[4].Visible =cc_VatTu.Series[5].Visible = value;
            }
        }    
        [DisplayName("Visible")]
        [Category("Legend")]
        public DefaultBoolean Legend
        {
            get
            {
                return cc_VatTu.Legend.Visibility;
            }

            set
            {
                cc_VatTu.Legend.Visibility = value;
            }
        }

        private void gv_BieuDoTaiChinh_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            //GridView view = sender as GridView;
            //GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            //if (e.MenuType == GridMenuType.Row|| e.MenuType == GridMenuType.Column)
            //{
            //    DXMenuItem menuItem = new DXMenuItem("Hiện xu hướng tài chính", this.fcn_Handle_Popup_ChenDong);
            //    menuItem.Tag = hitInfo.Column;
            //    e.Menu.Items.Add(menuItem);          
                
            //    DXMenuItem menuItemHide = new DXMenuItem("Ẩn xu hướng tài chính", this.fcn_Handle_Popup_An);
            //    menuItem.Tag = hitInfo.Column;
            //    e.Menu.Items.Add(menuItemHide);

            //}
        }
        //private void fcn_Handle_Popup_ChenDong(object sender, EventArgs e)
        //{
        //    SeriXuHuong = true;
        //}     
        //private void fcn_Handle_Popup_An(object sender, EventArgs e)
        //{
        //    SeriXuHuong = false;
        //}
    }
}
