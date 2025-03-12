
namespace PhanMemQuanLyThiCong
{
    partial class ctrl_XemFile
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
            this.scroll_ListFile = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pn_xemTruocFile = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.pn_xemTruocFile)).BeginInit();
            this.SuspendLayout();
            // 
            // scroll_ListFile
            // 
            this.scroll_ListFile.Appearance.BackColor = System.Drawing.Color.Silver;
            this.scroll_ListFile.Appearance.Options.UseBackColor = true;
            this.scroll_ListFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scroll_ListFile.Location = new System.Drawing.Point(0, 393);
            this.scroll_ListFile.Name = "scroll_ListFile";
            this.scroll_ListFile.Size = new System.Drawing.Size(1036, 86);
            this.scroll_ListFile.TabIndex = 72;
            // 
            // pn_xemTruocFile
            // 
            this.pn_xemTruocFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_xemTruocFile.Location = new System.Drawing.Point(0, 0);
            this.pn_xemTruocFile.Name = "pn_xemTruocFile";
            this.pn_xemTruocFile.Size = new System.Drawing.Size(1036, 393);
            this.pn_xemTruocFile.TabIndex = 73;
            this.pn_xemTruocFile.Text = "Xem file";
            this.pn_xemTruocFile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pn_xemTruocFile_MouseMove);
            // 
            // ctrl_XemFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pn_xemTruocFile);
            this.Controls.Add(this.scroll_ListFile);
            this.Name = "ctrl_XemFile";
            this.Size = new System.Drawing.Size(1036, 479);
            ((System.ComponentModel.ISupportInitialize)(this.pn_xemTruocFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.XtraScrollableControl scroll_ListFile;
        private DevExpress.XtraEditors.GroupControl pn_xemTruocFile;
    }
}
