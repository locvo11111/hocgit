using DevExpress.Utils.Extensions;
using DevExpress.XtraCharts;
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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Ctrl_ChartKhoiLuongThanhTien : UserControl
    {
        DataTable _dt;
        public Ctrl_ChartKhoiLuongThanhTien() //dt gồm các cột: Ngay, KhoiLuongKeHoach, ThanhTienKeHoach, KhoiLuongThiCong, ThanhTienThiCong
        {
            InitializeComponent();
        }

        public List<Chart_KhoiLuongThanhTien> DataSource
        {
            get
            {
                return cc_VatTu.DataSource as List<Chart_KhoiLuongThanhTien>;
            }
            set
            {
                cc_VatTu.DataSource = value;
            }

        }
        public List<Chart_KhoiLuongThanhTien> PushData(List<KLHN> dt, int Ngay,bool IsGiaoThau)
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
                long ThanhTienTC = (long)item.Sum(x => x.ThanhTienThiCong);
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
                        cc_VatTu.DataSource = lsOutCondition;
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
                        cc_VatTu.DataSource = lsOutCondition;
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
            cc_VatTu.DataSource = lsOutCondition;
            return lsOutCondition;

        }
        public List<Chart_KhoiLuongThanhTien> PushData(List<KLHN> dt,bool IsGiauThau)
        {
            if (dt.Count() == 0)
            {
                cc_VatTu.DataSource = null;
                return new List<Chart_KhoiLuongThanhTien>();
            }
            List<Chart_KhoiLuongThanhTien> lsOut = new List<Chart_KhoiLuongThanhTien>();
            var grsDate = dt.GroupBy(x => x.Ngay).OrderBy(x => x.Key);
            long luyKeTTKeHoach = 0;
            long luyKeTTThiCong = 0;
            foreach (var item in grsDate)
            {
                long ThanhTienKH = (long)item.Sum(x => x.ThanhTienKeHoach);
                long ThanhTienTC = IsGiauThau?(long)item.Sum(x => x.ThanhTienThiCong):(long)item.Sum(x => x.ThanhTienThiCong);
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
            cc_VatTu.DataSource = lsOut;
            return lsOut;
        }
        private Image GetChartImage(ChartControl chart, ImageFormat format)
        {
            // Create an image. 
            Image image = null;

            // Create an image of the chart. 
            using (MemoryStream s = new MemoryStream())
            {
                chart.ExportToImage(s, format);
                image = Image.FromStream(s);
            }

            // Return the image. 
            return image;
        }
        private void SaveChartImageToFile(ChartControl chart, ImageFormat format, String fileName)
        {
            // Create an image in the specified format from the chart 
            // and save it to the specified path. 
            chart.ExportToImage(fileName, format);
        }
    }
}
