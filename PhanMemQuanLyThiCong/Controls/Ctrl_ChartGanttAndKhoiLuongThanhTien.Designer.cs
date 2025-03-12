
namespace PhanMemQuanLyThiCong.Controls
{
    partial class Ctrl_ChartGanttAndKhoiLuongThanhTien
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.GanttDiagram ganttDiagram2 = new DevExpress.XtraCharts.GanttDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SeriesPoint seriesPoint3 = new DevExpress.XtraCharts.SeriesPoint("Task 1", new object[] {
            ((object)(new System.DateTime(2022, 9, 1, 0, 0, 0, 0))),
            ((object)(new System.DateTime(2022, 9, 17, 0, 0, 0, 0)))}, 0);
            DevExpress.XtraCharts.SideBySideGanttSeriesView sideBySideGanttSeriesView3 = new DevExpress.XtraCharts.SideBySideGanttSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SeriesPoint seriesPoint4 = new DevExpress.XtraCharts.SeriesPoint("Task 1", new object[] {
            ((object)(new System.DateTime(2022, 9, 9, 0, 0, 0, 0))),
            ((object)(new System.DateTime(2022, 9, 24, 0, 0, 0, 0)))}, 0);
            DevExpress.XtraCharts.SideBySideGanttSeriesView sideBySideGanttSeriesView4 = new DevExpress.XtraCharts.SideBySideGanttSeriesView();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tab_TienDo = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.cc_Gantt = new DevExpress.XtraCharts.ChartControl();
            this.tab_KhoiLuongThanhTien = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.ctrl_ChartKhoiLuongThanhTien1 = new PhanMemQuanLyThiCong.Controls.Ctrl_ChartKhoiLuongThanhTien();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbb_VatTu = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tab_TienDo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cc_Gantt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ganttDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideGanttSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideGanttSeriesView4)).BeginInit();
            this.tab_KhoiLuongThanhTien.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPane1
            // 
            this.tabPane1.Controls.Add(this.tab_TienDo);
            this.tabPane1.Controls.Add(this.tab_KhoiLuongThanhTien);
            this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPane1.Location = new System.Drawing.Point(0, 0);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tab_TienDo,
            this.tab_KhoiLuongThanhTien});
            this.tabPane1.RegularSize = new System.Drawing.Size(1089, 600);
            this.tabPane1.SelectedPage = this.tab_TienDo;
            this.tabPane1.Size = new System.Drawing.Size(1089, 600);
            this.tabPane1.TabIndex = 8;
            this.tabPane1.Text = "tabPane1";
            // 
            // tab_TienDo
            // 
            this.tab_TienDo.AccessibleDescription = "sfg";
            this.tab_TienDo.AccessibleName = "sfg";
            this.tab_TienDo.Caption = "Tiến độ";
            this.tab_TienDo.Controls.Add(this.cc_Gantt);
            this.tab_TienDo.Controls.Add(this.panelControl1);
            this.tab_TienDo.Name = "tab_TienDo";
            this.tab_TienDo.Size = new System.Drawing.Size(639, 336);
            // 
            // cc_Gantt
            // 
            ganttDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            ganttDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            ganttDiagram2.EnableAxisYScrolling = true;
            ganttDiagram2.EnableAxisYZooming = true;
            this.cc_Gantt.Diagram = ganttDiagram2;
            this.cc_Gantt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cc_Gantt.Location = new System.Drawing.Point(0, 0);
            this.cc_Gantt.Name = "cc_Gantt";
            series3.ArgumentDataMember = "VatTu";
            series3.Name = "Kế hoạch";
            series3.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] {
            seriesPoint3});
            series3.ValueDataMembersSerializable = "NgayBatDau;NgayKetThuc";
            series3.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series3.View = sideBySideGanttSeriesView3;
            series4.ArgumentDataMember = "VatTu";
            series4.Name = "Thi công";
            series4.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] {
            seriesPoint4});
            series4.ValueDataMembersSerializable = "NgayBatDauThiCong;NgayKetThucThiCong";
            series4.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series4.View = sideBySideGanttSeriesView4;
            this.cc_Gantt.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
            this.cc_Gantt.Size = new System.Drawing.Size(639, 303);
            this.cc_Gantt.TabIndex = 0;
            // 
            // tab_KhoiLuongThanhTien
            // 
            this.tab_KhoiLuongThanhTien.Caption = "Khối lượng thành tiền";
            this.tab_KhoiLuongThanhTien.Controls.Add(this.ctrl_ChartKhoiLuongThanhTien1);
            this.tab_KhoiLuongThanhTien.Controls.Add(this.panel1);
            this.tab_KhoiLuongThanhTien.Controls.Add(this.comboBox1);
            this.tab_KhoiLuongThanhTien.Name = "tab_KhoiLuongThanhTien";
            this.tab_KhoiLuongThanhTien.Size = new System.Drawing.Size(1089, 567);
            // 
            // ctrl_ChartKhoiLuongThanhTien1
            // 
            this.ctrl_ChartKhoiLuongThanhTien1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl_ChartKhoiLuongThanhTien1.Location = new System.Drawing.Point(0, 0);
            this.ctrl_ChartKhoiLuongThanhTien1.Name = "ctrl_ChartKhoiLuongThanhTien1";
            this.ctrl_ChartKhoiLuongThanhTien1.Size = new System.Drawing.Size(1089, 526);
            this.ctrl_ChartKhoiLuongThanhTien1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbb_VatTu);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 526);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1089, 41);
            this.panel1.TabIndex = 4;
            // 
            // cbb_VatTu
            // 
            this.cbb_VatTu.DisplayMember = "Value";
            this.cbb_VatTu.FormattingEnabled = true;
            this.cbb_VatTu.Location = new System.Drawing.Point(204, 7);
            this.cbb_VatTu.Name = "cbb_VatTu";
            this.cbb_VatTu.Size = new System.Drawing.Size(265, 21);
            this.cbb_VatTu.TabIndex = 1;
            this.cbb_VatTu.ValueMember = "Key";
            this.cbb_VatTu.SelectedIndexChanged += new System.EventHandler(this.cbb_VatTu_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vật tư";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(433, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.searchLookUpEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 303);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(639, 33);
            this.panelControl1.TabIndex = 1;
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(74, 6);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(183, 20);
            this.searchLookUpEdit1.TabIndex = 0;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "labelControl1";
            // 
            // Ctrl_ChartGanttAndKhoiLuongThanhTien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabPane1);
            this.Name = "Ctrl_ChartGanttAndKhoiLuongThanhTien";
            this.Size = new System.Drawing.Size(1089, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tab_TienDo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(ganttDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideGanttSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideGanttSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cc_Gantt)).EndInit();
            this.tab_KhoiLuongThanhTien.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tab_TienDo;
        private DevExpress.XtraCharts.ChartControl cc_Gantt;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tab_KhoiLuongThanhTien;
        private Ctrl_ChartKhoiLuongThanhTien ctrl_ChartKhoiLuongThanhTien1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbb_VatTu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
    }
}
