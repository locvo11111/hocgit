using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Ctrl_KiemSoatTienDoDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_KiemSoatTienDoDuAn()
        {
            InitializeComponent();
        }
        public Dictionary<string, string> Dic = new Dictionary<string, string>();
        public bool IsSelect { get; set; } = false;
        private int CountDuAn { get; set; }
        public string Condition { get; set; } = "";
        public string Condition1 { get; set; } = "";
        public DateTime _de_NKTTron { get { return de_NKTTron.DateTime; } set { de_NKTTron.DateTime = value; } }
        public DateTime _de_NBDBar { get { return de_NBDBar.DateTime; } set { de_NBDBar.DateTime = value; } }
        public DateTime _de_NKTBar { get { return de_NKTBar.DateTime; } set { de_NKTBar.DateTime = value; } }
        public void Fcn_UpdateBieuDoTron()
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu BIỂU ĐỒ TRÒN");
            string dbString = $"SELECT COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn," +
                $"COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn," +
                $"cttk.* FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
$"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
$"ON cttk.CodeCongTac = dmct.Code \r\n" +
$"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
$"ON dmct.CodeHangMuc = hm.Code \r\n" +
$"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
$"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
$"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
$"ON ctrinh.CodeDuAn = da.Code \r\n" +
$"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
$"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
$"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code WHERE cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) AND cttk.CodeNhaThau IS NOT NULL";
            DataTable dtcttk = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lstCode = dtcttk.AsEnumerable().Select(x => x["CodeCha"].ToString()).ToArray();
            string[] lstCodeCT = dtcttk.AsEnumerable().Where(x => !lstCode.Contains(x["Code"].ToString())).Select(x => x["Code"].ToString()).ToArray();

            List<KiemSoat> KS = new List<KiemSoat>();
            double TyLeCham = 0, TyLeNhanh = 0, TyLeDat = 0;
            bool Nhanh = false, Cham = false, Dat = false;
            int CountDa = 0;
            var grDuAn = dtcttk.AsEnumerable().GroupBy(x => x["CodeDuAn"]);
            List<KLHN> LKKHDayAll = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lstCodeCT,new List<DateTime>() { de_NKTTron.DateTime.Date} );
            Dic.Clear();
            string[] lstCodeDuAn = grDuAn.Select(x => x.Key.ToString()).ToArray(); ;
            foreach (var DA in grDuAn)
            {
                CountDa++;
                var grCTac = DA.GroupBy(x => x["Code"].ToString());
                foreach (var item in grCTac)
                {
                    if (lstCode.Contains(item.Key))
                        continue;
                    KLHN LKKHDay = LKKHDayAll.Where(x => x.ParentCode == item.Key).FirstOrDefault();
                    if(LKKHDay.Progress ==ProgressStateEnum.Cham)
                    {
                        Cham = true;
                        break;
                    }    
                    else if(LKKHDay.Progress == ProgressStateEnum.Nhanh)
                    {
                        Nhanh = true;
                        Dat = true;
                    }
                    else
                    {
                        Dat = true;
                        Nhanh = false;
                    }


                }
                if (Cham)
                {
                    Dic.Add(DA.Key.ToString(), "Dự án chậm");
                    TyLeCham++;
                }
                else if (Nhanh)
                {
                    Dic.Add(DA.Key.ToString(), "Dự án nhanh");
                    TyLeNhanh++;
                }
                else
                {
                    Dic.Add(DA.Key.ToString(), "Dự án đạt");
                    TyLeDat++;
                }
            }
            dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE Code NOT IN ({MyFunction.fcn_Array2listQueryCondition(lstCodeDuAn)})";
            DataTable dtDuAn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            double All = TyLeCham + TyLeNhanh + TyLeDat+ dtDuAn.Rows.Count;
            KS.Add(new KiemSoat
            {
                Ten = "Dự án nhanh",
                TyLe = TyLeNhanh / All

            });
            KS.Add(new KiemSoat
            {
                Ten = "Dự án chậm",
                TyLe = TyLeCham / All

            });
            KS.Add(new KiemSoat
            {
                Ten = "Dự án đạt",
                TyLe = TyLeDat / All

            });
            if (dtDuAn.Rows.Count != 0)
            {
                CountDa = CountDa + dtDuAn.Rows.Count;
                KS.Add(new KiemSoat
                {
                    Ten = "Dự án không thực hiện",
                    TyLe = dtDuAn.Rows.Count / All

                });
            }
            if (KS is null)
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }
            cc_BieuDoTron.DataSource = KS;
            Series landAreaSeries = cc_BieuDoTron.Series[0];
            PieSeriesView pieView = landAreaSeries.View as PieSeriesView;
            if (pieView == null) return;
            pieView.TotalLabel.Visible = true;
            pieView.TotalLabel.TextPattern = $"{CountDa} Dự án";
            WaitFormHelper.CloseWaitForm();
        }   
        public void Fcn_UpdateBarChart()
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu BIỂU ĐỒ HẰNG NGÀY");
            string dbString = $"SELECT COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn," +
                $"COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn," +
                $"cttk.* FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
