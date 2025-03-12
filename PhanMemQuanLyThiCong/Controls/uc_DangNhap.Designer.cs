namespace PhanMemQuanLyThiCong.Controls
{
    partial class uc_DangNhap
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_DangNhap));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.bt_DangNhap = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.UserNameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.loginRequestBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.RememberMeCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.ItemForUserName = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForRememberMe = new DevExpress.XtraLayout.LayoutControlItem();
            this.PasswordTextEdit = new PhanMemQuanLyThiCong.Controls.uc_CustomValidateEditor();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserNameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginRequestBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RememberMeCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForRememberMe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.PasswordTextEdit);
            this.dataLayoutControl1.Controls.Add(this.bt_DangNhap);
            this.dataLayoutControl1.Controls.Add(this.labelControl1);
            this.dataLayoutControl1.Controls.Add(this.UserNameTextEdit);
            this.dataLayoutControl1.Controls.Add(this.RememberMeCheckEdit);
            this.dataLayoutControl1.DataSource = this.loginRequestBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(408, 169);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // bt_DangNhap
            // 
            this.bt_DangNhap.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.bt_DangNhap.Appearance.Options.UseFont = true;
            this.bt_DangNhap.Location = new System.Drawing.Point(205, 121);
            this.bt_DangNhap.Name = "bt_DangNhap";
            this.bt_DangNhap.Size = new System.Drawing.Size(191, 24);
            this.bt_DangNhap.StyleController = this.dataLayoutControl1;
            this.bt_DangNhap.TabIndex = 8;
            this.bt_DangNhap.Text = "Đăng nhập";
            this.bt_DangNhap.Click += new System.EventHandler(this.bt_DangNhap_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(154, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(100, 19);
            this.labelControl1.StyleController = this.dataLayoutControl1;
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "ĐĂNG NHẬP";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(408, 169);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForUserName,
            this.ItemForRememberMe,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(388, 149);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem1.Control = this.labelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(388, 23);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bt_DangNhap;
            this.layoutControlItem2.Location = new System.Drawing.Point(193, 109);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(195, 28);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 109);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(193, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 23);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(388, 10);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 137);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(388, 12);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // UserNameTextEdit
            // 
            this.UserNameTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.loginRequestBindingSource, "UserName", true));
            this.UserNameTextEdit.Location = new System.Drawing.Point(127, 45);
            this.UserNameTextEdit.Name = "UserNameTextEdit";
            this.UserNameTextEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.UserNameTextEdit.Size = new System.Drawing.Size(269, 20);
            this.UserNameTextEdit.StyleController = this.dataLayoutControl1;
            this.UserNameTextEdit.TabIndex = 4;
            // 
            // loginRequestBindingSource
            // 
            this.loginRequestBindingSource.DataSource = typeof(PhanMemQuanLyThiCong.Model.LoginRequest);
            // 
            // RememberMeCheckEdit
            // 
            this.RememberMeCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.loginRequestBindingSource, "RememberMe", true));
            this.RememberMeCheckEdit.Location = new System.Drawing.Point(12, 97);
            this.RememberMeCheckEdit.Name = "RememberMeCheckEdit";
            this.RememberMeCheckEdit.Properties.Caption = "Ghi nhớ đăng nhập";
            this.RememberMeCheckEdit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.RememberMeCheckEdit.Size = new System.Drawing.Size(384, 20);
            this.RememberMeCheckEdit.StyleController = this.dataLayoutControl1;
            this.RememberMeCheckEdit.TabIndex = 6;
            // 
            // ItemForUserName
            // 
            this.ItemForUserName.Control = this.UserNameTextEdit;
            this.ItemForUserName.Location = new System.Drawing.Point(0, 33);
            this.ItemForUserName.Name = "ItemForUserName";
            this.ItemForUserName.Size = new System.Drawing.Size(388, 24);
            this.ItemForUserName.Text = "Tên người dùng/Email";
            this.ItemForUserName.TextSize = new System.Drawing.Size(103, 13);
            // 
            // ItemForRememberMe
            // 
            this.ItemForRememberMe.Control = this.RememberMeCheckEdit;
            this.ItemForRememberMe.Location = new System.Drawing.Point(0, 85);
            this.ItemForRememberMe.Name = "ItemForRememberMe";
            this.ItemForRememberMe.Size = new System.Drawing.Size(388, 24);
            this.ItemForRememberMe.Text = "Ghi nhớ đăng nhập";
            this.ItemForRememberMe.TextSize = new System.Drawing.Size(0, 0);
            this.ItemForRememberMe.TextVisible = false;
            // 
            // PasswordTextEdit
            // 
            this.PasswordTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.loginRequestBindingSource, "Password", true));
            this.PasswordTextEdit.Location = new System.Drawing.Point(127, 69);
            this.PasswordTextEdit.Name = "PasswordTextEdit";
            editorButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions1.SvgImage")));
            editorButtonImageOptions1.SvgImageSize = new System.Drawing.Size(16, 16);
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.PasswordTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Show", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "Mật khẩu", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.PasswordTextEdit.Properties.UseSystemPasswordChar = true;
            this.PasswordTextEdit.Size = new System.Drawing.Size(269, 24);
            this.PasswordTextEdit.StyleController = this.dataLayoutControl1;
            this.PasswordTextEdit.TabIndex = 9;
            this.PasswordTextEdit.TBTMode = PhanMemQuanLyThiCong.Common.Enums.ValidationModeEditorEnum.PASSWORD;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.PasswordTextEdit;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 57);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(388, 28);
            this.layoutControlItem3.Text = "Mật khẩu";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(103, 13);
            // 
            // uc_DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "uc_DangNhap";
            this.Size = new System.Drawing.Size(408, 169);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            this.dataLayoutControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserNameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginRequestBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RememberMeCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForRememberMe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit UserNameTextEdit;
        private System.Windows.Forms.BindingSource loginRequestBindingSource;
        private DevExpress.XtraEditors.CheckEdit RememberMeCheckEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForUserName;
        private DevExpress.XtraLayout.LayoutControlItem ItemForRememberMe;
        private DevExpress.XtraEditors.SimpleButton bt_DangNhap;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private uc_CustomValidateEditor PasswordTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}
