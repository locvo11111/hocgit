using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.ChamCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.ChamCong
{
    public partial class ChamCongCaNhan : DevExpress.XtraEditors.XtraUserControl
    {
        public string m_path, m_TemPHinhAnh;
        public ChamCongCaNhan()
        {
            InitializeComponent();
        }
        public void Fcn_LoadData()
        {
            //m_path = Path.Combine(BaseFrom.m_FullTempathDA, $"Resource/Files/{DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN}");
            //m_TemPHinhAnh = $@"{BaseFrom.m_templatePath}\FileHinhAnh\QLTC.jpg";
            //if (!Directory.Exists(m_path))
            //    Directory.CreateDirectory(m_path);
            string dbString = $"SELECT DINHKEM.Code as UrlImage,NV.*,VT.Ten as ChucVu,PB.Ten as PhongBan FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} NV " +
                $"LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_VITRINHANVIEN} VT " +
                $" ON VT.Code=NV.ChucVu " +
                $"LEFT JOIN {DanhSachNhanVienConstant.TBL_CHAMCONG_TENPHONGBAN} PB ON PB.Code=NV.PhongBan " +
                $"LEFT JOIN { DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM} DINHKEM ON DINHKEM.CodeParent=NV.Code AND DINHKEM.State=1";
            List<DanhSachNhanVienModel> DanhSachNV = DataProvider.InstanceTHDA.ExecuteQueryModel<DanhSachNhanVienModel>(dbString);
            //DanhSachNV.ForEach(x => { x.FilePath = $@"{m_path}\{x.Code}\{x.HinhAnh}"; x.PhoToInit(); });
            slue_TenNhanVien.Properties.DataSource = DanhSachNV;
            //TongHopHelper.CreatePictureEdit(slue_TenNhanVien, "PhoTo");
            dg_NgayChamCong.DateTime = DateTime.Now;
            sc_ThongTinChamCong.ActiveViewType = SchedulerViewType.Month;
        }

        private void slue_TenNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            DanhSachNhanVienModel NV = slue_TenNhanVien.GetSelectedDataRow() as DanhSachNhanVienModel;
            if (NV.UrlImage is null)
            {
                pictureEdit1.EditValue = Properties.Resources.QLTC;
            }
            else
            {
                string filePath = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM, NV.Code), NV.UrlImage);
                pictureEdit1.EditValue = Image.FromFile(filePath);
            }
            Fcn_UpdateChamCongNhanVien();
        }

        private void slue_TenNhanVien_Popup(object sender, EventArgs e)
        {
            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            PopupSearchLookUpEditForm f = (edit as IPopupControl).PopupWindow as PopupSearchLookUpEditForm;
            f.Width = 650;
        }

        private void sp_ChamCong_Click(object sender, EventArgs e)
        {
            Form_CaiDatThongSoChamCong ChamCong = new Form_CaiDatThongSoChamCong();
            DanhSachNhanVienModel NV = slue_TenNhanVien.GetSelectedDataRow() as DanhSachNhanVienModel;
            ChamCong.Fcn_Update(NV);
            ChamCong.ShowDialog();
            Fcn_UpdateChamCongNhanVien();
        }
        private void Fcn_UpdateChamCongNhanVien()   
        {
            DanhSachNhanVienModel NV = slue_TenNhanVien.GetSelectedDataRow() as DanhSachNhanVienModel;
            schedulerDataStorage.Appointments.Clear();
            if (NV is null)
                return;
            string dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_GIOCHAMCONG} WHERE " +
                $"\"CodeNhanVien\"='{NV.Code}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            List<ChiTietChamCong_Gio_Ngay>GioThucTe = DataProvider.InstanceTHDA.ExecuteQueryModel<ChiTietChamCong_Gio_Ngay>(dbString);
            if (!GioThucTe.Any())
                return;
            dbString = $"SELECT * FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_CAIDATGIOLAM} WHERE" +
                $" \"CongTruong_VanPhong\"='{NV.NhanVienVanPhongCongTruong}' AND \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            List<CaiDaGioLamChamCong> GioLamMacDinh = DataProvider.InstanceTHDA.ExecuteQueryModel<CaiDaGioLamChamCong>(dbString);
            foreach(var item in GioThucTe)
            {
                Appointment app = schedulerDataStorage.CreateAppointment(AppointmentType.Normal);
                app.Start = item.GioChamCongVao.Value;
                app.End = item.GioChamCongRa.Value;
                app.Subject = item.NghiLam == "" && item.TangCa == "" ? $"{item.GioChamCongVao.Value.ToString("HH:mm")}-{ item.GioChamCongRa.Value.ToString("HH:mm")}" :
                    $"{item.GioChamCongVao.Value.ToString("HH:mm")}-{ item.GioChamCongRa.Value.ToString("HH:mm")}({item.ChuThich})";
                app.AllDay = false;
                if (item.Buoi!="Theo giờ")
                {
                    CaiDaGioLamChamCong _GioLam = GioLamMacDinh.Where(x => x.BuoiSang_Chieu_Toi ==
item.Buoi && x.NgayBatDau <= item.DateTime.Value && x.NgayKetThuc >= item.DateTime.Value).FirstOrDefault();
                    item.GioRaMacDinh = _GioLam.GioRa;
                    item.PhutRaMacDinh = _GioLam.PhutRa;
                    item.GioVaoMacDinh = _GioLam.GioVao;
                    item.PhutVaoMacDinh = _GioLam.PhutVao;
                }
                item.ChiTietChamCong();
                app.LabelKey = item.Label;
                app.AllDay = false;
                app.StatusKey = item.Code;
                app.Location = item.ChuThich;
                app.Description = item.GhiChu;
                schedulerDataStorage.Appointments.Add(app);
            }
        }
        private void ChamCongCaNhan_Load(object sender, EventArgs e)
        {
            sc_ThongTinChamCong.DataStorage.Appointments.Clear();

            string[] IssueList = { "Bình thường", "Sớm", "Trễ","Nghỉ phép","Tăng ca" };
            Color[] IssueColorList = { Color.Ivory, Color.Green, Color.Red, Color.Yellow, Color.Orange };

            IAppointmentLabelStorage labelStorage = sc_ThongTinChamCong.DataStorage.Appointments.Labels;
            labelStorage.Clear();
            int count = IssueList.Length;
            for (int i = 0; i < count; i++)
            {
                IAppointmentLabel label = labelStorage.CreateNewLabel(i, IssueList[i]);
                label.SetColor(IssueColorList[i]);
                labelStorage.Add(label);
            }
            sc_ThongTinChamCong.ActiveViewType = SchedulerViewType.Month;
        }

        private void sc_ThongTinChamCong_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e)
        {
            if (e.Appointment.Start == e.Appointment.End)
            {
                e.Text = e.Appointment.Start.ToString("HH:mm");
            }
            else
            {
                e.Text = e.Appointment.Start.ToString("HH:mm") + " - " + e.Appointment.End.ToString("HH:mm");
                if (e.Appointment.Location != null && e.Appointment.Location != "")
                    e.Text += $"({e.Appointment.Location})";
            }
        }

        private void sc_ThongTinChamCong_CustomDrawTimeCell(object sender, CustomDrawObjectEventArgs e)
        {
            if (sc_ThongTinChamCong.ActiveView is DevExpress.XtraScheduler.MonthView)
            {

                if (((MonthSingleWeekCell)(e.ObjectInfo)).Date.DayOfWeek == DayOfWeek.Sunday)
                {

                    ((MonthSingleWeekCell)(e.ObjectInfo)).Appearance.BackColor = ColorTranslator.FromHtml("#8DE9DF");
                    ((MonthSingleWeekCell)(e.ObjectInfo)).Appearance.ForeColor = Color.White;
                    e.DrawDefault();
                    e.Handled = true;
                }


            }
        }

        private void sc_ThongTinChamCong_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            DevExpress.XtraScheduler.SchedulerControl scheduler = ((DevExpress.XtraScheduler.SchedulerControl)(sender));
            CustomAppointmentForm form = new CustomAppointmentForm(scheduler, e.Appointment, e.OpenRecurrenceForm);
            if (form.Controller.StatusKey.ToString()=="0")
                e.Handled=true;
            else
            {
                try
                {
                    e.DialogResult = form.ShowDialog();
                    e.Handled = true;
                }
                finally
                {
                    form.Dispose();
                }
            }
        }


        private void sc_ThongTinChamCong_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Clear();
            if (e.Menu.Id == DevExpress.XtraScheduler.SchedulerMenuItemId.DefaultMenu)
            {
                e.Menu.Items.Add(new SchedulerMenuItem("Cài đặt ngày nghỉ", Fcn_CaiDatNgayNghi));        
                e.Menu.Items.Add(new SchedulerMenuItem("Cài đặt tăng ca", Fcn_CaiDatTangCa));
            }
        }
        private void Fcn_CaiDatTangCa(object sender, EventArgs e)
        {
            DanhSachNhanVienModel NV = slue_TenNhanVien.GetSelectedDataRow() as DanhSachNhanVienModel;
            DateTime Date = sc_ThongTinChamCong.SelectedInterval.Start;
            Form_CaiDatTangCaCaNhan CaNhan = new Form_CaiDatTangCaCaNhan();
            CaNhan.Fcn_LoadData(NV, Date);
            CaNhan.ShowDialog();
            Fcn_UpdateChamCongNhanVien();
        }     
        private void Fcn_CaiDatNgayNghi(object sender, EventArgs e)
        {
            DanhSachNhanVienModel NV = slue_TenNhanVien.GetSelectedDataRow() as DanhSachNhanVienModel;
            DateTime Date = sc_ThongTinChamCong.SelectedInterval.Start;
            Form_CaiDatNgayNghiCaNhan CaNhan = new Form_CaiDatNgayNghiCaNhan();
            CaNhan.Fcn_LoadData(NV, Date);
            CaNhan.ShowDialog();
            Fcn_UpdateChamCongNhanVien();
        }

        private void sc_ThongTinChamCong_InitNewAppointment(object sender, AppointmentEventArgs e)
        {

        }

        private void sc_ThongTinChamCong_CustomizeAppointmentFlyout(object sender, CustomizeAppointmentFlyoutEventArgs e)
        {
            e.ShowReminder = false;
        }

        private void searchLookUpEdit1View_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column == bandedGridColumn5 && e.IsGetData)
            {
                DevExpress.XtraGrid.Views.Tile.TileView  view = sender as DevExpress.XtraGrid.Views.Tile.TileView;
                string fileCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_UrlImage) as string ?? string.Empty;
                object objCode = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), col_Code);
                if (objCode != null && !string.IsNullOrEmpty(objCode.ToString()))
                {

                    if (!string.IsNullOrEmpty(fileCode))
                    {
                        string filePath = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIENFILEDINGKEM, objCode.ToString()), fileCode);
                        e.Value = FileHelper.fcn_ImageStreamDoc(filePath) ?? Properties.Resources.QLTC;
                    }
                    else
                        e.Value = Properties.Resources.QLTC;
                }
            }
        }

        static RepositoryItemPictureEdit CreatePictureEdit(SearchLookUpEdit LookUp, string name)
        {
            RepositoryItemPictureEdit ret = new RepositoryItemPictureEdit();
            LookUp.Properties.RepositoryItems.Add(ret);
            ret.PictureInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            ret.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            ret.Name = name;
            return ret;
        }
    }
}
