namespace PhanMemQuanLyThiCong.Controls
{
    partial class Uc_QLChuSoHuu
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Add = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_Ten = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ChuSoHuu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.col_GhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bt_Delete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_Delete)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.btn_Add);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 428);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(629, 41);
            this.panel1.TabIndex = 1;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.BackColor = System.Drawing.Color.Red;
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseBackColor = true;
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_macos_close_24;
            this.simpleButton2.Location = new System.Drawing.Point(364, 5);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(83, 32);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "Hủy";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton1.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_database_export_24;
            this.simpleButton1.Location = new System.Drawing.Point(199, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(159, 32);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Lưu thay đổi và đóng";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Appearance.BackColor = System.Drawing.Color.Green;
            this.btn_Add.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Appearance.Options.UseBackColor = true;
            this.btn_Add.Appearance.Options.UseFont = true;
            this.btn_Add.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_add_user_241;
            this.btn_Add.Location = new System.Drawing.Point(53, 5);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(140, 32);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "Thêm chủ sở hữu";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.bt_Delete,
            this.repositoryItemComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(629, 428);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gridControl1_ProcessGridKey);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_Ten,
            this.col_ChuSoHuu,
            this.col_GhiChu,
            this.col_Code,
            this.gridColumn1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.NewItemRowText = "Thêm chủ sở hữu";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // col_Ten
            // 
            this.col_Ten.Caption = "Tên chủ sở hữu";
            this.col_Ten.FieldName = "Ten";
            this.col_Ten.MinWidth = 150;
            this.col_Ten.Name = "col_Ten";
            this.col_Ten.Visible = true;
            this.col_Ten.VisibleIndex = 0;
            this.col_Ten.Width = 150;
            // 
            // col_ChuSoHuu
            // 
            this.col_ChuSoHuu.Caption = "Chủ sở hữu/Cá nhân/Thuê ngoài";
            this.col_ChuSoHuu.ColumnEdit = this.repositoryItemComboBox1;
            this.col_ChuSoHuu.FieldName = "LoaiSoHuu";
            this.col_ChuSoHuu.Name = "col_ChuSoHuu";
            this.col_ChuSoHuu.Visible = true;
            this.col_ChuSoHuu.VisibleIndex = 1;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "Cá nhân",
            "Thuê ngoài",
            "Chủ sở hữu",
            "Mượn"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // col_GhiChu
            // 
            this.col_GhiChu.Caption = "Ghi chú";
            this.col_GhiChu.FieldName = "GhiChu";
            this.col_GhiChu.MinWidth = 100;
            this.col_GhiChu.Name = "col_GhiChu";
            this.col_GhiChu.Visible = true;
            this.col_GhiChu.VisibleIndex = 2;
            this.col_GhiChu.Width = 100;
            // 
            // col_Code
            // 
            this.col_Code.Caption = "Code";
            this.col_Code.FieldName = "Code";
            this.col_Code.MinWidth = 100;
            this.col_Code.Name = "col_Code";
            this.col_Code.OptionsColumn.AllowEdit = false;
            this.col_Code.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Xóa";
            this.gridColumn1.ColumnEdit = this.bt_Delete;
            this.gridColumn1.MaxWidth = 50;
            this.gridColumn1.MinWidth = 50;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 50;
            // 
            // bt_Delete
            // 
            this.bt_Delete.AutoHeight = false;
            editorButtonImageOptions1.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_remove_24;
            this.bt_Delete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.bt_Delete.Name = "bt_Delete";
            this.bt_Delete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
            // 
            // Uc_QLChuSoHuu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Uc_QLChuSoHuu";
            this.Size = new System.Drawing.Size(629, 469);
            this.Load += new System.EventHandler(this.Uc_QLChuSoHuu_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_Delete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btn_Add;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col_Ten;
        private DevExpress.XtraGrid.Columns.GridColumn col_GhiChu;
        private DevExpress.XtraGrid.Columns.GridColumn col_Code;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit bt_Delete;
        private DevExpress.XtraGrid.Columns.GridColumn col_ChuSoHuu;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
    }
}
