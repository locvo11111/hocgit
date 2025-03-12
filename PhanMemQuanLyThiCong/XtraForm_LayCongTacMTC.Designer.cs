
namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_LayCongTacMTC
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sb_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ChonAll = new DevExpress.XtraEditors.SimpleButton();
            this.sb_Save = new DevExpress.XtraEditors.SimpleButton();
            this.TL_HopDong = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn11 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TL_HopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_Huy);
            this.layoutControl1.Controls.Add(this.sb_ChonAll);
            this.layoutControl1.Controls.Add(this.sb_Save);
            this.layoutControl1.Controls.Add(this.TL_HopDong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(674, 538);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_Huy
            // 
            this.sb_Huy.Appearance.BackColor = System.Drawing.Color.Red;
            this.sb_Huy.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sb_Huy.Appearance.Options.UseBackColor = true;
            this.sb_Huy.Appearance.Options.UseFont = true;
            this.sb_Huy.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_cancel_25px;
            this.sb_Huy.Location = new System.Drawing.Point(448, 497);
            this.sb_Huy.Name = "sb_Huy";
            this.sb_Huy.Size = new System.Drawing.Size(214, 29);
            this.sb_Huy.StyleController = this.layoutControl1;
            this.sb_Huy.TabIndex = 12;
            this.sb_Huy.Text = "Hủy chọn";
            this.sb_Huy.Click += new System.EventHandler(this.sb_Huy_Click);
            // 
            // sb_ChonAll
            // 
            this.sb_ChonAll.Appearance.BackColor = System.Drawing.Color.Lime;
            this.sb_ChonAll.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sb_ChonAll.Appearance.Options.UseBackColor = true;
            this.sb_ChonAll.Appearance.Options.UseFont = true;
            this.sb_ChonAll.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_add_25;
            this.sb_ChonAll.Location = new System.Drawing.Point(230, 497);
            this.sb_ChonAll.Name = "sb_ChonAll";
            this.sb_ChonAll.Size = new System.Drawing.Size(214, 29);
            this.sb_ChonAll.StyleController = this.layoutControl1;
            this.sb_ChonAll.TabIndex = 11;
            this.sb_ChonAll.Text = "Chọn tất cả";
            this.sb_ChonAll.Click += new System.EventHandler(this.sb_ChonAll_Click);
            // 
            // sb_Save
            // 
            this.sb_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sb_Save.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_Save.Appearance.Options.UseBackColor = true;
            this.sb_Save.Appearance.Options.UseFont = true;
            this.sb_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sb_Save.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_database_export_24;
            this.sb_Save.Location = new System.Drawing.Point(12, 497);
            this.sb_Save.Name = "sb_Save";
            this.sb_Save.Size = new System.Drawing.Size(214, 28);
            this.sb_Save.StyleController = this.layoutControl1;
            this.sb_Save.TabIndex = 10;
            this.sb_Save.Text = "Lưu thay đổi và đóng";
            this.sb_Save.Click += new System.EventHandler(this.sb_Save_Click);
            // 
            // TL_HopDong
            // 
            this.TL_HopDong.CheckBoxFieldName = "Chon";
            this.TL_HopDong.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn6,
            this.treeListColumn7,
            this.treeListColumn8,
            this.treeListColumn9,
            this.treeListColumn10,
            this.treeListColumn11});
            this.TL_HopDong.Location = new System.Drawing.Point(12, 12);
            this.TL_HopDong.Name = "TL_HopDong";
            this.TL_HopDong.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.TL_HopDong.OptionsView.AutoWidth = false;
            this.TL_HopDong.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.TL_HopDong.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
            this.TL_HopDong.Size = new System.Drawing.Size(650, 481);
            this.TL_HopDong.TabIndex = 11;
            this.TL_HopDong.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.TL_HopDong_NodeCellStyle);
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
            this.treeListColumn2.Caption = "Mã hiệu công tác";
            this.treeListColumn2.FieldName = "MaHieu";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 170;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Tên công tác";
            this.treeListColumn3.FieldName = "TenCongViec";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            this.treeListColumn3.Width = 372;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Khối lượng hợp đồng";
            this.treeListColumn4.FieldName = "KhoiLuongHopDong";
            this.treeListColumn4.Format.FormatString = "n2";
            this.treeListColumn4.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Width = 106;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Khối lượng kế hoạch";
            this.treeListColumn5.FieldName = "KhoiLuongKeHoach";
            this.treeListColumn5.Format.FormatString = "n2";
            this.treeListColumn5.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Width = 104;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Đơn giá hợp đồng";
            this.treeListColumn6.FieldName = "DonGiaHopDong";
            this.treeListColumn6.Format.FormatString = "n2";
            this.treeListColumn6.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Width = 107;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Đơn giá kế hoạch";
            this.treeListColumn7.FieldName = "DonGiaKeHoach";
            this.treeListColumn7.Format.FormatString = "n2";
            this.treeListColumn7.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Width = 111;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.Caption = "Khối lượng phát sinh";
            this.treeListColumn8.FieldName = "KhoiLuongPhatSinh";
            this.treeListColumn8.Format.FormatString = "n2";
            this.treeListColumn8.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.Width = 107;
            // 
            // treeListColumn9
            // 
            this.treeListColumn9.Caption = "Đơn giá phát sinh";
            this.treeListColumn9.FieldName = "DonGiaPS";
            this.treeListColumn9.Format.FormatString = "n2";
            this.treeListColumn9.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn9.Name = "treeListColumn9";
            this.treeListColumn9.Width = 105;
            // 
            // treeListColumn10
            // 
            this.treeListColumn10.Caption = "Đơn vị";
            this.treeListColumn10.FieldName = "DonVi";
            this.treeListColumn10.Name = "treeListColumn10";
            this.treeListColumn10.Visible = true;
            this.treeListColumn10.VisibleIndex = 2;
            this.treeListColumn10.Width = 85;
            // 
            // treeListColumn11
            // 
            this.treeListColumn11.Caption = "Đơn giá thi công";
            this.treeListColumn11.FieldName = "DonGiaThiCong";
            this.treeListColumn11.Format.FormatString = "n2";
            this.treeListColumn11.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeListColumn11.Name = "treeListColumn11";
            this.treeListColumn11.Width = 108;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(674, 538);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.TL_HopDong;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(654, 485);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Save;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 485);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(218, 33);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sb_ChonAll;
            this.layoutControlItem4.Location = new System.Drawing.Point(218, 485);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(218, 33);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sb_Huy;
            this.layoutControlItem5.Location = new System.Drawing.Point(436, 485);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(218, 33);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // XtraForm_LayCongTacMTC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 538);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_LayCongTacMTC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lấy công tác cho Máy thi công";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TL_HopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

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
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sb_Huy;
        private DevExpress.XtraEditors.SimpleButton sb_ChonAll;
        private DevExpress.XtraEditors.SimpleButton sb_Save;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}