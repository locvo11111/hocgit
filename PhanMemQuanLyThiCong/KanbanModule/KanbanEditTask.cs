using DevExpress.DevAV;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.EditForm.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.ChatBox;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Constant.Enum;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Function;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using PhanMemQuanLyThiCong.Urcs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Model;

namespace PhanMemQuanLyThiCong.KanbanModule
{
    public partial class KanbanEditTask : EditFormUserControl
    {

        DevExpress.XtraGrid.Views.Tile.TileView OwnerView;
        GridControl OwnerGrid { get { return OwnerView.GridControl; } }
        TaskRecord _task;
        List<AppUserViewModel> AllUsers { get; set; }
        List<Guid> Members { get; set; }
        List<SubTask> Checklist { get; set; }
        string _apiFilePath = "";
        string Rooturl = CusHttpClient.InstanceCustomer.BaseAddress.ToString().Replace("/api/","/");
        public KanbanEditTask(DevExpress.XtraGrid.Views.Tile.TileView ownerView, List<AppUserViewModel> allUser)
        {
            InitializeComponent();
            this.OwnerView = ownerView;
            //LabelComboBoxEdit.Properties.Items.AddRange(typeof(TaskLabel).GetEnumValues());
            this.AllUsers = allUser;
            //this.memberTiles.ItemClick += tileControl1_ItemClick;
            this.VisibleChanged += (s, e) => { if (Visible) ControlShown(); };
            //this.gridChecklist.CellValueChanged += gridView1_CellValueChanged;
            this.gridChecklist.RowCountChanged += (s, e) => { UpdateProgressBar(); };
            
        }

        //void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        //{
        //    if (e.Item == addMemberItem)
        //        return;
        //    var menu = new DXPopupMenu();
        //    menu.MenuViewType = MenuViewType.Menu;
        //    string id = e.Item.Tag as string;
        //    var removeItem = new DXMenuItem("Remove from card", new EventHandler(OnRemoveItemClick)) { Tag = id };
        //    menu.Items.Add(removeItem);
        //    ShowPopup(menu);
        //}

        //void ShowPopup(DXPopupMenu menu)
        //{
        //    Control parentControl = memberTiles;
        //    Point pt = parentControl.PointToClient(Control.MousePosition);
        //    ((IDXDropDownControl)menu).Show(OwnerGrid.MenuManager, parentControl, pt);
        //}

        void ControlShown()
        {
            _task = OwnerView.FocusedRowObject as TaskRecord;
            LabelComboBoxEdit.SelectedIndex = _task.LabelCore;

            this.Members = _task.Users;
            this.Checklist = _task.SubTasks;
            list = new BindingList<SubTask>(Checklist);
            this.gc_DanhSachCongViec.DataSource = list;
            this.memberTiles.AnimateArrival = false;
            this.layoutControl1.MenuManager = OwnerGrid.MenuManager;

            listFile = new BindingList<FileViewModel>(_task.FilesVM);
            gc_TaiLieuDinhKem.DataSource = listFile;
            PopulateMembers();
            UpdateProgressBar();
            LoadChat(); _task = OwnerView.FocusedRowObject as TaskRecord;
            LabelComboBoxEdit.SelectedIndex = _task.LabelCore;

            this.Members = _task.Users;
            this.Checklist = _task.SubTasks;
            list = new BindingList<SubTask>(Checklist);
            this.gc_DanhSachCongViec.DataSource = list;
            this.memberTiles.AnimateArrival = false;
            this.layoutControl1.MenuManager = OwnerGrid.MenuManager;

            listFile = new BindingList<FileViewModel>(_task.FilesVM);
            gc_TaiLieuDinhKem.DataSource = listFile;
            PopulateMembers();
            UpdateProgressBar();
            LoadChat();
        }
        void PopulateMembers()
        {
            tileGroup.Items.Clear();
            addMemberItem.Visible = true;
            tileGroup.Items.Add(addMemberItem);

            //var memberIDs = GetMemeberIDs();
            foreach (var userId in Members)
            {
                AppUserViewModel user = AllUsers.Where(x => x.Id.Equals(userId)).FirstOrDefault();
                if (user is null)
                    continue;
                //var bytes = user.AvatarArr;
                var item = new TileItem();
                Image img = KanbanHelper.CreateMemberGlyph(user, LookAndFeel, ScaleDPI.ScaleVertical(30));
                item.Image = img;
                item.ImageAlignment = TileItemContentAlignment.MiddleCenter;
                var superTip = new SuperToolTip();
                superTip.Items.AddTitle(user.FullName);
                item.SuperTip = superTip;
                item.Tag = user.Id;
                tileGroup.Items.Insert(0, item);
                addMemberItem.Visible = tileGroup.Items.Count == 5 ? false : true;
            }
        }

        void UpdateProgressBar()
        {
            //int count = Checklist.Select("TaskID = '" + TaskIdStr + "'").Count();

            List<SubTask> subTask = Checklist;
            int count = subTask.Count;
            if (count == 0)
            {
                progressBarControl1.Properties.Maximum = 1;
                progressBarControl1.Position = 0;
            }
            else
            {
                progressBarControl1.Properties.Maximum = count;
                progressBarControl1.Position = subTask.Where(x => x.IsDone).Count();
            }
        }

