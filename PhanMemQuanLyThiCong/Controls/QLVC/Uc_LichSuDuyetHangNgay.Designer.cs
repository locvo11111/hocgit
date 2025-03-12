
namespace PhanMemQuanLyThiCong.Controls.QLVC
{
    partial class Uc_LichSuDuyetHangNgay
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
            this.gc_LichSuDuỵet = new DevExpress.XtraGrid.GridControl();
            this.gv_LichSuDuỵet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gc_LichSuDuỵet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_LichSuDuỵet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_LichSuDuỵet
            // 
            this.gc_LichSuDuỵet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_LichSuDuỵet.Location = new System.Drawing.Point(0, 0);
            this.gc_LichSuDuỵet.MainView = this.gv_LichSuDuỵet;
            this.gc_LichSuDuỵet.Name = "gc_LichSuDuỵet";
            this.gc_LichSuDuỵet.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1,
            this.repositoryItemImageComboBox1});
            this.gc_LichSuDuỵet.Size = new System.Drawing.Size(600, 343);
            this.gc_LichSuDuỵet.TabIndex = 0;
            this.gc_LichSuDuỵet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_LichSuDuỵet});
            // 
            // gv_LichSuDuỵet
            // 
            this.gv_LichSuDuỵet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gv_LichSuDuỵet.GridControl = this.gc_LichSuDuỵet;
            this.gv_LichSuDuỵet.Name = "gv_LichSuDuỵet";
            this.gv_LichSuDuỵet.OptionsBehavior.ReadOnly = true;
            this.gv_LichSuDuỵet.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Code";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày gửi duyệt";
            this.gridColumn2.ColumnEdit = this.repositoryItemDateEdit1;
            this.gridColumn2.FieldName = "Ngay";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "CodeCha";
            this.gridColumn3.FieldName = "CodeCha";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Khối lượng";
            this.gridColumn4.FieldName = "KhoiLuong";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Trạng thái";
            this.gridColumn5.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn5.FieldName = "TrangThai";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Đã duyệt", 2, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Đang xét duyệt", 1, 1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.svgImageCollection1;
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("bo_task", "image://svgimages/business objects/bo_task.svg");
            this.svgImageCollection1.Add("bo_scheduler", "image://svgimages/business objects/bo_scheduler.svg");
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Tên người gửi duyệt";
            this.gridColumn6.FieldName = "FullNameSend";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // Uc_LichSuDuyetHangNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gc_LichSuDuỵet);
            this.Name = "Uc_LichSuDuyetHangNgay";
            this.Size = new System.Drawing.Size(600, 343);
            ((System.ComponentModel.ISupportInitialize)(this.gc_LichSuDuỵet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_LichSuDuỵet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_LichSuDuỵet;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_LichSuDuỵet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
    }
}
