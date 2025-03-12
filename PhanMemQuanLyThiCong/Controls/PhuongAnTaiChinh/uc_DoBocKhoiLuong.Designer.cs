namespace PhanMemQuanLyThiCong.Controls.PhuongAnTaiChinh
{
    partial class uc_DoBocKhoiLuong
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtDonVi = new DevExpress.XtraEditors.TextEdit();
            this.bt_ok = new DevExpress.XtraEditors.SimpleButton();
            this.bt_huy = new DevExpress.XtraEditors.SimpleButton();
            this.lb_TenCongTac = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.spSheetDoBoc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.spreadsheetFormulaBar2 = new DevExpress.XtraSpreadsheet.SpreadsheetFormulaBar();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonVi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.txtDonVi);
            this.layoutControl1.Controls.Add(this.bt_ok);
            this.layoutControl1.Controls.Add(this.bt_huy);
            this.layoutControl1.Controls.Add(this.lb_TenCongTac);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(841, 220);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtDonVi
            // 
            this.txtDonVi.Location = new System.Drawing.Point(47, 21);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.Size = new System.Drawing.Size(123, 20);
            this.txtDonVi.StyleController = this.layoutControl1;
            this.txtDonVi.TabIndex = 11;
            // 
            // bt_ok
            // 
            this.bt_ok.Appearance.BackColor = System.Drawing.Color.Blue;
            this.bt_ok.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_ok.Appearance.Options.UseBackColor = true;
            this.bt_ok.Appearance.Options.UseFont = true;
            this.bt_ok.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.apply_16x1610;
            this.bt_ok.Location = new System.Drawing.Point(675, 194);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(93, 22);
            this.bt_ok.StyleController = this.layoutControl1;
            this.bt_ok.TabIndex = 10;
            this.bt_ok.Text = "Đồng ý";
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // bt_huy
            // 
            this.bt_huy.Appearance.BackColor = System.Drawing.Color.Red;
            this.bt_huy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_huy.Appearance.Options.UseBackColor = true;
            this.bt_huy.Appearance.Options.UseFont = true;
            this.bt_huy.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.cancel_16x1610;
            this.bt_huy.Location = new System.Drawing.Point(772, 194);
            this.bt_huy.Name = "bt_huy";
            this.bt_huy.Size = new System.Drawing.Size(65, 22);
            this.bt_huy.StyleController = this.layoutControl1;
            this.bt_huy.TabIndex = 9;
            this.bt_huy.Text = "Hủy";
            this.bt_huy.Click += new System.EventHandler(this.bt_huy_Click);
            // 
            // lb_TenCongTac
            // 
            this.lb_TenCongTac.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lb_TenCongTac.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lb_TenCongTac.Appearance.Options.UseFont = true;
            this.lb_TenCongTac.Appearance.Options.UseForeColor = true;
            this.lb_TenCongTac.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lb_TenCongTac.Location = new System.Drawing.Point(4, 4);
            this.lb_TenCongTac.Name = "lb_TenCongTac";
            this.lb_TenCongTac.Size = new System.Drawing.Size(833, 13);
            this.lb_TenCongTac.StyleController = this.layoutControl1;
            this.lb_TenCongTac.TabIndex = 5;
            this.lb_TenCongTac.Text = "labelControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.emptySpaceItem2,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(841, 220);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lb_TenCongTac;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(837, 17);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_ok;
            this.layoutControlItem3.Location = new System.Drawing.Point(671, 190);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(97, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(97, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(97, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.bt_huy;
            this.layoutControlItem4.Location = new System.Drawing.Point(768, 190);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 190);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(671, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtDonVi;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 17);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(170, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(170, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(170, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Đơn vị";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(31, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(170, 17);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(667, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.spSheetDoBoc);
            this.panelControl1.Controls.Add(this.splitterControl1);
            this.panelControl1.Controls.Add(this.spreadsheetFormulaBar2);
            this.panelControl1.Location = new System.Drawing.Point(4, 45);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(833, 145);
            this.panelControl1.TabIndex = 14;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.panelControl1;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 41);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(837, 149);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // spSheetDoBoc
            // 
            this.spSheetDoBoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spSheetDoBoc.Location = new System.Drawing.Point(2, 36);
            this.spSheetDoBoc.Name = "spSheetDoBoc";
            this.spSheetDoBoc.Options.Behavior.Save = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
            this.spSheetDoBoc.Options.Behavior.SaveAs = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
            this.spSheetDoBoc.Options.HorizontalScrollbar.Visibility = DevExpress.XtraSpreadsheet.SpreadsheetScrollbarVisibility.Hidden;
            this.spSheetDoBoc.Options.TabSelector.Visibility = DevExpress.XtraSpreadsheet.SpreadsheetElementVisibility.Hidden;
            this.spSheetDoBoc.Options.VerticalScrollbar.Visibility = DevExpress.XtraSpreadsheet.SpreadsheetScrollbarVisibility.Hidden;
            this.spSheetDoBoc.Size = new System.Drawing.Size(829, 107);
            this.spSheetDoBoc.TabIndex = 0;
            this.spSheetDoBoc.Text = "spreadsheetControl1";
            // 
            // spreadsheetFormulaBar2
            // 
            this.spreadsheetFormulaBar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.spreadsheetFormulaBar2.Location = new System.Drawing.Point(2, 2);
            this.spreadsheetFormulaBar2.MinimumSize = new System.Drawing.Size(0, 24);
            this.spreadsheetFormulaBar2.Name = "spreadsheetFormulaBar2";
            this.spreadsheetFormulaBar2.Size = new System.Drawing.Size(829, 24);
            this.spreadsheetFormulaBar2.SpreadsheetControl = this.spSheetDoBoc;
            this.spreadsheetFormulaBar2.TabIndex = 2;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(2, 26);
            this.splitterControl1.MinSize = 20;
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(829, 10);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // uc_DoBocKhoiLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "uc_DoBocKhoiLuong";
            this.Size = new System.Drawing.Size(841, 220);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDonVi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LabelControl lb_TenCongTac;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton bt_ok;
        private DevExpress.XtraEditors.SimpleButton bt_huy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit txtDonVi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spSheetDoBoc;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraSpreadsheet.SpreadsheetFormulaBar spreadsheetFormulaBar2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}
