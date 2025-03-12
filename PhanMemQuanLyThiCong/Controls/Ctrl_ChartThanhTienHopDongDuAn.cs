using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.CustomEditor;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.HopDong;
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

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Ctrl_ChartThanhTienHopDongDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        static Dictionary<string, List<Infor_HopDong>> dicBieuDo = new Dictionary<string, List<Infor_HopDong>>();
        DataTable _dt;
        public Ctrl_ChartThanhTienHopDongDuAn()
        {
            InitializeComponent();
        }
        public void Fcn_LoadData()
        {
            RepositoryItemAnyControl item = new RepositoryItemAnyControl();
            item.Control = cc_HopDong;

            ColChart.View.GridControl.RepositoryItems.Add(item);

            ColChart.ColumnEdit = item;
            //dicBieuDo.Clear();
            gv_BieuDoTaiChinh.RowHeight = 100;

        }
        static string newCodeDuAn = "";
        public void FcnLoadDataGrid(TypeDVTH typeDVTH)
        {
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];
            string dbString = $"SELECT \"Code\",\"Ten\" FROM {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"CodeDuAn\"<>\"Code\"";
            if (typeDVTH == TypeDVTH.TuThucHien)
                dbString = $"SELECT \"Code\",\"Ten\" FROM {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"CodeTongThau\" IS NOT NULL";
            else if(typeDVTH == TypeDVTH.NhaThauPhu)
                dbString = $"SELECT \"Code\",\"Ten\" FROM {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' AND \"CodeTongThau\" IS NULL";
            else if (colFk == "DuAn")
                dbString = $"SELECT \"Code\",\"TenDuAn\" as Ten FROM {tbl} WHERE \"Code\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt_NhaThau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dt_NhaThau.Columns.Add("Thu", typeof(long));
            dt_NhaThau.Columns.Add("Chi", typeof(long));
            foreach (DataRow item in dt_NhaThau.Rows)
            {
                List<Infor_HopDong> HD = Fcn_Update(item["Code"].ToString(),typeDVTH);
                if (typeDVTH == TypeDVTH.DuAn)
                    item["Code"] = newCodeDuAn;
                if (dicBieuDo.Keys.Contains(item["Code"].ToString()))
                {
                    dicBieuDo.Remove(item["Code"].ToString());
                    dicBieuDo.Add(item["Code"].ToString(), HD);
                }
                else
                    dicBieuDo.Add(item["Code"].ToString(), HD);
            }
            gc_BieuDoTaiChinh.DataSource = dt_NhaThau;
            gv_BieuDoTaiChinh.ExpandAllGroups();
            Fcn_LoadData();
        }
        private List<Infor_HopDong> Fcn_Update(string CodeDVTH, TypeDVTH typeDVTH)
        {
            string dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} WHERE \"CodeDonViThucHien\"='{CodeDVTH}'";
            DataTable dt_HopDong = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<Infor_HopDong> HD = DuAnHelper.ConvertToList<Infor_HopDong>(dt_HopDong);
            List<Infor_HopDong> HDHT = HD;
            HDHT.ForEach(x => x.NgayHoanThanh = DateTime.Now);
            DataTable Dt_HD = new DataTable();
            if(typeDVTH== TypeDVTH.NhaCungCap)
            {
                foreach (Infor_HopDong item in HDHT)
                {
                    dbString = $"SELECT \"Code\" FROM {QLVT.TBL_QLVT_YEUCAUVT} WHERE \"CodeHd\" = '{item.Code}'";
                    Dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    string lstCode = MyFunction.fcn_Array2listQueryCondition(Dt_HD.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                    dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVTKLHN} WHERE \"CodeCha\" IN ({lstCode})";
                    DataTable Dt_KL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (Dt_KL.Rows.Count == 0)
                    {
                        item.NgayBatDau = item.NgayHoanThanh = DateTime.Now;
                    }
                    else
                    {
                        item.NgayBatDau = Dt_KL.AsEnumerable().Min(x => DateTime.Parse(x["Ngay"].ToString()));
                        item.NgayHoanThanh = Dt_KL.AsEnumerable().Max(x => DateTime.Parse(x["Ngay"].ToString()));
                    }
                }
            }
            else
            {
                foreach (Infor_HopDong item in HDHT)
                {
                    dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                   $"INNER JOIN {MyConstant.TBL_HopDong_PhuLuc} " +
                   $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                   $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
                   $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl = {MyConstant.TBL_ThongtinphulucHD}.Code " +
                   $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
                   $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd = {MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
                   $"WHERE {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong='{item.Code}' " +
                   $"AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
                    Dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if(typeDVTH== TypeDVTH.NhaThau)
                    {
                        string lstCode = MyFunction.fcn_Array2listQueryCondition(Dt_HD.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray());
                        dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeCongTac\" IN ({lstCode}) AND \"CodeNhaThau\" IS  NULL ";
                        DataTable Dt_CTtheoCK = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        lstCode = MyFunction.fcn_Array2listQueryCondition(Dt_CTtheoCK.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                        dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" IN ({lstCode}) AND \"KhoiLuongThiCong\" IS NOT NULL ";
                    }
                    else
                    {
                        string lstCode = MyFunction.fcn_Array2listQueryCondition(Dt_HD.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                        dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" IN ({lstCode}) AND \"KhoiLuongThiCong\" IS NOT NULL ";
                    }
                    DataTable Dt_KL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (Dt_KL.Rows.Count == 0)
                    {
                        item.NgayBatDau = item.NgayHoanThanh = DateTime.Now;
                    }
                    else
                    {
                        item.NgayBatDau = Dt_KL.AsEnumerable().Min(x => DateTime.Parse(x["Ngay"].ToString()));
                        item.NgayHoanThanh = Dt_KL.AsEnumerable().Max(x => DateTime.Parse(x["Ngay"].ToString()));
                    }
                }
            }

            GanttDiagram diagram = (GanttDiagram)cc_HopDong.Diagram;
            diagram.AxisY.ConstantLines[0].AxisValue = DateTime.Now;
            cc_HopDong.Series[0].DataSource = HD;
            cc_HopDong.Series[1].DataSource = HDHT;
            return HD;
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
        public Series PlannedSeries
        {
            get { return cc_HopDong.GetSeriesByName("Kế hoạch"); }
        }
        public Series CompletedSeries
        {
            get { return cc_HopDong.GetSeriesByName("Hoàn thành"); }
        }
        public GanttDiagram Diagram
        {
            get { return cc_HopDong != null ? cc_HopDong.Diagram as GanttDiagram : null; }
        }
        public ConstantLine ProgressLine
        {
            get { return Diagram.AxisY.ConstantLines[0]; }
        }
        public void SetProgressState(DateTime dateTimeValue)
        {
            List<Infor_HopDong> HD = cc_HopDong.Series[0].DataSource as List<Infor_HopDong>;
            DateTime rightAxisLimit = HD.Max(x => x.NgayKetThuc);
            if (dateTimeValue > rightAxisLimit)
                dateTimeValue = rightAxisLimit;
            if (CompletedSeries != null && PlannedSeries != null)
            {
                CompletedSeries.Points.BeginUpdate();
                CompletedSeries.Points.Clear();
                foreach (SeriesPoint point in PlannedSeries.Points)
                {
                    DateTime plannedStartDate = point.DateTimeValues[0];
                    if (DateTime.Compare(plannedStartDate, dateTimeValue) >= 0)
                        continue;
                    DateTime plannedFinishDate = point.DateTimeValues[1];
                    DateTime completedFinishDate;
                    if (DateTime.Compare(dateTimeValue, plannedFinishDate) > 0)
                        completedFinishDate = plannedFinishDate;
                    else
                        completedFinishDate = dateTimeValue;
                    CompletedSeries.Points.Add(new SeriesPoint(point.Argument, new DateTime[] { plannedStartDate, completedFinishDate }));
                }
                CompletedSeries.Points.EndUpdate();
            }
            if (HasConstantLine)
                ProgressLine.AxisValue = dateTimeValue;
        }
        public bool HasConstantLine
        {
            get { return Diagram != null && Diagram.AxisY.ConstantLines.Count > 0; }
        }
        private void cc_HopDong_ConstantLineMoved(object sender, DevExpress.XtraCharts.ConstantLineMovedEventArgs e)
        {
            SetProgressState((DateTime)e.ConstantLine.AxisValue);
        }
        [DisplayName("Visible")]
        [Category("Legend")]
        public DefaultBoolean Legend
        {
            get
            {
                return cc_HopDong.Legend.Visibility;
            }

            set
            {
                cc_HopDong.Legend.Visibility = value;
            }
        }
    }
}
