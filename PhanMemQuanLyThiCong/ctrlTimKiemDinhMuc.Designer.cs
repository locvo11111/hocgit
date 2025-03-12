
namespace PhanMemQuanLyThiCong
{
    partial class ctrlTimKiemDinhMuc
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.col_Chon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uc_ChonTinhThanh1 = new PhanMemQuanLyThiCong.Controls.uc_ChonTinhThanh();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.bt_OK = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.gc_DinhMuc = new DevExpress.XtraGrid.GridControl();
            this.gv_DinhMuc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_DinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_DinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // col_Chon
            // 
            this.col_Chon.Caption = "Chọn";
            this.col_Chon.FieldName = "Chon";
            this.col_Chon.MaxWidth = 50;
            this.col_Chon.MinWidth = 50;
            this.col_Chon.Name = "col_Chon";
            this.col_Chon.Visible = true;
            this.col_Chon.VisibleIndex = 0;
            this.col_Chon.Width = 50;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uc_ChonTinhThanh1);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.bt_OK);
            this.panel1.Controls.Add(this.bt_cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 450);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 24);
            this.panel1.TabIndex = 1;
            // 
            // uc_ChonTinhThanh1
            // 
            this.uc_ChonTinhThanh1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uc_ChonTinhThanh1.Location = new System.Drawing.Point(154, 0);
            this.uc_ChonTinhThanh1.Name = "uc_ChonTinhThanh1";
            this.uc_ChonTinhThanh1.Size = new System.Drawing.Size(431, 24);
            this.uc_ChonTinhThanh1.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.Blue;
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton1.Location = new System.Drawing.Point(0, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(154, 24);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Chọn định mức thực hiện";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // bt_OK
            // 
            this.bt_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bt_OK.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_OK.Location = new System.Drawing.Point(753, 0);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(103, 24);
            this.bt_OK.TabIndex = 0;
            this.bt_OK.Text = "Đồng ý";
            this.bt_OK.UseVisualStyleBackColor = false;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.BackColor = System.Drawing.Color.Red;
            this.bt_cancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.bt_cancel.ForeColor = System.Drawing.Color.White;
            this.bt_cancel.Location = new System.Drawing.Point(856, 0);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(103, 24);
            this.bt_cancel.TabIndex = 0;
            this.bt_cancel.Text = "Hủy bỏ";
            this.bt_cancel.UseVisualStyleBackColor = false;
            this.bt_cancel.Click += new System.EventHandler(this.bt_cancle_Click);
            // 
            // gc_DinhMuc
            // 
            this.gc_DinhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_DinhMuc.Location = new System.Drawing.Point(0, 0);
            this.gc_DinhMuc.MainView = this.gv_DinhMuc;
            this.gc_DinhMuc.Name = "gc_DinhMuc";
            this.gc_DinhMuc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gc_DinhMuc.Size = new System.Drawing.Size(959, 450);
            this.gc_DinhMuc.TabIndex = 2;
            this.gc_DinhMuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_DinhMuc});
            // 
            // gv_DinhMuc
            // 
            this.gv_DinhMuc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_Chon,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.col_Chon;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = true;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gv_DinhMuc.FormatRules.Add(gridFormatRule1);
            this.gv_DinhMuc.GridControl = this.gc_DinhMuc;
            this.gv_DinhMuc.Name = "gv_DinhMuc";
            this.gv_DinhMuc.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mã định mức";
            this.gridColumn2.FieldName = "MaDinhMuc";
            this.gridColumn2.MaxWidth = 120;
            this.gridColumn2.MinWidth = 120;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 120;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên định mức";
            this.gridColumn3.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn3.FieldName = "TenDinhMuc";
            this.gridColumn3.MinWidth = 250;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 250;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Đơn vị";
            this.gridColumn4.FieldName = "DonVi";
            this.gridColumn4.MaxWidth = 70;
            this.gridColumn4.MinWidth = 70;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Chi tiết định mức";
            this.gridColumn5.FieldName = "CTDinhMuc";
            this.gridColumn5.MaxWidth = 70;
            this.gridColumn5.MinWidth = 150;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 150;
            // 
            // ctrlTimKiemDinhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gc_DinhMuc);
            this.Controls.Add(this.panel1);
            this.Name = "ctrlTimKiemDinhMuc";
            this.Size = new System.Drawing.Size(959, 474);
            this.Load += new System.EventHandler(this.ctrlTimKiemDinhMuc_Load);
            this.VisibleChanged += new System.EventHandler(this.ctrlTimKiemDinhMuc_VisibleChanged_1);
            this.Leave += new System.EventHandler(this.ctrlTimKiemDinhMuc_Leave);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_DinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_DinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.Button bt_OK;
        private DevExpress.XtraGrid.GridControl gc_DinhMuc;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_DinhMuc;
        private DevExpress.XtraGrid.Columns.GridColumn col_Chon;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private Controls.uc_ChonTinhThanh uc_ChonTinhThanh1;
    }
}
