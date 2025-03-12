
namespace PhanMemQuanLyThiCong
{
    partial class ctrl_TimKiemYeuCauVatLieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrl_TimKiemYeuCauVatLieu));
            this.gc_TimKiemVatLieu = new DevExpress.XtraGrid.GridControl();
            this.gv_TimKiemYeuCau = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_ChonAll = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_BoChonAll = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gc_TimKiemVatLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_TimKiemYeuCau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_TimKiemVatLieu
            // 
            this.gc_TimKiemVatLieu.Location = new System.Drawing.Point(12, 12);
            this.gc_TimKiemVatLieu.MainView = this.gv_TimKiemYeuCau;
            this.gc_TimKiemVatLieu.Name = "gc_TimKiemVatLieu";
            this.gc_TimKiemVatLieu.Size = new System.Drawing.Size(1139, 562);
            this.gc_TimKiemVatLieu.TabIndex = 0;
            this.gc_TimKiemVatLieu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_TimKiemYeuCau});
            // 
            // gv_TimKiemYeuCau
            // 
            this.gv_TimKiemYeuCau.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gv_TimKiemYeuCau.GridControl = this.gc_TimKiemVatLieu;
            this.gv_TimKiemYeuCau.Name = "gv_TimKiemYeuCau";
            this.gv_TimKiemYeuCau.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nguồn phát sinh";
            this.gridColumn1.FieldName = "Nguon";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mã vật tư";
            this.gridColumn2.FieldName = "MaVatTu";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên vật tư";
            this.gridColumn3.FieldName = "TenVatTu";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Khối lượng hợp đồng";
            this.gridColumn4.FieldName = "HopDongKl";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Đơn giá hiện trường";
            this.gridColumn5.FieldName = "DonGIa";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
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
            // btn_ChonAll
            // 
            this.btn_ChonAll.BackColor = System.Drawing.Color.Yellow;
            this.btn_ChonAll.Location = new System.Drawing.Point(867, 578);
            this.btn_ChonAll.Name = "btn_ChonAll";
            this.btn_ChonAll.Size = new System.Drawing.Size(284, 40);
            this.btn_ChonAll.TabIndex = 5;
            this.btn_ChonAll.Text = "Chọn tất cả";
            this.btn_ChonAll.UseVisualStyleBackColor = false;
            this.btn_ChonAll.Click += new System.EventHandler(this.btn_ChonAll_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.BackColor = System.Drawing.Color.Lime;
            this.btn_Ok.Location = new System.Drawing.Point(296, 578);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(283, 40);
            this.btn_Ok.TabIndex = 3;
            this.btn_Ok.Text = "Đồng ý";
            this.btn_Ok.UseVisualStyleBackColor = false;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_BoChonAll
            // 
            this.btn_BoChonAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_BoChonAll.Location = new System.Drawing.Point(583, 578);
            this.btn_BoChonAll.Name = "btn_BoChonAll";
            this.btn_BoChonAll.Size = new System.Drawing.Size(280, 40);
            this.btn_BoChonAll.TabIndex = 4;
            this.btn_BoChonAll.Text = "Bỏ chọn tất cả";
            this.btn_BoChonAll.UseVisualStyleBackColor = false;
            this.btn_BoChonAll.Click += new System.EventHandler(this.btn_BoChonAll_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.BackColor = System.Drawing.Color.Aqua;
            this.bt_cancel.Location = new System.Drawing.Point(12, 578);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(280, 40);
            this.bt_cancel.TabIndex = 2;
            this.bt_cancel.Text = "Hủy bỏ";
            this.bt_cancel.UseVisualStyleBackColor = false;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_cancel);
            this.layoutControl1.Controls.Add(this.gc_TimKiemVatLieu);
            this.layoutControl1.Controls.Add(this.btn_ChonAll);
            this.layoutControl1.Controls.Add(this.btn_BoChonAll);
            this.layoutControl1.Controls.Add(this.btn_Ok);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1163, 630);
            this.layoutControl1.TabIndex = 16;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1163, 630);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gc_TimKiemVatLieu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1143, 566);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_Ok;
            this.layoutControlItem2.Location = new System.Drawing.Point(284, 566);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(287, 44);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_BoChonAll;
            this.layoutControlItem3.Location = new System.Drawing.Point(571, 566);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(284, 44);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btn_ChonAll;
            this.layoutControlItem4.Location = new System.Drawing.Point(855, 566);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(288, 44);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.bt_cancel;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 566);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(284, 44);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // ctrl_TimKiemYeuCauVatLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ctrl_TimKiemYeuCauVatLieu";
            this.Size = new System.Drawing.Size(1163, 630);
            ((System.ComponentModel.ISupportInitialize)(this.gc_TimKiemVatLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_TimKiemYeuCau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_TimKiemVatLieu;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_TimKiemYeuCau;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private System.Windows.Forms.Button btn_ChonAll;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_BoChonAll;
        private System.Windows.Forms.Button bt_cancel;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}
