namespace PhanMemQuanLyThiCong.Controls.DrainageControls.CumDuAn
{
    partial class ucEdit_CumDuAn
    {
        /// <summary>
        /// designer variable.
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
        /// method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.lb_type = new DevExpress.XtraEditors.LabelControl();
            this.bt_update = new DevExpress.XtraEditors.SimpleButton();
            this.bt_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.lb_info = new DevExpress.XtraEditors.LabelControl();
            this.rtb_Note = new System.Windows.Forms.RichTextBox();
            this.CumDuAnBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ViTriTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForViTri = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CumDuAnBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViTriTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForViTri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.lb_type);
            this.dataLayoutControl1.Controls.Add(this.bt_update);
            this.dataLayoutControl1.Controls.Add(this.bt_Cancel);
            this.dataLayoutControl1.Controls.Add(this.lb_info);
            this.dataLayoutControl1.Controls.Add(this.rtb_Note);
            this.dataLayoutControl1.Controls.Add(this.ViTriTextEdit);
            this.dataLayoutControl1.DataSource = this.CumDuAnBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(639, 211);
            this.dataLayoutControl1.TabIndex = 1;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // lb_type
            // 
            this.lb_type.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lb_type.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lb_type.Appearance.Options.UseFont = true;
            this.lb_type.Appearance.Options.UseForeColor = true;
            this.lb_type.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lb_type.Location = new System.Drawing.Point(12, 12);
            this.lb_type.Name = "lb_type";
            this.lb_type.Size = new System.Drawing.Size(615, 16);
            this.lb_type.StyleController = this.dataLayoutControl1;
            this.lb_type.TabIndex = 10;
            this.lb_type.Text = "labelControl1";
            // 
            // bt_update
            // 
            this.bt_update.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_update.Appearance.Options.UseFont = true;
            this.bt_update.ImageOptions.SvgImage = global::PhanMemQuanLyThiCong.Properties.Resources.updatedataextract;
            this.bt_update.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.bt_update.Location = new System.Drawing.Point(407, 177);
            this.bt_update.Name = "bt_update";
            this.bt_update.Size = new System.Drawing.Size(116, 22);
            this.bt_update.StyleController = this.dataLayoutControl1;
            this.bt_update.TabIndex = 9;
            this.bt_update.Text = "Cập nhật";
            this.bt_update.Click += new System.EventHandler(this.bt_update_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bt_Cancel.Appearance.Options.UseFont = true;
            this.bt_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Cancel.ImageOptions.SvgImage = global::PhanMemQuanLyThiCong.Properties.Resources.actions_deletecircled;
            this.bt_Cancel.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.bt_Cancel.Location = new System.Drawing.Point(527, 177);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(100, 22);
            this.bt_Cancel.StyleController = this.dataLayoutControl1;
            this.bt_Cancel.TabIndex = 8;
            this.bt_Cancel.Text = "Hủy bỏ";
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // lb_info
            // 
            this.lb_info.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lb_info.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lb_info.Appearance.Options.UseFont = true;
            this.lb_info.Appearance.Options.UseForeColor = true;
            this.lb_info.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lb_info.Location = new System.Drawing.Point(12, 32);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(615, 16);
            this.lb_info.StyleController = this.dataLayoutControl1;
            this.lb_info.TabIndex = 7;
            this.lb_info.Text = "labelControl1";
            // 
            // rtb_Note
            // 
            this.rtb_Note.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.CumDuAnBindingSource, "Description", true));
            this.rtb_Note.Location = new System.Drawing.Point(59, 76);
            this.rtb_Note.Name = "rtb_Note";
            this.rtb_Note.Size = new System.Drawing.Size(568, 97);
            this.rtb_Note.TabIndex = 6;
            this.rtb_Note.Text = "";
            // 
            // CumDuAnBindingSource
            // 
            this.CumDuAnBindingSource.DataSource = typeof(PhanMemQuanLyThiCong.Model.CumDuAnDto);
            // 
            // ViTriTextEdit
            // 
            this.ViTriTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CumDuAnBindingSource, "Name", true));
            this.ViTriTextEdit.Location = new System.Drawing.Point(59, 52);
            this.ViTriTextEdit.Name = "ViTriTextEdit";
            this.ViTriTextEdit.Size = new System.Drawing.Size(568, 20);
            this.ViTriTextEdit.StyleController = this.dataLayoutControl1;
            this.ViTriTextEdit.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(639, 211);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForViTri,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(619, 191);
            // 
            // ItemForViTri
            // 
            this.ItemForViTri.Control = this.ViTriTextEdit;
            this.ItemForViTri.Location = new System.Drawing.Point(0, 40);
            this.ItemForViTri.Name = "ItemForViTri";
            this.ItemForViTri.Size = new System.Drawing.Size(619, 24);
            this.ItemForViTri.Text = "Tên";
            this.ItemForViTri.TextSize = new System.Drawing.Size(35, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.rtb_Note;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(619, 101);
            this.layoutControlItem1.Text = "Ghi chú";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(35, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lb_info;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(619, 20);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bt_Cancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(515, 165);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(104, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.bt_update;
            this.layoutControlItem4.Location = new System.Drawing.Point(395, 165);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(120, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 165);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(395, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lb_type;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(619, 20);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // ucEdit_CumDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "ucEdit_CumDuAn";
            this.Size = new System.Drawing.Size(639, 211);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CumDuAnBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViTriTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForViTri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.SimpleButton bt_update;
        private DevExpress.XtraEditors.SimpleButton bt_Cancel;
        private DevExpress.XtraEditors.LabelControl lb_info;
        private System.Windows.Forms.RichTextBox rtb_Note;
        private DevExpress.XtraEditors.TextEdit ViTriTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForViTri;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.LabelControl lb_type;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.BindingSource CumDuAnBindingSource;
    }
}