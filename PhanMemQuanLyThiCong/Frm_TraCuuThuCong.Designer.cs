namespace PhanMemQuanLyThiCong
{
    partial class Frm_TraCuuThuCong
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TraCuuThuCong));
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.bt_Setting = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colMaCongTac = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTenCongTac = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDonVi = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCTDinhMuc = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.congTacBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.congTacBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 12F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 60F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 40F)});
            this.tablePanel1.Controls.Add(this.bt_Setting);
            this.tablePanel1.Controls.Add(this.labelControl1);
            this.tablePanel1.Controls.Add(this.btn_OK);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tablePanel1.Location = new System.Drawing.Point(0, 631);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1198, 31);
            this.tablePanel1.TabIndex = 0;
            // 
            // bt_Setting
            // 
            this.bt_Setting.Appearance.BackColor = System.Drawing.Color.Blue;
            this.bt_Setting.Appearance.Options.UseBackColor = true;
            this.tablePanel1.SetColumn(this.bt_Setting, 2);
            this.bt_Setting.Location = new System.Drawing.Point(759, 4);
            this.bt_Setting.Name = "bt_Setting";
            this.tablePanel1.SetRow(this.bt_Setting, 0);
            this.bt_Setting.Size = new System.Drawing.Size(436, 23);
            this.bt_Setting.TabIndex = 40;
            this.bt_Setting.Text = "Cài đặt lại";
            this.bt_Setting.Click += new System.EventHandler(this.bt_Setting_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.tablePanel1.SetColumn(this.labelControl1, 1);
            this.labelControl1.Location = new System.Drawing.Point(97, 8);
            this.labelControl1.Name = "labelControl1";
            this.tablePanel1.SetRow(this.labelControl1, 0);
            this.labelControl1.Size = new System.Drawing.Size(487, 15);
            this.labelControl1.TabIndex = 39;
            this.labelControl1.Text = "Vui lòng chọn check 1 định mức tương ứng để áp dụng cho công tác thủ công của bạn" +
    "";
            // 
            // btn_OK
            // 
            this.btn_OK.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_OK.Appearance.BorderColor = System.Drawing.Color.Blue;
            this.btn_OK.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btn_OK.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btn_OK.Appearance.Options.UseBackColor = true;
            this.btn_OK.Appearance.Options.UseBorderColor = true;
            this.btn_OK.Appearance.Options.UseFont = true;
            this.btn_OK.Appearance.Options.UseForeColor = true;
            this.btn_OK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.tablePanel1.SetColumn(this.btn_OK, 0);
            this.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_OK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_OK.ImageOptions.Image")));
            this.btn_OK.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_OK.Location = new System.Drawing.Point(2, 2);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.MaximumSize = new System.Drawing.Size(100, 0);
            this.btn_OK.Name = "btn_OK";
            this.tablePanel1.SetRow(this.btn_OK, 0);
            this.btn_OK.Size = new System.Drawing.Size(90, 27);
            this.btn_OK.TabIndex = 38;
            this.btn_OK.Text = "ĐỒNG Ý";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // treeList
            // 
            this.treeList.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList.Appearance.HeaderPanel.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.treeList.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeList.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.treeList.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList.Appearance.Row.Options.UseFont = true;
            this.treeList.CheckBoxFieldName = "Chon";
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colMaCongTac,
            this.colTenCongTac,
            this.colDonVi,
            this.colCTDinhMuc,
            this.colID,
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3});
            this.treeList.CustomizationFormBounds = new System.Drawing.Rectangle(704, 415, 264, 272);
            this.treeList.DataSource = this.congTacBindingSource;
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.KeyFieldName = "Code";
            this.treeList.Location = new System.Drawing.Point(0, 0);
            this.treeList.Name = "treeList";
            this.treeList.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Radio;
            this.treeList.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
            this.treeList.ParentFieldName = "ParentId";
            this.treeList.Size = new System.Drawing.Size(1198, 631);
            this.treeList.TabIndex = 1;
            this.treeList.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList_NodeCellStyle);
            // 
            // colMaCongTac
            // 
            this.colMaCongTac.Caption = "Mã công tác";
            this.colMaCongTac.FieldName = "MaHieuCongTac";
            this.colMaCongTac.Name = "colMaCongTac";
            this.colMaCongTac.Visible = true;
            this.colMaCongTac.VisibleIndex = 0;
            this.colMaCongTac.Width = 150;
            // 
            // colTenCongTac
            // 
            this.colTenCongTac.Caption = "Tên công tác";
            this.colTenCongTac.FieldName = "TenCongTac";
            this.colTenCongTac.Name = "colTenCongTac";
            this.colTenCongTac.Visible = true;
            this.colTenCongTac.VisibleIndex = 1;
            this.colTenCongTac.Width = 350;
            // 
            // colDonVi
            // 
            this.colDonVi.Caption = "Đơn vị";
            this.colDonVi.FieldName = "DonVi";
            this.colDonVi.Name = "colDonVi";
            this.colDonVi.Visible = true;
            this.colDonVi.VisibleIndex = 2;
            this.colDonVi.Width = 100;
            // 
            // colCTDinhMuc
            // 
            this.colCTDinhMuc.Caption = "CT Định mức";
            this.colCTDinhMuc.FieldName = "CTDinhMuc";
            this.colCTDinhMuc.Name = "colCTDinhMuc";
            this.colCTDinhMuc.Visible = true;
            this.colCTDinhMuc.VisibleIndex = 3;
            this.colCTDinhMuc.Width = 245;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 4;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Từ khóa";
            this.treeListColumn1.FieldName = "keysMatch";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Số từ trùng";
            this.treeListColumn2.FieldName = "keyCount";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "ParentID";
            this.treeListColumn3.FieldName = "ParentId";
            this.treeListColumn3.Name = "treeListColumn3";
            // 
            // Frm_TraCuuThuCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 662);
            this.Controls.Add(this.treeList);
            this.Controls.Add(this.tablePanel1);
            this.Name = "Frm_TraCuuThuCong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tra cứu ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_TraCuuThuCong_FormClosed);
            this.Load += new System.EventHandler(this.Frm_TraCuuThuCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.congTacBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.SimpleButton btn_OK;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMaCongTac;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTenCongTac;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDonVi;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCTDinhMuc;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colID;
        private System.Windows.Forms.BindingSource congTacBindingSource;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraEditors.SimpleButton bt_Setting;
    }
}