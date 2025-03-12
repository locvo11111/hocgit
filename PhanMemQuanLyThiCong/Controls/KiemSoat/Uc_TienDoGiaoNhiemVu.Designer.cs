
namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    partial class Uc_TienDoGiaoNhiemVu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_TienDoGiaoNhiemVu));
            this.gc_HopDong = new DevExpress.XtraGantt.GanttControl();
            this.treeListColumn18 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn19 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn54 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn60 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn155 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_XuatFile = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gc_HopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_HopDong
            // 
            this.gc_HopDong.ChartMappings.FinishDateFieldName = "NgayKetThucThiCong";
            this.gc_HopDong.ChartMappings.StartDateFieldName = "NgayBatDauThiCong";
            this.gc_HopDong.ChartMappings.TextFieldName = "Description";
            this.gc_HopDong.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn18,
            this.treeListColumn19,
            this.treeListColumn54,
            this.treeListColumn60,
            this.treeListColumn155,
            this.treeListColumn1});
            this.gc_HopDong.Location = new System.Drawing.Point(12, 12);
            this.gc_HopDong.MinWidth = 100;
            this.gc_HopDong.Name = "gc_HopDong";
            this.gc_HopDong.OptionsBehavior.PopulateServiceColumns = true;
            this.gc_HopDong.OptionsCustomization.AllowModifyProgress = DevExpress.Utils.DefaultBoolean.False;
            this.gc_HopDong.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gc_HopDong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gc_HopDong.Size = new System.Drawing.Size(1310, 476);
            this.gc_HopDong.SplitterPosition = 639;
            this.gc_HopDong.TabIndex = 8;
            this.gc_HopDong.TreeListMappings.KeyFieldName = "UID";
            this.gc_HopDong.TreeListMappings.ParentFieldName = "ParentUID";
            this.gc_HopDong.WorkWeek.AddRange(new DevExpress.XtraGantt.WorkDayOfWeek[] {
            new DevExpress.XtraGantt.WorkDayOfWeek(System.DayOfWeek.Sunday, new DevExpress.XtraGantt.Scheduling.WorkTime[] {
                        new DevExpress.XtraGantt.Scheduling.WorkTime(System.TimeSpan.Parse("01:00:00"), System.TimeSpan.Parse("23:00:00"))}),
            new DevExpress.XtraGantt.WorkDayOfWeek(System.DayOfWeek.Monday, new DevExpress.XtraGantt.Scheduling.WorkTime[] {
                        new DevExpress.XtraGantt.Scheduling.WorkTime(System.TimeSpan.Parse("01:00:00"), System.TimeSpan.Parse("23:00:00"))}),
            new DevExpress.XtraGantt.WorkDayOfWeek(System.DayOfWeek.Tuesday, new DevExpress.XtraGantt.Scheduling.WorkTime[] {
                        new DevExpress.XtraGantt.Scheduling.WorkTime(System.TimeSpan.Parse("01:00:00"), System.TimeSpan.Parse("23:00:00"))}),
            new DevExpress.XtraGantt.WorkDayOfWeek(System.DayOfWeek.Wednesday, new DevExpress.XtraGantt.Scheduling.WorkTime[] {
                        new DevExpress.XtraGantt.Scheduling.WorkTime(System.TimeSpan.Parse("01:00:00"), System.TimeSpan.Parse("23:00:00"))}),
            new DevExpress.XtraGantt.WorkDayOfWeek(System.DayOfWeek.Thursday, new DevExpress.XtraGantt.Scheduling.WorkTime[] {
                        new DevExpress.XtraGantt.Scheduling.WorkTime(System.TimeSpan.Parse("01:00:00"), System.TimeSpan.Parse("23:00:00"))}),
            new DevExpress.XtraGantt.WorkDayOfWeek(System.DayOfWeek.Friday, new DevExpress.XtraGantt.Scheduling.WorkTime[] {
                        new DevExpress.XtraGantt.Scheduling.WorkTime(System.TimeSpan.Parse("01:00:00"), System.TimeSpan.Parse("23:00:00"))}),
            new DevExpress.XtraGantt.WorkDayOfWeek(System.DayOfWeek.Saturday, new DevExpress.XtraGantt.Scheduling.WorkTime[] {
                        new DevExpress.XtraGantt.Scheduling.WorkTime(System.TimeSpan.Parse("01:00:00"), System.TimeSpan.Parse("23:00:00"))})});
            this.gc_HopDong.ZoomMode = DevExpress.XtraGantt.GanttZoomMode.FixedIntervals;
            this.gc_HopDong.CustomDrawTimescaleColumn += new DevExpress.XtraGantt.CustomDrawTimescaleColumnEventHandler(this.gc_HopDong_CustomDrawTimescaleColumn);
            this.gc_HopDong.CustomDrawTask += new DevExpress.XtraGantt.CustomDrawTaskEventHandler(this.gc_HopDong_CustomDrawTask);
            this.gc_HopDong.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.gc_HopDong_NodeCellStyle);
            // 
            // treeListColumn18
            // 
            this.treeListColumn18.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn18.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.treeListColumn18.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn18.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn18.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn18.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.treeListColumn18.Caption = "Tiêu đề";
            this.treeListColumn18.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn18.FieldName = "TenCongViec";
            this.treeListColumn18.Name = "treeListColumn18";
            this.treeListColumn18.Visible = true;
            this.treeListColumn18.VisibleIndex = 0;
            this.treeListColumn18.Width = 269;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListColumn19
            // 
            this.treeListColumn19.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn19.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn19.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn19.Caption = "Ngày bắt đầu";
            this.treeListColumn19.FieldName = "NgayBatDauThiCong";
            this.treeListColumn19.Name = "treeListColumn19";
            this.treeListColumn19.Visible = true;
            this.treeListColumn19.VisibleIndex = 2;
            this.treeListColumn19.Width = 100;
            // 
            // treeListColumn54
            // 
            this.treeListColumn54.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn54.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn54.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn54.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn54.Caption = "Số ngày";
            this.treeListColumn54.FieldName = "ThoiGianThiCong";
            this.treeListColumn54.MinWidth = 50;
            this.treeListColumn54.Name = "treeListColumn54";
            this.treeListColumn54.Visible = true;
            this.treeListColumn54.VisibleIndex = 3;
            this.treeListColumn54.Width = 50;
            // 
            // treeListColumn60
            // 
            this.treeListColumn60.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn60.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn60.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn60.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn60.Caption = "Ngày kết thúc";
            this.treeListColumn60.FieldName = "NgayKetThucThiCong";
            this.treeListColumn60.Name = "treeListColumn60";
            this.treeListColumn60.Visible = true;
            this.treeListColumn60.VisibleIndex = 4;
            this.treeListColumn60.Width = 100;
            // 
            // treeListColumn155
            // 
            this.treeListColumn155.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn155.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn155.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn155.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn155.Caption = "Tỷ lệ hoàn thành";
            this.treeListColumn155.FieldName = "Progress";
            this.treeListColumn155.Format.FormatString = "{0}%";
            this.treeListColumn155.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.treeListColumn155.Name = "treeListColumn155";
            this.treeListColumn155.Visible = true;
            this.treeListColumn155.VisibleIndex = 5;
            this.treeListColumn155.Width = 100;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.Caption = "Người tham gia";
            this.treeListColumn1.FieldName = "MoTa";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_XuatFile);
            this.layoutControl1.Controls.Add(this.gc_HopDong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1334, 526);
            this.layoutControl1.TabIndex = 9;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1334, 526);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gc_HopDong;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1314, 480);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // sb_XuatFile
            // 
            this.sb_XuatFile.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.sb_XuatFile.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.sb_XuatFile.Appearance.Options.UseBackColor = true;
            this.sb_XuatFile.Appearance.Options.UseFont = true;
            this.sb_XuatFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.sb_XuatFile.Location = new System.Drawing.Point(12, 492);
            this.sb_XuatFile.Name = "sb_XuatFile";
            this.sb_XuatFile.Size = new System.Drawing.Size(1310, 22);
            this.sb_XuatFile.StyleController = this.layoutControl1;
            this.sb_XuatFile.TabIndex = 9;
            this.sb_XuatFile.Text = "Xuất File";
            this.sb_XuatFile.Click += new System.EventHandler(this.sb_XuatFile_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_XuatFile;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 480);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1314, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // Uc_TienDoGiaoNhiemVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 526);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Uc_TienDoGiaoNhiemVu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiến độ giao nhiệm vụ";
            ((System.ComponentModel.ISupportInitialize)(this.gc_HopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGantt.GanttControl gc_HopDong;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn18;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn19;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn54;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn60;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn155;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sb_XuatFile;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
