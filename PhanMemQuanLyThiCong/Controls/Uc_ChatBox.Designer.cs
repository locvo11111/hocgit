using System.Reflection;

namespace PhanMemQuanLyThiCong.Urcs
{
    partial class Uc_ChatBox
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
            this.components = new System.ComponentModel.Container();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.messagesView1 = new PhanMemQuanLyThiCong.Views.MessagesView();
            this.contactsView1 = new DevExpress.ChatClient.Views.ContactsView();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = null;
            // 
            // messagesView1
            // 
            this.messagesView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesView1.Location = new System.Drawing.Point(0, 0);
            this.messagesView1.Margin = new System.Windows.Forms.Padding(0);
            this.messagesView1.Name = "messagesView1";
            this.messagesView1.Size = new System.Drawing.Size(975, 636);
            this.messagesView1.TabIndex = 2;
            // 
            // contactsView1
            // 
            this.contactsView1.Location = new System.Drawing.Point(239, 51);
            this.contactsView1.Margin = new System.Windows.Forms.Padding(0);
            this.contactsView1.Name = "contactsView1";
            this.contactsView1.Size = new System.Drawing.Size(270, 600);
            this.contactsView1.TabIndex = 3;
            // 
            // Uc_ChatBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.messagesView1);
            this.Controls.Add(this.contactsView1);
            this.Name = "Uc_ChatBox";
            this.Size = new System.Drawing.Size(975, 636);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        internal PhanMemQuanLyThiCong.Views.MessagesView messagesView1;
        internal DevExpress.ChatClient.Views.ContactsView contactsView1;
    }
}
