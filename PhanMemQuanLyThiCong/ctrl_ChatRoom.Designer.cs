
namespace PhanMemQuanLyThiCong
{
    partial class ctrl_ChatRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrl_ChatRoom));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.itemsView1 = new DevExpress.XtraGrid.Views.Items.ItemsView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(3, 0);
            this.gridControl1.MainView = this.itemsView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(287, 157);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.itemsView1});
            // 
            // itemsView1
            // 
            this.itemsView1.GridControl = this.gridControl1;
            // 
            // 
            // 
            this.itemsView1.HtmlTemplate.Styles = resources.GetString("itemsView1.HtmlTemplate.Styles");
            this.itemsView1.HtmlTemplate.Template = resources.GetString("itemsView1.HtmlTemplate.Template");
            this.itemsView1.Name = "itemsView1";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(579, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrl_ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.button1);
            this.Name = "ctrl_ChatRoom";
            this.Size = new System.Drawing.Size(579, 405);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Items.ItemsView itemsView1;
        private System.Windows.Forms.Button button1;
    }
}
