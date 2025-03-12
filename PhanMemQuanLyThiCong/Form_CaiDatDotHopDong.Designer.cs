
namespace PhanMemQuanLyThiCong
{
    partial class Form_CaiDatDotHopDong
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
            this.gc_ThongTinHD = new DevExpress.XtraGrid.GridControl();
            this.gv_ThongTinHopDong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_ChonDot = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Ricce_Dot = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sb_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.sbHuy = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gc_ThongTinHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ThongTinHopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ricce_Dot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_ThongTinHD
            // 
            this.gc_ThongTinHD.Location = new System.Drawing.Point(12, 12);
            this.gc_ThongTinHD.MainView = this.gv_ThongTinHopDong;
            this.gc_ThongTinHD.Name = "gc_ThongTinHD";
            this.gc_ThongTinHD.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.Ricce_Dot,
            this.repositoryItemCheckedComboBoxEdit1});
            this.gc_ThongTinHD.Size = new System.Drawing.Size(977, 278);
            this.gc_ThongTinHD.TabIndex = 0;
            this.gc_ThongTinHD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_ThongTinHopDong});
            // 
            // gv_ThongTinHopDong
            // 
            this.gv_ThongTinHopDong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gc_ChonDot});
            this.gv_ThongTinHopDong.GridControl = this.gc_ThongTinHD;
            this.gv_ThongTinHopDong.Name = "gv_ThongTinHopDong";
            this.gv_ThongTinHopDong.OptionsView.ShowGroupPanel = false;
            this.gv_ThongTinHopDong.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gv_ThongTinHopDong_ShowingEditor);
            this.gv_ThongTinHopDong.ShownEditor += new System.EventHandler(this.gv_ThongTinHopDong_ShownEditor);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên hợp đồng con liên quan";
            this.gridColumn1.FieldName = "TenHopDong";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gc_ChonDot
            // 
            this.gc_ChonDot.Caption = "Lựa chọn đợt hợp đồng";
            this.gc_ChonDot.ColumnEdit = this.Ricce_Dot;
            this.gc_ChonDot.FieldName = "Dot";
            this.gc_ChonDot.Name = "gc_ChonDot";
            this.gc_ChonDot.Visible = true;
            this.gc_ChonDot.VisibleIndex = 1;
            // 
            // Ricce_Dot
            // 
            this.Ricce_Dot.AutoHeight = false;
            this.Ricce_Dot.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Ricce_Dot.Name = "Ricce_Dot";
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_Ok);
            this.layoutControl1.Controls.Add(this.sbHuy);
            this.layoutControl1.Controls.Add(this.gc_ThongTinHD);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1001, 342);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_Ok
            // 
            this.sb_Ok.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sb_Ok.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_Ok.Appearance.Options.UseBackColor = true;
            this.sb_Ok.Appearance.Options.UseFont = true;
            this.sb_Ok.Location = new System.Drawing.Point(502, 294);
            this.sb_Ok.Name = "sb_Ok";
            this.sb_Ok.Size = new System.Drawing.Size(487, 36);
            this.sb_Ok.StyleController = this.layoutControl1;
            this.sb_Ok.TabIndex = 5;
            this.sb_Ok.Text = "Lưu thay đổi";
            this.sb_Ok.Click += new System.EventHandler(this.sb_Ok_Click);
            // 
            // sbHuy
            // 
            this.sbHuy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sbHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sbHuy.Appearance.Options.UseBackColor = true;
            this.sbHuy.Appearance.Options.UseFont = true;
            this.sbHuy.Location = new System.Drawing.Point(12, 294);
            this.sbHuy.Name = "sbHuy";
            this.sbHuy.Size = new System.Drawing.Size(486, 36);
            this.sbHuy.StyleController = this.layoutControl1;
            this.sbHuy.TabIndex = 4;
            this.sbHuy.Text = "Hủy thay đổi";
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
            this.Root.Size = new System.Drawing.Size(1001, 342);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gc_ThongTinHD;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(981, 282);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbHuy;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 282);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(490, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Ok;
            this.layoutControlItem3.Location = new System.Drawing.Point(490, 282);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(491, 40);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Form_CaiDatDotHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 342);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Form_CaiDatDotHopDong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt đợt cho Hợp đồng:";
            ((System.ComponentModel.ISupportInitialize)(this.gc_ThongTinHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_ThongTinHopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ricce_Dot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_ThongTinHD;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_ThongTinHopDong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_ChonDot;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit Ricce_Dot;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sb_Ok;
        private DevExpress.XtraEditors.SimpleButton sbHuy;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
    }
}