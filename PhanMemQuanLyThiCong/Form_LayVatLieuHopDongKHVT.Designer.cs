
namespace PhanMemQuanLyThiCong
{
    partial class Form_LayVatLieuHopDongKHVT
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_LayVatLieuHopDongKHVT));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.rg_Select = new DevExpress.XtraEditors.RadioGroup();
            this.lue_HangMuc = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lUE_ToChucCaNhan = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sB_ok = new DevExpress.XtraEditors.SimpleButton();
            this.sB_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sB_All = new DevExpress.XtraEditors.SimpleButton();
            this.gc_TimKiemVatLieu = new DevExpress.XtraGrid.GridControl();
            this.gv_TimKiemYeuCau = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rg_Select.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_HangMuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lUE_ToChucCaNhan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_TimKiemVatLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_TimKiemYeuCau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.rg_Select);
            this.panelControl1.Controls.Add(this.lue_HangMuc);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.lUE_ToChucCaNhan);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(317, 547);
            this.panelControl1.TabIndex = 20;
            // 
            // rg_Select
            // 
            this.rg_Select.Location = new System.Drawing.Point(5, 136);
            this.rg_Select.Name = "rg_Select";
            this.rg_Select.Properties.Columns = 1;
            this.rg_Select.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lấy theo Định mức"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lấy theo Kế hoạch")});
            this.rg_Select.Size = new System.Drawing.Size(312, 49);
            this.rg_Select.TabIndex = 93;
            // 
            // lue_HangMuc
            // 
            this.lue_HangMuc.Location = new System.Drawing.Point(5, 91);
            this.lue_HangMuc.Name = "lue_HangMuc";
            this.lue_HangMuc.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lue_HangMuc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lue_HangMuc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", "Code", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên hạng mục")});
            this.lue_HangMuc.Properties.DisplayMember = "Ten";
            this.lue_HangMuc.Properties.NullText = "";
            this.lue_HangMuc.Properties.ValueMember = "Code";
            this.lue_HangMuc.Size = new System.Drawing.Size(312, 20);
            this.lue_HangMuc.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(5, 69);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 16);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Hạng mục";
            // 
            // lUE_ToChucCaNhan
            // 
            this.lUE_ToChucCaNhan.Location = new System.Drawing.Point(5, 29);
            this.lUE_ToChucCaNhan.Name = "lUE_ToChucCaNhan";
            this.lUE_ToChucCaNhan.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lUE_ToChucCaNhan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lUE_ToChucCaNhan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "CodeCT", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CongTrinh", "Tên công trình")});
            this.lUE_ToChucCaNhan.Properties.DisplayMember = "CongTrinh";
            this.lUE_ToChucCaNhan.Properties.NullText = "";
            this.lUE_ToChucCaNhan.Properties.ValueMember = "ID";
            this.lUE_ToChucCaNhan.Size = new System.Drawing.Size(312, 20);
            this.lUE_ToChucCaNhan.TabIndex = 9;
            this.lUE_ToChucCaNhan.EditValueChanged += new System.EventHandler(this.lUE_ToChucCaNhan_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(5, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 16);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Công trình";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sB_ok);
            this.layoutControl1.Controls.Add(this.sB_Cancel);
            this.layoutControl1.Controls.Add(this.sB_All);
            this.layoutControl1.Controls.Add(this.gc_TimKiemVatLieu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(317, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(945, 547);
            this.layoutControl1.TabIndex = 21;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sB_ok
            // 
            this.sB_ok.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sB_ok.Appearance.Options.UseBackColor = true;
            this.sB_ok.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sB_ok.ImageOptions.SvgImage")));
            this.sB_ok.Location = new System.Drawing.Point(12, 499);
            this.sB_ok.Name = "sB_ok";
            this.sB_ok.Size = new System.Drawing.Size(305, 36);
            this.sB_ok.StyleController = this.layoutControl1;
            this.sB_ok.TabIndex = 8;
            this.sB_ok.Text = "Đồng ý";
            this.sB_ok.Click += new System.EventHandler(this.sB_ok_Click);
            // 
            // sB_Cancel
            // 
            this.sB_Cancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sB_Cancel.Appearance.Options.UseBackColor = true;
            this.sB_Cancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sB_Cancel.ImageOptions.SvgImage")));
            this.sB_Cancel.Location = new System.Drawing.Point(629, 499);
            this.sB_Cancel.Name = "sB_Cancel";
            this.sB_Cancel.Size = new System.Drawing.Size(304, 36);
            this.sB_Cancel.StyleController = this.layoutControl1;
            this.sB_Cancel.TabIndex = 7;
            this.sB_Cancel.Text = "Hủy chọn";
            this.sB_Cancel.Click += new System.EventHandler(this.sB_Cancel_Click);
            // 
            // sB_All
            // 
            this.sB_All.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.sB_All.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.sB_All.Appearance.Options.UseBackColor = true;
            this.sB_All.Appearance.Options.UseFont = true;
            this.sB_All.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sB_All.ImageOptions.SvgImage")));
            this.sB_All.Location = new System.Drawing.Point(321, 499);
            this.sB_All.Name = "sB_All";
            this.sB_All.Size = new System.Drawing.Size(304, 36);
            this.sB_All.StyleController = this.layoutControl1;
            this.sB_All.TabIndex = 9;
            this.sB_All.Text = "Chọn tất cả";
            this.sB_All.Click += new System.EventHandler(this.sB_All_Click);
            // 
            // gc_TimKiemVatLieu
            // 
            this.gc_TimKiemVatLieu.Location = new System.Drawing.Point(12, 12);
            this.gc_TimKiemVatLieu.MainView = this.gv_TimKiemYeuCau;
            this.gc_TimKiemVatLieu.Name = "gc_TimKiemVatLieu";
            this.gc_TimKiemVatLieu.Size = new System.Drawing.Size(921, 483);
            this.gc_TimKiemVatLieu.TabIndex = 4;
            this.gc_TimKiemVatLieu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_TimKiemYeuCau});
            // 
            // gv_TimKiemYeuCau
            // 
            this.gv_TimKiemYeuCau.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn1,
            this.gridColumn7,
            this.gridColumn8});
            this.gv_TimKiemYeuCau.GridControl = this.gc_TimKiemVatLieu;
            this.gv_TimKiemYeuCau.Name = "gv_TimKiemYeuCau";
            this.gv_TimKiemYeuCau.OptionsView.ColumnAutoWidth = false;
            this.gv_TimKiemYeuCau.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mã vật tư";
            this.gridColumn2.FieldName = "MaVatLieu";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 111;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên vật tư";
            this.gridColumn3.FieldName = "VatTu";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 382;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Khối lượng định mức";
            this.gridColumn4.DisplayFormat.FormatString = "n2";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "KhoiLuongDinhMuc";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 116;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Đơn giá định mức";
            this.gridColumn5.DisplayFormat.FormatString = "n2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "DonGiaGoc";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 106;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Chọn";
            this.gridColumn6.FieldName = "Chon";
            this.gridColumn6.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.gridColumn6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("gridColumn6.ImageOptions.Image")));
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Đơn vị";
            this.gridColumn1.FieldName = "DonVi";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Khối lượng kế hoạch";
            this.gridColumn7.DisplayFormat.FormatString = "n2";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "KhoiLuongKeHoach";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 112;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Đơn giá kế hoạch";
            this.gridColumn8.DisplayFormat.FormatString = "n2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "DonGiaKeHoach";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 110;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(945, 547);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gc_TimKiemVatLieu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(925, 487);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sB_Cancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(617, 487);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(308, 40);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sB_All;
            this.layoutControlItem2.Location = new System.Drawing.Point(309, 487);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(308, 40);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sB_ok;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 487);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(309, 40);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Form_LayVatLieuHopDongKHVT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 547);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "Form_LayVatLieuHopDongKHVT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lấy vật liệu KHVT";
            this.Load += new System.EventHandler(this.Form_LayVatLieuHopDongKHVT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rg_Select.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_HangMuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lUE_ToChucCaNhan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_TimKiemVatLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_TimKiemYeuCau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.RadioGroup rg_Select;
        private DevExpress.XtraEditors.LookUpEdit lue_HangMuc;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lUE_ToChucCaNhan;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sB_ok;
        private DevExpress.XtraEditors.SimpleButton sB_Cancel;
        private DevExpress.XtraEditors.SimpleButton sB_All;
        private DevExpress.XtraGrid.GridControl gc_TimKiemVatLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_TimKiemYeuCau;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}