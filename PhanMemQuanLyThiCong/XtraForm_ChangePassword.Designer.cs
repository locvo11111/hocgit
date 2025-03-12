namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_ChangePassword
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
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txt_OldPass = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txt_NewPass = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bt_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bt_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txt_Confirm = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_OldPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NewPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Confirm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txt_Confirm);
            this.layoutControl1.Controls.Add(this.bt_Ok);
            this.layoutControl1.Controls.Add(this.bt_cancel);
            this.layoutControl1.Controls.Add(this.txt_NewPass);
            this.layoutControl1.Controls.Add(this.txt_OldPass);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(502, 119);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(502, 119);
            this.Root.TextVisible = false;
            // 
            // txt_OldPass
            // 
            this.txt_OldPass.Location = new System.Drawing.Point(134, 12);
            this.txt_OldPass.Name = "txt_OldPass";
            this.txt_OldPass.Properties.UseSystemPasswordChar = true;
            this.txt_OldPass.Size = new System.Drawing.Size(356, 20);
            this.txt_OldPass.StyleController = this.layoutControl1;
            this.txt_OldPass.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txt_OldPass;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(482, 24);
            this.layoutControlItem1.Text = "Mật khẩu hiện tại";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(110, 13);
            // 
            // txt_NewPass
            // 
            this.txt_NewPass.Location = new System.Drawing.Point(134, 36);
            this.txt_NewPass.Name = "txt_NewPass";
            this.txt_NewPass.Properties.PasswordChar = '*';
            this.txt_NewPass.Properties.UseSystemPasswordChar = true;
            this.txt_NewPass.Size = new System.Drawing.Size(356, 20);
            this.txt_NewPass.StyleController = this.layoutControl1;
            this.txt_NewPass.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txt_NewPass;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(482, 24);
            this.layoutControlItem2.Text = "Mật khẩu mới";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(110, 13);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Appearance.BackColor = System.Drawing.Color.Red;
            this.bt_cancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_cancel.Appearance.Options.UseBackColor = true;
            this.bt_cancel.Appearance.Options.UseFont = true;
            this.bt_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_cancel.Location = new System.Drawing.Point(370, 84);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(120, 22);
            this.bt_cancel.StyleController = this.layoutControl1;
            this.bt_cancel.TabIndex = 6;
            this.bt_cancel.Text = "Hủy";
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_cancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(358, 72);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(124, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(124, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(124, 27);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // bt_Ok
            // 
            this.bt_Ok.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bt_Ok.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_Ok.Appearance.Options.UseBackColor = true;
            this.bt_Ok.Appearance.Options.UseFont = true;
            this.bt_Ok.Location = new System.Drawing.Point(12, 84);
            this.bt_Ok.Name = "bt_Ok";
            this.bt_Ok.Size = new System.Drawing.Size(354, 22);
            this.bt_Ok.StyleController = this.layoutControl1;
            this.bt_Ok.TabIndex = 7;
            this.bt_Ok.Text = "Xác nhận";
            this.bt_Ok.Click += new System.EventHandler(this.bt_Ok_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.bt_Ok;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(358, 27);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // txt_Confirm
            // 
            this.txt_Confirm.Location = new System.Drawing.Point(134, 60);
            this.txt_Confirm.Name = "txt_Confirm";
            this.txt_Confirm.Properties.PasswordChar = '*';
            this.txt_Confirm.Properties.UseSystemPasswordChar = true;
            this.txt_Confirm.Size = new System.Drawing.Size(356, 20);
            this.txt_Confirm.StyleController = this.layoutControl1;
            this.txt_Confirm.TabIndex = 8;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txt_Confirm;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(482, 24);
            this.layoutControlItem5.Text = "Xác nhận mật khẩu mới";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(110, 13);
            // 
            // XtraForm_ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 119);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "XtraForm_ChangePassword";
            this.Text = "Đổi mật khẩu";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_OldPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NewPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Confirm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txt_NewPass;
        private DevExpress.XtraEditors.TextEdit txt_OldPass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txt_Confirm;
        private DevExpress.XtraEditors.SimpleButton bt_Ok;
        private DevExpress.XtraEditors.SimpleButton bt_cancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}