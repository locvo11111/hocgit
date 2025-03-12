namespace PhanMemQuanLyThiCong
{
    partial class DevForm_ThemCaNhanVaoTHDA
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
            this.lookupUser = new DevExpress.XtraEditors.LookUpEdit();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lc_NguoiCanThem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lc_VaiTro = new DevExpress.XtraLayout.LayoutControlItem();
            this.lc_NhomQuyen = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lookupUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lc_NguoiCanThem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lc_VaiTro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lc_NhomQuyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // lookupUser
            // 
            this.lookupUser.EditValue = "Nhập email hoặc tên người dùng";
            this.lookupUser.Location = new System.Drawing.Point(84, 12);
            this.lookupUser.Name = "lookupUser";
            this.lookupUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupUser.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lookupUser.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            this.lookupUser.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookupUser.Size = new System.Drawing.Size(458, 20);
            this.lookupUser.StyleController = this.dataLayoutControl1;
            this.lookupUser.TabIndex = 0;
            this.lookupUser.AutoSearch += new DevExpress.XtraEditors.Controls.LookUpEditAutoSearchEventHandler(this.lookupUser_AutoSearch);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.radioGroup1);
            this.dataLayoutControl1.Controls.Add(this.comboBoxEdit2);
            this.dataLayoutControl1.Controls.Add(this.comboBoxEdit1);
            this.dataLayoutControl1.Controls.Add(this.lookupUser);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(571, 127);
            this.dataLayoutControl1.TabIndex = 1;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(84, 60);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội bộ", true, null, "Nội bộ"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Khách", true, null, "Khách")});
            this.radioGroup1.Size = new System.Drawing.Size(458, 34);
            this.radioGroup1.StyleController = this.dataLayoutControl1;
            this.radioGroup1.TabIndex = 6;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.Location = new System.Drawing.Point(84, 98);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Size = new System.Drawing.Size(458, 20);
            this.comboBoxEdit2.StyleController = this.dataLayoutControl1;
            this.comboBoxEdit2.TabIndex = 5;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(84, 36);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "Bình Bùi",
            "Hà Châu"});
            this.comboBoxEdit1.Size = new System.Drawing.Size(458, 20);
            this.comboBoxEdit1.StyleController = this.dataLayoutControl1;
            this.comboBoxEdit1.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lc_NguoiCanThem,
            this.lc_VaiTro,
            this.lc_NhomQuyen,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(554, 130);
            this.Root.TextVisible = false;
            // 
            // lc_NguoiCanThem
            // 
            this.lc_NguoiCanThem.Control = this.lookupUser;
            this.lc_NguoiCanThem.Location = new System.Drawing.Point(0, 0);
            this.lc_NguoiCanThem.Name = "lc_NguoiCanThem";
            this.lc_NguoiCanThem.Size = new System.Drawing.Size(534, 24);
            this.lc_NguoiCanThem.Text = "Email";
            this.lc_NguoiCanThem.TextSize = new System.Drawing.Size(60, 13);
            // 
            // lc_VaiTro
            // 
            this.lc_VaiTro.Control = this.comboBoxEdit1;
            this.lc_VaiTro.Location = new System.Drawing.Point(0, 24);
            this.lc_VaiTro.Name = "lc_VaiTro";
            this.lc_VaiTro.Size = new System.Drawing.Size(534, 24);
            this.lc_VaiTro.Text = "Vai trò";
            this.lc_VaiTro.TextSize = new System.Drawing.Size(60, 13);
            // 
            // lc_NhomQuyen
            // 
            this.lc_NhomQuyen.Control = this.comboBoxEdit2;
            this.lc_NhomQuyen.Location = new System.Drawing.Point(0, 86);
            this.lc_NhomQuyen.Name = "lc_NhomQuyen";
            this.lc_NhomQuyen.Size = new System.Drawing.Size(534, 24);
            this.lc_NhomQuyen.Text = "Nhóm quyền";
            this.lc_NhomQuyen.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.radioGroup1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(534, 38);
            this.layoutControlItem1.Text = "Hình thức";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 13);
            // 
            // DevForm_ThemCaNhanVaoTHDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 127);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "DevForm_ThemCaNhanVaoTHDA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DevForm_ThemCaNhanVaoTHDA";
            this.Load += new System.EventHandler(this.DevForm_ThemCaNhanVaoTHDA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookupUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lc_NguoiCanThem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lc_VaiTro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lc_NhomQuyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lookupUser;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lc_NguoiCanThem;
        private DevExpress.XtraLayout.LayoutControlItem lc_VaiTro;
        private DevExpress.XtraLayout.LayoutControlItem lc_NhomQuyen;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}