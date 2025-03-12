namespace PhanMemQuanLyThiCong.Controls
{
    partial class uc_Paging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_Paging));
            this.bt_first = new DevExpress.XtraEditors.SimpleButton();
            this.bt_prev = new DevExpress.XtraEditors.SimpleButton();
            this.pagenumber = new DevExpress.XtraEditors.TextEdit();
            this.bt_next = new DevExpress.XtraEditors.SimpleButton();
            this.bt_last = new DevExpress.XtraEditors.SimpleButton();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.pagenumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_first
            // 
            this.bt_first.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.bt_first.Location = new System.Drawing.Point(2, 2);
            this.bt_first.Name = "bt_first";
            this.bt_first.Size = new System.Drawing.Size(22, 22);
            this.bt_first.StyleController = this.dataLayoutControl1;
            this.bt_first.TabIndex = 0;
            // 
            // bt_prev
            // 
            this.bt_prev.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.bt_prev.Location = new System.Drawing.Point(28, 2);
            this.bt_prev.Name = "bt_prev";
            this.bt_prev.Size = new System.Drawing.Size(22, 22);
            this.bt_prev.StyleController = this.dataLayoutControl1;
            this.bt_prev.TabIndex = 1;
            // 
            // pagenumber
            // 
            this.pagenumber.EditValue = "123/123";
            this.pagenumber.Location = new System.Drawing.Point(54, 3);
            this.pagenumber.Name = "pagenumber";
            this.pagenumber.Size = new System.Drawing.Size(53, 20);
            this.pagenumber.StyleController = this.dataLayoutControl1;
            this.pagenumber.TabIndex = 3;
            // 
            // bt_next
            // 
            this.bt_next.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.bt_next.Location = new System.Drawing.Point(111, 2);
            this.bt_next.Name = "bt_next";
            this.bt_next.Size = new System.Drawing.Size(22, 22);
            this.bt_next.StyleController = this.dataLayoutControl1;
            this.bt_next.TabIndex = 4;
            // 
            // bt_last
            // 
            this.bt_last.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
            this.bt_last.Location = new System.Drawing.Point(137, 2);
            this.bt_last.Name = "bt_last";
            this.bt_last.Size = new System.Drawing.Size(22, 22);
            this.bt_last.StyleController = this.dataLayoutControl1;
            this.bt_last.TabIndex = 5;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.bt_last);
            this.dataLayoutControl1.Controls.Add(this.bt_next);
            this.dataLayoutControl1.Controls.Add(this.pagenumber);
            this.dataLayoutControl1.Controls.Add(this.bt_prev);
            this.dataLayoutControl1.Controls.Add(this.bt_first);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(161, 26);
            this.dataLayoutControl1.TabIndex = 7;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(161, 26);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem2.Control = this.bt_first;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(26, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem3.Control = this.bt_prev;
            this.layoutControlItem3.Location = new System.Drawing.Point(26, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(26, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem4.Control = this.pagenumber;
            this.layoutControlItem4.Location = new System.Drawing.Point(52, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(57, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem5.Control = this.bt_next;
            this.layoutControlItem5.Location = new System.Drawing.Point(109, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(26, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem6.Control = this.bt_last;
            this.layoutControlItem6.Location = new System.Drawing.Point(135, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(26, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(26, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // uc_Paging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.MaximumSize = new System.Drawing.Size(161, 26);
            this.MinimumSize = new System.Drawing.Size(161, 26);
            this.Name = "uc_Paging";
            this.Size = new System.Drawing.Size(161, 26);
            ((System.ComponentModel.ISupportInitialize)(this.pagenumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton bt_first;
        private DevExpress.XtraEditors.SimpleButton bt_prev;
        private DevExpress.XtraEditors.TextEdit pagenumber;
        private DevExpress.XtraEditors.SimpleButton bt_next;
        private DevExpress.XtraEditors.SimpleButton bt_last;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
