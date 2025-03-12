namespace PhanMemQuanLyThiCong
{
    partial class XtraForm_CaiDatNguoiThucHien
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
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.lbc_Approve = new DevExpress.XtraEditors.ListBoxControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.lbc_Edit = new DevExpress.XtraEditors.ListBoxControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lbc_view = new DevExpress.XtraEditors.ListBoxControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lbc_Admin = new DevExpress.XtraEditors.ListBoxControl();
            this.bt_Huy = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_Approve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_Edit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbc_Admin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tablePanel1);
            this.layoutControl1.Controls.Add(this.bt_Huy);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(878, 179, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(871, 444);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F)});
            this.tablePanel1.Controls.Add(this.groupControl4);
            this.tablePanel1.Controls.Add(this.groupControl3);
            this.tablePanel1.Controls.Add(this.groupControl2);
            this.tablePanel1.Controls.Add(this.groupControl1);
            this.tablePanel1.Location = new System.Drawing.Point(12, 12);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(847, 394);
            this.tablePanel1.TabIndex = 20;
            // 
            // groupControl4
            // 
            this.tablePanel1.SetColumn(this.groupControl4, 1);
            this.groupControl4.Controls.Add(this.lbc_Approve);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl4.Location = new System.Drawing.Point(427, 200);
            this.groupControl4.Name = "groupControl4";
            this.tablePanel1.SetRow(this.groupControl4, 1);
            this.groupControl4.Size = new System.Drawing.Size(418, 191);
            this.groupControl4.TabIndex = 21;
            this.groupControl4.Text = "Quyền";
            // 
            // lbc_Approve
            // 
            this.lbc_Approve.DisplayMember = "Email";
            this.lbc_Approve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbc_Approve.Location = new System.Drawing.Point(2, 23);
            this.lbc_Approve.Name = "lbc_Approve";
            this.lbc_Approve.Size = new System.Drawing.Size(414, 166);
            this.lbc_Approve.TabIndex = 18;
            // 
            // groupControl3
            // 
            this.tablePanel1.SetColumn(this.groupControl3, 0);
            this.groupControl3.Controls.Add(this.lbc_Edit);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(3, 200);
            this.groupControl3.Name = "groupControl3";
            this.tablePanel1.SetRow(this.groupControl3, 1);
            this.groupControl3.Size = new System.Drawing.Size(418, 191);
            this.groupControl3.TabIndex = 20;
            this.groupControl3.Text = "Quyền chỉnh sửa";
            // 
            // lbc_Edit
            // 
            this.lbc_Edit.DisplayMember = "Email";
            this.lbc_Edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbc_Edit.Location = new System.Drawing.Point(2, 23);
            this.lbc_Edit.Name = "lbc_Edit";
            this.lbc_Edit.Size = new System.Drawing.Size(414, 166);
            this.lbc_Edit.TabIndex = 15;
            // 
            // groupControl2
            // 
            this.tablePanel1.SetColumn(this.groupControl2, 1);
            this.groupControl2.Controls.Add(this.lbc_view);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(427, 3);
            this.groupControl2.Name = "groupControl2";
            this.tablePanel1.SetRow(this.groupControl2, 0);
            this.groupControl2.Size = new System.Drawing.Size(418, 191);
            this.groupControl2.TabIndex = 19;
            this.groupControl2.Text = "Quyền Xem/Theo dõi";
            // 
            // lbc_view
            // 
            this.lbc_view.DisplayMember = "Email";
            this.lbc_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbc_view.Location = new System.Drawing.Point(2, 23);
            this.lbc_view.Name = "lbc_view";
            this.lbc_view.Size = new System.Drawing.Size(414, 166);
            this.lbc_view.TabIndex = 16;
            // 
            // groupControl1
            // 
            this.tablePanel1.SetColumn(this.groupControl1, 0);
            this.groupControl1.Controls.Add(this.lbc_Admin);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.tablePanel1.SetRow(this.groupControl1, 0);
            this.groupControl1.Size = new System.Drawing.Size(418, 191);
            this.groupControl1.TabIndex = 18;
            this.groupControl1.Text = "Admin";
            // 
            // lbc_Admin
            // 
            this.lbc_Admin.DisplayMember = "Email";
            this.lbc_Admin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbc_Admin.Location = new System.Drawing.Point(2, 23);
            this.lbc_Admin.Name = "lbc_Admin";
            this.lbc_Admin.Size = new System.Drawing.Size(414, 166);
            this.lbc_Admin.TabIndex = 17;
            // 
            // bt_Huy
            // 
            this.bt_Huy.Appearance.BackColor = System.Drawing.Color.Red;
            this.bt_Huy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_Huy.Appearance.Options.UseBackColor = true;
            this.bt_Huy.Appearance.Options.UseFont = true;
            this.bt_Huy.Location = new System.Drawing.Point(12, 410);
            this.bt_Huy.Name = "bt_Huy";
            this.bt_Huy.Size = new System.Drawing.Size(847, 22);
            this.bt_Huy.StyleController = this.layoutControl1;
            this.bt_Huy.TabIndex = 9;
            this.bt_Huy.Text = "Đóng";
            this.bt_Huy.Click += new System.EventHandler(this.bt_Huy_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(871, 444);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.bt_Huy;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 398);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(851, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tablePanel1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(851, 398);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // XtraForm_CaiDatNguoiThucHien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 444);
            this.Controls.Add(this.layoutControl1);
            this.Name = "XtraForm_CaiDatNguoiThucHien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Người thực hiện";
            this.Load += new System.EventHandler(this.XtraForm_CaiDatNguoiThucHien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbc_Approve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbc_Edit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbc_view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbc_Admin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton bt_Huy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.ListBoxControl lbc_view;
        private DevExpress.XtraEditors.ListBoxControl lbc_Edit;
        private DevExpress.XtraEditors.ListBoxControl lbc_Admin;
        private DevExpress.XtraEditors.ListBoxControl lbc_Approve;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}