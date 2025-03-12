
namespace PhanMemQuanLyThiCong.Controls.QLVC
{
    partial class Uc_DanhSachKhoChung
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_Ten = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_GhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bt_Delete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.sb_Close = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sb_Save = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Add = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.col_SortId = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_Delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
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
            this.gridControl1.Size = new System.Drawing.Size(708, 460);
            this.gridControl1.TabIndex = 4;
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
            this.col_GhiChu,
            this.col_Code,
            this.gridColumn1,
            this.col_SortId});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.NewItemRowText = "Thêm chủ sở hữu";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // col_Ten
            // 
            this.col_Ten.Caption = "Tên kho chung";
            this.col_Ten.FieldName = "Ten";
            this.col_Ten.MinWidth = 150;
            this.col_Ten.Name = "col_Ten";
            this.col_Ten.Visible = true;
            this.col_Ten.VisibleIndex = 0;
            this.col_Ten.Width = 150;
            // 
            // col_GhiChu
            // 
            this.col_GhiChu.Caption = "Ghi chú";
            this.col_GhiChu.FieldName = "GhiChu";
            this.col_GhiChu.MinWidth = 100;
            this.col_GhiChu.Name = "col_GhiChu";
            this.col_GhiChu.Visible = true;
            this.col_GhiChu.VisibleIndex = 1;
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
            this.gridColumn1.VisibleIndex = 2;
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
            // sb_Close
            // 
            this.sb_Close.Appearance.BackColor = System.Drawing.Color.Red;
            this.sb_Close.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_Close.Appearance.Options.UseBackColor = true;
            this.sb_Close.Appearance.Options.UseFont = true;
            this.sb_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sb_Close.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_macos_close_24;
            this.sb_Close.Location = new System.Drawing.Point(470, 12);
            this.sb_Close.Name = "sb_Close";
            this.sb_Close.Size = new System.Drawing.Size(226, 28);
            this.sb_Close.StyleController = this.layoutControl1;
            this.sb_Close.TabIndex = 2;
            this.sb_Close.Text = "Hủy";
            this.sb_Close.Click += new System.EventHandler(this.sb_Close_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_Close);
            this.layoutControl1.Controls.Add(this.sb_Save);
            this.layoutControl1.Controls.Add(this.btn_Add);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutControl1.Location = new System.Drawing.Point(0, 460);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(708, 56);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_Save
            // 
            this.sb_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sb_Save.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_Save.Appearance.Options.UseBackColor = true;
            this.sb_Save.Appearance.Options.UseFont = true;
            this.sb_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sb_Save.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_database_export_24;
            this.sb_Save.Location = new System.Drawing.Point(241, 12);
            this.sb_Save.Name = "sb_Save";
            this.sb_Save.Size = new System.Drawing.Size(225, 28);
            this.sb_Save.StyleController = this.layoutControl1;
            this.sb_Save.TabIndex = 1;
            this.sb_Save.Text = "Lưu thay đổi và đóng";
            this.sb_Save.Click += new System.EventHandler(this.sb_Save_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Appearance.BackColor = System.Drawing.Color.Green;
            this.btn_Add.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Appearance.Options.UseBackColor = true;
            this.btn_Add.Appearance.Options.UseFont = true;
            this.btn_Add.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_add_user_241;
            this.btn_Add.Location = new System.Drawing.Point(12, 12);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(225, 28);
            this.btn_Add.StyleController = this.layoutControl1;
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "Thêm kho chung";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(708, 56);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btn_Add;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(229, 36);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_Save;
            this.layoutControlItem2.Location = new System.Drawing.Point(229, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(229, 36);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Close;
            this.layoutControlItem3.Location = new System.Drawing.Point(458, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(230, 36);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // col_SortId
            // 
            this.col_SortId.Caption = "col_SortId";
            this.col_SortId.FieldName = "SortId";
            this.col_SortId.Name = "col_SortId";
            // 
            // Uc_DanhSachKhoChung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Uc_DanhSachKhoChung";
            this.Size = new System.Drawing.Size(708, 516);
            this.Load += new System.EventHandler(this.Uc_DanhSachKhoChung_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bt_Delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col_Ten;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn col_GhiChu;
        private DevExpress.XtraGrid.Columns.GridColumn col_Code;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit bt_Delete;
        private DevExpress.XtraEditors.SimpleButton sb_Close;
        private DevExpress.XtraEditors.SimpleButton sb_Save;
        private DevExpress.XtraEditors.SimpleButton btn_Add;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn col_SortId;
    }
}
