
namespace PhanMemQuanLyThiCong
{
    partial class Form_Luachonhopdong
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
            this.btn_huy = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.dtgv_danhsachhopdong = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_danhsachhopdong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_huy);
            this.layoutControl1.Controls.Add(this.btn_ok);
            this.layoutControl1.Controls.Add(this.dtgv_danhsachhopdong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(607, 0, 812, 500);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(564, 342);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_huy
            // 
            this.btn_huy.BackColor = System.Drawing.Color.Aqua;
            this.btn_huy.Location = new System.Drawing.Point(8, 301);
            this.btn_huy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_huy.Name = "btn_huy";
            this.btn_huy.Size = new System.Drawing.Size(273, 31);
            this.btn_huy.TabIndex = 6;
            this.btn_huy.Text = "Hủy";
            this.btn_huy.UseVisualStyleBackColor = false;
            this.btn_huy.Click += new System.EventHandler(this.btn_huy_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_ok.Location = new System.Drawing.Point(283, 301);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(273, 31);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "Ok";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // dtgv_danhsachhopdong
            // 
            this.dtgv_danhsachhopdong.AllowUserToAddRows = false;
            this.dtgv_danhsachhopdong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_danhsachhopdong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            this.dtgv_danhsachhopdong.Location = new System.Drawing.Point(8, 10);
            this.dtgv_danhsachhopdong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtgv_danhsachhopdong.Name = "dtgv_danhsachhopdong";
            this.dtgv_danhsachhopdong.RowHeadersVisible = false;
            this.dtgv_danhsachhopdong.RowHeadersWidth = 51;
            this.dtgv_danhsachhopdong.RowTemplate.Height = 24;
            this.dtgv_danhsachhopdong.Size = new System.Drawing.Size(548, 287);
            this.dtgv_danhsachhopdong.TabIndex = 4;
            this.dtgv_danhsachhopdong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgv_danhsachhopdong_CellContentClick);
            // 
            // check
            // 
            this.check.HeaderText = "Chọn";
            this.check.MinimumWidth = 6;
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.check.Width = 125;
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
            this.Root.Size = new System.Drawing.Size(564, 342);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.layoutControlItem1.Control = this.dtgv_danhsachhopdong;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(550, 291);
            this.layoutControlItem1.Text = "Danh sách hợp đồng";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btn_ok;
            this.layoutControlItem2.Location = new System.Drawing.Point(275, 291);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(275, 35);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btn_huy;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 291);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(275, 35);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Form_Luachonhopdong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 342);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form_Luachonhopdong";
            this.Text = "Form_Luachonhopdong";
            this.Load += new System.EventHandler(this.Form_Luachonhopdong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_danhsachhopdong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.Button btn_huy;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.DataGridView dtgv_danhsachhopdong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
    }
}