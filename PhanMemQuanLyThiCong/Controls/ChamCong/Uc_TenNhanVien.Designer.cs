
namespace PhanMemQuanLyThiCong.Controls.ChamCong
{
    partial class Uc_TenNhanVien
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
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.sb_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.sb_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.glue_NhanVien = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glue_NhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.sb_Ok);
            this.layoutControl2.Controls.Add(this.sb_Huy);
            this.layoutControl2.Controls.Add(this.glue_NhanVien);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(792, 70);
            this.layoutControl2.TabIndex = 3;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // sb_Ok
            // 
            this.sb_Ok.Appearance.BackColor = System.Drawing.Color.Lime;
            this.sb_Ok.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_Ok.Appearance.Options.UseBackColor = true;
            this.sb_Ok.Appearance.Options.UseFont = true;
            this.sb_Ok.Location = new System.Drawing.Point(12, 36);
            this.sb_Ok.Name = "sb_Ok";
            this.sb_Ok.Size = new System.Drawing.Size(381, 22);
            this.sb_Ok.StyleController = this.layoutControl2;
            this.sb_Ok.TabIndex = 17;
            this.sb_Ok.Text = "Ok";
            this.sb_Ok.Click += new System.EventHandler(this.sb_Ok_Click);
            // 
            // sb_Huy
            // 
            this.sb_Huy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.sb_Huy.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_Huy.Appearance.Options.UseBackColor = true;
            this.sb_Huy.Appearance.Options.UseFont = true;
            this.sb_Huy.Location = new System.Drawing.Point(397, 36);
            this.sb_Huy.Name = "sb_Huy";
            this.sb_Huy.Size = new System.Drawing.Size(383, 22);
            this.sb_Huy.StyleController = this.layoutControl2;
            this.sb_Huy.TabIndex = 16;
            this.sb_Huy.Text = "Hủy";
            this.sb_Huy.Click += new System.EventHandler(this.sb_Huy_Click);
            // 
            // glue_NhanVien
            // 
            this.glue_NhanVien.Location = new System.Drawing.Point(117, 12);
            this.glue_NhanVien.Name = "glue_NhanVien";
            this.glue_NhanVien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_NhanVien.Properties.DisplayMember = "TenNhanVien";
            this.glue_NhanVien.Properties.NullText = "";
            this.glue_NhanVien.Properties.PopupView = this.gridLookUpEdit1View;
            this.glue_NhanVien.Properties.ValueMember = "Code";
            this.glue_NhanVien.Size = new System.Drawing.Size(663, 20);
            this.glue_NhanVien.StyleController = this.layoutControl2;
            this.glue_NhanVien.TabIndex = 15;
            this.glue_NhanVien.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(this.glue_NhanVien_CloseUp);
            this.glue_NhanVien.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.glue_NhanVien_CustomDisplayText);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsBehavior.Editable = false;
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsSelection.MultiSelect = true;
            this.gridLookUpEdit1View.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridLookUpEdit1View.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Fast;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridLookUpEdit1View_RowCellClick);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(792, 70);
            this.layoutControlGroup1.Text = "`";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem5.Control = this.glue_NhanVien;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(772, 24);
            this.layoutControlItem5.Text = "Tên nhân viên";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(93, 17);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sb_Huy;
            this.layoutControlItem1.Location = new System.Drawing.Point(385, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(387, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_Ok;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(385, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Code";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mã nhân viên";
            this.gridColumn2.FieldName = "MaNhanVien";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên nhân viên";
            this.gridColumn3.FieldName = "TenNhanVien";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Chức vụ";
            this.gridColumn4.FieldName = "ChucVu";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // Uc_TenNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl2);
            this.Name = "Uc_TenNhanVien";
            this.Size = new System.Drawing.Size(792, 70);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glue_NhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.GridLookUpEdit glue_NhanVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton sb_Ok;
        private DevExpress.XtraEditors.SimpleButton sb_Huy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
