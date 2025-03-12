using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Constant.Enum;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.MLogging;
using PhanMemQuanLyThiCong.Model;


//using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PhanMemQuanLyThiCong.Controls.DrainageControls.CumDuAn
{
    public partial class uc_CumDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        public bool Changed = false;
        public uc_CumDuAn()
        {
            InitializeComponent();
        }
        private async void uc_CumDuAn_Load(object sender, EventArgs e)
        {

            RepositoryItemurp_ActionButtonEditRepository repo = new RepositoryItemurp_ActionButtonEditRepository();
            repo.CusButtonClick += repositoryItemurp_ActionButtonEditRepository1_CusButtonClick;
            col_Action.ColumnEdit = repo;
            LoadData();
        }

        private async Task<List<CumDuAnDto>> LoadData()
        {
            try
            {
                WaitFormHelper.ShowWaitForm("Đang tải cụm dự án");

                var res = await CusHttpClient.InstanceCustomer.MGetAsync<List<CumDuAnDto>>(RouteAPI.CumDuAn_GetAll);
                if (!res.MESSAGE_TYPECODE)
                {
                    MessageShower.ShowError($"Không thể tải thông tin cụm dự án!\r\n{res.MESSAGE_CONTENT}");
                    return new List<CumDuAnDto>();
                }

                BeginInvoke(new Action(() =>
                {
                    tl_data.DataSource = res.Dto;
                }));
                return res.Dto;


            }
            catch (Exception ex)
            {
                //var err = CusLogHelper.GetLogStringFromException(ex);
                MessageShower.ShowError($"Lỗi tải thông tin cụm dự án!\r\n");

                Logging.Error(ex.Message);
                return new List<CumDuAnDto>();

            }
            finally
            {
                WaitFormHelper.CloseWaitForm();
            }

        }


        private void bt_Add_Click(object sender, EventArgs e)
        {
            var uc = new ucEdit_CumDuAn();
            uc.Mode = ActionTypeEnum.ADD;
            uc.ActionSucceed += Handler_ComponentChanged;

            var form = uc.CreateFormToShowControl("Thêm vị trí", false);
            form.ShowDialog();
            //LoadData();
        }

        private async void uc_sorting1_SortingChanged(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void uc_Paging1_PagingChanged(object sender, EventArgs e)
        {
            await LoadData();

        }

        private void tl_data_CustomUnboundColumnData(object sender, DevExpress.XtraTreeList.TreeListCustomColumnDataEventArgs e)
        {

        }

        private void repositoryItemurp_ActionButtonEditRepository1_CusButtonClick(ButtonActionTypeEnum e)
        {
            var il = tl_data.GetFocusedRow() as CumDuAnDto;
            var uc = new ucEdit_CumDuAn(il);
            switch (e)
            {
                case ButtonActionTypeEnum.Edit:

                    
                    uc.Mode = ActionTypeEnum.UPDATE;
                    break;
                case ButtonActionTypeEnum.View:
                    uc.Mode = ActionTypeEnum.VIEW;
                    break;
                case ButtonActionTypeEnum.Delete:

                    

                    var dr = MessageShower.ShowYesNoQuestion($"Bạn có chắc chắn muốn xóa {il.Name} khỏi hệ thống không?");

                    if (dr != DialogResult.Yes)
                        return;

                    var ret = Task.Run(async () => await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>(RouteAPI.CumDuAn_Delete, il.Id)).Result;

                    if (ret.MESSAGE_TYPECODE)
                    {
                        MessageShower.ShowInformation("Đã xóa cụm dự án!");
                        tl_data.Nodes.Remove(tl_data.FocusedNode);
                    }
                    else
                    {
                        MessageShower.ShowInformation("Lỗi xóa cụm dự án!");
                    }
                    return;
                default:
                    uc.Dispose();
                    return;
            }

            var form = Common.Helper.ControlHelper.CreateFormToShowControl(uc, "Xem/Chỉnh sửa thông tin", false);
            uc.ActionSucceed += Handler_ComponentChanged;
            form.ShowDialog();
        }

        private async void Handler_ComponentChanged(CumDuAnDto component)
        {
            Changed = true;
            await LoadData();
            
            
        }

        private void uc_sorting1_SortingChanged_1(object sender, EventArgs e)
        {
            Task.Run(async () => await LoadData()).Wait();
        }

        private void bt_refresh_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void tl_data_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (var node in tl_data.GetNodeList())
            {
                //var crNode = e.Node;
                var prevNodes = tl_data.GetNodeList().Where(x => x.ParentNode == node.ParentNode && x.Id < node.Id);

                //if (e.Column.FieldName == "STT")
                //{
                var STT = node.ParentNode?.GetValue("STT")?.ToString();
                if (!string.IsNullOrEmpty(STT))
                {
                    STT += ".";
                };

                STT += (prevNodes.Count() + 1).ToString();
                node.SetValue("STT", STT);
                //}
            }
        }

    }


}
