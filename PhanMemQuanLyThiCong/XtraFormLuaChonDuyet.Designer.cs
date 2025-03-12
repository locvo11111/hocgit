
namespace PhanMemQuanLyThiCong
{
    partial class XtraFormLuaChonDuyet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormLuaChonDuyet));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ce_PhieuDuyet = new DevExpress.XtraEditors.CheckEdit();
            this.sb_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sb_DuyetTheoQuyTrinh = new DevExpress.XtraEditors.SimpleButton();
            this.sb_Duyet1Buoc = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ce_PhieuDuyet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ce_PhieuDuyet);
            this.layoutControl1.Controls.Add(this.sb_Cancel);
            this.layoutControl1.Controls.Add(this.sb_DuyetTheoQuyTrinh);
            this.layoutControl1.Controls.Add(this.sb_Duyet1Buoc);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(802, 81);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ce_PhieuDuyet
            // 
            this.ce_PhieuDuyet.Location = new System.Drawing.Point(12, 38);
            this.ce_PhieuDuyet.Name = "ce_PhieuDuyet";
            this.ce_PhieuDuyet.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.ce_PhieuDuyet.Properties.Appearance.Options.UseFont = true;
            this.ce_PhieuDuyet.Properties.Caption = "In phiếu duyệt";
            this.ce_PhieuDuyet.Size = new System.Drawing.Size(257, 21);
            this.ce_PhieuDuyet.StyleController = this.layoutControl1;
            this.ce_PhieuDuyet.TabIndex = 7;
            // 
            // sb_Cancel
            // 
            this.sb_Cancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sb_Cancel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_Cancel.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.sb_Cancel.Appearance.Options.UseBackColor = true;
            this.sb_Cancel.Appearance.Options.UseFont = true;
            this.sb_Cancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sb_Cancel.ImageOptions.Image")));
            this.sb_Cancel.Location = new System.Drawing.Point(533, 12);
            this.sb_Cancel.Name = "sb_Cancel";
            this.sb_Cancel.Size = new System.Drawing.Size(257, 22);
            this.sb_Cancel.StyleController = this.layoutControl1;
            this.sb_Cancel.TabIndex = 6;
            this.sb_Cancel.Text = "Thoát";
            this.sb_Cancel.Click += new System.EventHandler(this.sb_Cancel_Click);
            // 
            // sb_DuyetTheoQuyTrinh
            // 
            this.sb_DuyetTheoQuyTrinh.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.sb_DuyetTheoQuyTrinh.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_DuyetTheoQuyTrinh.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.sb_DuyetTheoQuyTrinh.Appearance.Options.UseBackColor = true;
            this.sb_DuyetTheoQuyTrinh.Appearance.Options.UseFont = true;
            this.sb_DuyetTheoQuyTrinh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sb_DuyetTheoQuyTrinh.ImageOptions.Image")));
            this.sb_DuyetTheoQuyTrinh.Location = new System.Drawing.Point(273, 12);
            this.sb_DuyetTheoQuyTrinh.Name = "sb_DuyetTheoQuyTrinh";
            this.sb_DuyetTheoQuyTrinh.Size = new System.Drawing.Size(256, 22);
            this.sb_DuyetTheoQuyTrinh.StyleController = this.layoutControl1;
            this.sb_DuyetTheoQuyTrinh.TabIndex = 5;
            this.sb_DuyetTheoQuyTrinh.Text = "Duyệt theo quy trình";
            this.sb_DuyetTheoQuyTrinh.Click += new System.EventHandler(this.sb_DuyetTheoQuyTrinh_Click);
            // 
            // sb_Duyet1Buoc
            // 
            this.sb_Duyet1Buoc.Appearance.BackColor = System.Drawing.Color.Lime;
            this.sb_Duyet1Buoc.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_Duyet1Buoc.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.sb_Duyet1Buoc.Appearance.Options.UseBackColor = true;
            this.sb_Duyet1Buoc.Appearance.Options.UseFont = true;
            this.sb_Duyet1Buoc.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sb_Duyet1Buoc.ImageOptions.Image")));
            this.sb_Duyet1Buoc.Location = new System.Drawing.Point(12, 12);
            this.sb_Duyet1Buoc.Name = "sb_Duyet1Buoc";
            this.sb_Duyet1Buoc.Size = new System.Drawing.Size(257, 22);
            this.sb_Duyet1Buoc.StyleController = this.layoutControl1;
            this.sb_Duyet1Buoc.TabIndex = 4;
            this.sb_Duyet1Buoc.Text = "Duyệt 1 bước";
            this.sb_Duyet1Buoc.Click += new System.EventHandler(this.sb_Duyet1Buoc_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(802, 81);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sb_Duyet1Buoc;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(261, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 51);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(782, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_DuyetTheoQuyTrinh;
            this.layoutControlItem2.Location = new System.Drawing.Point(261, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(260, 51);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Cancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(521, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(261, 51);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ce_PhieuDuyet;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(261, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // XtraFormLuaChonDuyet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 81);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraFormLuaChonDuyet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lựa chọn cách duyệt";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.XtraFormLuaChonDuyet_FormClosed);
            this.Load += new System.EventHandler(this.XtraFormLuaChonDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ce_PhieuDuyet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.CheckEdit ce_PhieuDuyet;
        private DevExpress.XtraEditors.SimpleButton sb_Cancel;
        private DevExpress.XtraEditors.SimpleButton sb_DuyetTheoQuyTrinh;
        private DevExpress.XtraEditors.SimpleButton sb_Duyet1Buoc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}