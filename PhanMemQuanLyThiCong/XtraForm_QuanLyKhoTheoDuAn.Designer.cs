
namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_QuanLyKhoTheoDuAn
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
            this.sb_Close = new DevExpress.XtraEditors.SimpleButton();
            this.sb_Save = new DevExpress.XtraEditors.SimpleButton();
            this.tl_TenKho = new DevExpress.XtraTreeList.TreeList();
            this.col_TenKho = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tl_TenKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sb_Close);
            this.layoutControl1.Controls.Add(this.sb_Save);
            this.layoutControl1.Controls.Add(this.tl_TenKho);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(901, 543);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sb_Close
            // 
            this.sb_Close.Appearance.BackColor = System.Drawing.Color.Red;
            this.sb_Close.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_Close.Appearance.Options.UseBackColor = true;
            this.sb_Close.Appearance.Options.UseFont = true;
            this.sb_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sb_Close.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_macos_close_24;
            this.sb_Close.Location = new System.Drawing.Point(452, 503);
            this.sb_Close.Name = "sb_Close";
            this.sb_Close.Size = new System.Drawing.Size(437, 28);
            this.sb_Close.StyleController = this.layoutControl1;
            this.sb_Close.TabIndex = 6;
            this.sb_Close.Text = "Hủy";
            // 
            // sb_Save
            // 
            this.sb_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sb_Save.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_Save.Appearance.Options.UseBackColor = true;
            this.sb_Save.Appearance.Options.UseFont = true;
            this.sb_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sb_Save.ImageOptions.Image = global::PhanMemQuanLyThiCong.Properties.Resources.icons8_database_export_24;
            this.sb_Save.Location = new System.Drawing.Point(12, 503);
            this.sb_Save.Name = "sb_Save";
            this.sb_Save.Size = new System.Drawing.Size(436, 28);
            this.sb_Save.StyleController = this.layoutControl1;
            this.sb_Save.TabIndex = 5;
            this.sb_Save.Text = "Lưu thay đổi và đóng";
            this.sb_Save.Click += new System.EventHandler(this.sb_Save_Click);
            // 
            // tl_TenKho
            // 
            this.tl_TenKho.CheckBoxFieldName = "Chon";
            this.tl_TenKho.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.col_TenKho});
            this.tl_TenKho.Location = new System.Drawing.Point(12, 12);
            this.tl_TenKho.Name = "tl_TenKho";
            this.tl_TenKho.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.tl_TenKho.Size = new System.Drawing.Size(877, 487);
            this.tl_TenKho.TabIndex = 4;
            this.tl_TenKho.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tl_TenKho_NodeCellStyle);
            this.tl_TenKho.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.tl_TenKho_BeforeCheckNode);
            this.tl_TenKho.CustomDrawNodeCheckBox += new DevExpress.XtraTreeList.CustomDrawNodeCheckBoxEventHandler(this.tl_TenKho_CustomDrawNodeCheckBox);
            // 
            // col_TenKho
            // 
            this.col_TenKho.Caption = "Tên Kho";
            this.col_TenKho.FieldName = "TenKho";
            this.col_TenKho.Name = "col_TenKho";
            this.col_TenKho.Visible = true;
            this.col_TenKho.VisibleIndex = 0;
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
            this.Root.Size = new System.Drawing.Size(901, 543);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tl_TenKho;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(881, 491);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sb_Save;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 491);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(440, 32);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sb_Close;
            this.layoutControlItem3.Location = new System.Drawing.Point(440, 491);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(441, 32);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // XtraForm_QuanLyKhoTheoDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 543);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_QuanLyKhoTheoDuAn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách kho";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tl_TenKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTreeList.TreeList tl_TenKho;
        private DevExpress.XtraTreeList.Columns.TreeListColumn col_TenKho;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sb_Save;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sb_Close;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}