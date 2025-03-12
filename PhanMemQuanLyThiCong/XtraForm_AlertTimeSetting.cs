using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_AlertTimeSetting : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_AlertTimeSetting()
        {
            InitializeComponent();
            LoadTimes();
            //repositoryItemTimeEdit1.
        }

        private void bt_Add_Click(object sender, EventArgs e)
        {
            var times = tl_QuyTrinh.DataSource as List<AlertTime>;
            times.Add(new AlertTime()
            {
                STT = tl_QuyTrinh.AllNodesCount + 1,
            });
            tl_QuyTrinh.RefreshDataSource();
            //times.Add
        }

        private async Task LoadTimes()
        {
            var result = await CusHttpClient.InstanceCustomer.MGetAsync<List<AlertTime>>(RouteAPI.TongDuAn_GetAlertTimes);
            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError($"Lỗi tải thời gian gửi cảnh báo!\r\n{result.MESSAGE_CONTENT}");
                Close();
                return;
            }
            int STT = 1;
            result.Dto.ForEach(x => x.STT = STT++);
            tl_QuyTrinh.DataSource = result.Dto;

        }

        private void tl_QuyTrinh_InitNewRow(object sender, DevExpress.XtraTreeList.TreeListInitNewRowEventArgs e)
        {
            e.SetValue("STT", tl_QuyTrinh.AllNodesCount);
            //e.SetValue("STT", tl_QuyTrinh.AllNodesCount);
        }

        private void repobt_Del_Click(object sender, EventArgs e)
        {
            var rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa bước duyệt này không?");

            if (rs == DialogResult.Yes)
            {
                tl_QuyTrinh.FocusedNode.Remove();
            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void bt_refresh_Click(object sender, EventArgs e)
        {
            await LoadTimes();
        }

        private async void bt_Save_Click(object sender, EventArgs e)
        {
            var datas = tl_QuyTrinh.DataSource as List<AlertTime>;
            int ind = 0;
            foreach (var item in datas.OrderBy(x => x.Time))
            {
                item.Id = ind++;
            }
            var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>(RouteAPI.TongDuAn_SetAllAlertTimes, datas);

            if (!result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowError($"Cập nhật không thành công!\r\n{result.MESSAGE_CONTENT}");

            }
            else
                MessageShower.ShowInformation("Đã cập nhật");
        }

        private void tl_QuyTrinh_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            var fieldName = tl_QuyTrinh.FocusedColumn.FieldName;

            if (fieldName == "Time")
            {
                string newVal = (string)e.Value;
                if (!DateTimeHelper.IsTimeSpan(newVal, out TimeSpan ts))
                {
                    e.Valid = false;
                    e.ErrorText = $"Vui lòng nhập đúng 1 trong định dạng thời gian sau {Environment.NewLine} (VD: [08h00; 8h00])";
                    return;
                }

                e.Value = $"{ts.Hours.ToString().PadLeft(2, '0')}:{ts.Minutes.ToString().PadLeft(2, '0')}:00";
            }

        }

        private void tl_QuyTrinh_ShowingEditor(object sender, CancelEventArgs e)
        {
            
        }

        private void tl_QuyTrinh_ShownEditor(object sender, EventArgs e)
        {
            var crText = tl_QuyTrinh.ActiveEditor.Text;
            if (!DateTime.TryParse(crText, out DateTime ts))
            {
                return;
            }
            tl_QuyTrinh.ActiveEditor.Text = $"{ts.Hour.ToString().PadLeft(2, '0')}h{ts.Minute.ToString().PadLeft(2, '0')}";


        }
    }
}