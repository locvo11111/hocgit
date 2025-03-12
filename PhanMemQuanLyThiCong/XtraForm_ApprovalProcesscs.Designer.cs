namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_ApprovalProcesscs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm_ApprovalProcesscs));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage_Setting = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage_Task = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage_Setting;
            this.xtraTabControl1.Size = new System.Drawing.Size(821, 439);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage_Task,
            this.xtraTabPage_Setting});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPage_Setting
            // 
            this.xtraTabPage_Setting.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage_Setting.ImageOptions.Image")));
            this.xtraTabPage_Setting.Name = "xtraTabPage_Setting";
            this.xtraTabPage_Setting.Size = new System.Drawing.Size(819, 411);
            this.xtraTabPage_Setting.Text = "Cài đặt quy trình";
            // 
            // xtraTabPage_Task
            // 
            this.xtraTabPage_Task.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage_Task.ImageOptions.Image")));
            this.xtraTabPage_Task.Name = "xtraTabPage_Task";
            this.xtraTabPage_Task.Size = new System.Drawing.Size(819, 411);
            this.xtraTabPage_Task.Text = "Quản lý xét duyệt";
            // 
            // XtraForm_ApprovalProcesscs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 439);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "XtraForm_ApprovalProcesscs";
            this.Text = "Quy trình xét duyệt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.XtraForm_ApprovalProcesscs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage_Setting;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage_Task;
    }
}
