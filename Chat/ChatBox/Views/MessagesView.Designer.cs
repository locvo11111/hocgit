
using PhanMemQuanLyThiCong;
using PhanMemQuanLyThiCong.ViewModels;

namespace PhanMemQuanLyThiCong.Views {
    partial class MessagesView {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagesView));
            this.tablePanel = new DevExpress.Utils.Layout.TablePanel();
            this.lbthongbao = new DevExpress.XtraEditors.LabelControl();
            this.typingBox = new DevExpress.XtraEditors.HtmlContentControl();
            this.messageEdit = new DevExpress.XtraEditors.MemoEdit();
            this.messageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolbarPanel = new DevExpress.XtraEditors.HtmlContentControl();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.messagesItemsView = new DevExpress.XtraGrid.Views.Items.ItemsView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvatar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImgText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImgImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUploadFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.messageMenuPopup = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel)).BeginInit();
            this.tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typingBox)).BeginInit();
            this.typingBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarPanel)).BeginInit();
            this.toolbarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesItemsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageMenuPopup)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
            this.tablePanel.Controls.Add(this.lbthongbao);
            this.tablePanel.Controls.Add(this.typingBox);
            this.tablePanel.Controls.Add(this.toolbarPanel);
            this.tablePanel.Controls.Add(this.gridControl);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 49F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 1F)});
            this.tablePanel.Size = new System.Drawing.Size(430, 600);
            this.tablePanel.TabIndex = 1;
            // 
            // lbthongbao
            // 
            this.lbthongbao.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbthongbao.Appearance.Options.UseFont = true;
            this.lbthongbao.Location = new System.Drawing.Point(360, 475);
            this.lbthongbao.Name = "lbthongbao";
            this.lbthongbao.Size = new System.Drawing.Size(150, 14);
            this.lbthongbao.TabIndex = 5;
            this.lbthongbao.Text = "Admin đang nhập.....";
            this.lbthongbao.Visible = false;
            // 
            // typingBox
            // 
            this.typingBox.AutoScroll = false;
            this.tablePanel.SetColumn(this.typingBox, 0);
            this.typingBox.Controls.Add(this.messageEdit);
            this.typingBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.typingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.typingBox.HtmlTemplate.Styles = resources.GetString("typingBox.HtmlTemplate.Styles");
            this.typingBox.HtmlTemplate.Template = resources.GetString("typingBox.HtmlTemplate.Template");
            this.typingBox.Location = new System.Drawing.Point(0, 540);
            this.typingBox.Margin = new System.Windows.Forms.Padding(0);
            this.typingBox.Name = "typingBox";
            this.tablePanel.SetRow(this.typingBox, 2);
            this.typingBox.Size = new System.Drawing.Size(430, 60);
            this.typingBox.TabIndex = 4;
            // 
            // messageEdit
            // 
            this.messageEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.messageBindingSource, "MessageText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.messageEdit.Location = new System.Drawing.Point(30, 23);
            this.messageEdit.Name = "messageEdit";
            this.messageEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.messageEdit.Properties.Appearance.Options.UseFont = true;
            this.messageEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.messageEdit.Properties.NullValuePrompt = "Nhập tin nhắn tại đây...";
            this.messageEdit.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.messageEdit.Properties.ValidateOnEnterKey = true;
            this.messageEdit.Size = new System.Drawing.Size(294, 18);
            this.messageEdit.TabIndex = 0;
            this.messageEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageEdit_KeyDown);
            // 
            // messageBindingSource
            // 
            this.messageBindingSource.DataSource = typeof(PhanMemQuanLyThiCong.ViewModels.MessagesViewModel);
            // 
            // toolbarPanel
            // 
            this.tablePanel.SetColumn(this.toolbarPanel, 0);
            this.toolbarPanel.Controls.Add(this.btn_Reset);
            this.toolbarPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.toolbarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.toolbarPanel.HtmlTemplate.Styles = resources.GetString("toolbarPanel.HtmlTemplate.Styles");
            this.toolbarPanel.HtmlTemplate.Template = resources.GetString("toolbarPanel.HtmlTemplate.Template");
            this.toolbarPanel.Location = new System.Drawing.Point(0, 0);
            this.toolbarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.toolbarPanel.Name = "toolbarPanel";
            this.tablePanel.SetRow(this.toolbarPanel, 0);
            this.toolbarPanel.Size = new System.Drawing.Size(430, 49);
            this.toolbarPanel.TabIndex = 3;
            // 
            // btn_Reset
            // 
            this.btn_Reset.BackColor = System.Drawing.Color.Transparent;
            this.btn_Reset.Location = new System.Drawing.Point(0, 36);
            this.btn_Reset.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(10, 10);
            this.btn_Reset.TabIndex = 2;
            this.btn_Reset.UseVisualStyleBackColor = false;
            this.btn_Reset.VisibleChanged += new System.EventHandler(this.btn_Reset_VisibleChanged);
            // 
            // gridControl
            // 
            this.tablePanel.SetColumn(this.gridControl, 0);
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 49);
            this.gridControl.MainView = this.messagesItemsView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl.Name = "gridControl";
            this.tablePanel.SetRow(this.gridControl, 1);
            this.gridControl.Size = new System.Drawing.Size(430, 491);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.messagesItemsView});
            // 
            // messagesItemsView
            // 
            this.messagesItemsView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.messagesItemsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.colAvatar,
            this.colText,
            this.colImgText,
            this.colTime,
            this.colImgImage,
            this.colUploadFile});
            this.messagesItemsView.GridControl = this.gridControl;
            this.messagesItemsView.Name = "messagesItemsView";
            this.messagesItemsView.QueryItemTemplate += new DevExpress.XtraGrid.Views.Items.QueryItemTemplateEventHandler(this.OnQueryItemTemplate);
            this.messagesItemsView.CustomizeItem += new DevExpress.XtraGrid.Views.Items.CustomizeItemEventHandler(this.OnCustomizeItem);
            // 
            // colUserName
            // 
            this.colUserName.FieldName = "MemberName";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 0;
            // 
            // colAvatar
            // 
            this.colAvatar.FieldName = "Logo";
            this.colAvatar.Name = "colAvatar";
            this.colAvatar.Visible = true;
            this.colAvatar.VisibleIndex = 1;
            // 
            // colText
            // 
            this.colText.FieldName = "Content";
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 2;
            // 
            // colImgText
            // 
            this.colImgText.FieldName = "ImgText";
            this.colImgText.Name = "colImgText";
            this.colImgText.Visible = true;
            this.colImgText.VisibleIndex = 3;
            // 
            // colTime
            // 
            this.colTime.Caption = "Time";
            this.colTime.FieldName = "Time";
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 4;
            // 
            // colImgImage
            // 
            this.colImgImage.FieldName = "ImgImage";
            this.colImgImage.Name = "colImgImage";
            this.colImgImage.Visible = true;
            this.colImgImage.VisibleIndex = 5;
            // 
            // colUploadFile
            // 
            this.colUploadFile.FieldName = "ImgFile";
            this.colUploadFile.Name = "colUploadFile";
            this.colUploadFile.Visible = true;
            this.colUploadFile.VisibleIndex = 6;
            // 
            // mvvmContext
            // 
            this.mvvmContext.ContainerControl = this;
            this.mvvmContext.ViewModelType = typeof(PhanMemQuanLyThiCong.ViewModels.MessagesViewModel);
            // 
            // messageMenuPopup
            // 
            this.messageMenuPopup.ContainerControl = this;
            this.messageMenuPopup.HideOnElementClick = DevExpress.Utils.DefaultBoolean.True;
            this.messageMenuPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            // 
            // MessagesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tablePanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MessagesView";
            this.Size = new System.Drawing.Size(430, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel)).EndInit();
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typingBox)).EndInit();
            this.typingBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarPanel)).EndInit();
            this.toolbarPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesItemsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageMenuPopup)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel;
        private DevExpress.XtraEditors.HtmlContentControl toolbarPanel;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Items.ItemsView messagesItemsView;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colAvatar;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraEditors.HtmlContentControl typingBox;
        private System.Windows.Forms.BindingSource messageBindingSource;
        private DevExpress.XtraEditors.MemoEdit messageEdit;
        private DevExpress.XtraEditors.HtmlContentPopup messageMenuPopup;
        private DevExpress.XtraEditors.LabelControl lbthongbao;
        private DevExpress.XtraGrid.Columns.GridColumn colImgText;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colImgImage;
        private DevExpress.XtraGrid.Columns.GridColumn colUploadFile;
        internal System.Windows.Forms.Button btn_Reset;
    }
}
