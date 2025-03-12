using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.LookAndFeel;
//using System.Drawing.Printing;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class TongHopCongTacVatLieu : DevExpress.XtraEditors.XtraUserControl
    {
        DateTime _NBD, _NKT;
        public TongHopCongTacVatLieu()
        {
            InitializeComponent();
        }
        public void Export()
        {
            //TL_TongHop.ShowRibbonPrintPreview();
            PreviewPrintableComponent(TL_TongHop, TL_TongHop.LookAndFeel);
        }
        public void Fcn_Update(DateTime NBD,DateTime NKT)
        {
            List<DonViThucHien> DVTH = DuAnHelper.GetDonViThucHiens();
            List<TongHopDuAn> TH = new List<TongHopDuAn>();
            string CodeGiaoThau = Guid.NewGuid().ToString();
            string CodeNhanThau = Guid.NewGuid().ToString();

            TH.Add(new TongHopDuAn
            {
                ParentID="0",
                ID=CodeGiaoThau,
                TenNhaThau="TỔNG HỢP TẤT CẢ"
            });  
            TH.Add(new TongHopDuAn
            {
                ParentID="0",
                ID= CodeNhanThau,
                TenNhaThau="NHẬN THẦU"
            });
            long TTKH_NT1 = 0, TTTC_NT1 = 0;
            foreach(DonViThucHien item in DVTH.OrderByDescending(x=>x.LoaiGiaoNhanThau))
            {

                if (item.IsGiaoThau)
                {
                    //dbString = $"SELECT *  FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND \"CodeNhaThau\" IS NULL";
                    //DataTable Dt_ChuKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    //string[] lstcode = Dt_ChuKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                    //DataTable dtTheoNgayTC = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lstcode, dateBD: NBD, dateKT: NKT, ignoreKLKH: true);
                    ////TTKH = dtTheoNgay.AsEnumerable().Where(x => x["ThanhTienKeHoach"] != DBNull.Value).Any() ? dtTheoNgay.AsEnumerable().Where(x => x["ThanhTienKeHoach"] != DBNull.Value).Sum(x => long.Parse(x["ThanhTienKeHoach"].ToString())) : 0;
                    //if (dtTheoNgayTC != null)
                    //{
                    //    dtTheoNgayTC.Columns["ThanhTienThiCong"].Expression = "[DonGiaThiCong]*[KhoiLuongThiCong]";
                    //    TTTC = dtTheoNgayTC.AsEnumerable().Where(x => x["ThanhTienThiCong"] != DBNull.Value).Any() ? dtTheoNgayTC.AsEnumerable().Where(x => x["ThanhTienThiCong"] != DBNull.Value).Sum(x => long.Parse(x["ThanhTienThiCong"].ToString())) : 0;
                    //}
                       
                    TH.Add(new TongHopDuAn
                    {
                        ParentID =CodeGiaoThau,
                        ID = item.Code,
                        TenNhaThau =item.Ten,
                        ThanhTienKeHoach= TTKH_NT1,
                        ThanhTienThiCong= TTTC_NT1
                    });
                    if (TH.Count() == 0)
                        continue;
                    long TTKH = TH.FindAll(x => x.TenNhaThau == "VẬT LIỆU").Any() ? TH.FindAll(x => x.TenNhaThau == "VẬT LIỆU").Sum(x => x.ThanhTienKeHoach) : 0;
                    long TTTC = TH.FindAll(x => x.TenNhaThau == "VẬT LIỆU").Any() ? TH.FindAll(x => x.TenNhaThau == "VẬT LIỆU").Sum(x => x.ThanhTienThiCong) : 0;
                    TH.Add(new TongHopDuAn
                    {
                        ParentID = item.Code,
                        ID = Guid.NewGuid().ToString(),
                        TenNhaThau ="VẬT LIỆU",
                        ThanhTienKeHoach = TTKH,
                        ThanhTienThiCong = TTTC
                    });
                    TTKH = TH.FindAll(x => x.TenNhaThau == "NHÂN CÔNG").Any() ? TH.FindAll(x => x.TenNhaThau == "NHÂN CÔNG").Sum(x => x.ThanhTienKeHoach) : 0;
                    TTTC = TH.FindAll(x => x.TenNhaThau == "NHÂN CÔNG").Any() ? TH.FindAll(x => x.TenNhaThau == "NHÂN CÔNG").Sum(x => x.ThanhTienThiCong) : 0;
                    TH.Add(new TongHopDuAn
                    {
                        ParentID = item.Code,
                        ID = Guid.NewGuid().ToString(),
                        TenNhaThau ="NHÂN CÔNG",
                        ThanhTienKeHoach = TTKH,
                        ThanhTienThiCong = TTTC
                    });
                    TTKH = TH.FindAll(x => x.TenNhaThau == "MÁY THI CÔNG").Any() ? TH.FindAll(x => x.TenNhaThau == "MÁY THI CÔNG").Sum(x => x.ThanhTienKeHoach) : 0;
                    TTTC = TH.FindAll(x => x.TenNhaThau == "MÁY THI CÔNG").Any() ? TH.FindAll(x => x.TenNhaThau == "MÁY THI CÔNG").Sum(x => x.ThanhTienThiCong) : 0;
                    TH.Add(new TongHopDuAn
                    {
                        ParentID = item.Code,
                        ID = Guid.NewGuid().ToString(),
                        TenNhaThau ="MÁY THI CÔNG",
                        ThanhTienKeHoach = TTKH,
                        ThanhTienThiCong = TTTC
                    });
                }
                else
                {
                    string dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE {item.ColCodeFK}='{item.Code}'";
                    DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    string[] lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                    var dtTheoNgay = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac, dateBD: NBD, dateKT: NKT);
                    if (dtTheoNgay == null)
                        continue;
                    //dtTheoNgay.Columns["ThanhTienThiCong"].Expression = "[DonGiaThiCong]*[KhoiLuongThiCong]";
                    long TTKH = 0, TTTC = 0;
                    TTKH = (long)Math.Round(dtTheoNgay.Sum(x => x.ThanhTienKeHoach) ?? 0);
                    dbString = $"SELECT * FROM {TDKH.TBL_KHVT_VatTu} WHERE {item.ColCodeFK}='{item.Code}'";
                    DataTable KHVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    TTTC = (long)Math.Round(dtTheoNgay.Sum(x => x.ThanhTienThiCong) ?? 0);
                    TH.Add(new TongHopDuAn
                    {
                        ParentID = CodeNhanThau,
                        ID = item.Code,
                        TenNhaThau = item.Ten,
                        ThanhTienKeHoach = TTKH,
                        ThanhTienThiCong = TTTC
                    });
                    TTKH_NT1 += TTKH;
                    TTTC_NT1 += TTTC;
                    if (KHVT.Rows.Count == 0)
                        continue;
                    TongHopDuAn VL = Fcn_UpdateLoaiVL("Vật liệu", KHVT, item, NBD, NKT);
                    TongHopDuAn NC = Fcn_UpdateLoaiVL("Nhân công", KHVT, item, NBD, NKT);
                    TongHopDuAn MTC = Fcn_UpdateLoaiVL("Máy thi công", KHVT, item, NBD, NKT);
                    if (VL != null)
                        TH.Add(VL);
                    if (NC != null)
                        TH.Add(NC);
                    if (MTC != null)
                        TH.Add(MTC);
                }

            }
            TL_TongHop.DataSource = TH;
            TL_TongHop.RefreshDataSource();
            TL_TongHop.Refresh();
            TL_TongHop.ExpandAll();

        }
        private TongHopDuAn Fcn_UpdateLoaiVL(string LoaiVatLieu,DataTable dtKHVT,DonViThucHien DVTH, DateTime NBD, DateTime NKT)
        {
            TongHopDuAn TH = new TongHopDuAn();
            long TTKH = 0, TTTC = 0;
            _NBD = NBD;
            _NKT = NKT;
            DataRow [] Crow = dtKHVT.AsEnumerable().Where(x => x["LoaiVatTu"].ToString() == LoaiVatLieu).ToArray();
            if (!Crow.Any())
                return null;
            string[] lsCodeCongTac = Crow.Select(x => x["Code"].ToString()).ToArray();
            var dtTheoNgay = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.VatLieu, lsCodeCongTac, dateBD: NBD, dateKT: NKT);
            if (dtTheoNgay==null)
                return null;
            TTKH = (long)Math.Round(dtTheoNgay.Sum(x => x.ThanhTienKeHoach) ?? 0);
            //if (DVTH.IsGiaoThau)
            //{
            //    string dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.* FROM {TDKH.TBL_KHVT_VatTu} " +
            //        $"LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc" +
            //        $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh={MyConstant.TBL_THONGTINCONGTRINH}.Code " +
            //        $" WHERE {TDKH.TBL_KHVT_VatTu}.CodeNhaThau IS NULL AND {TDKH.TBL_KHVT_VatTu}.LoaiVatTu='{LoaiVatLieu}' AND {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn ='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            //    DataTable KHVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //    lsCodeCongTac = KHVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            //    var dtTheoNgayTC = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.VatLieu, lsCodeCongTac, dateBD: NBD, dateKT: NKT,ignoreKLKH:true);
            //    if (dtTheoNgayTC != null)
            //        TTTC = dtTheoNgayTC.AsEnumerable().Where(x => x["ThanhTienThiCong"] != DBNull.Value).Any() ? dtTheoNgay.AsEnumerable().Where(x => x["ThanhTienThiCong"] != DBNull.Value).Sum(x => long.Parse(x["ThanhTienThiCong"].ToString())) : 0;
            //}
            //else
            //{             
            TTTC = (long)Math.Round(dtTheoNgay.Sum(x => x.ThanhTienThiCong) ?? 0);
            //}
            TH.ParentID = DVTH.Code;
            TH.ID = Guid.NewGuid().ToString();
            TH.TenNhaThau = LoaiVatLieu.ToUpper();
            TH.ThanhTienKeHoach = TTKH;
            TH.ThanhTienThiCong = TTTC;
            return TH;


        }
        private void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = true,
                PaperKind = System.Drawing.Printing.PaperKind.A3,
                Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20),
            };
            // Create a link that will print a control.
            //...
            // Subscribe to the CreateReportHeaderArea event used to generate the report header.
            link.CreateReportHeaderArea += link_CreateReportHeaderArea;
            link.PrintingSystemBase.Document.AutoFitToPagesWidth = 1;
            WaitFormHelper.CloseWaitForm();
            // Show the report.
            link.ShowRibbonPreview(lookAndFeel);
        }
        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            //var sheetThongTin = BaseFrom.SpreadsheetControl.Document.Worksheets[CommonConstants.SHEETTHONGTINCONGTRINH];
            //string tenCongTrinh = "";
            //if (sheetThongTin.DefinedNames.Contains(CommonConstants.TTCT_TTC_TENCONGTRINH))
            //{
            //    tenCongTrinh = sheetThongTin.Range[CommonConstants.TTCT_TTC_TENCONGTRINH].Value.TextValue;
            //}
            //string diaDiemXD = "";
            //if (sheetThongTin.DefinedNames.Contains(CommonConstants.TTCT_TTC_DIADIEMXAYDUNG))
            //{
            //    diaDiemXD = sheetThongTin.Range[CommonConstants.TTCT_TTC_DIADIEMXAYDUNG].Value.TextValue;
            //}
            List<Tbl_ThongTinDuAnViewModel> lst = SharedControls.slke_ThongTinDuAn.Properties.DataSource as List<Tbl_ThongTinDuAnViewModel>;
            List<Tbl_ThongTinDuAnViewModel> Manage = lst.FindAll(x => x.Code == "Manage").ToList();
            if (Manage.Count() != 0)
                lst.Remove(Manage.SingleOrDefault());
            string reportHeader = $"Dự án: {SharedControls.slke_ThongTinDuAn.Text} {Environment.NewLine}Địa chỉ: {lst.FirstOrDefault().DiaChi}";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            e.Graph.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            RectangleF rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);

            reportHeader = $"Thời gian: Từ {_NBD.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} đến {_NKT.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} ";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Far);
            e.Graph.Font = new Font("Times New Roman", 14, FontStyle.Regular);
            RectangleF recnew = new RectangleF(0, 55, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, recnew, BorderSide.None);

        }
        public class TongHopDuAn
        {
            public string TenNhaThau { get; set; }
            public string ID { get; set; }
            public string ParentID { get; set; }
            public long ThanhTienKeHoach { get; set; }
            public long ThanhTienThiCong { get; set; }



        }

        private void TL_TongHop_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 0:
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                    break;
                case 1:
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    break;
                //case 2:
                //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //    e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                //    break;
                //case 3:
                //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //    e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                //    break;
                //case 4:
                //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //    break;
                default:
                    break;
            }
        }

        private void TL_TongHop_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue,(long) 0) && (e.Node.Level == 0))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }
    }
}

