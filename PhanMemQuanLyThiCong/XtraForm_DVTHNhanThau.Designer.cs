
namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_DVTHNhanThau
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
            this.sb_Save = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sb_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ctrl_DonViThucHienDuAn = new PhanMemQuanLyThiCong.Controls.Ctrl_DonViThucHienDuAn();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // sb_Save
            // 
            this.sb_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sb_Save.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_Save.Appearance.Options.UseBackColor = true;
            this.sb_Save.Appearance.Options.UseFont = true;
            this.sb_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sb_Save.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_database_export_24;
            this.sb_Save.Location = new System.Drawing.Point(12, 36);
            this.sb_Save.Name = "sb_Save";
            this.sb_Save.Size = new System.Drawing.Size(279, 28);
            this.sb_Save.StyleController = this.layoutControl1;
            this.sb_Save.TabIndex = 11;
            this.sb_Save.Text = "Lưu thay đổi và đóng";
            this.sb_Save.Click += new System.EventHandler(this.sb_Save_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ctrl_DonViThucHienDuAn);
            this.layoutControl1.Controls.Add(this.sb_Huy);
            this.layoutControl1.Controls.Add(this.sb_Save);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(586, 87);
            this.layoutControl1.TabIndex = 14;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_Huy
            // 
            this.sb_Huy.Appearance.BackColor = System.Drawing.Color.Red;
            this.sb_Huy.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sb_Huy.Appearance.Options.UseBackColor = true;
            this.sb_Huy.Appearance.Options.UseFont = true;
            this.sb_Huy.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_cancel_25px;
            this.sb_Huy.Location = new System.Drawing.Point(295, 36);
            this.sb_Huy.Name = "sb_Huy";
            this.sb_Huy.Size = new System.Drawing.Size(279, 29);
            this.sb_Huy.StyleController = this.layoutControl1;
            this.sb_Huy.TabIndex = 13;
            this.sb_Huy.Text = "Hủy chọn";
            this.sb_Huy.Click += new System.EventHandler(this.sb_Huy_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(586, 87);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sb_Save;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(283, 33);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 57);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(566, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_Huy;
            this.layoutControlItem2.Location = new System.Drawing.Point(283, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(283, 33);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ctrl_DonViThucHienDuAn
            // 
            this.ctrl_DonViThucHienDuAn.DataSource = null;
            this.ctrl_DonViThucHienDuAn.EditValue = "Chọn đơn vị thực hiện";
            this.ctrl_DonViThucHienDuAn.Location = new System.Drawing.Point(107, 12);
            this.ctrl_DonViThucHienDuAn.MaximumSize = new System.Drawing.Size(1000, 20);
            this.ctrl_DonViThucHienDuAn.MinimumSize = new System.Drawing.Size(0, 20);
            this.ctrl_DonViThucHienDuAn.Name = "ctrl_DonViThucHienDuAn";
            this.ctrl_DonViThucHienDuAn.Size = new System.Drawing.Size(467, 20);
            this.ctrl_DonViThucHienDuAn.TabIndex = 15;
            this.ctrl_DonViThucHienDuAn.Visible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ctrl_DonViThucHienDuAn;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(566, 24);
            this.layoutControlItem3.Text = "Đơn vị nhận thầu";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(83, 13);
            // 
            // XtraForm_DVTHNhanThau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 87);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_DVTHNhanThau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lựa chọn đơn vị nhận thầu";
            this.Load += new System.EventHandler(this.XtraForm_DVTHNhanThau_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sb_Save;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Controls.Ctrl_DonViThucHienDuAn ctrl_DonViThucHienDuAn;
        private DevExpress.XtraEditors.SimpleButton sb_Huy;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}