        private async void LoadChat()
        {
            //await ConnextService._Connection.InvokeAsync("AddUserToGroups", BaseFrom.BanQuyenKeyInfo, ConnextService.ManageGroups, false);
            //var findGroup = ConnextService.ManageGroups.Find(x => x.TaskId == _task.Code);
            //if (findGroup is null)
            //{
            //    findGroup = new GeneralGroupChatViewModel()
            //    {
            //        TaskId = _task.Code,
            //    };
            //    ConnextService.ManageGroups.Add(findGroup);

            //    await ConnextService.AddUserToGroups(new List<GeneralGroupChatViewModel>() { findGroup });
            //}

            ConnextService.groupIndex = new GeneralGroupChatViewModel()
            {
                ParentId = _task.Code.ToString(),
                ChatType = ChatTypeEnum.GiaoNhiemVu
            };

            ConnextService.ManageGroups = new List<GeneralGroupChatViewModel>() { ConnextService.groupIndex };

            SharedControls.uc_ChatBox.Parent = pn_Chat;
            SharedControls.uc_ChatBox.contactsView1.btn_Reset.PerformClick();
        }

        BindingList<SubTask> list = new BindingList<SubTask>();
        BindingList<FileViewModel> listFile = new BindingList<FileViewModel>();
        private void KanbanEditTask_Load(object sender, EventArgs e)
        {


        }

        private void repositoryItemCheckEdit1_CheckStateChanged(object sender, EventArgs e)
        {
            gridChecklist.CloseEditor();
            UpdateProgressBar();
        }

        private void gridChecklist_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridChecklist_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gridChecklist.SetRowCellValue(e.RowHandle, colCaption, "Việc mới");
            gridChecklist.SetRowCellValue(e.RowHandle, colChecked, false);
            gridChecklist.SetRowCellValue(e.RowHandle, colId, Guid.NewGuid().ToString());
        }

        void addMemberItem_ItemClick(object sender, TileItemEventArgs e)
        {

            var menu = new DXPopupMenu();
            menu.MenuViewType = MenuViewType.Menu;
            foreach (var user in AllUsers)
            {
                Guid id = user.Id;
                string fullName = user.FullName;
                var memberMenuItem = new DXMenuItem(fullName, new EventHandler(OnAddMemberMenuClick)) { Tag = id };
                menu.Items.Add(memberMenuItem);
            }
            ShowPopup(menu);
        }

        void OnAddMemberMenuClick(object sender, EventArgs e)
        {
            Guid newId = (Guid)(sender as DXMenuItem).Tag;
            AppUserViewModel user = AllUsers.Where(x => x.Id == newId).Single();
            //Stream stream = KanbanHelper.CreateMemberGlyph(user, LookAndFeel, ScaleDPI.ScaleVertical(30));
            
            Members.Add(newId);
             //var newRow = Members.NewRow();
            //newRow["TaskID"] = TaskId;
            //newRow["MemberID"] = newId;
            //Members.Rows.Add(newRow);
            PopulateMembers();
        }
        void ShowPopup(DXPopupMenu menu)
        {
            Control parentControl = memberTiles;
            Point pt = parentControl.PointToClient(Control.MousePosition);
            ((IDXDropDownControl)menu).Show(OwnerGrid.MenuManager?? new SkinMenuManager(UserLookAndFeel.Default), parentControl, pt);
        }

        private void bt_Update_Click(object sender, EventArgs e)
        {

        }

        private void gridChecklist_EditFormShowing(object sender, EditFormShowingEventArgs e)
        {
            //e.clo
        }

        private void bt_ThemTaiLieu_Click(object sender, EventArgs e)
        {
            var openFileDialog = SharedControls.openFileDialog;
            openFileDialog.Filter = "All files (*.*)|*.*|WORD (*.doc, *.docx)|*.doc;*.docx|PDF (*.pdf)|*.pdf|EXCEL (*." +
                                        "xls, *.xlsx)|*.xls;*.xlsx|Hình ảnh (*.pnj, *.jpg)|*.pnj;*.jpg";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] filesExisting = _task.FilesVM.Select(x => x.FileName).ToArray();
                List<string> fileExisted = new List<string>();
                foreach (string file in openFileDialog.FileNames)
                {
                    if (filesExisting.Contains(Path.GetFileName(file)))
                    {
                        fileExisted.Add(Path.GetFileName(file));
                        continue;
                    }

                    FileViewModel newFile = new FileViewModel()
                                            {
                                                FilePath = file
                                            };

                    
                    newFile.Content = FileHelper.GetBytes(file);
                    _task.FilesVM.Add(newFile);
                }

                if (fileExisted.Any())
                {
                    string noti = "Các tệp tin sau đã tồn tại: " + string.Join(", ", fileExisted);
                    MessageShower.ShowWarning(noti);
                }

                gc_TaiLieuDinhKem.RefreshDataSource();
            }
        }

        private async void repoBt_XemTruoc_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            FileViewModel file = gv_TaiLieuDinhKem.GetFocusedRow() as FileViewModel;
            //file.FilePath;// = (file.FilePath);
            if (file.Content != null)
                ChatFunction.xemTruocFileCoBan(file.FilePath, null, null, file.Content);
            else
            {
                var ret = await CusHttpClient.InstanceCustomer.MGetAsync<FileViewModel>($"{RouteAPI.TASK_GetFile}/{_task.Code}/{file.FileName}");
                if (ret.MESSAGE_TYPECODE)
                {
                    file.Content = ret.Dto.Content;
                    ChatFunction.xemTruocFileCoBan(file.FilePath, null, null, file.Content);
                }
                else
                    MessageShower.ShowError("Không thể tải file để xem trước");
            }
        }

        private void LabelComboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            _task.LabelCore = LabelComboBoxEdit.SelectedIndex;
        }

        private void bt_Xoa_Click(object sender, EventArgs e)
        {
            _task.FilesVM.RemoveAt(gv_TaiLieuDinhKem.FocusedRowHandle);
            gc_TaiLieuDinhKem.RefreshDataSource();
        }

        private void memberTiles_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
