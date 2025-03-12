namespace PhanMemQuanLyThiCong.ChatBox.Views
{
    partial class AddFile
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
            this.btn_huybofile = new DevExpress.XtraEditors.SimpleButton();
            this.btn_addfile = new DevExpress.XtraEditors.SimpleButton();
            this.check_filedachon = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btn_xoafile = new DevExpress.XtraEditors.SimpleButton();
            this.btn_themmoifile = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check_filedachon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_huybofile);
            this.panelControl1.Controls.Add(this.btn_addfile);
            this.panelControl1.Controls.Add(this.check_filedachon);
            this.panelControl1.Controls.Add(this.btn_xoafile);
            this.panelControl1.Controls.Add(this.btn_themmoifile);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(352, 465);
            this.panelControl1.TabIndex = 0;
            // 
            // btn_huybofile
            // 
            this.btn_huybofile.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_huybofile.Appearance.Options.UseFont = true;
            this.btn_huybofile.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_huybofile.Location = new System.Drawing.Point(264, 424);
            this.btn_huybofile.Name = "btn_huybofile";
            this.btn_huybofile.Size = new System.Drawing.Size(75, 32);
            this.btn_huybofile.TabIndex = 4;
            this.btn_huybofile.Text = "Hủy";
            this.btn_huybofile.Click += new System.EventHandler(this.btn_huybofile_Click);
            // 
            // btn_addfile
            // 
            this.btn_addfile.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addfile.Appearance.Options.UseFont = true;
            this.btn_addfile.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_addfile.Location = new System.Drawing.Point(12, 424);
            this.btn_addfile.Name = "btn_addfile";
            this.btn_addfile.Size = new System.Drawing.Size(75, 32);
            this.btn_addfile.TabIndex = 3;
            this.btn_addfile.Text = "Thêm";
            // 
            // check_filedachon
            // 
            this.check_filedachon.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_filedachon.Appearance.Options.UseFont = true;
            this.check_filedachon.CausesValidation = false;
            this.check_filedachon.CheckOnClick = true;
            this.check_filedachon.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.check_filedachon.Location = new System.Drawing.Point(12, 48);
            this.check_filedachon.Name = "check_filedachon";
            this.check_filedachon.Size = new System.Drawing.Size(327, 369);
            this.check_filedachon.TabIndex = 2;
            this.check_filedachon.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.check_filedachon_ItemCheck);
            // 
            // btn_xoafile
            // 
            this.btn_xoafile.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoafile.Appearance.Options.UseFont = true;
            this.btn_xoafile.Location = new System.Drawing.Point(264, 5);
            this.btn_xoafile.Name = "btn_xoafile";
            this.btn_xoafile.Size = new System.Drawing.Size(75, 37);
            this.btn_xoafile.TabIndex = 1;
            this.btn_xoafile.Text = "Xóa";
            this.btn_xoafile.Click += new System.EventHandler(this.btn_xoafile_Click);
            // 
            // btn_themmoifile
            // 
            this.btn_themmoifile.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_themmoifile.Appearance.Options.UseFont = true;
            this.btn_themmoifile.Location = new System.Drawing.Point(12, 5);
            this.btn_themmoifile.Name = "btn_themmoifile";
            this.btn_themmoifile.Size = new System.Drawing.Size(75, 37);
            this.btn_themmoifile.TabIndex = 0;
            this.btn_themmoifile.Text = "Chọn File";
            this.btn_themmoifile.Click += new System.EventHandler(this.btn_themmoifile_Click);
            // 
            // AddFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 465);
            this.Controls.Add(this.panelControl1);
            this.Name = "AddFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm mới File";
            this.Load += new System.EventHandler(this.AddFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.check_filedachon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl check_filedachon;
        private DevExpress.XtraEditors.SimpleButton btn_xoafile;
        private DevExpress.XtraEditors.SimpleButton btn_themmoifile;
        private DevExpress.XtraEditors.SimpleButton btn_huybofile;
        private DevExpress.XtraEditors.SimpleButton btn_addfile;
    }
}