using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using PhanMemQuanLyThiCong;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Constant.Enum;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Validate;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace PhanMemQuanLyThiCong.Controls.DrainageControls.CumDuAn
{
    public partial class ucEdit_CumDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        CumDuAnDto _item;
        string _suffix = "";
        public ucEdit_CumDuAn(CumDuAnDto item = null, string suffix = null)
        {
            InitializeComponent();
            _item = item;
            _suffix = suffix ?? "";
            ViTriTextEdit.Text = item?.Name;
            rtb_Note.Text = item?.Description;

            CumDuAnBindingSource.DataSource = _item ?? new CumDuAnDto();
            dxValidationProvider1.SetValidationRule(ViTriTextEdit,
                new CustomValidationTextEditRule { ErrorText = "Không được để trống ô này", ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical });

        }
        public CumDuAnDto Component
        {

            get { return _item; }
            set 
            { 
                _item = value;
                CumDuAnBindingSource.DataSource = _item;
            }
        }


        private void bt_update_Click(object sender, EventArgs e)
        {
            var cpn = CumDuAnBindingSource.DataSource as CumDuAnDto;
            if (_Mode == ActionTypeEnum.ADD)
            {
                if (dxValidationProvider1.Validate())
                {
                    cpn.Id = Guid.NewGuid();
                    var ret = Task.Run(async () => await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>(RouteAPI.CumDuAn_Create, cpn)).Result;
                    if (ret.MESSAGE_TYPECODE)
                    {
                        MessageShower.ShowInformation("Thêm mới thành công");
                        RaiseReloadParent();
                        Dispose();
                        return;
                    }
                    else
                    {
                        MessageShower.ShowInformation($"Thêm mới không thành công.\r\n{ret.MESSAGE_CONTENT}");
                    }
                }
            }
            else if (_Mode == ActionTypeEnum.UPDATE)
            {
                var ret = Task.Run(async () => await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>(RouteAPI.CumDuAn_Update, cpn)).Result;
                if (ret.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowInformation("Đã cập nhật");
                    Mode = ActionTypeEnum.VIEW;
                    RaiseReloadParent();
                    Dispose();
                    return;
                }
                else
                {
                    MessageShower.ShowInformation($"Cập nhật không thành công.\r\n{ret.MESSAGE_CONTENT}");
                }
            }
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            if (MessageShower.ShowYesNoQuestion("Bạn có muốn hủy bỏ các thay đổi") != DialogResult.Yes)
            {
                Dispose();
            }    
        }

        private void RaiseReloadParent()
        {
            ((EventHandlerActionSucceed)(base.Events[EventReloadParent]))?.Invoke(CumDuAnBindingSource.DataSource as CumDuAnDto);
        }

        public ActionTypeEnum _Mode;
        [Description("TBTMode")]
        [Category("TBT")]
        public ActionTypeEnum Mode
        {
            get
            {
                return _Mode;
            }
            set
            {
                _Mode = value;
                lb_info.Text = _Mode.GetEnumDisplayName().ToUpper() + " " + _suffix;
                if (_Mode == ActionTypeEnum.VIEW)
                {
                    bt_update.Enabled = false;
                    ViTriTextEdit.ReadOnly = true;
                    rtb_Note.ReadOnly = true;
                }
                else
                {
                    ViTriTextEdit.ReadOnly = false;
                    rtb_Note.ReadOnly = false;
                    bt_update.Enabled = true;
                    if (_Mode == ActionTypeEnum.ADD)
                    {
                        bt_update.ImageOptions.SvgImage = Resources.actions_add;
                        bt_update.Text = "Thêm mới";
                    }
                    else
                    {
                        bt_update.ImageOptions.SvgImage = Resources.actions_edit;
                        bt_update.Text = "Cập nhật";
                    }
                }
            }
        }

      
        #region EVENT
        public delegate void EventHandlerActionSucceed(CumDuAnDto model);
        private readonly static object EventReloadParent = new object();
        public event EventHandlerActionSucceed ActionSucceed
        {
            add
            {
                base.Events.AddHandler(EventReloadParent, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventReloadParent, value);

            }
        }
        #endregion

    }
}