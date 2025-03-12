namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_LayMayThiCong
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bt_OK = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.tl_MTC = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_MTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bt_OK);
            this.layoutControl1.Controls.Add(this.bt_cancel);
            this.layoutControl1.Controls.Add(this.tl_MTC);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1011, 573);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bt_OK
            // 
            this.bt_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bt_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_OK.Location = new System.Drawing.Point(832, 531);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(76, 30);
            this.bt_OK.TabIndex = 5;
            this.bt_OK.Text = "Đồng ý";
            this.bt_OK.UseVisualStyleBackColor = false;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.BackColor = System.Drawing.Color.Red;
            this.bt_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_cancel.Location = new System.Drawing.Point(912, 531);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(87, 30);
            this.bt_cancel.TabIndex = 6;
            this.bt_cancel.Text = "Hủy bỏ";
            this.bt_cancel.UseVisualStyleBackColor = false;
            // 
            // tl_MTC
            // 
            this.tl_MTC.CheckBoxFieldName = "Chon";
            this.tl_MTC.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn5,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn7,
            this.treeListColumn4,
            this.treeListColumn6});
            this.tl_MTC.KeyFieldName = "Code";
            this.tl_MTC.Location = new System.Drawing.Point(12, 12);
            this.tl_MTC.Name = "tl_MTC";
            this.tl_MTC.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.tl_MTC.OptionsBehavior.ReadOnly = true;
            this.tl_MTC.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.tl_MTC.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
            this.tl_MTC.ParentFieldName = "ParentCode";
            this.tl_MTC.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.tl_MTC.Size = new System.Drawing.Size(987, 515);
            this.tl_MTC.TabIndex = 4;
            this.tl_MTC.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_MTC_NodeCellStyle);
            this.tl_MTC.DataSourceChanged += new System.EventHandler(this.tl_MTC_DataSourceChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Tên máy";
            this.treeListColumn1.ColumnEdit = this.repositoryItemMemoEdit1;
            this.treeListColumn1.FieldName = "VatTu";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 187;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Đơn vị";
            this.treeListColumn5.FieldName = "DonVi";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 4;
            this.treeListColumn5.Width = 129;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Số hiệu";
            this.treeListColumn2.FieldName = "MaVatLieu";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 129;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Trạng thái";
            this.treeListColumn3.FieldName = "TrangThai";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 2;
            this.treeListColumn3.Width = 130;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Mức tiêu thụ";
            this.treeListColumn7.FieldName = "HaoPhi";
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 6;
            this.treeListColumn7.Width = 129;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Giá mua";
            this.treeListColumn4.FieldName = "GiaMuaMay";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 3;
            this.treeListColumn4.Width = 129;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Giá thuê";
            this.treeListColumn6.FieldName = "GiaCaMay";
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 5;
            this.treeListColumn6.Width = 129;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1011, 573);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tl_MTC;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(991, 519);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bt_OK;
            this.layoutControlItem2.Location = new System.Drawing.Point(820, 519);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(80, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_cancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(900, 519);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(91, 34);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 519);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(820, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // XtraForm_LayMayThiCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 573);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_LayMayThiCong";
            this.Text = "Danh sách máy thi công";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_MTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList tl_MTC;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Button bt_cancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}