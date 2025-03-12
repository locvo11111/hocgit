
namespace PhanMemQuanLyThiCong.Controls.MTC
{
    partial class XtraForm_ThemDinhMucMay
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
            this.sb_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ChonAll = new DevExpress.XtraEditors.SimpleButton();
            this.tl_TenDinhMuc = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.sb_Save = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_TenDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_Huy);
            this.layoutControl1.Controls.Add(this.sb_ChonAll);
            this.layoutControl1.Controls.Add(this.tl_TenDinhMuc);
            this.layoutControl1.Controls.Add(this.sb_Save);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(695, 408);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_Huy
            // 
            this.sb_Huy.Appearance.BackColor = System.Drawing.Color.Red;
            this.sb_Huy.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sb_Huy.Appearance.Options.UseBackColor = true;
            this.sb_Huy.Appearance.Options.UseFont = true;
            this.sb_Huy.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_cancel_25px;
            this.sb_Huy.Location = new System.Drawing.Point(462, 357);
            this.sb_Huy.Name = "sb_Huy";
            this.sb_Huy.Size = new System.Drawing.Size(221, 29);
            this.sb_Huy.StyleController = this.layoutControl1;
            this.sb_Huy.TabIndex = 9;
            this.sb_Huy.Text = "Hủy chọn";
            this.sb_Huy.Click += new System.EventHandler(this.sb_Huy_Click);
            // 
            // sb_ChonAll
            // 
            this.sb_ChonAll.Appearance.BackColor = System.Drawing.Color.Lime;
            this.sb_ChonAll.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sb_ChonAll.Appearance.Options.UseBackColor = true;
            this.sb_ChonAll.Appearance.Options.UseFont = true;
            this.sb_ChonAll.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_add_25;
            this.sb_ChonAll.Location = new System.Drawing.Point(237, 357);
            this.sb_ChonAll.Name = "sb_ChonAll";
            this.sb_ChonAll.Size = new System.Drawing.Size(221, 29);
            this.sb_ChonAll.StyleController = this.layoutControl1;
            this.sb_ChonAll.TabIndex = 8;
            this.sb_ChonAll.Text = "Chọn tất cả";
            this.sb_ChonAll.Click += new System.EventHandler(this.sb_ChonAll_Click);
            // 
            // tl_TenDinhMuc
            // 
            this.tl_TenDinhMuc.CheckBoxFieldName = "Chon";
            this.tl_TenDinhMuc.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn6});
            this.tl_TenDinhMuc.KeyFieldName = "Code";
            this.tl_TenDinhMuc.Location = new System.Drawing.Point(12, 12);
            this.tl_TenDinhMuc.Name = "tl_TenDinhMuc";
            this.tl_TenDinhMuc.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.tl_TenDinhMuc.OptionsSelection.MultiSelect = true;
            this.tl_TenDinhMuc.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.tl_TenDinhMuc.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
            this.tl_TenDinhMuc.ParentFieldName = "CodeMay";
            this.tl_TenDinhMuc.Size = new System.Drawing.Size(671, 341);
            this.tl_TenDinhMuc.TabIndex = 7;
            this.tl_TenDinhMuc.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_TenDinhMuc_NodeCellStyle);
            this.tl_TenDinhMuc.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.tl_TenDinhMuc_AfterCheckNode);
            this.tl_TenDinhMuc.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.tl_TenDinhMuc_CustomDrawNodeCell);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Code";
            this.treeListColumn1.FieldName = "Code";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Tên định mức";
            this.treeListColumn2.FieldName = "DinhMucCongViec";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Đơn vị";
            this.treeListColumn3.FieldName = "DonVi";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Mức tiêu thụ";
            this.treeListColumn4.FieldName = "MucTieuThu";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 2;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Ghi chú";
            this.treeListColumn5.FieldName = "GhiChu";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 3;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "CodeMay";
            this.treeListColumn6.FieldName = "CodeMay";
            this.treeListColumn6.Name = "treeListColumn6";
            // 
            // sb_Save
            // 
            this.sb_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sb_Save.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_Save.Appearance.Options.UseBackColor = true;
            this.sb_Save.Appearance.Options.UseFont = true;
            this.sb_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sb_Save.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_database_export_24;
            this.sb_Save.Location = new System.Drawing.Point(12, 357);
            this.sb_Save.Name = "sb_Save";
            this.sb_Save.Size = new System.Drawing.Size(221, 28);
            this.sb_Save.StyleController = this.layoutControl1;
            this.sb_Save.TabIndex = 4;
            this.sb_Save.Text = "Lưu thay đổi và đóng";
            this.sb_Save.Click += new System.EventHandler(this.sb_Save_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(695, 408);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 378);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(675, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_Save;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 345);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(225, 33);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.tl_TenDinhMuc;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(675, 345);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sb_ChonAll;
            this.layoutControlItem1.Location = new System.Drawing.Point(225, 345);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(225, 33);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Huy;
            this.layoutControlItem3.Location = new System.Drawing.Point(450, 345);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(225, 33);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // XtraForm_ThemDinhMucMay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 408);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_ThemDinhMucMay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm máy và Tên quy đổi tương ứng";
            this.Load += new System.EventHandler(this.XtraForm_ThemDinhMucMay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_TenDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList tl_TenDinhMuc;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraEditors.SimpleButton sb_Save;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraEditors.SimpleButton sb_Huy;
        private DevExpress.XtraEditors.SimpleButton sb_ChonAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}