
namespace PhanMemQuanLyThiCong.Controls.ChamCong
{
    partial class Form_CaiDatNgayNghiCaNhan
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
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.me_GhiChu = new DevExpress.XtraEditors.MemoEdit();
            this.glue_NhanVien = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.de_NgayChamCong = new DevExpress.XtraEditors.DateEdit();
            this.sb_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.sb_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.ce_ChamMuti = new DevExpress.XtraEditors.CheckEdit();
            this.cbe_LyDoNghi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbe_SangChieuToi = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.me_GhiChu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_NhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayChamCong.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayChamCong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_ChamMuti.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_LyDoNghi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_SangChieuToi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.me_GhiChu);
            this.layoutControl2.Controls.Add(this.glue_NhanVien);
            this.layoutControl2.Controls.Add(this.de_NgayChamCong);
            this.layoutControl2.Controls.Add(this.sb_Huy);
            this.layoutControl2.Controls.Add(this.sb_Ok);
            this.layoutControl2.Controls.Add(this.ce_ChamMuti);
            this.layoutControl2.Controls.Add(this.cbe_LyDoNghi);
            this.layoutControl2.Controls.Add(this.cbe_SangChieuToi);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(821, 253);
            this.layoutControl2.TabIndex = 2;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // me_GhiChu
            // 
            this.me_GhiChu.Location = new System.Drawing.Point(89, 88);
            this.me_GhiChu.Name = "me_GhiChu";
            this.me_GhiChu.Size = new System.Drawing.Size(720, 102);
            this.me_GhiChu.StyleController = this.layoutControl2;
            this.me_GhiChu.TabIndex = 16;
            // 
            // glue_NhanVien
            // 
            this.glue_NhanVien.Enabled = false;
            this.glue_NhanVien.Location = new System.Drawing.Point(412, 194);
            this.glue_NhanVien.Name = "glue_NhanVien";
            this.glue_NhanVien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_NhanVien.Properties.DisplayMember = "TenNhanVien";
            this.glue_NhanVien.Properties.NullText = "";
            this.glue_NhanVien.Properties.PopupView = this.gridLookUpEdit1View;
            this.glue_NhanVien.Properties.ValueMember = "Code";
            this.glue_NhanVien.Size = new System.Drawing.Size(397, 20);
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
            // de_NgayChamCong
            // 
            this.de_NgayChamCong.EditValue = null;
            this.de_NgayChamCong.Location = new System.Drawing.Point(89, 12);
            this.de_NgayChamCong.Name = "de_NgayChamCong";
            this.de_NgayChamCong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_NgayChamCong.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_NgayChamCong.Size = new System.Drawing.Size(720, 20);
            this.de_NgayChamCong.StyleController = this.layoutControl2;
            this.de_NgayChamCong.TabIndex = 11;
            // 
            // sb_Huy
            // 
            this.sb_Huy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.sb_Huy.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_Huy.Appearance.Options.UseBackColor = true;
            this.sb_Huy.Appearance.Options.UseFont = true;
            this.sb_Huy.Location = new System.Drawing.Point(412, 218);
            this.sb_Huy.Name = "sb_Huy";
            this.sb_Huy.Size = new System.Drawing.Size(397, 22);
            this.sb_Huy.StyleController = this.layoutControl2;
            this.sb_Huy.TabIndex = 10;
            this.sb_Huy.Text = "Hủy";
            this.sb_Huy.Click += new System.EventHandler(this.sb_Huy_Click);
            // 
            // sb_Ok
            // 
            this.sb_Ok.Appearance.BackColor = System.Drawing.Color.Lime;
            this.sb_Ok.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_Ok.Appearance.Options.UseBackColor = true;
            this.sb_Ok.Appearance.Options.UseFont = true;
            this.sb_Ok.Location = new System.Drawing.Point(12, 219);
            this.sb_Ok.Name = "sb_Ok";
            this.sb_Ok.Size = new System.Drawing.Size(396, 22);
            this.sb_Ok.StyleController = this.layoutControl2;
            this.sb_Ok.TabIndex = 9;
            this.sb_Ok.Text = "Ok";
            this.sb_Ok.Click += new System.EventHandler(this.sb_Ok_Click);
            // 
            // ce_ChamMuti
            // 
            this.ce_ChamMuti.Location = new System.Drawing.Point(12, 194);
            this.ce_ChamMuti.Name = "ce_ChamMuti";
            this.ce_ChamMuti.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.ce_ChamMuti.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.ce_ChamMuti.Properties.Appearance.Options.UseFont = true;
            this.ce_ChamMuti.Properties.Appearance.Options.UseForeColor = true;
            this.ce_ChamMuti.Properties.Caption = "Cài đặt cho nhiều nhân viên";
            this.ce_ChamMuti.Size = new System.Drawing.Size(396, 21);
            this.ce_ChamMuti.StyleController = this.layoutControl2;
            this.ce_ChamMuti.TabIndex = 7;
            this.ce_ChamMuti.CheckedChanged += new System.EventHandler(this.ce_ChamMuti_CheckedChanged);
            // 
            // cbe_LyDoNghi
            // 
            this.cbe_LyDoNghi.EditValue = "Nghỉ phép";
            this.cbe_LyDoNghi.Location = new System.Drawing.Point(89, 36);
            this.cbe_LyDoNghi.Name = "cbe_LyDoNghi";
            this.cbe_LyDoNghi.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.cbe_LyDoNghi.Properties.Appearance.Options.UseFont = true;
            this.cbe_LyDoNghi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbe_LyDoNghi.Properties.Items.AddRange(new object[] {
            "Nghỉ phép",
            "Nghỉ ốm",
            "Học tập ",
            "Nghỉ đẻ",
            "Nghỉ công tác",
            "Nghỉ theo chế độ",
            "Nghỉ thứ 7, Chủ nhật",
            "Nghỉ không lý do"});
            this.cbe_LyDoNghi.Size = new System.Drawing.Size(720, 22);
            this.cbe_LyDoNghi.StyleController = this.layoutControl2;
            this.cbe_LyDoNghi.TabIndex = 5;
            // 
            // cbe_SangChieuToi
            // 
            this.cbe_SangChieuToi.EditValue = "1";
            this.cbe_SangChieuToi.Location = new System.Drawing.Point(89, 62);
            this.cbe_SangChieuToi.Name = "cbe_SangChieuToi";
            this.cbe_SangChieuToi.Properties.AllowMultiSelect = true;
            this.cbe_SangChieuToi.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.cbe_SangChieuToi.Properties.Appearance.Options.UseFont = true;
            this.cbe_SangChieuToi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbe_SangChieuToi.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((short)(1)), "Sáng", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((short)(2)), "Chiều"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((short)(3)), "Tối")});
            this.cbe_SangChieuToi.Size = new System.Drawing.Size(720, 22);
            this.cbe_SangChieuToi.StyleController = this.layoutControl2;
            this.cbe_SangChieuToi.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(821, 253);
            this.layoutControlGroup1.Text = "`";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem2.Control = this.cbe_LyDoNghi;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(801, 26);
            this.layoutControlItem2.Text = "Lý do nghỉ";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(65, 17);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem3.Control = this.cbe_SangChieuToi;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(801, 26);
            this.layoutControlItem3.Text = "Buổi nghỉ";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(65, 17);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ce_ChamMuti;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 182);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(400, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sb_Ok;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 207);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(400, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sb_Huy;
            this.layoutControlItem7.Location = new System.Drawing.Point(400, 206);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(401, 27);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem8.Control = this.de_NgayChamCong;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(801, 24);
            this.layoutControlItem8.Text = "Ngày nghỉ";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(65, 17);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.glue_NhanVien;
            this.layoutControlItem5.Location = new System.Drawing.Point(400, 182);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(401, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.me_GhiChu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(801, 106);
            this.layoutControlItem1.Text = "Ghi chú";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(65, 15);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 178);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(604, 53);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
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
            // Form_CaiDatNgayNghiCaNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 253);
            this.Controls.Add(this.layoutControl2);
            this.Name = "Form_CaiDatNgayNghiCaNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt ngày nghỉ";
            this.Load += new System.EventHandler(this.Form_CaiDatNgayNghiCaNhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.me_GhiChu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glue_NhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayChamCong.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_NgayChamCong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ce_ChamMuti.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_LyDoNghi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbe_SangChieuToi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
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
        private DevExpress.XtraEditors.DateEdit de_NgayChamCong;
        private DevExpress.XtraEditors.SimpleButton sb_Huy;
        private DevExpress.XtraEditors.SimpleButton sb_Ok;
        private DevExpress.XtraEditors.CheckEdit ce_ChamMuti;
        private DevExpress.XtraEditors.ComboBoxEdit cbe_LyDoNghi;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.MemoEdit me_GhiChu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbe_SangChieuToi;
    }
}