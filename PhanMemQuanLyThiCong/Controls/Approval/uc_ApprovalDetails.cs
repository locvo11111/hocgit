using DevExpress.Office.DigitalSignatures;
using DevExpress.XtraEditors;
using DevExpress.XtraSpellChecker;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Enums.Approval;
using VChatCore.ViewModels.Approval;

namespace PhanMemQuanLyThiCong.Controls.Approval
{
    public partial class uc_ApprovalDetails : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_ApprovalDetails()
        {
            InitializeComponent();
        }


        private async void LoadApproval()
        {
            WaitFormHelper.ShowWaitForm("Đang tải công tác....");

            string prefix = ((ApprovalTypeEnum)cbb_ChonQuyTrinh.SelectedIndex).GetEnumName();
            //switch (cbb_ChonQuyTrinh.SelectedIndex)
            //{
            //    case 0:
            //        prefix = "GiaoViec";
            //        break;
            //    case 1:
            //        prefix = "YeuCauVatTu";

            //        break;
            //    case 2:
            //        prefix = "NhapVatTu";

            //        break;
            //    case 3:
            //        prefix = "XuatVatTu";

            //        break;
                
            //    case 4:
            //        prefix = "ChuyenKho";

            //        break;
                   

            //}    
            if (xtraTabControl_Approval.SelectedTabPage == xtraTabPage_DoiDuyet)
            {
                var result = await CusHttpClient.InstanceCustomer.MGetAsync<List<ApprovalInfoViewModel>>($"Approval{prefix}/GetCongTacDoiDuyet");
                if (result.MESSAGE_TYPECODE)
                {
                    var ls = result.Dto;
                    ls.Add(new ApprovalInfoViewModel()
                    {
                        Id = "0",
                        IsRootNode = true,
                        TenCongTac = "CÔNG TÁC ĐẾN BẠN DUYỆT"
                    });
                    
                    ls.Add(new ApprovalInfoViewModel()
                    {
                        Id = "1",
                        IsRootNode = true,
                        TenCongTac = "CÔNG TÁC ĐỢI NGƯỜI KHÁC DUYỆT TRƯỚC"
                    });

                    tl_DoiDuyet.DataSource = ls;
                }
                else
                {
                    tl_DoiDuyet.DataSource = null;
                    MessageShower.ShowError($"Không thể tải công tác đợi duyệt!\r\n{result.MESSAGE_CONTENT}");
                }
            }
            else if (xtraTabControl_Approval.SelectedTabPage == xtraTabPage_DaDuyet)
            {
                var result = await CusHttpClient.InstanceCustomer.MGetAsync<List<ApprovalInfoViewModel>>($"Approval{prefix}/GetCongTacDaDuyet");
                if (result.MESSAGE_TYPECODE)
                {
                    var ls = result.Dto;
                    tl_DaDuyet.DataSource = ls;
                }
                else
                {
                    tl_DaDuyet.DataSource = null;
                    MessageShower.ShowError($"Không thể tải công tác đã duyệt!\r\n{result.MESSAGE_CONTENT}");
                }
            }
            WaitFormHelper.CloseWaitForm();
        }

        private void xtraTabControl_Approval_Click(object sender, EventArgs e)
        {
            LoadApproval();
        }

        private void uc_ApprovalDetails_Load(object sender, EventArgs e)
        {

            //Load combobox ApprovalType
            var types = EnumHelper.GetDisplayNames<ApprovalTypeEnum>();
            cbb_ChonQuyTrinh.Properties.Items.AddRange(types.ToArray());

            cbb_ChonQuyTrinh.SelectedIndexChanged -= cbb_ChonQuyTrinh_SelectedIndexChanged;
            cbb_ChonQuyTrinh.SelectedIndex = 0;
            cbb_ChonQuyTrinh.SelectedIndexChanged += cbb_ChonQuyTrinh_SelectedIndexChanged;
            LoadApproval();
        }

        private async void repobt_Duyet_Click(object sender, EventArgs e)
        {
            ApprovalInfoViewModel task = tl_DoiDuyet.GetFocusedRow() as ApprovalInfoViewModel;
            
            if (task is null)
            {
                MessageShower.ShowWarning("Chưa chọn công tác!");
                return;
            }

            var dr = MessageShower.ShowYesNoQuestion($"Xác nhận DUYỆT công tác \"{task.TenCongTac}\" với nội dung \"{task.Note}\"?");
            if (dr != DialogResult.Yes)
                return;

            ApprovalDetailViewModel ad = new ApprovalDetailViewModel()
            {
                ApprovalProcessId = task.ProcessId,
                CodeDeXuatVatTu = task.Id,
                CodeChuyenKho = task.Id,
                CodeXuatVatTu = task.Id,
                CodeNhapVatTu = task.Id,
                CodeCongViecCha = task.Id,
                ApprovalNote = task.Note,
            };

            string prefix = ((ApprovalTypeEnum)cbb_ChonQuyTrinh.SelectedIndex).GetEnumName();

            var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"Approval{prefix}/DuyetCongTac", ad);
            if (result.MESSAGE_TYPECODE)
            {
                MessageShower.ShowInformation("Đã duyệt công tác!");
                LoadApproval();
            }
            else
            {
                string mess = $"Lỗi duyệt công tác!";
                if (result.STATUS_CODE != 0)
                {
                    mess += $"\r\n{result.MESSAGE_CONTENT}";
                }
                MessageShower.ShowError(mess);
            }

        }

        private void xtraTabControl_Approval_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadApproval();

        }

        private void tl_DoiDuyet_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Column == tlCol_Duyet)
            {
                if (e.Node.Level == 0 || e.Node.GetValue("ParentId").ToString() == "1")
                {

                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
                }    
            }
        }

        private void tl_DoiDuyet_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {

            if (e.Node.Level == 0)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 2)
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);

            if (e.Node.GetValue("Id")?.ToString() == "0")
            {
                e.Appearance.ForeColor = Color.Red;

            }
        }

        private void cbb_ChonQuyTrinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApproval();
        }
    }
}
