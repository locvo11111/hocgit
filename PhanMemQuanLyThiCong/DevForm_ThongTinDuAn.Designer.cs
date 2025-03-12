namespace PhanMemQuanLyThiCong
{
    partial class DevForm_ThongTinDuAn
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
            this.fluentDesignFormContainer1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.ace_Permission = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_DanhSachDA = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            this.accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_PhanCapQuanLy = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_PhanQuyenChiTiet = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // fluentDesignFormContainer1
            // 
            this.fluentDesignFormContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fluentDesignFormContainer1.Location = new System.Drawing.Point(260, 31);
            this.fluentDesignFormContainer1.Name = "fluentDesignFormContainer1";
            this.fluentDesignFormContainer1.Size = new System.Drawing.Size(431, 442);
            this.fluentDesignFormContainer1.TabIndex = 0;
            // 
            // accordionControl1
            // 
            this.accordionControl1.AllowItemSelection = true;
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_Permission,
            this.ace_DanhSachDA});
            this.accordionControl1.Location = new System.Drawing.Point(0, 31);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(260, 442);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            this.accordionControl1.ElementClick += new DevExpress.XtraBars.Navigation.ElementClickEventHandler(this.accordionControl1_ElementClick);
            // 
            // ace_Permission
            // 
            this.ace_Permission.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ace_Permission.Appearance.Default.Options.UseFont = true;
            this.ace_Permission.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_PhanCapQuanLy,
            this.ace_PhanQuyenChiTiet});
            this.ace_Permission.Expanded = true;
            this.ace_Permission.Name = "ace_Permission";
            this.ace_Permission.Text = "Quản lý Phòng/Ban";
            // 
            // ace_DanhSachDA
            // 
            this.ace_DanhSachDA.Name = "ace_DanhSachDA";
            this.ace_DanhSachDA.Text = "Danh sách Dự Án";
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(691, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.Form = this;
            // 
            // accordionControlElement3
            // 
            this.accordionControlElement3.Name = "accordionControlElement3";
            this.accordionControlElement3.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement3.Text = "Dự án 1";
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement4.Text = "Dự án 2";
            // 
            // ace_PhanCapQuanLy
            // 
            this.ace_PhanCapQuanLy.Name = "ace_PhanCapQuanLy";
            this.ace_PhanCapQuanLy.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_PhanCapQuanLy.Text = "Phân cấp quản lý";
            // 
            // ace_PhanQuyenChiTiet
            // 
            this.ace_PhanQuyenChiTiet.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.ace_PhanQuyenChiTiet.Name = "ace_PhanQuyenChiTiet";
            this.ace_PhanQuyenChiTiet.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_PhanQuyenChiTiet.Text = "Phân quyền chi tiết";
            // 
            // DevForm_ThongTinDuAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 473);
            this.ControlContainer = this.fluentDesignFormContainer1;
            this.Controls.Add(this.fluentDesignFormContainer1);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Name = "DevForm_ThongTinDuAn";
            this.NavigationControl = this.accordionControl1;
            this.Text = "Form_ThongTinTongHopDuAn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DevForm_ThongTinTongHopDuAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer fluentDesignFormContainer1;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_DanhSachDA;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_Permission;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_PhanCapQuanLy;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_PhanQuyenChiTiet;
    }
}