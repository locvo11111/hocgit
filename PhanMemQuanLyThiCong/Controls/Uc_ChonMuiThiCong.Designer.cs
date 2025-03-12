
namespace PhanMemQuanLyThiCong.Controls
{
    partial class Uc_ChonMuiThiCong
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tl_MTC = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Sb_Thoat = new DevExpress.XtraEditors.SimpleButton();
            this.sb_OK = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sb_BoChon = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_MTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_BoChon);
            this.layoutControl1.Controls.Add(this.tl_MTC);
            this.layoutControl1.Controls.Add(this.Sb_Thoat);
            this.layoutControl1.Controls.Add(this.sb_OK);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(562, 324);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tl_MTC
            // 
            this.tl_MTC.CheckBoxFieldName = "Chon";
            this.tl_MTC.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3});
            this.tl_MTC.Location = new System.Drawing.Point(12, 12);
            this.tl_MTC.Name = "tl_MTC";
            this.tl_MTC.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.tl_MTC.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Radio;
            this.tl_MTC.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Radio;
            this.tl_MTC.Size = new System.Drawing.Size(538, 274);
            this.tl_MTC.TabIndex = 7;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Code";
            this.treeListColumn1.FieldName = "Code";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Tên mũi thi công";
            this.treeListColumn2.FieldName = "Ten";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 337;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "GhiChu";
            this.treeListColumn3.FieldName = "GhiChu";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            this.treeListColumn3.Width = 176;
            // 
            // Sb_Thoat
            // 
            this.Sb_Thoat.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Sb_Thoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.Sb_Thoat.Appearance.Options.UseBackColor = true;
            this.Sb_Thoat.Appearance.Options.UseFont = true;
            this.Sb_Thoat.Location = new System.Drawing.Point(373, 290);
            this.Sb_Thoat.Name = "Sb_Thoat";
            this.Sb_Thoat.Size = new System.Drawing.Size(177, 22);
            this.Sb_Thoat.StyleController = this.layoutControl1;
            this.Sb_Thoat.TabIndex = 5;
            this.Sb_Thoat.Text = "Thoát";
            this.Sb_Thoat.Click += new System.EventHandler(this.Sb_Thoat_Click);
            // 
            // sb_OK
            // 
            this.sb_OK.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sb_OK.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_OK.Appearance.Options.UseBackColor = true;
            this.sb_OK.Appearance.Options.UseFont = true;
            this.sb_OK.Location = new System.Drawing.Point(12, 290);
            this.sb_OK.Name = "sb_OK";
            this.sb_OK.Size = new System.Drawing.Size(177, 22);
            this.sb_OK.StyleController = this.layoutControl1;
            this.sb_OK.TabIndex = 4;
            this.sb_OK.Text = "Ok";
            this.sb_OK.Click += new System.EventHandler(this.sb_OK_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(562, 324);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_OK;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 278);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(181, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.Sb_Thoat;
            this.layoutControlItem3.Location = new System.Drawing.Point(361, 278);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(181, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tl_MTC;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(542, 278);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // sb_BoChon
            // 
            this.sb_BoChon.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.sb_BoChon.Appearance.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.sb_BoChon.Appearance.Options.UseBackColor = true;
            this.sb_BoChon.Appearance.Options.UseFont = true;
            this.sb_BoChon.Location = new System.Drawing.Point(193, 290);
            this.sb_BoChon.Name = "sb_BoChon";
            this.sb_BoChon.Size = new System.Drawing.Size(176, 22);
            this.sb_BoChon.StyleController = this.layoutControl1;
            this.sb_BoChon.TabIndex = 8;
            this.sb_BoChon.Text = "Bỏ chọn mũi";
            this.sb_BoChon.Click += new System.EventHandler(this.sb_BoChon_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sb_BoChon;
            this.layoutControlItem4.Location = new System.Drawing.Point(181, 278);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(180, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // Uc_ChonMuiThiCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "Uc_ChonMuiThiCong";
            this.Size = new System.Drawing.Size(562, 324);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_MTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton Sb_Thoat;
        private DevExpress.XtraEditors.SimpleButton sb_OK;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraTreeList.TreeList tl_MTC;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sb_BoChon;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}
