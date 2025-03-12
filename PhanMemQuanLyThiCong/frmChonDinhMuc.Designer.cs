namespace PhanMemQuanLyThiCong
{
    partial class frmChonDinhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonDinhMuc));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Chon = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.categoryDinhMucBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChecked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDinhMucBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 50);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(142, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "CÁC ĐỊNH MỨC THỰC HIỆN CHO DỰ ÁN";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Cancel);
            this.panel2.Controls.Add(this.btn_Chon);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 399);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(662, 50);
            this.panel2.TabIndex = 1;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Cancel.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_Cancel.Appearance.Options.UseBackColor = true;
            this.btn_Cancel.Appearance.Options.UseBorderColor = true;
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseForeColor = true;
            this.btn_Cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.ImageOptions.Image")));
            this.btn_Cancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel.Location = new System.Drawing.Point(132, 7);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(118, 34);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Hủy";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Chon
            // 
            this.btn_Chon.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Chon.Appearance.BorderColor = System.Drawing.Color.Green;
            this.btn_Chon.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Chon.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btn_Chon.Appearance.Options.UseBackColor = true;
            this.btn_Chon.Appearance.Options.UseBorderColor = true;
            this.btn_Chon.Appearance.Options.UseFont = true;
            this.btn_Chon.Appearance.Options.UseForeColor = true;
            this.btn_Chon.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btn_Chon.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Chon.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Chon.ImageOptions.Image")));
            this.btn_Chon.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Chon.Location = new System.Drawing.Point(9, 7);
            this.btn_Chon.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Chon.Name = "btn_Chon";
            this.btn_Chon.Size = new System.Drawing.Size(118, 34);
            this.btn_Chon.TabIndex = 2;
            this.btn_Chon.Text = "Đồng ý";
            this.btn_Chon.Click += new System.EventHandler(this.btn_Chon_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gridControl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 50);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(662, 349);
            this.panel3.TabIndex = 2;
            // 
            // gridControl
            // 
            this.gridControl.DataSource = this.categoryDinhMucBindingSource;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(662, 349);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // categoryDinhMucBindingSource
            // 
            this.categoryDinhMucBindingSource.DataSource = typeof(PhanMemQuanLyThiCong.Common.ViewModel.CategoryDinhMuc);
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView.Appearance.Row.Options.UseFont = true;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChecked,
            this.colID,
            this.colName});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);
            // 
            // colChecked
            // 
            this.colChecked.Caption = "Chọn";
            this.colChecked.FieldName = "Checked";
            this.colChecked.Name = "colChecked";
            this.colChecked.Visible = true;
            this.colChecked.VisibleIndex = 0;
            this.colChecked.Width = 64;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Width = 198;
            // 
            // colName
            // 
            this.colName.Caption = "Tên định mức";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 488;
            // 
            // frmChonDinhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 449);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmChonDinhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn đầu mục định mức";
            this.Load += new System.EventHandler(this.FrmChonDinhMuc_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDinhMucBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraEditors.SimpleButton btn_Chon;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.BindingSource categoryDinhMucBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colChecked;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
    }
}