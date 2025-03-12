using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class uc_Notification : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_Notification()
        {
            InitializeComponent();
        }

        private void NotificationFilter()
        {
            string name = rg_noti.GetAccessibleName();

            if (name is null || name == "ALL")
            {
                gv_noti.ClearColumnsFilter();
            }
            else
            {
                col_Type.FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo($"[Type] = {name}");
            }
        }

        public async void InitNotification()
        {
            if ((BaseFrom.IsFullAccess || BaseFrom.IsValidAccount)
                && !string.IsNullOrEmpty(CusHttpClient.InstanceCustomer.BaseAddress))
            {
                //customPopupContainerEdit_noti.Enabled = true;
                var response = await CusHttpClient.InstanceCustomer.MGetAsync<List<NotificationViewModel>>($"{RouteAPI.Notification_CONTROLLER}/GetByUserId/{BaseFrom.BanQuyenKeyInfo.UserId}");


                if (!response.MESSAGE_TYPECODE)
                {
                    AlertShower.ShowInfo("Không thể tải thông báo", "Lỗi");
                    gc_noti.DataSource = new BindingList<NotificationViewModel>();
                    SharedControls.badge_Noti.Visible = false;
                    BaseFrom.count_noti = 0;
                }
                else
                {
                    gc_noti.DataSource = new BindingList<NotificationViewModel>(response.Dto);
                    BaseFrom.count_noti = response.Dto.Where(x => x.State == NotificationStateEnum.NEW).Count();
                    if (BaseFrom.count_noti > 0)
                    {
                        SharedControls.badge_Noti.Visible = true;
                        SharedControls.badge_Noti.Properties.Text = BaseFrom.count_noti.ToString();
                    }
                    else
                        SharedControls.badge_Noti.Visible = false;

                    NotificationFilter();
                }
            }
        }

        public object DataSource
        {
            get { return gc_noti.DataSource; }
            set { gc_noti.DataSource = value;}
        }

        public void RefreshDataSource()
        {
            gc_noti.RefreshDataSource();
        }

        private void rg_noti_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotificationFilter();
        }
    }
}
