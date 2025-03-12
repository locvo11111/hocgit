using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Items;
using PhanMemQuanLyThiCong.ChatBox;
using PhanMemQuanLyThiCong.ChatBox.Model;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.ViewModels;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Views
{
    //[Obfuscation(ApplyToMembers = false, Exclude = false, Feature = "-rename")]
    public partial class MessagesView : XtraUserControl
    {
        private Timer _timer;

        //private HubConnection connection;
        //private MessagesViewModel viewmodel;
        public MessagesView()
        {
            InitializeComponent();
            if (!mvvmContext.IsDesignMode)
            {
                InitializeStyles();
                InitializeBindings();
                InitializeMessageEdit();
            }
        }

        private void InitializeStyles()
        {
            Styles.Menu.Apply(messageMenuPopup);
            //Styles.Toolbar.Apply(toolbarPanel);
            Styles.TypingBox.Apply(typingBox);
            Styles.NoMessages.Apply(messagesItemsView.EmptyViewHtmlTemplate);
            //Styles.Message.Apply(messagesItemsView.EmptyViewHtmlTemplate);
        }

        private void InitializeBindings()
        {
            _timer = new Timer() { Interval = 1500 };
            var fluent = mvvmContext.OfType<MessagesViewModel>();
            fluent.SetBinding(gridControl, gc => gc.DataSource, x => x.Messages);
            fluent.SetBinding(messagesItemsView, mv => mv.FocusedRowObject, x => x.SelectedMessage);
            fluent.SetBinding(toolbarPanel, tp => tp.DataContext, x => x.Contact);

            //fluent.SetTrigger(x => x.Messages, contacts => { messagesItemsView.RefreshData(); messagesItemsView.MoveLast(); });
            fluent.SetTrigger(x => x.Messages, contacts => { messagesItemsView.RefreshData(); messagesItemsView.MoveLast(); });
            fluent.SetTrigger(x => x.UpdatedMessageIndices, indices => messagesItemsView.RefreshData(indices));
            fluent.SetTrigger(x => x.Contact, group => messagesItemsView.MoveLast());

            fluent.WithEvent(this, nameof(HandleCreated))
                .EventToCommand(x => x.OnCreate);
            fluent.WithEvent(this, nameof(HandleDestroyed))
                .EventToCommand(x => x.OnDestroy);

            fluent.BindCommandToElement(toolbarPanel, "btnPhoneCall", x => x.ImgCall);
            fluent.BindCommandToElement(toolbarPanel, "btnVideoCall", x => x.FileCall);
            //fluent.BindCommandToElement(toolbarPanel, "btnInfo", x => x.ShowContact);
            //fluent.BindCommandToElement(toolbarPanel, "btnUser", x => x.ShowUser);
            fluent.BindCommandToElement(typingBox, "btn_Send", x => x.SendMessage);
            fluent.WithKey(messageEdit, Keys.Enter)
                .KeyToCommand(x => x.SendMessage);

            fluent.SetObjectDataSourceBinding(messageBindingSource, x => x.Update);
            fluent.BindCommandToElement(messageMenuPopup, "savefile", x => x.SaveFile);
            //fluent.BindCommandToElement(messageMenuPopup, "miLike", x => x.LikeMessage);
            //fluent.BindCommandToElement(messageMenuPopup, "miCopy", x => x.CopyMessage);
            //fluent.BindCommandToElement(messageMenuPopup, "miCopyText", x => x.CopyMessageText);
            //fluent.BindCommandToElement(messageMenuPopup, "miDelete", x => x.DeleteMessage);
            fluent.WithEvent(btn_Reset, "Click")
                .EventToCommand((x) => x.RegisterMessageEvent(), null);   

            messagesItemsView.ElementMouseClick += OnMessagesViewElementMouseClick;
            messagesItemsView.TopRowPixelChanged += OnMessagesTopRowPixelChanged;
            messageMenuPopup.Hidden += OnMessageMenuPopupHidden;
            _timer.Tick += OnIntervalTimeElapsed;
        }
        private void ClientIsTypingGroup(string client, bool isTyping)
        {
            //if (client != viewmodel._userId)
            //{
            lbthongbao.Visible = true;
            lbthongbao.Text = isTyping ? $"{client} đang viết..." : string.Empty;
            // }
        }

        private async void OnIntervalTimeElapsed(object sender, EventArgs e)
        {
            _timer.Stop();
            //await connection.InvokeAsync("TypeMessage", viewmodel._userId, false);
            lbthongbao.Visible = false;
        }

        private DevExpress.Utils.Html.CssStyle activeMoreStyle;

        void OnMessagesViewElementMouseClick(object sender, ItemsViewHtmlElementMouseEventArgs e)
        {
            if (e.ElementId == "btnMore")
            {
                activeMoreStyle = e.Element.Style;
                activeMoreRowHandle = e.RowHandle;
                activeMoreStyle.SetProperty("opacity", "1");
            }
            if (e.ElementId == "btnMore" || e.ElementId == "btnLike")
                ShowMenu(e);
        }

        private void ShowMenu(ItemsViewHtmlElementMouseEventArgs e)
        {
            var size = ScaleDPI.ScaleSize(new Size(212, 180));
            var location = new Point(
                e.Bounds.X - (size.Width - e.Bounds.Width) / 2,
                e.Bounds.Y - size.Height + ScaleDPI.ScaleVertical(8));
            messageMenuPopup.Show(gridControl, gridControl.RectangleToScreen(new Rectangle(location, size)));
        }

        private void OnMessagesTopRowPixelChanged(object sender, EventArgs e)
        {
            messageMenuPopup.Hide();
        }

        private int? activeMoreRowHandle;

        void OnMessageMenuPopupHidden(object sender, EventArgs e)
        {
            activeMoreStyle = null;
            if (activeMoreRowHandle.HasValue)
                messagesItemsView.RefreshRow(activeMoreRowHandle.Value);
            activeMoreRowHandle = null;
        }

        private void InitializeMessageEdit()
        {
            var autoHeightEdit = messageEdit as IAutoHeightControlEx;
            autoHeightEdit.AutoHeightEnabled = true;
            autoHeightEdit.HeightChanged += OnMessageHeightChanged;
        }

        private void OnMessageHeightChanged(object sender, EventArgs e)
        {
            var contentSize = typingBox.GetContentSize();
            typingBox.Height = contentSize.Height;
        }

        private void OnQueryItemTemplate(object sender, QueryItemTemplateEventArgs e)
        {
            ManageMessageViewModel message = e.Row as ManageMessageViewModel;
            if (message == null)
                return;
            if (message.UserId == ConnextService.UserId)
            {
                switch (message.IsType)
                {
                    case Common.Enums.FileTypeEnum.MESSAGE:
                        Styles.MyMessage.Apply(e.Template);
                        break;

                    case Common.Enums.FileTypeEnum.FILE:
                        Styles.MyMessagesFile.Apply(e.Template);
                        break;

                    case Common.Enums.FileTypeEnum.IMAGE:
                        Styles.MyMessagesImg.Apply(e.Template);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (message.IsType)
                {
                    case Common.Enums.FileTypeEnum.MESSAGE:
                        Styles.Message.Apply(e.Template);
                        break;

                    case Common.Enums.FileTypeEnum.FILE:
                        Styles.MessageFile.Apply(e.Template);
                        break;

                    case Common.Enums.FileTypeEnum.IMAGE:
                        Styles.MessageImg.Apply(e.Template);
                        break;

                    default:
                        break;
                }
            }
        }

        private void OnCustomizeItem(object sender, CustomizeItemArgs e)
        {
            var message = e.Row as ManageMessageViewModel;
            if (message == null)
                return;
            //if(message.IsLiked) {
            //    var btnLike = e.ElementInfo.FindElementById("btnLike");
            //    var btnMore = e.ElementInfo.FindElementById("btnMore");
            //    if(btnLike != null && btnMore != null) {
            //        btnLike.Hidden = false;
            //        btnMore.Hidden = true;
            //    }
            //}
            //if(message.IsFirstMessageOfBlock)
            //    return;
            //if(!message.IsOwnMessage) {
            //if(!message.IsFirstMessageOfReply) {
            //    var sent = e.ElementInfo.FindElementById("sent");
            //    if(sent != null)
            //        sent.Hidden = true;
            //}
        }

        private async void btnDownLoad_Click(object sender, ItemsViewHtmlElementMouseEventArgs e)
        {
            if (e.RowHandle < 0) return;
            var msg = messagesItemsView.GetRow(e.RowHandle) as ManageMessageViewModel;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            DialogResult result = folderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string fileRoot = folderBrowserDialog.SelectedPath;
                try
                {
                    string urlPath = $@"{ConnextService.UriChat}{msg.FilePath}";
                    string fileName = Path.GetFileName(urlPath);
                    System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                    var res = await httpClient.GetStreamAsync(urlPath);
                    string filesave = $@"{fileRoot}\{fileName}";
                    if (File.Exists(filesave))
                        File.Delete(filesave);
                    //Save file tạm vào trong thư mục
                    using (var fs = new FileStream(filesave, FileMode.CreateNew))
                    {
                        await res.CopyToAsync(fs);
                    }
                    DialogResult dialogResult = MessageShower.ShowYesNoQuestion("File đã được tải xuống. Bạn có muốn file hay không ???", "Thông báo");
                    if (dialogResult == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", filesave);
                    }
                }
                catch (Exception ex)
                {
                    MessageShower.ShowError($"Đã có lỗi khi tải file xuống", "Thông báo");
                    return;
                }
            }
        }

        //[Obfuscation(ApplyToMembers = true, Exclude = false, Feature = "-rename")]

        private sealed class Styles
        {
            public static Style Toolbar = new ToolbarStyle();
            public static Style Message = new MessageStyle();
            public static Style MyMessage = new MyMessageStyle();
            public static Style NoMessages = new NoMessagesStyle();
            public static Style Menu = new MenuStyle();
            public static Style TypingBox = new TypingBoxStyle();
            public static Style MyMessagesImg = new MyMessagesImgStyle();
            public static Style MyMessagesFile = new MyMessagesFileStyle();
            public static Style MessageImg = new MessageImgStyle();
            public static Style MessageFile = new MessageFileStyle();

            private sealed class ToolbarStyle : Style
            {
                public ToolbarStyle() : base("ToolbarStyle", "ToolbarStyle") { }
            }

            sealed class MessageStyle : Style
            {
                public MessageStyle() : base("MessageStyle", "MessageStyle") { }
            }

            sealed class MyMessageStyle : Style
            {
                public MyMessageStyle() : base("MyMessageStyle", "MyMessageStyle") { }
            }

            sealed class MenuStyle : Style
            {
                public MenuStyle() : base("MenuStyle", "MenuStyle") { }
            }

            sealed class TypingBoxStyle : Style
            {
                public TypingBoxStyle() : base("TypingBoxStyle", "TypingBoxStyle") { }
            }

            private sealed class NoMessagesStyle : Style
            {
                public NoMessagesStyle() : base("NoMessagesStyle", "NoMessagesStyle") { }
            }

            sealed class MyMessagesImgStyle : Style
            {
                public MyMessagesImgStyle() : base("MyMessagesImgStyle", "MyMessagesImgStyle") { }
            }

            sealed class MyMessagesFileStyle : Style
            {
                public MyMessagesFileStyle() : base("MyMessagesFileStyle", "MyMessagesFileStyle") { }
            }

            sealed class MessageImgStyle : Style
            {
                public MessageImgStyle() : base("MessageImgStyle", "MessageImgStyle") { }
            }

            sealed class MessageFileStyle : Style
            {
                public MessageFileStyle() : base("MessageFileStyle", "MessageFileStyle") { }
            }
        }

        private void messageEdit_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void messageEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btn_Reset_VisibleChanged(object sender, EventArgs e)
        {
            if (btn_Reset.Visible)
            {
                btn_Reset.PerformClick();
            }
        }
    }
}