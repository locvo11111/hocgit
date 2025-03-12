using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class CustomRepositoryItemChartControl : DevExpress.XtraEditors.CustomEditor.RepositoryItemAnyControl
    {
        public CustomRepositoryItemChartControl() { }
        DevExpress.XtraCharts.ChartControl Chart { get { return this.Control as DevExpress.XtraCharts.ChartControl; } }
        public override DevExpress.XtraPrinting.IVisualBrick GetBrick(DevExpress.XtraEditors.PrintCellHelperInfo info)
        {
            using (var chartBrick = new DevExpress.XtraPrinting.ImageBrick())
            {
                var chartBitmap = new Bitmap(info.Rectangle.Width, info.Rectangle.Height);
                Chart.DataSource = info.EditValue;
                Chart.Size = chartBitmap.Size;

                Chart.DrawToBitmap(chartBitmap, new Rectangle(Point.Empty, chartBitmap.Size));
                chartBrick.Image = chartBitmap;
                chartBrick.SizeMode = DevExpress.XtraPrinting.ImageSizeMode.Normal;
                return chartBrick;
            }
        }
    }
}
