using CustomDevExEditor;
using DevExpress.Spreadsheet;
using DevExpress.Utils.VisualEffects;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Controls.KiemSoat;
using PhanMemQuanLyThiCong.Urcs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public static class SharedControls
    {
        public static System.Windows.Forms.ComboBox cbb_DBKH_ChonDot;
        public static SpreadsheetControl spsheet_TD_KH_LapKeHoach;
        public static CheckEdit ce_NgoaiKeHoach;
        public static CheckEdit ce_GianNgay;
        public static CheckEdit ce_RePlan;
        //public static SpreadsheetControl spsheet_GV_KH_ChiTietCacHMCongViec;
        public static CheckBox cb_HienKeHoach;
        public static SearchLookUpEdit slke_ThongTinDuAn;
        public static System.Windows.Forms.ComboBox cbo_MeNuTenHopDong;
        public static SpreadsheetControl spsheet_GV_KH_ChiTietCacHMCongViec;
        public static SpreadsheetControl m_SpreadSheet;
        public static RadioGroup rg_GV_DauViec;
        public static System.Windows.Forms.ComboBox cbb_DauViecNho;
        public static System.Windows.Forms.ComboBox cbb_DauViecLon;
        //public static DataGridView dgv_NoiDungDangThucHien;
        public static DevExpress.XtraTreeList.TreeList tL_QLVC_TongHop;
        public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rILUE_TenKhoTH;
        public static GridControl gc_ThuChiTamUng_DeXuat;
        public static Ctrl_DonViThucHienDuAn ctrl_DonViThucHienDuAnTDKH;
        public static Ctrl_DonViThucHienDuAn ctrl_DonViThucHienGiaoViec;
        public static Ctrl_KiemSoatTienDoDuAn ctrl_KiemSoatTienDoDuAn;
        public static CheckEdit ce_CongTacNgoaiKeHoach;

        //public static SplashScreenManager splashWaitForm;
        public static ToastNotificationsManager toastNotificationManager1;
        public static OpenFileDialog openFileDialog;
        public static SaveFileDialog saveFileDialog;
        public static XtraTabControl tabMain;
        public static XtraTabControl xtraTab_KiemSoat;

        public static XtraTabPage xtraTabPage_CongVanDiDen;
        public static XtraTabPage xtraTabPage_DSDuAn_CT;
        public static XtraTabPage xtraTabPage_NhanCong;
        public static XtraTabPage xtraTabPage_QLMay_TB;
        public static XtraTabPage xtraTabPage_GiaoViec;
        public static XtraTabPage xtraTabPage_ThuChi_TamUng;
        public static XtraTabPage xtraTab_QLVatLieu_VanChuyen;
        public static XtraTabPage xtraTab_KiemSoatTienDo;
        public static XtraTabPage xtraTab_BaoCaoLoiNhuan;
        public static XtraTabPage xtraTab_VLMTCNC;
        public static XtraTabPage xtraTab_ThamDinhDauVao;
        public static Worksheet ws_VatLieu;

        public static uc_Notification uc_noti;

        public static PhanMemQuanLyThiCong360 main;
        public static Dictionary<IXtraTabPage, Badge> dictNotify = new Dictionary<IXtraTabPage, Badge>();
        public static IXtraTabPage tabChatDuyet { get; set; }
        public static IXtraTabPage tabChatGuiDuyet { get; set; }
        public static IXtraTabPage tabChatDaDuyet { get; set; }

        public static Badge badge_Duyet { get; set; }
        public static Badge badge_GuiDuyet { get; set; }
        public static Badge badge_DaDuyet { get; set; }

        public static AlertControl alertControl { get; set; } = null;

        public static Form Form { get; set; }

        public static CustomPopupContainerEdit customPopupContainerEdit_noti;
        //public static GridControl gc_noti;
        public static Badge badge_Noti;
        public static XtraForm_NhapKhoiLuongHangNgay ctrl_NhapKLHN;

        public static XtraTabControl xtraTabControl_TienDoKeHoach {get;set;}

        public static CheckBox cb_TDKH_HienDienGiai { get; set;}
        public static CheckBox ce_TDKH_HienHTPD { get; set;}
        public static CheckBox cb_TDKH_HienCongTac { get; set;}
        public static FileSystemWatcher watcher { get; set; }

        public static Button btn_TT_HienThiThongTinMoi { get; set; }

        public static CheckEdit ce_Mode { get; set; }
        public static ToolStripMenuItem thôngTinPhiênBảnToolStripMenuItem { get; set; }
        public static ToolStripMenuItem đăngKýSửDụngPhầnMềmQuảnLýThiCôngToolStripMenuItem { get; set; }
        public static LabelControl lb_ThongBaoBanQuyen { get; set; }
        public static NavigationPane np_KhoiLuongHangNgay { get; set; }
        //public static bool _CheckTrangThaiNghiemThu { get; set; } = false;
        public static bool _CheckTrangThaiThongThuong { get; set; } = false;
        public static bool _CheckTrangThaiThongThuongTH { get; set; } = false;
        public static bool _CheckTrangThaiDeNghi { get; set; } = false;
        public static bool _PhanKhai { get; set; } = false;
        public static CheckEdit ce_LocTheoNgay { get; set; }
        public static DateEdit de_Loc_TuNgay { get; set; }
        public static DateEdit de_Loc_DenNgay { get; set; }
        public static XtraForm_PhuongAnTaiChinh uc_PATC { get; set; } = null;
        public static Uc_ChatBox uc_ChatBox { get; set; } = null;

        public static string[] TabPageNameNotDependOnPrj { get; set; } = null; 
        public static string[] TabPageNameDependOnPrj { get; set; } = null;
        public static bool IsNgay { get; set; } = false;

        public static LabelControl lb_HienTai { get; set; }
        public static SearchLookUpEdit slke_ChonCTHM {  get; set; }
        public static int Error {  get; set; }
        //public static Ctrl_BaoCaoTa
        //public static System.Windows.Forms.ComboBox cbb_DBKH_ChonDot;
    }
}
