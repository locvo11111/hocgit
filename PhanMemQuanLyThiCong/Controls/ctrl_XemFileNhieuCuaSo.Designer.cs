
namespace PhanMemQuanLyThiCong
{
    partial class ctrl_XemFileNhieuCuaSo
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
            this.cbb_SoCuaSo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbb_SoCuaSo
            // 
            this.cbb_SoCuaSo.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbb_SoCuaSo.FormattingEnabled = true;
            this.cbb_SoCuaSo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbb_SoCuaSo.Location = new System.Drawing.Point(806, 0);
            this.cbb_SoCuaSo.Name = "cbb_SoCuaSo";
            this.cbb_SoCuaSo.Size = new System.Drawing.Size(58, 21);
            this.cbb_SoCuaSo.TabIndex = 0;
            this.cbb_SoCuaSo.SelectedIndexChanged += new System.EventHandler(this.cbb_SoCuaSo_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbb_SoCuaSo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 450);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 27);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(751, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(55, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Số cửa sổ";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(864, 450);
            this.Root.TextVisible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(864, 450);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ctrl_XemFileNhieuCuaSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ctrl_XemFileNhieuCuaSo";
            this.Size = new System.Drawing.Size(864, 477);
            this.Load += new System.EventHandler(this.ctrl_XemFileNhieuCuaSo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbb_SoCuaSo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
    }
}
