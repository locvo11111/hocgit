
namespace PhanMemQuanLyThiCong
{
    partial class Form_DoBoc_ChiaKhoiLuongPhatSinh
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
            this.bt_Luu = new System.Windows.Forms.Button();
            this.bt_huy = new System.Windows.Forms.Button();
            this.cbb_soLanPhatSinh = new System.Windows.Forms.ComboBox();
            this.dgv_KhoiLuongChiTiet = new System.Windows.Forms.DataGridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_KhoiLuongChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_Luu);
            this.layoutControl1.Controls.Add(this.bt_huy);
            this.layoutControl1.Controls.Add(this.cbb_soLanPhatSinh);
            this.layoutControl1.Controls.Add(this.dgv_KhoiLuongChiTiet);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(814, 466);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_Luu
            // 
            this.bt_Luu.Location = new System.Drawing.Point(409, 392);
            this.bt_Luu.Name = "bt_Luu";
            this.bt_Luu.Size = new System.Drawing.Size(393, 62);
            this.bt_Luu.TabIndex = 8;
            this.bt_Luu.Text = "Lưu lại";
            this.bt_Luu.UseVisualStyleBackColor = true;
            this.bt_Luu.Click += new System.EventHandler(this.bt_Luu_Click);
            // 
            // bt_huy
            // 
            this.bt_huy.Location = new System.Drawing.Point(12, 392);
            this.bt_huy.Name = "bt_huy";
            this.bt_huy.Size = new System.Drawing.Size(393, 62);
            this.bt_huy.TabIndex = 7;
            this.bt_huy.Text = "Hủy bỏ";
            this.bt_huy.UseVisualStyleBackColor = true;
            this.bt_huy.Click += new System.EventHandler(this.bt_huy_Click);
            // 
            // cbb_soLanPhatSinh
            // 
            this.cbb_soLanPhatSinh.DisplayMember = "Value";
            this.cbb_soLanPhatSinh.FormattingEnabled = true;
            this.cbb_soLanPhatSinh.Location = new System.Drawing.Point(12, 30);
            this.cbb_soLanPhatSinh.Name = "cbb_soLanPhatSinh";
            this.cbb_soLanPhatSinh.Size = new System.Drawing.Size(790, 21);
            this.cbb_soLanPhatSinh.TabIndex = 6;
            this.cbb_soLanPhatSinh.ValueMember = "Key";
            this.cbb_soLanPhatSinh.SelectedIndexChanged += new System.EventHandler(this.cbb_soLanPhatSinh_SelectedIndexChanged);
            // 
            // dgv_KhoiLuongChiTiet
            // 
            this.dgv_KhoiLuongChiTiet.AllowUserToAddRows = false;
            this.dgv_KhoiLuongChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_KhoiLuongChiTiet.Enabled = false;
            this.dgv_KhoiLuongChiTiet.Location = new System.Drawing.Point(12, 71);
            this.dgv_KhoiLuongChiTiet.Name = "dgv_KhoiLuongChiTiet";
            this.dgv_KhoiLuongChiTiet.RowHeadersVisible = false;
            this.dgv_KhoiLuongChiTiet.Size = new System.Drawing.Size(790, 317);
            this.dgv_KhoiLuongChiTiet.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(814, 466);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dgv_KhoiLuongChiTiet;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 43);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(794, 337);
            this.layoutControlItem2.Text = "Xem trước khối lượng sau khi chia";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(159, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.bt_huy;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 380);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(397, 66);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.bt_Luu;
            this.layoutControlItem5.Location = new System.Drawing.Point(397, 380);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(397, 66);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cbb_soLanPhatSinh;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(794, 43);
            this.layoutControlItem3.Text = "Chọn lần phát sinh để chia toàn bộ khối lượng dư";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(234, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // Form_DoBoc_ChiaKhoiLuongPhatSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 466);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Form_DoBoc_ChiaKhoiLuongPhatSinh";
            this.Text = "Form_DoBoc_ChiaKhoiLuongPhatSinh";
            this.Load += new System.EventHandler(this.Form_DoBoc_ChiaKhoiLuongPhatSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_KhoiLuongChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.DataGridView dgv_KhoiLuongChiTiet;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.Button bt_Luu;
        private System.Windows.Forms.Button bt_huy;
        private System.Windows.Forms.ComboBox cbb_soLanPhatSinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}