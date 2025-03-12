using DevExpress.LookAndFeel;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGantt;
using DevExpress.XtraPrinting;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.KanbanModule;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Uc_TienDoGiaoNhiemVu : DevExpress.XtraEditors.XtraForm
    {
        List<TaskRecord> _tasks = new List<TaskRecord>();
        List<AppUserViewModel> _users;
        public Uc_TienDoGiaoNhiemVu()
        {
            InitializeComponent();
        }
        public async void Fcn_LoadData()
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

            //Tạo avatar cho những người dùng k có avatar
            foreach (var user in _users)
            {
                KanbanHelper.CreateMemberGlyph(user, LookAndFeel, ScaleDPI.ScaleVertical(30));
            }

            // Lấy Task List
            _tasks = await TaskHelper.GetAllTask();

            if (_tasks is null)
                MessageShower.ShowInformation("Không thể tải công việc! Kiểm tra kết nối internet!");

            List<BarChartViewModel> source = new List<BarChartViewModel>();
            foreach(var item in _users)
            {
                List<TaskRecord> NewTask = _tasks.Where(x => x.Users.ToArray().Contains(item.Id)).ToList();
                if (!NewTask.Any())
                    continue;
                source.Add(new BarChartViewModel()
                {
                    TenCongViec = item.FullName.ToUpper(),
                    ParentUID = "0",
                    UID = item.Id.ToString(),
                    TaskType = 0

                });
                foreach(var Child in NewTask)
                {
                    List<AppUserViewModel> NewUser = _users.Where(x => Child.Users.ToArray().Contains(x.Id)).ToList();
                    string MoTa = String.Join(",", NewUser.Select(x => x.FullName).ToArray());
                    if (!Child.StartDate.HasValue && !Child.EndDate.HasValue)
                    {
                        source.Add(new BarChartViewModel()
                        {
                            TenCongViec = Child.Caption,
                            Progress = Child.ProgressCal * 100,
                            ParentUID = item.Id.ToString(),
                            UID = Guid.NewGuid().ToString(),
                            TaskType = 1,
                            MoTa = MoTa,
                            Description = $"{Child.ProgressCal * 100}%"
                        });
                        continue;
                    }
                    source.Add(new BarChartViewModel()
                    {
                        TenCongViec = Child.Caption,
                        Progress = Child.ProgressCal * 100,
                        ParentUID = item.Id.ToString(),
                        UID = Guid.NewGuid().ToString(),
                        NgayBatDauThiCong =Child.StartDate.Value.AddHours(1),
                        NgayKetThucThiCong =Child.EndDate.Value.AddHours(23),
                        TaskType = 1,
                        MoTa=MoTa,
                        Description = $"{Child.ProgressCal * 100}%"
                    });
                }

            }


            for (int i = 0; i >= 0; i--)
            {
                source.Where(x => x.TaskType == i).ForEach(y =>
                {
                    y.NgayBatDauThiCong = source.Where(z => z.ParentUID == y.UID).Min(t => t.NgayBatDauThiCong)??DateTime.Now.AddHours(1);
                    y.NgayKetThucThiCong = source.Where(z => z.ParentUID == y.UID).Max(t => t.NgayKetThucThiCong)??DateTime.Now.AddHours(23);
                    y.Progress = source.Where(z => z.ParentUID == y.UID).Count()==0?0:Math.Round(source.Where(z => z.ParentUID == y.UID).Sum(t => t.Progress) / (source.Where(z => z.ParentUID == y.UID).Count()));
                });
            }
            gc_HopDong.DataSource = source;
            gc_HopDong.RefreshDataSource();
            gc_HopDong.Refresh();
            gc_HopDong.ExpandAll();
            WaitFormHelper.CloseWaitForm();
        }

        private void gc_HopDong_CustomDrawTask(object sender, DevExpress.XtraGantt.CustomDrawTaskEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.BackColor = Color.Green;
                e.Appearance.ProgressColor = Color.Green;

            }
            else
            {
                e.Appearance.BackColor = Color.Orange;
                e.Appearance.ProgressColor = Color.Red;
            }
        }

        private void gc_HopDong_CustomDrawTimescaleColumn(object sender, DevExpress.XtraGantt.CustomDrawTimescaleColumnEventArgs e)
        {
            GanttTimescaleColumn column = e.Column;
            if (column.StartDate <= DateTime.Now && column.FinishDate >= DateTime.Now)
            {
                e.DrawBackground();
                float x = (float)e.GetPosition(DateTime.Now);
                float width = 4;
                RectangleF deadLineRect = new RectangleF(x, column.Bounds.Y, width, column.Bounds.Height);
                e.Cache.FillRectangle(DXSkinColors.FillColors.Danger, deadLineRect);
                e.DrawHeader();
                e.Handled = true;
            }
        }

        private void gc_HopDong_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level ==0)
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }
        private void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu", "Vui Lòng chờ!");
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = true,
                PaperKind = System.Drawing.Printing.PaperKind.A3,
                Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20),
            };
            link.CreateReportHeaderArea += link_CreateReportHeaderArea;
            link.PrintingSystemBase.Document.AutoFitToPagesWidth = 1;
            WaitFormHelper.CloseWaitForm();
            link.ShowRibbonPreview(lookAndFeel);
        }
        private void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            //string reportHeader = $"Dự án: {slke_ThongTinDuAn.Text} {Environment.NewLine}Địa chỉ: {lst.FirstOrDefault().DiaChi}";
            string reportHeader = $"BIỂU ĐỒ TIẾN ĐỘ GIAO NHIỆM VỤ";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
            e.Graph.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            RectangleF rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);

            //reportHeader = $"Thời gian: Từ {De_BeginTienDo.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} đến {De_EndTienDO.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} ";
            //e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Far);
            //e.Graph.Font = new Font("Times New Roman", 14, FontStyle.Regular);
            //RectangleF recnew = new RectangleF(0, 55, e.Graph.ClientPageSize.Width, 50);
            //e.Graph.DrawString(reportHeader.ToUpper(), Color.Black, recnew, BorderSide.None);

        }
        private void sb_XuatFile_Click(object sender, EventArgs e)
        {
            PreviewPrintableComponent(gc_HopDong, gc_HopDong.LookAndFeel);
        }
    }
}