$"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
$"ON cttk.CodeCongTac = dmct.Code \r\n" +
$"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
$"ON dmct.CodeHangMuc = hm.Code \r\n" +
$"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
$"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
$"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
$"ON ctrinh.CodeDuAn = da.Code \r\n" +
$"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
$"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
$"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code WHERE cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) AND cttk.CodeNhaThau IS NOT NULL";
            DataTable dtcttk = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string[] lstCode = dtcttk.AsEnumerable().Select(x => x["CodeCha"].ToString()).ToArray();
            string[] lstCodeCT = dtcttk.AsEnumerable().Where(x => !lstCode.Contains(x["Code"].ToString())).Select(x => x["Code"].ToString()).ToArray();

            List<KiemSoat> KS = new List<KiemSoat>();
            double TyLeCham = 0, TyLeNhanh = 0, TyLeDat = 0;
            bool Nhanh = false, Cham = false, Dat = false;
            var grDuAn = dtcttk.AsEnumerable().GroupBy(x => x["CodeDuAn"]);
            string [] lstCodeDuAn = grDuAn.Select(x => x.Key.ToString()).ToArray();
            int CountDa = 0;
            bool Check = false;
            List<DateTime> listOfDate = new List<DateTime>();
            for (DateTime i = de_NBDBar.DateTime.Date; i <= de_NKTBar.DateTime.Date; i = i.AddDays(1))
                listOfDate.Add(i);
            List<KLHN> LKKHDayAll = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lstCodeCT, listOfDate);
            for (DateTime i = de_NBDBar.DateTime.Date; i <= de_NKTBar.DateTime.Date; i = i.AddDays(1))
            {
                TyLeCham = 0; TyLeNhanh = 0; TyLeDat = 0;
                Nhanh = false; Cham = false; Dat = false;
                foreach (var DA in grDuAn)
                {
                    if(!Check)
                        CountDa++;
                    var grCTac = DA.GroupBy(x => x["Code"].ToString());
                    foreach (var item in grCTac)
                    {
                        if (lstCode.Contains(item.Key))
                            continue;

                        KLHN LKKHDay = LKKHDayAll.Where(x => x.ParentCode == item.Key&&x.Ngay.Date==i.Date).FirstOrDefault();

                        if (LKKHDay.Progress == ProgressStateEnum.Cham)
                        {
                            Cham = true;
                            break;
                        }
                        else if (LKKHDay.Progress == ProgressStateEnum.Nhanh)
                        {
                            Nhanh = true;
                            Dat = true;
                        }
                        else
                        {
                            Dat = true;
                            Nhanh = false;
                        }


                    }
                    if (Cham)
                        TyLeCham++;
                    else if (Nhanh)
                        TyLeNhanh++;
                    else
                        TyLeDat++;
                }
                dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} WHERE Code NOT IN ({MyFunction.fcn_Array2listQueryCondition(lstCodeDuAn)})";
                DataTable dtDuAn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (!Check)
                    CountDa=CountDa+dtDuAn.Rows.Count;
                double All = TyLeCham + TyLeNhanh + TyLeDat+ dtDuAn.Rows.Count;

                KS.Add(new KiemSoat
                {
                    Date = i.Date,
                    TyLeCham = TyLeCham / All,
                    TyLeDat = TyLeDat / All,
                    TyLeNhanh = TyLeNhanh / All,
                    TyLeKhongThucHien=dtDuAn.Rows.Count/All
                });
                Check = true;

            }
            if (KS is null)
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }
            cc_Bar.DataSource = KS;
            cc_Bar.Titles[1].Text = $"Tổng số Dự án: {CountDa} ";
            CountDuAn = CountDa;
            WaitFormHelper.CloseWaitForm();
        }

        public class KiemSoat
        {
            public string Ten { get; set; }///NHANH CHẬM ĐẠT
            public double TyLe { get; set; }
            public double TyLeNhanh { get; set; }
            public double TyLeCham { get; set; }
            public double TyLeDat { get; set; }
            public double TyLeKhongThucHien { get; set; }
            public DateTime Date { get; set; }
        }

        private void Ctrl_KiemSoatTienDoDuAn_Load(object sender, EventArgs e)
        {
            de_NKTTron.DateTime = de_NKTBar.DateTime = DateTime.Now;
            de_NBDBar.DateTime = DateTime.Now.AddDays(-4);
        }

        private void cc_BieuDoTron_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if(e.SeriesPoint.Argument.ToString()== "Dự án chậm")
            {
                e.SeriesDrawOptions.Color = Color.Red;
            }
            else if (e.SeriesPoint.Argument.ToString() == "Dự án nhanh")
            {
                e.SeriesDrawOptions.Color = Color.Aqua;
            }
            else if(e.SeriesPoint.Argument.ToString() == "Dự án đạt")
                e.SeriesDrawOptions.Color = Color.Green;
            else
                e.SeriesDrawOptions.Color = Color.Orange;
            if (e.SeriesPoint.Values[0] > 0)
            {
                e.LabelText = $"{Math.Round(e.SeriesPoint.Values[0], 3) * 100}%\r\n{Math.Ceiling(e.SeriesPoint.Values[0] * CountDuAn)}/{CountDuAn} Dự án";

            }
        }

        private void sb_UpdateBieuDoTron_Click(object sender, EventArgs e)
        {
            Fcn_UpdateBieuDoTron();
            MessageShower.ShowInformation("Cập nhập thành công!!!");
        }

        private void sb_UpdateBarChart_Click(object sender, EventArgs e)
        {
            Fcn_UpdateBarChart();
        }

        private void splitContainerControl1_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Width * 2 / 3;
        }

        private void sb_XemChiTiet_Click(object sender, EventArgs e)
        {
            IsSelect = false;
            Condition = "";
            if (!Dic.Any())
                return;
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Caption = "Lựa chọn Dự án";
            args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Yes, DialogResult.No, DialogResult.Abort, DialogResult.Cancel };
            args.Showing += Args_Showing_DuAn;
            DevExpress.XtraEditors.XtraMessageBox.Show(args);
        }
        private void Args_Showing_DuAn(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Appearance.FontStyleDelta = FontStyle.Bold;
            e.Form.Appearance.FontSizeDelta = 2;
            foreach (var control in e.Form.Controls)
            {
                SimpleButton button = control as SimpleButton;
                if (button != null)
                {
                    button.ImageOptions.SvgImageSize = new Size(16, 16);
                    switch (button.DialogResult.ToString())
                    {
                        case ("OK"):
                            button.ImageOptions.SvgImage = svgImageCollection1[0];
                            button.Text = "Dự án nhanh";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Dictionary<string, string> DicNhanh =Dic.Where(x=>x.Value=="Dự án nhanh").ToDictionary(x=>x.Key,x=>x.Value);
                                if (!DicNhanh.Any())
                                {
                                    MessageShower.ShowInformation("Không có Dự án nào đang nhanh hơn tiến độ!!!!!! ");
                                    return;
                                }
                                IsSelect = true;
                                Condition = $"da.Code IN ({MyFunction.fcn_Array2listQueryCondition(DicNhanh.Select(x => x.Key).ToArray())}) AND";
                                Condition1 = $" AND dact.Code IN ({MyFunction.fcn_Array2listQueryCondition(DicNhanh.Select(x => x.Key).ToArray())})";
                                e.Form.Close();
                            };
                            break;
                        case ("Yes"):
                            button.ImageOptions.SvgImage = svgImageCollection1[1];
                            button.Text = "Dự án chậm";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Dictionary<string, string> DicCham = Dic.Where(x => x.Value == "Dự án chậm").ToDictionary(x => x.Key, x => x.Value);
                                if (!DicCham.Any())
                                {
                                    MessageShower.ShowInformation("Không có Dự án nào đang chậm hơn tiến độ!!!!!! ");
                                    return;
                                }
                                IsSelect = true;
                                Condition = $"da.Code IN ({MyFunction.fcn_Array2listQueryCondition(DicCham.Select(x => x.Key).ToArray())}) AND";
                                Condition1 = $" AND dact.Code IN ({MyFunction.fcn_Array2listQueryCondition(DicCham.Select(x => x.Key).ToArray())})";
                                e.Form.Close();
                            };
                            break;     
                        case ("No"):
                            button.ImageOptions.SvgImage = svgImageCollection1[2];
                            button.Text = "Dự án đạt";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Dictionary<string, string> DicDat = Dic.Where(x => x.Value == "Dự án đạt").ToDictionary(x => x.Key, x => x.Value);
                                if (!DicDat.Any())
                                {
                                    MessageShower.ShowInformation("Không có Dự án nào đang đạt!!!!!! ");
                                    return;
                                }
                                IsSelect = true;
                                Condition = $"da.Code IN ({MyFunction.fcn_Array2listQueryCondition(DicDat.Select(x => x.Key).ToArray())}) AND";
                                Condition1 = $" AND dact.Code IN ({MyFunction.fcn_Array2listQueryCondition(DicDat.Select(x => x.Key).ToArray())})";
                                e.Form.Close();
                            };
                            break;                
                        case ("Abort"):
                            button.ImageOptions.SvgImage = svgImageCollection1[3];
                            button.Text = "Tất cả dự án";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                IsSelect = true;
                                Condition = "All";
                                e.Form.Close();
                            };
                            break;
                        default:
                            button.ImageOptions.SvgImage = svgImageCollection1[4];
                            button.Text = "Thoát";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) => { e.Form.Close(); };
                            break;
                    }
                }
            }
        }
        public event EventHandler Sb_XemChiTiet_Click
        {
            add
            {
                sb_XemChiTiet.Click += value;
            }
            remove
            {
                sb_XemChiTiet.Click -= value;
            }
        }

        private void cc_Bar_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (e.SeriesPoint.Values[0] > 0)
            {
                e.LabelText = $"{Math.Round(e.SeriesPoint.Values[0],3)*100}%\r\n{Math.Ceiling(e.SeriesPoint.Values[0]*CountDuAn)}/{CountDuAn}";

            }      
            //if (e.SeriesPoint.Values[1] > 0)
            //{
            //    e.LabelText = e.SeriesPoint.Values[1].ToString();

            //}     
            //if (e.SeriesPoint.Values[2] > 0)
            //{
            //    e.LabelText = e.SeriesPoint.Values[2].ToString();

            //}         
            //if (e.SeriesPoint.Values[3] > 0)
            //{
            //    e.LabelText = e.SeriesPoint.Values[3].ToString();

            //}
        }

        private void cc_Bar_BoundDataChanged(object sender, EventArgs e)
        {
            //int count = 0;
            //for (DateTime i = de_NBDBar.DateTime.Date; i <= de_NKTBar.DateTime.Date; i = i.AddDays(1))
            //{
            //    if (cc_Bar.Series[1].Points.Count != 0)
            //    {
            //        ImageAnnotation NewImage = new ImageAnnotation();
            //        NewImage.Image.Image = imageCollection1.Images[0];
            //        SeriesPoint seriesPoint = cc_Bar.Series[1].Points[count];
            //        NewImage.AnchorPoint = new SeriesPointAnchorPoint(seriesPoint);
            //        RelativePosition relativePosition = new RelativePosition();
            //        relativePosition.Angle = 90;
            //        relativePosition.ConnectorLength = 20;
            //        NewImage.ShapePosition = relativePosition;
            //        count++;
            //    }
            //}
        }
    }
}
