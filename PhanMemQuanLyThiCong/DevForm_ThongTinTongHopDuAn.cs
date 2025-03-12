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
using PhanMemQuanLyThiCong.Common.SQLite;
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
//using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{
    public partial class DevForm_ThongTinTongHopDuAn : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        List<ThongTinTHDAViewModel> lsTHDA;
        const string prefix_CaiDatNhaThau = "CaiDatNhaThau_";
        const string prefix_CaiDatTask = "CaiDatTask_";
        public DevForm_ThongTinTongHopDuAn()
        {
            InitializeComponent();
        }

        private async void DevForm_ThongTinTongHopDuAn_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang tải thông tin tổng hợp dự án");
            try
            {
                await fcn_GetListTHDA();
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

        private async Task fcn_GetListTHDA()
        {
            
            var res = await CusHttpClient.InstanceCustomer.client.MGetAsync<List<ThongTinTHDAViewModel>>($"{RouteAPI.TongDuAn_CONTROLLER}/GetAllTHDAWithDA");
            if (res.MESSAGE_TYPECODE)
            {
                lsTHDA = res.Dto;
                foreach (var THDA in lsTHDA)
                {
                    var ace_THDA = new AccordionControlElement()
                    {
                        Text = THDA.Tentonghopduan,
                        AccessibleName = THDA.Code,
                        Style = ElementStyle.Group,
                    };

                    ace_DanhSachTHDA.Elements.Add(ace_THDA);

                    foreach (var ThongTinDA in THDA.ThongTinDuAns)
                    {
                        var ace_ThongTinDA = new AccordionControlElement()
                        { 
                            Text = ThongTinDA.TenDuAn,
                            AccessibleName = ThongTinDA.Code,
                            Style = ElementStyle.Group,
                        };
                        //ace_ThongTinDA.Name = $"DuAn_{ace_ThongTinDA.Name}";
                        ace_THDA.Elements.Add(ace_ThongTinDA);

                        var ace_Contractor = new AccordionControlElement()
                        {
                            Text = "Cài đặt nhà thầu",
                            AccessibleName = ThongTinDA.Code,
                            Style = ElementStyle.Item,
                        };
                        ace_Contractor.Name = $"{prefix_CaiDatNhaThau}{ace_Contractor.Name}";


                        var ace_Task = new AccordionControlElement()
                        {
                            Text = "Cài đặt quyền công tác",
                            AccessibleName = ThongTinDA.Code,
                            Style = ElementStyle.Item,
                        };
                        ace_Task.Name = $"{prefix_CaiDatTask}{ace_Task.Name}";

                        ace_ThongTinDA.Elements.Add(ace_Contractor);
                        ace_ThongTinDA.Elements.Add(ace_Task);
                    }
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

            if (e.Element == ace_Permission)
            {
                ctrl = new devctrl_NhomQuyen();
            }
            else if (e.Element.Level == 1 && e.Element.OwnerElement == ace_DanhSachTHDA)
            {
                string code = ac.AccessibleName;
                var THDA = lsTHDA.Where(x => x.Code == code).FirstOrDefault();
                ctrl = new DevControl_ThongTinChiTietTongHopDuAn(THDA);
            }
            else if (e.Element.Level == 2 && e.Element.OwnerElement.OwnerElement == ace_DanhSachTHDA)
            {
                fluentDesignFormContainer1.Controls.Clear();
                return;
            }
            else if (e.Element.Level == 3 && e.Element.OwnerElement.OwnerElement.OwnerElement == ace_DanhSachTHDA)
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
            }
            else
                return;
            
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
        }
    }
}
