
namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    partial class Uc_KhoiLuongHangNgay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_KhoiLuongHangNgay));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sb_LamMoiDuLieu = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ReadExcel = new DevExpress.XtraEditors.SimpleButton();
            this.de_End = new DevExpress.XtraEditors.DateEdit();
            this.de_Begin = new DevExpress.XtraEditors.DateEdit();
            this.bt_Export = new DevExpress.XtraEditors.SimpleButton();
            this.rg_type = new DevExpress.XtraEditors.RadioGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.spsheet_TongHop = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.spreadsheetFormulaBar1 = new DevExpress.XtraSpreadsheet.SpreadsheetFormulaBar();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.de_End.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_Begin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_Begin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_LamMoiDuLieu);
            this.layoutControl1.Controls.Add(this.sb_ReadExcel);
            this.layoutControl1.Controls.Add(this.de_End);
            this.layoutControl1.Controls.Add(this.de_Begin);
            this.layoutControl1.Controls.Add(this.bt_Export);
            this.layoutControl1.Controls.Add(this.rg_type);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1013, 636);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_LamMoiDuLieu
            // 
            this.sb_LamMoiDuLieu.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.sb_LamMoiDuLieu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_LamMoiDuLieu.Appearance.Options.UseBackColor = true;
            this.sb_LamMoiDuLieu.Appearance.Options.UseFont = true;
            this.sb_LamMoiDuLieu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sb_LamMoiDuLieu.ImageOptions.Image")));
            this.sb_LamMoiDuLieu.Location = new System.Drawing.Point(659, 16);
            this.sb_LamMoiDuLieu.Name = "sb_LamMoiDuLieu";
            this.sb_LamMoiDuLieu.Size = new System.Drawing.Size(131, 36);
            this.sb_LamMoiDuLieu.StyleController = this.layoutControl1;
            this.sb_LamMoiDuLieu.TabIndex = 84;
            this.sb_LamMoiDuLieu.Text = "Làm mới dữ liệu";
            this.sb_LamMoiDuLieu.Click += new System.EventHandler(this.sb_LamMoiDuLieu_Click);
            // 
            // sb_ReadExcel
            // 
            this.sb_ReadExcel.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.sb_ReadExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_ReadExcel.Appearance.Options.UseBackColor = true;
            this.sb_ReadExcel.Appearance.Options.UseFont = true;
            this.sb_ReadExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sb_ReadExcel.ImageOptions.Image")));
            this.sb_ReadExcel.Location = new System.Drawing.Point(794, 16);
            this.sb_ReadExcel.Name = "sb_ReadExcel";
            this.sb_ReadExcel.Size = new System.Drawing.Size(104, 36);
            this.sb_ReadExcel.StyleController = this.layoutControl1;
            this.sb_ReadExcel.TabIndex = 83;
            this.sb_ReadExcel.Text = "Lưu dữ liệu";
            this.sb_ReadExcel.Click += new System.EventHandler(this.sb_ReadExcel_Click);
            // 
            // de_End
            // 
            this.de_End.EditValue = null;
            this.de_End.Enabled = false;
            this.de_End.Location = new System.Drawing.Point(541, 36);
            this.de_End.Name = "de_End";
            this.de_End.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_End.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_End.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.de_End.Size = new System.Drawing.Size(114, 20);
            this.de_End.StyleController = this.layoutControl1;
            this.de_End.TabIndex = 85;
            // 
            // de_Begin
            // 
            this.de_Begin.EditValue = null;
            this.de_Begin.Enabled = false;
            this.de_Begin.Location = new System.Drawing.Point(541, 12);
            this.de_Begin.Name = "de_Begin";
            this.de_Begin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_Begin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_Begin.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.de_Begin.Size = new System.Drawing.Size(114, 20);
            this.de_Begin.StyleController = this.layoutControl1;
            this.de_Begin.TabIndex = 84;
            // 
            // bt_Export
            // 
            this.bt_Export.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bt_Export.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_Export.Appearance.Options.UseBackColor = true;
            this.bt_Export.Appearance.Options.UseFont = true;
            this.bt_Export.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bt_Export.ImageOptions.Image")));
            this.bt_Export.Location = new System.Drawing.Point(902, 16);
            this.bt_Export.MinimumSize = new System.Drawing.Size(0, 35);
            this.bt_Export.Name = "bt_Export";
            this.bt_Export.Size = new System.Drawing.Size(99, 35);
            this.bt_Export.StyleController = this.layoutControl1;
            this.bt_Export.TabIndex = 6;
            this.bt_Export.Text = "Xuất báo cáo";
            this.bt_Export.Click += new System.EventHandler(this.bt_Export_Click);
            // 
            // rg_type
            // 
            this.rg_type.Location = new System.Drawing.Point(12, 12);
            this.rg_type.Name = "rg_type";
            this.rg_type.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.rg_type.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.rg_type.Properties.Appearance.Options.UseBackColor = true;
            this.rg_type.Properties.Appearance.Options.UseFont = true;
            this.rg_type.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Công tác", true, null, "CongTac"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Vật tư", true, null, "VatTu")});
            this.rg_type.Size = new System.Drawing.Size(435, 44);
            this.rg_type.StyleController = this.layoutControl1;
            this.rg_type.TabIndex = 9;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.spsheet_TongHop);
            this.panelControl1.Controls.Add(this.splitterControl1);
            this.panelControl1.Controls.Add(this.spreadsheetFormulaBar1);
            this.panelControl1.Location = new System.Drawing.Point(12, 60);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(989, 564);
            this.panelControl1.TabIndex = 4;
            // 
            // spsheet_TongHop
            // 
            this.spsheet_TongHop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spsheet_TongHop.Location = new System.Drawing.Point(2, 36);
            this.spsheet_TongHop.Name = "spsheet_TongHop";
            this.spsheet_TongHop.Options.Culture = new System.Globalization.CultureInfo("vi-VN");
            this.spsheet_TongHop.Options.TabSelector.Visibility = DevExpress.XtraSpreadsheet.SpreadsheetElementVisibility.Hidden;
            this.spsheet_TongHop.Size = new System.Drawing.Size(985, 526);
            this.spsheet_TongHop.TabIndex = 0;
            this.spsheet_TongHop.Text = "spreadsheetControl1";
            this.spsheet_TongHop.PopupMenuShowing += new DevExpress.XtraSpreadsheet.PopupMenuShowingEventHandler(this.spsheet_TongHop_PopupMenuShowing);
            this.spsheet_TongHop.CellBeginEdit += new DevExpress.XtraSpreadsheet.CellBeginEditEventHandler(this.spsheet_TongHop_CellBeginEdit);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(2, 26);
            this.splitterControl1.MinSize = 20;
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(985, 10);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // spreadsheetFormulaBar1
            // 
            this.spreadsheetFormulaBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.spreadsheetFormulaBar1.Location = new System.Drawing.Point(2, 2);
            this.spreadsheetFormulaBar1.MinimumSize = new System.Drawing.Size(0, 24);
            this.spreadsheetFormulaBar1.Name = "spreadsheetFormulaBar1";
            this.spreadsheetFormulaBar1.Size = new System.Drawing.Size(985, 24);
            this.spreadsheetFormulaBar1.SpreadsheetControl = this.spsheet_TongHop;
            this.spreadsheetFormulaBar1.TabIndex = 2;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem10,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1013, 636);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.rg_type;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(54, 38);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(439, 48);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(993, 568);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem10.Control = this.bt_Export;
            this.layoutControlItem10.Location = new System.Drawing.Point(890, 0);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(103, 0);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(103, 39);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(103, 48);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.de_Begin;
            this.layoutControlItem4.Location = new System.Drawing.Point(439, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(208, 24);
            this.layoutControlItem4.Text = "Ngày bắt đầu";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.de_End;
            this.layoutControlItem5.Location = new System.Drawing.Point(439, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(208, 24);
            this.layoutControlItem5.Text = "Ngày kết thúc";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem2.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem2.Control = this.sb_ReadExcel;
            this.layoutControlItem2.Location = new System.Drawing.Point(782, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(108, 48);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem6.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem6.Control = this.sb_LamMoiDuLieu;
            this.layoutControlItem6.Location = new System.Drawing.Point(647, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(135, 48);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // Uc_KhoiLuongHangNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "Uc_KhoiLuongHangNgay";
            this.Size = new System.Drawing.Size(1013, 636);
            this.Load += new System.EventHandler(this.Uc_KhoiLuongHangNgay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.de_End.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_End.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_Begin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_Begin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit de_End;
        private DevExpress.XtraEditors.DateEdit de_Begin;
        private DevExpress.XtraEditors.SimpleButton sb_ReadExcel;
        private DevExpress.XtraEditors.SimpleButton bt_Export;
        private DevExpress.XtraEditors.RadioGroup rg_type;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spsheet_TongHop;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraSpreadsheet.SpreadsheetFormulaBar spreadsheetFormulaBar1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sb_LamMoiDuLieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
