namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    partial class XtraFormNot
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_xoaFileDuyet = new DevExpress.XtraEditors.SimpleButton();
            this.check_listfileduyet = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btn_themfileduyet = new DevExpress.XtraEditors.SimpleButton();
            this.btn_dieuchinh = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_noidungduyet = new System.Windows.Forms.TextBox();
            this.lb_tencongtacduyet = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check_listfileduyet)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_xoaFileDuyet);
            this.panelControl1.Controls.Add(this.check_listfileduyet);
            this.panelControl1.Controls.Add(this.btn_themfileduyet);
            this.panelControl1.Controls.Add(this.btn_dieuchinh);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.txt_noidungduyet);
            this.panelControl1.Controls.Add(this.lb_tencongtacduyet);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(484, 546);
            this.panelControl1.TabIndex = 0;
            // 
            // btn_xoaFileDuyet
            // 
            this.btn_xoaFileDuyet.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoaFileDuyet.Appearance.Options.UseFont = true;
            this.btn_xoaFileDuyet.Location = new System.Drawing.Point(395, 64);
            this.btn_xoaFileDuyet.Name = "btn_xoaFileDuyet";
            this.btn_xoaFileDuyet.Size = new System.Drawing.Size(75, 23);
            this.btn_xoaFileDuyet.TabIndex = 22;
            this.btn_xoaFileDuyet.Text = "Xóa file";
            this.btn_xoaFileDuyet.Click += new System.EventHandler(this.btn_xoaFileDuyet_Click);
            // 
            // check_listfileduyet
            // 
            this.check_listfileduyet.Location = new System.Drawing.Point(12, 102);
            this.check_listfileduyet.Name = "check_listfileduyet";
            this.check_listfileduyet.Size = new System.Drawing.Size(458, 315);
            this.check_listfileduyet.TabIndex = 21;
            // 
            // btn_themfileduyet
            // 
            this.btn_themfileduyet.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_themfileduyet.Appearance.Options.UseFont = true;
            this.btn_themfileduyet.Location = new System.Drawing.Point(314, 64);
            this.btn_themfileduyet.Name = "btn_themfileduyet";
            this.btn_themfileduyet.Size = new System.Drawing.Size(75, 23);
            this.btn_themfileduyet.TabIndex = 20;
            this.btn_themfileduyet.Text = "Thêm file";
            this.btn_themfileduyet.Click += new System.EventHandler(this.btn_themfileduyet_Click);
            // 
            // btn_dieuchinh
            // 
            this.btn_dieuchinh.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dieuchinh.Appearance.Options.UseFont = true;
            this.btn_dieuchinh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_dieuchinh.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_dieuchinh.Location = new System.Drawing.Point(7, 511);
            this.btn_dieuchinh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_dieuchinh.Name = "btn_dieuchinh";
            this.btn_dieuchinh.Size = new System.Drawing.Size(463, 29);
            this.btn_dieuchinh.TabIndex = 19;
            this.btn_dieuchinh.Text = "Gửi duyệt";
            this.btn_dieuchinh.Click += new System.EventHandler(this.btn_dieuchinh_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(6, 71);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(103, 15);
            this.labelControl3.TabIndex = 18;
            this.labelControl3.Text = "Các file cần duyệt:";
            // 
            // txt_noidungduyet
            // 
            this.txt_noidungduyet.Location = new System.Drawing.Point(6, 444);
            this.txt_noidungduyet.Multiline = true;
            this.txt_noidungduyet.Name = "txt_noidungduyet";
            this.txt_noidungduyet.Size = new System.Drawing.Size(466, 61);
            this.txt_noidungduyet.TabIndex = 17;
            // 
            // lb_tencongtacduyet
            // 
            this.lb_tencongtacduyet.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_tencongtacduyet.Appearance.Options.UseFont = true;
            this.lb_tencongtacduyet.Location = new System.Drawing.Point(7, 31);
            this.lb_tencongtacduyet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lb_tencongtacduyet.Name = "lb_tencongtacduyet";
            this.lb_tencongtacduyet.Size = new System.Drawing.Size(75, 15);
            this.lb_tencongtacduyet.TabIndex = 16;
            this.lb_tencongtacduyet.Text = "labelControl3";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(7, 423);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 15);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Ghi chú:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(6, 0);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 15);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Tên công tác:";
            // 
            // XtraFormNot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 546);
            this.Controls.Add(this.panelControl1);
            this.Name = "XtraFormNot";
            this.Text = "Thông tin gửi công tác";
            this.Load += new System.EventHandler(this.XtraFormNot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check_listfileduyet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl check_listfileduyet;
        private DevExpress.XtraEditors.SimpleButton btn_themfileduyet;
        private DevExpress.XtraEditors.SimpleButton btn_dieuchinh;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.TextBox txt_noidungduyet;
        private DevExpress.XtraEditors.LabelControl lb_tencongtacduyet;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_xoaFileDuyet;
    }
}