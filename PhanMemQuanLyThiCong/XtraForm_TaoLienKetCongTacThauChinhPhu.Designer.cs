
namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_TaoLienKetCongTacThauChinhPhu
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
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tl_Link = new DevExpress.XtraTreeList.TreeList();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_Ok = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tl_Link)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_Ok);
            this.layoutControl1.Controls.Add(this.sb_Huy);
            this.layoutControl1.Controls.Add(this.tl_Link);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(797, 461);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(797, 461);
            this.Root.TextVisible = false;
            // 
            // tl_Link
            // 
            this.tl_Link.CheckBoxFieldName = "Chon";
            this.tl_Link.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn6,
            this.treeListColumn4});
            this.tl_Link.KeyFieldName = "Id";
            this.tl_Link.Location = new System.Drawing.Point(12, 12);
            this.tl_Link.Name = "tl_Link";
            this.tl_Link.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Radio;
            this.tl_Link.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.None;
            this.tl_Link.ParentFieldName = "ParentId";
            this.tl_Link.Size = new System.Drawing.Size(773, 404);
            this.tl_Link.TabIndex = 4;
            this.tl_Link.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_Link_NodeCellStyle);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tl_Link;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(777, 408);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // sb_Huy
            // 
            this.sb_Huy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sb_Huy.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_Huy.Appearance.Options.UseBackColor = true;
            this.sb_Huy.Appearance.Options.UseFont = true;
            this.sb_Huy.Location = new System.Drawing.Point(400, 420);
            this.sb_Huy.Name = "sb_Huy";
            this.sb_Huy.Size = new System.Drawing.Size(385, 29);
            this.sb_Huy.StyleController = this.layoutControl1;
            this.sb_Huy.TabIndex = 6;
            this.sb_Huy.Text = "Hủy";
            this.sb_Huy.Click += new System.EventHandler(this.sb_Huy_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Huy;
            this.layoutControlItem3.Location = new System.Drawing.Point(388, 408);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(389, 33);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // sb_Ok
            // 
            this.sb_Ok.Appearance.BackColor = System.Drawing.Color.Lime;
            this.sb_Ok.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_Ok.Appearance.Options.UseBackColor = true;
            this.sb_Ok.Appearance.Options.UseFont = true;
            this.sb_Ok.Location = new System.Drawing.Point(12, 420);
            this.sb_Ok.Name = "sb_Ok";
            this.sb_Ok.Size = new System.Drawing.Size(384, 29);
            this.sb_Ok.StyleController = this.layoutControl1;
            this.sb_Ok.TabIndex = 7;
            this.sb_Ok.Text = "Ok";
            this.sb_Ok.Click += new System.EventHandler(this.sb_Ok_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sb_Ok;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 408);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(388, 33);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Code";
            this.treeListColumn1.FieldName = "Code";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Mã công tác";
            this.treeListColumn2.FieldName = "MaHieuCongTac";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Tên công tác";
            this.treeListColumn3.FieldName = "TenCongTac";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 0;
            this.treeListColumn3.Width = 288;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Khối lượng hợp đồng";
            this.treeListColumn4.FieldName = "KhoiLuongHopDong";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 2;
            this.treeListColumn4.Width = 252;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Đơn vị";
            this.treeListColumn6.FieldName = "DonVi";
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 1;
            this.treeListColumn6.Width = 208;
            // 
            // XtraForm_TaoLienKetCongTacThauChinhPhu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 461);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_TaoLienKetCongTacThauChinhPhu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo liên kết Công tác";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tl_Link)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton sb_Ok;
        private DevExpress.XtraEditors.SimpleButton sb_Huy;
        private DevExpress.XtraTreeList.TreeList tl_Link;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
    }
}