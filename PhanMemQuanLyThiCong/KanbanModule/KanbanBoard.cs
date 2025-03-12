using DevExpress.Mvvm.Native;
using DevExpress.Office.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraScheduler;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Controls.KiemSoat;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using PhanMemQuanLyThiCong.Repositories;
using PhanMemQuanLyThiCong.Services.ChatServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Model;
//using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace PhanMemQuanLyThiCong.KanbanModule
{
    public partial class KanbanBoard : DevExpress.XtraEditors.XtraForm
    {
        List<TaskRecord> _tasks = new List<TaskRecord>();
        List<AppUserViewModel> _users;
        AppUserViewModel crUser;
        public KanbanBoard()
        {
            InitializeComponent();
            
        }

        private async void gc_Task_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang đồng bộ người dùng");
            _users = await UserHelper.GetAllUserInCusSever();
            if (_users is null)
            {
                MessageShower.ShowError("Không thể lấy thông tin người dùng");
                WaitFormHelper.CloseWaitForm();

                this.Close();
                return;
            }

            foreach (var user in _users)
            {
                KanbanHelper.CreateMemberGlyph(user, LookAndFeel, ScaleDPI.ScaleVertical(30));
            }

            AppUserViewModel crUser = await UserHelper.GetCurrentUser();
            if (crUser is null)
                MessageShower.ShowInformation("Vui lòng đăng nhập trước khi vào phần giao việc");

            View_Task.OptionsEditForm.CustomEditFormLayout = new KanbanEditTask(View_Task, _users);
            WaitFormHelper.CloseWaitForm();

            reload_Task();
        }

        List<TaskRecord> lsTaskRecord = new List<TaskRecord>();
        private async void reload_Task()
        {
            WaitFormHelper.ShowWaitForm("Đang tải nhiệm vụ");

            gc_Task.DataSource = null;
            _tasks = await TaskHelper.GetAllTask();

            if (_tasks is null)
                MessageShower.ShowInformation("Không thể tải công việc! Kiểm tra kết nối internet!");

            foreach (var task in _tasks)
            {
                task.FilesVM = task.Files.Select(x => new FileViewModel()
                {
                    FilePath = x,
                    isEdited = false,
                }).ToList();
            }
            gc_Task.DataSource = _tasks;
            WaitFormHelper.CloseWaitForm();
        }

        Color GetLabelColor(TaskLabel label)
        {
            switch (label)
            {
                case TaskLabel.Red: return ColorTranslator.FromHtml("#f06562");
                case TaskLabel.Green: return ColorTranslator.FromHtml("#1fb876");
                case TaskLabel.Yellow: return ColorTranslator.FromHtml("#fca90a");
                default: return ColorTranslator.FromHtml("#969696");
            }
        }

        private void View_Task_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            //if (elements.Count == 0) return;
            var task = View_Task.GetRow(e.RowHandle) as TaskRecord;
            //if ()

            e.Item["Label"].Appearance.Normal.BackColor = GetLabelColor(task.Label);

            if (e.Item.Name == "itemMember" && task.Users is null)
                return;

            //(e.Item.Elements as TileViewItemElementCollection).GetElementByName("ProgressTC_KH").Text = $"{task.SubTasks.Where(x => x.IsDone).Count()}/{task.SubTasks.Count}";

            var elements = GetMembersElements(task.Users);
            TileViewItemElement prev = null;
            //e.Item.Elements.ToList().RemoveAll(x => x.Text == "userImage");
            foreach (var element in elements)
            {
                e.Item.Elements.Add(element);
                if (prev != null)
                {
                    element.AnchorElement = prev;
                    element.AnchorAlignment = AnchorAlignment.Left;
                    element.AnchorIndent = 4;
                }
                else
                {
                    element.RowIndex = 3;
                    element.ColumnIndex = 4;
                    element.ImageAlignment = TileItemContentAlignment.BottomRight;
                }
                prev = element;
            }
        }

        List<TileViewItemElement> GetMembersElements(List<Guid> users)
        {
            var result = new List<TileViewItemElement>();
            foreach (var userId in users)
            {
                AppUserViewModel user = _users.Where(x => x.Id == userId).FirstOrDefault();
                if (user is null)
                    continue;
                Image image = KanbanHelper.CreateMemberGlyph(user, LookAndFeel, ScaleDPI.ScaleVertical(30));
                //user.AvatarArr = stream.ToArray();
                var element = new TileViewItemElement();
                //element.Text = "userImage";
                element.Image = image;
                //stream.Dispose();
                //stream.Close();
                result.Add(element);
            }
            return result;
        }

        private void View_Task_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
             
        }

        private void View_Task_GroupHeaderContextButtonClick(object sender, GroupHeaderContextButtonClickEventArgs e)
        {
            var status = (TaskStep)e.GroupValue;
            AddNewCard(status);
        }

        private async void AddNewCard(TaskStep status)
        {
            string newCaption = XtraInputBox.Show("", "Thêm công việc", "Công việc mới");
            if (String.IsNullOrEmpty(newCaption)) return;
            var newTask = KanbanHelper.CreateNewTask();
            newTask.Code = Guid.NewGuid();
            newTask.StepCore = (int)status;
            newTask.Caption = newCaption;

            var response = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>("Task/CreateOrEdit", newTask);

            if (!response.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Thêm nhiệm vụ mới không thành công! VUi lòng kiểm tra kết nối mạng");
                return;
            }    

            _tasks.Add(newTask);
            gc_Task.RefreshDataSource();
        }

        private void View_Task_ItemDoubleClick(object sender, TileViewItemClickEventArgs e)
        {
            //KanbanEditTask form = new KanbanEditTask(View_Task, _users);
            //form.ShowDialog();

        }

        private async void View_Task_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {


        }

        private async void View_Task_EditFormHidden(object sender, DevExpress.XtraGrid.Views.Grid.EditFormHiddenEventArgs e)
        {
            if (e.Result == DevExpress.XtraGrid.Views.Grid.EditFormResult.Cancel) 
                return;
            
            var task = View_Task.FocusedRowObject as TaskRecord;
            //task.FilesVM = task.FilesVM.Where(x => x.Content != null).ToList();
            var tt = JsonConvert.SerializeObject(task);

            var response = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>("Task/CreateOrEdit", task);

            if (response.MESSAGE_TYPECODE)
                MessageShower.ShowInformation("Cập nhật công việc thành công!");
            else
            {
                MessageShower.ShowError("Cập nhật công việc không thành công!");
                reload_Task();
            }
        }

        private async void View_Task_ItemDrop(object sender, ItemDropEventArgs e)
        {
            TaskRecord crTask = (gc_Task.DataSource as List<TaskRecord>)[e.ListSourceRowIndex];

            var response = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>("Task/CreateOrEdit", crTask);

            View_Task.ItemCustomize += View_Task_ItemCustomize;
            gc_Task.RefreshDataSource();
        }

        private void View_Task_ItemDrag(object sender, DevExpress.XtraGrid.Views.Tile.ItemDragEventArgs e)
        {
            View_Task.ItemCustomize -= View_Task_ItemCustomize;
        }

        private void View_Task_EditFormPrepared(object sender, DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventArgs e)
        {
            Control ctrl = MyExtenstions.FindControl(e.Panel, "Update");
            if (ctrl != null)
                ctrl.Text = "Cập nhật";
            ctrl = MyExtenstions.FindControl(e.Panel, "Cancel");
            if (ctrl != null)
                ctrl.Text = "Hủy bỏ";
        }

        private void sb_XemTienDo_Click(object sender, EventArgs e)
        {
            Uc_TienDoGiaoNhiemVu TienDo = new Uc_TienDoGiaoNhiemVu();
            TienDo.Fcn_LoadData();
            TienDo.ShowDialog();
        }

        private void repobe_delete_Click(object sender, EventArgs e)
        {

        }

        private async void bt_xoa_Click(object sender, EventArgs e)
        {
            var obj = View_Task.FocusedRowObject as TaskRecord;
            
            if (obj is null)
            {
                MessageShower.ShowWarning("Vui lòng chọn công tác trước");
            }

            var dr = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa nhiệm vụ: "+ obj.Caption);

            if (dr == DialogResult.Yes)
            {
            var response = await CusHttpClient.InstanceCustomer.MGetAsync<bool>($"Task/Delete/{obj.Code}");

                if (!response.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError($"Lỗi xóa công tác: \r\n{response.MESSAGE_CONTENT}");
                }
                else
                {
                    MessageShower.ShowInformation($"Đã xóa công tác!");
                    View_Task.DeleteRow(View_Task.FocusedRowHandle);

                }
            }
        }
    }
}
