namespace PhanMemQuanLyThiCong
{
    partial class Frm_Sort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Sort));
            this.group_PhamVi = new DevExpress.XtraEditors.GroupControl();
            this.rb_HangMucActive = new System.Windows.Forms.RadioButton();
            this.rb_AllHangMuc = new System.Windows.Forms.RadioButton();
            this.gr_Sort = new DevExpress.XtraEditors.GroupControl();
            this.rb_SortThuTu = new System.Windows.Forms.RadioButton();
            this.rb_SortTrinhTuThiCong = new System.Windows.Forms.RadioButton();
            this.rad_XapXepHM = new System.Windows.Forms.RadioButton();
            this.btn_HuySort = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Apply = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.group_PhamVi)).BeginInit();
            this.group_PhamVi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gr_Sort)).BeginInit();
            this.gr_Sort.SuspendLayout();
            this.SuspendLayout();
            // 
            // group_PhamVi
            // 
            this.group_PhamVi.AppearanceCaption.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_PhamVi.AppearanceCaption.Options.UseFont = true;
            this.group_PhamVi.Controls.Add(this.rb_HangMucActive);
            this.group_PhamVi.Controls.Add(this.rb_AllHangMuc);
            this.group_PhamVi.Location = new System.Drawing.Point(12, 12);
            this.group_PhamVi.Name = "group_PhamVi";
            this.group_PhamVi.Size = new System.Drawing.Size(340, 108);
            this.group_PhamVi.TabIndex = 0;
            this.group_PhamVi.Text = "PHẠM VI ÁP DỤNG";
            // 
            // rb_HangMucActive
            // 
            this.rb_HangMucActive.AutoSize = true;
            this.rb_HangMucActive.Checked = true;
            this.rb_HangMucActive.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_HangMucActive.ForeColor = System.Drawing.Color.Green;
            this.rb_HangMucActive.Location = new System.Drawing.Point(23, 73);
            this.rb_HangMucActive.Name = "rb_HangMucActive";
            this.rb_HangMucActive.Size = new System.Drawing.Size(226, 19);
            this.rb_HangMucActive.TabIndex = 1;
            this.rb_HangMucActive.TabStop = true;
            this.rb_HangMucActive.Tag = "HANGMUCACTIVE";
            this.rb_HangMucActive.Text = "Áp dụng cho 1 hạng mục đang chọn";
            this.rb_HangMucActive.UseVisualStyleBackColor = true;
            // 
            // rb_AllHangMuc
            // 
            this.rb_AllHangMuc.AutoSize = true;
            this.rb_AllHangMuc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_AllHangMuc.ForeColor = System.Drawing.Color.Green;
            this.rb_AllHangMuc.Location = new System.Drawing.Point(23, 38);
            this.rb_AllHangMuc.Name = "rb_AllHangMuc";
            this.rb_AllHangMuc.Size = new System.Drawing.Size(292, 19);
            this.rb_AllHangMuc.TabIndex = 0;
            this.rb_AllHangMuc.Tag = "ALLHANGMUC";
            this.rb_AllHangMuc.Text = "Áp dụng cho toàn bộ hạng mục trong công trình";
            this.rb_AllHangMuc.UseVisualStyleBackColor = true;
            // 
            // gr_Sort
            // 
            this.gr_Sort.AppearanceCaption.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gr_Sort.AppearanceCaption.Options.UseFont = true;
            this.gr_Sort.Controls.Add(this.rad_XapXepHM);
            this.gr_Sort.Controls.Add(this.rb_SortThuTu);
            this.gr_Sort.Controls.Add(this.rb_SortTrinhTuThiCong);
            this.gr_Sort.Location = new System.Drawing.Point(12, 133);
            this.gr_Sort.Name = "gr_Sort";
            this.gr_Sort.Size = new System.Drawing.Size(340, 142);
            this.gr_Sort.TabIndex = 2;
            this.gr_Sort.Text = "LOẠI SẮP XẾP";
            // 
            // rb_SortThuTu
            // 
            this.rb_SortThuTu.AutoSize = true;
            this.rb_SortThuTu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_SortThuTu.ForeColor = System.Drawing.Color.Green;
            this.rb_SortThuTu.Location = new System.Drawing.Point(23, 73);
            this.rb_SortThuTu.Name = "rb_SortThuTu";
            this.rb_SortThuTu.Size = new System.Drawing.Size(280, 19);
            this.rb_SortThuTu.TabIndex = 1;
            this.rb_SortThuTu.Tag = "THUTU";
            this.rb_SortThuTu.Text = "Sắp xếp công tác theo từng Nhóm/Phân đoạn";
            this.rb_SortThuTu.UseVisualStyleBackColor = true;
            // 
            // rb_SortTrinhTuThiCong
            // 
            this.rb_SortTrinhTuThiCong.AutoSize = true;
            this.rb_SortTrinhTuThiCong.Checked = true;
            this.rb_SortTrinhTuThiCong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_SortTrinhTuThiCong.ForeColor = System.Drawing.Color.Green;
            this.rb_SortTrinhTuThiCong.Location = new System.Drawing.Point(23, 39);
            this.rb_SortTrinhTuThiCong.Name = "rb_SortTrinhTuThiCong";
            this.rb_SortTrinhTuThiCong.Size = new System.Drawing.Size(172, 19);
            this.rb_SortTrinhTuThiCong.TabIndex = 0;
            this.rb_SortTrinhTuThiCong.TabStop = true;
            this.rb_SortTrinhTuThiCong.Tag = "TRINHTUTHICONG";
            this.rb_SortTrinhTuThiCong.Text = "Tự động sắp xếp công tác";
            this.rb_SortTrinhTuThiCong.UseVisualStyleBackColor = true;
            // 
            // rad_XapXepHM
            // 
            this.rad_XapXepHM.AutoSize = true;
            this.rad_XapXepHM.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rad_XapXepHM.ForeColor = System.Drawing.Color.Green;
            this.rad_XapXepHM.Location = new System.Drawing.Point(23, 108);
            this.rad_XapXepHM.Name = "rad_XapXepHM";
            this.rad_XapXepHM.Size = new System.Drawing.Size(175, 19);
            this.rad_XapXepHM.TabIndex = 2;
            this.rad_XapXepHM.Tag = "THUTU";
            this.rad_XapXepHM.Text = "Sắp xếp toàn bộ Hạng Mục";
            this.rad_XapXepHM.UseVisualStyleBackColor = true;
            // 
            // btn_HuySort
            // 
            this.btn_HuySort.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_HuySort.Appearance.BorderColor = System.Drawing.Color.Blue;
            this.btn_HuySort.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btn_HuySort.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btn_HuySort.Appearance.Options.UseBackColor = true;
            this.btn_HuySort.Appearance.Options.UseBorderColor = true;
            this.btn_HuySort.Appearance.Options.UseFont = true;
            this.btn_HuySort.Appearance.Options.UseForeColor = true;
            this.btn_HuySort.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btn_HuySort.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_HuySort.ImageOptions.Image")));
            this.btn_HuySort.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_HuySort.Location = new System.Drawing.Point(125, 280);
            this.btn_HuySort.Margin = new System.Windows.Forms.Padding(2);
            this.btn_HuySort.MaximumSize = new System.Drawing.Size(120, 30);
            this.btn_HuySort.Name = "btn_HuySort";
            this.btn_HuySort.Size = new System.Drawing.Size(106, 30);
            this.btn_HuySort.TabIndex = 41;
            this.btn_HuySort.Text = "HỦY SẮP XẾP";
            this.btn_HuySort.Click += new System.EventHandler(this.btn_HuySort_Click);
            // 
            // btn_Huy
            // 
            this.btn_Huy.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Huy.Appearance.BorderColor = System.Drawing.Color.Red;
            this.btn_Huy.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Huy.Appearance.ForeColor = System.Drawing.Color.Red;
            this.btn_Huy.Appearance.Options.UseBackColor = true;
            this.btn_Huy.Appearance.Options.UseBorderColor = true;
            this.btn_Huy.Appearance.Options.UseFont = true;
            this.btn_Huy.Appearance.Options.UseForeColor = true;
            this.btn_Huy.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btn_Huy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Huy.ImageOptions.Image")));
            this.btn_Huy.Location = new System.Drawing.Point(235, 280);
            this.btn_Huy.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Huy.MaximumSize = new System.Drawing.Size(90, 30);
            this.btn_Huy.Name = "btn_Huy";
            this.btn_Huy.Size = new System.Drawing.Size(86, 30);
            this.btn_Huy.TabIndex = 40;
            this.btn_Huy.Text = "HỦY";
            this.btn_Huy.Click += new System.EventHandler(this.btn_Huy_Click);
            // 
            // btn_Apply
            // 
            this.btn_Apply.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Apply.Appearance.BorderColor = System.Drawing.Color.Blue;
            this.btn_Apply.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btn_Apply.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btn_Apply.Appearance.Options.UseBackColor = true;
            this.btn_Apply.Appearance.Options.UseBorderColor = true;
            this.btn_Apply.Appearance.Options.UseFont = true;
            this.btn_Apply.Appearance.Options.UseForeColor = true;
            this.btn_Apply.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btn_Apply.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Apply.ImageOptions.Image")));
            this.btn_Apply.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Apply.Location = new System.Drawing.Point(35, 280);
            this.btn_Apply.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Apply.MaximumSize = new System.Drawing.Size(90, 30);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(86, 30);
            this.btn_Apply.TabIndex = 38;
            this.btn_Apply.Text = "ÁP DỤNG";
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // Frm_Sort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 316);
            this.Controls.Add(this.btn_HuySort);
            this.Controls.Add(this.btn_Huy);
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.gr_Sort);
            this.Controls.Add(this.group_PhamVi);
            this.Name = "Frm_Sort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SẮP XẾP CÔNG TÁC";
            this.Load += new System.EventHandler(this.Frm_Sort_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_Sort_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.group_PhamVi)).EndInit();
            this.group_PhamVi.ResumeLayout(false);
            this.group_PhamVi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gr_Sort)).EndInit();
            this.gr_Sort.ResumeLayout(false);
            this.gr_Sort.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl group_PhamVi;
        private System.Windows.Forms.RadioButton rb_HangMucActive;
        private System.Windows.Forms.RadioButton rb_AllHangMuc;
        private DevExpress.XtraEditors.GroupControl gr_Sort;
        private System.Windows.Forms.RadioButton rb_SortThuTu;
        private System.Windows.Forms.RadioButton rb_SortTrinhTuThiCong;
        public DevExpress.XtraEditors.SimpleButton btn_Apply;
        public DevExpress.XtraEditors.SimpleButton btn_Huy;
        public DevExpress.XtraEditors.SimpleButton btn_HuySort;
        private System.Windows.Forms.RadioButton rad_XapXepHM;
    }
}