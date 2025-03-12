
namespace PhanMemQuanLyThiCong
{
    partial class FormLayVatLieuHopDong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLayVatLieuHopDong));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.rg_DonGia = new DevExpress.XtraEditors.RadioGroup();
            this.ce_haophi = new DevExpress.XtraEditors.CheckEdit();
            this.ce_Loc = new DevExpress.XtraEditors.CheckEdit();
            this.rg_Select = new DevExpress.XtraEditors.RadioGroup();
            this.de_begin = new DevExpress.XtraEditors.DateEdit();
            this.de_end = new DevExpress.XtraEditors.DateEdit();
            this.ctrl_DonViThucHienDuAn = new PhanMemQuanLyThiCong.Controls.Ctrl_DonViThucHienDuAn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
            this.sb_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.sb_All = new DevExpress.XtraEditors.SimpleButton();
            this.TL_HopDong = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rg_DonGia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_haophi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_Loc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_Select.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_end.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_end.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TL_HopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.rg_DonGia);
            this.panelControl1.Controls.Add(this.ce_haophi);
            this.panelControl1.Controls.Add(this.ce_Loc);
            this.panelControl1.Controls.Add(this.rg_Select);
            this.panelControl1.Controls.Add(this.de_begin);
            this.panelControl1.Controls.Add(this.de_end);
            this.panelControl1.Controls.Add(this.ctrl_DonViThucHienDuAn);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1257, 69);
            this.panelControl1.TabIndex = 1;
            // 
            // rg_DonGia
            // 
            this.rg_DonGia.Dock = System.Windows.Forms.DockStyle.Left;
            this.rg_DonGia.Location = new System.Drawing.Point(167, 2);
            this.rg_DonGia.Name = "rg_DonGia";
            this.rg_DonGia.Properties.Columns = 1;
            this.rg_DonGia.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lấy theo đơn giá kế hoạch"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lấy theo đơn giá thi công")});
            this.rg_DonGia.Size = new System.Drawing.Size(165, 65);
            this.rg_DonGia.TabIndex = 94;
            // 
            // ce_haophi
            // 
            this.ce_haophi.EditValue = true;
            this.ce_haophi.Location = new System.Drawing.Point(690, 43);
            this.ce_haophi.Name = "ce_haophi";
            this.ce_haophi.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.ce_haophi.Properties.Appearance.Options.UseFont = true;
            this.ce_haophi.Properties.Caption = "Hiện thị hao phí";
            this.ce_haophi.Size = new System.Drawing.Size(260, 21);
            this.ce_haophi.TabIndex = 93;
            this.ce_haophi.CheckedChanged += new System.EventHandler(this.ce_haophi_CheckedChanged);
            // 
            // ce_Loc
            // 
            this.ce_Loc.EditValue = true;
            this.ce_Loc.Location = new System.Drawing.Point(690, 22);
            this.ce_Loc.Name = "ce_Loc";
            this.ce_Loc.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.ce_Loc.Properties.Appearance.Options.UseFont = true;
            this.ce_Loc.Properties.Caption = "Lọc theo khối lượng kế hoạch";
            this.ce_Loc.Size = new System.Drawing.Size(260, 21);
            this.ce_Loc.TabIndex = 93;
            this.ce_Loc.CheckedChanged += new System.EventHandler(this.ce_Loc_CheckedChanged);
            // 
            // rg_Select
            // 
            this.rg_Select.Dock = System.Windows.Forms.DockStyle.Left;
            this.rg_Select.Location = new System.Drawing.Point(2, 2);
            this.rg_Select.Name = "rg_Select";
            this.rg_Select.Properties.Columns = 1;
            this.rg_Select.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lấy theo Hợp đồng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lấy theo Kế hoạch")});
            this.rg_Select.Size = new System.Drawing.Size(165, 65);
            this.rg_Select.TabIndex = 92;
            this.rg_Select.SelectedIndexChanged += new System.EventHandler(this.rg_Select_SelectedIndexChanged);
            // 
            // de_begin
            // 
            this.de_begin.EditValue = new System.DateTime(2023, 4, 10, 11, 4, 39, 0);
            this.de_begin.Enabled = false;
            this.de_begin.Location = new System.Drawing.Point(338, 43);
            this.de_begin.Name = "de_begin";
            this.de_begin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_begin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_begin.Size = new System.Drawing.Size(162, 20);
            this.de_begin.TabIndex = 91;
            this.de_begin.EditValueChanged += new System.EventHandler(this.de_begin_EditValueChanged);
            // 
            // de_end
            // 
            this.de_end.EditValue = new System.DateTime(2023, 4, 10, 11, 4, 52, 0);
            this.de_end.Enabled = false;
            this.de_end.Location = new System.Drawing.Point(504, 43);
            this.de_end.Name = "de_end";
            this.de_end.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_end.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_end.Size = new System.Drawing.Size(166, 20);
            this.de_end.TabIndex = 91;
            this.de_end.EditValueChanged += new System.EventHandler(this.de_end_EditValueChanged);
            // 
            // ctrl_DonViThucHienDuAn
            // 
            this.ctrl_DonViThucHienDuAn.DataSource = null;
            this.ctrl_DonViThucHienDuAn.EditValue = "Chọn đơn vị thực hiện";
            this.ctrl_DonViThucHienDuAn.Location = new System.Drawing.Point(1056, 24);
            this.ctrl_DonViThucHienDuAn.MaximumSize = new System.Drawing.Size(1000, 20);
            this.ctrl_DonViThucHienDuAn.MinimumSize = new System.Drawing.Size(0, 20);
            this.ctrl_DonViThucHienDuAn.Name = "ctrl_DonViThucHienDuAn";
            this.ctrl_DonViThucHienDuAn.Size = new System.Drawing.Size(189, 20);
            this.ctrl_DonViThucHienDuAn.TabIndex = 83;
            this.ctrl_DonViThucHienDuAn.DVTHChanged += new System.EventHandler(this.ctrl_DonViThucHienDuAn_DVTHChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(543, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 82;
            this.label2.Text = "Ngày kết thúc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(379, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 82;
            this.label1.Text = "Ngày bắt đầu";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(947, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 82;
            this.label4.Text = "Đơn vị thực hiện:";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_ok);
            this.layoutControl1.Controls.Add(this.sb_Huy);
            this.layoutControl1.Controls.Add(this.sb_All);
            this.layoutControl1.Controls.Add(this.TL_HopDong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 69);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1257, 532);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_ok
            // 
            this.sb_ok.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sb_ok.Appearance.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.sb_ok.Appearance.Options.UseBackColor = true;
            this.sb_ok.Appearance.Options.UseFont = true;
            this.sb_ok.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sb_ok.ImageOptions.SvgImage")));
            this.sb_ok.Location = new System.Drawing.Point(836, 484);
            this.sb_ok.Name = "sb_ok";
            this.sb_ok.Size = new System.Drawing.Size(409, 36);
            this.sb_ok.StyleController = this.layoutControl1;
            this.sb_ok.TabIndex = 6;
            this.sb_ok.Text = "Đồng ý";
            this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
            // 
            // sb_Huy
            // 
            this.sb_Huy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sb_Huy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.sb_Huy.Appearance.Options.UseBackColor = true;
            this.sb_Huy.Appearance.Options.UseFont = true;
            this.sb_Huy.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sb_Huy.ImageOptions.SvgImage")));
            this.sb_Huy.Location = new System.Drawing.Point(12, 484);
            this.sb_Huy.Name = "sb_Huy";
            this.sb_Huy.Size = new System.Drawing.Size(408, 36);
            this.sb_Huy.StyleController = this.layoutControl1;
            this.sb_Huy.TabIndex = 13;
            this.sb_Huy.Text = "Hủy chọn tất cả";
            this.sb_Huy.Click += new System.EventHandler(this.sb_Huy_Click);
            // 
            // sb_All
            // 
            this.sb_All.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sb_All.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_All.Appearance.Options.UseBackColor = true;
            this.sb_All.Appearance.Options.UseFont = true;
            this.sb_All.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sb_All.ImageOptions.SvgImage")));
            this.sb_All.Location = new System.Drawing.Point(424, 484);
            this.sb_All.Name = "sb_All";
            this.sb_All.Size = new System.Drawing.Size(408, 36);
            this.sb_All.StyleController = this.layoutControl1;
            this.sb_All.TabIndex = 12;
            this.sb_All.Text = "Chọn tất cả";
            this.sb_All.Click += new System.EventHandler(this.sb_All_Click);
            // 
            // TL_HopDong
            // 
            this.TL_HopDong.ActiveFilterString = "[KhoiLuongKeHoach] > \'0\'";
            this.TL_HopDong.CheckBoxFieldName = "Chon";
            this.TL_HopDong.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn6,
            this.treeListColumn7,
            this.treeListColumn10});
            this.TL_HopDong.Location = new System.Drawing.Point(12, 12);
            this.TL_HopDong.Name = "TL_HopDong";
            this.TL_HopDong.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.TL_HopDong.OptionsView.AutoWidth = false;
            this.TL_HopDong.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.TL_HopDong.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
            this.TL_HopDong.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
            this.TL_HopDong.Size = new System.Drawing.Size(1233, 468);
            this.TL_HopDong.TabIndex = 11;
            this.TL_HopDong.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.TL_HopDong_NodeCellStyle);
            this.TL_HopDong.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.TL_HopDong_BeforeFocusNode);
            this.TL_HopDong.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.TL_HopDong_CustomDrawNodeCell);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Chon";
            this.treeListColumn1.FieldName = "Chon";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Width = 85;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Mã hiệu vật liệu";
            this.treeListColumn2.FieldName = "MaHieu";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 153;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Tên vật liệu";
            this.treeListColumn3.FieldName = "TenCongViec";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            this.treeListColumn3.Width = 474;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Khối lượng hợp đồng";
            this.treeListColumn4.FieldName = "KhoiLuongHopDong";
            this.treeListColumn4.Format.FormatString = "n2";
            this.treeListColumn4.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 4;
            this.treeListColumn4.Width = 186;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Khối lượng kế hoạch";
            this.treeListColumn5.FieldName = "KhoiLuongKeHoach";
            this.treeListColumn5.Format.FormatString = "n2";
            this.treeListColumn5.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 3;
            this.treeListColumn5.Width = 186;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Đơn giá thi công";
            this.treeListColumn6.FieldName = "DonGiaThiCong";
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Width = 111;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Đơn giá thi công";
            this.treeListColumn7.FieldName = "DonGiaKeHoach";
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 5;
            this.treeListColumn7.Width = 177;
            // 
            // treeListColumn10
            // 
            this.treeListColumn10.Caption = "Đơn vị";
            this.treeListColumn10.FieldName = "DonVi";
            this.treeListColumn10.Name = "treeListColumn10";
            this.treeListColumn10.Visible = true;
            this.treeListColumn10.VisibleIndex = 2;
            this.treeListColumn10.Width = 130;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1257, 532);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.TL_HopDong;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1237, 472);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Huy;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 472);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(412, 40);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_All;
            this.layoutControlItem2.Location = new System.Drawing.Point(412, 472);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(412, 40);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sb_ok;
            this.layoutControlItem4.Location = new System.Drawing.Point(824, 472);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(413, 40);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // FormLayVatLieuHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 601);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "FormLayVatLieuHopDong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách vật liệu";
            this.Load += new System.EventHandler(this.FormLayVatLieuHopDong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rg_DonGia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_haophi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_Loc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rg_Select.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_begin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_end.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_end.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TL_HopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.RadioGroup rg_Select;
        private DevExpress.XtraEditors.DateEdit de_begin;
        private DevExpress.XtraEditors.DateEdit de_end;
        private Controls.Ctrl_DonViThucHienDuAn ctrl_DonViThucHienDuAn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList TL_HopDong;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sb_All;
        private DevExpress.XtraEditors.SimpleButton sb_Huy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.CheckEdit ce_Loc;
        private DevExpress.XtraEditors.CheckEdit ce_haophi;
        private DevExpress.XtraEditors.RadioGroup rg_DonGia;
    }
}