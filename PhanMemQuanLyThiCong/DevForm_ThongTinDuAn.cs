using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.Http;
using PhanMemQuanLyThiCong.Common.MLogging;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Dto;
using PhanMemQuanLyThiCong.Controls.PhanQuyen;
//using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{
    public partial class DevForm_ThongTinDuAn : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        List<Tbl_ThongTinDuAnViewModel> lsDA;
        const string prefix_CaiDatNhaThau = "CaiDatNhaThau_";
        const string prefix_CaiDatTask = "CaiDatTask_";
        const string prefix_CaiDatProject = "CaiDatProject_";
        public DevForm_ThongTinDuAn()
        {
            InitializeComponent();
        }

        private async void DevForm_ThongTinTongHopDuAn_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tải thông tin tổng hợp dự án");
            try
            {
                await fcn_GetListDuAn();
            }
            catch (Exception ex)
            {
                Logging.Error(ex.Message);
                MessageShower.ShowInformation("Không thể tải dữ liệu! Vui lòng kiểm tra kết nối internet của bạn!");
                WaitFormHelper.CloseWaitForm();

                this.Close();
            }
            WaitFormHelper.CloseWaitForm();
        }

        private async Task fcn_GetListDuAn()
        {
            var res = await CusHttpClient.InstanceCustomer.MGetAsync<List<Tbl_ThongTinDuAnViewModel>>($"{Server.Tbl_ThongTinDuAn}/{RouteAPI.SUFFIX_GetAll}");
            if (res.MESSAGE_TYPECODE)
            {
                lsDA = res.Dto;
                var allPermission = BaseFrom.allPermission;
                if (!BaseFrom.IsFullAccess
                    && !allPermission.HaveInitProjectPermission)
                {
                    lsDA = lsDA.Where(x => allPermission.AllProject.Contains(x.Code)).ToList();
                }

                foreach (var ThongTinDA in lsDA)
                {
                    var ace_ThongTinDA = new AccordionControlElement()
                    { 
                        Text = ThongTinDA.TenDuAn,
                        AccessibleName = ThongTinDA.Code,
                        Style = ElementStyle.Group,
                    };
                    ace_DanhSachDA.Elements.Add(ace_ThongTinDA);

                    var ace_Project = new AccordionControlElement()
                    {
                        Text = "1. Cài đặt dự án",
                        AccessibleName = ThongTinDA.Code,
                        Style = ElementStyle.Item,
                    };
                    ace_Project.Name = $"{prefix_CaiDatProject}{ace_Project.Name}";
                    
                    var ace_Contractor = new AccordionControlElement()
                    {
                        Text = "2. Cài đặt nhà thầu",
                        AccessibleName = ThongTinDA.Code,
                        Style = ElementStyle.Item,
                    };
                    ace_Contractor.Name = $"{prefix_CaiDatNhaThau}{ace_Contractor.Name}";


                    var ace_Task = new AccordionControlElement()
                    {
                        Text = "3. Cài đặt quyền công tác",
                        AccessibleName = ThongTinDA.Code,
                        Style = ElementStyle.Item,
                    };
                    ace_Task.Name = $"{prefix_CaiDatTask}{ace_Task.Name}";

                    ace_ThongTinDA.Elements.Add(ace_Project);
                    ace_ThongTinDA.Elements.Add(ace_Contractor);
                    ace_ThongTinDA.Elements.Add(ace_Task);
                }
            }
            else
            {
                MessageShower.ShowInformation("Không thể lấy thông tin THDA");
                this.Close();
            }
        }

        private async void DevForm_ThemCaNhanVaoTHDA_Load(object sender, EventArgs e)
        {
        }

        private void accordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            AccordionControlElement ac = e.Element;
            XtraUserControl ctrl;

            if (e.Element == ace_PhanQuyenChiTiet)
            {
                //if (!BaseFrom.IsFullAccess)
                //{
                //    MessageShower.ShowError("Đây là tính năng dành cho chủ sở hữu bản quyền!");
                //    return;
                //}    
                ctrl = new devctrl_NhomQuyen();
            }
            else if (e.Element.Level == 2 && e.Element.OwnerElement.OwnerElement == ace_DanhSachDA)
            {
                if (ac.Name.StartsWith(prefix_CaiDatProject))
                {
                    string code = ac.OwnerElement.AccessibleName;
                    var DA = lsDA.Where(x => x.Code == code).FirstOrDefault();
                    ctrl = new DevControl_ThongTinDuAn(DA);
                }
                else if (ac.Name.StartsWith(prefix_CaiDatNhaThau))
                {
                    string codeDA = ac.AccessibleName;
                    ctrl = new uc_DVTHUser(codeDA);
                }
                else if (ac.Name.StartsWith(prefix_CaiDatTask))
                {
                    ctrl = new uc_UserInTask(ac.AccessibleName);
                }
                else
                    return;
            }
            else if  (e.Element  == ace_PhanCapQuanLy)
            {
                ctrl = new uc_PhanCapQuanLy();

            }

            /*            else if (e.Element.Level == 3 && e.Element.OwnerElement.OwnerElement.OwnerElement == ace_DanhSachDA)
                        {
                            if (ac.Name.StartsWith(prefix_CaiDatNhaThau))
                            {
                                string codeDA = ac.AccessibleName;
                                ctrl = new uc_DVTHUser(codeDA);
                            }
                            else if (ac.Name.StartsWith(prefix_CaiDatTask))
                            {
                                ctrl = new uc_UserInTask(ac.AccessibleName);
                            }
                            else 
                                return;
                        }*/
            else
            {
                fluentDesignFormContainer1.Controls.Clear();
                return;
            }
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
        }
    }
}
