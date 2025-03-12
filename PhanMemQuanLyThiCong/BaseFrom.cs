using DevExpress.DevAV.Chat.Model;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit.Fields;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{
    public static class BaseFrom
    {
        public static void ResetLoaded()
        {
            DoBocChuanLoaded
            = KeHoachLoaded
            = VatLieuLoaded
            = NhanCongLoaded
            = MayThiCongLoaded
            = TongHopVatTuLoaded = false;
        }
        public static Version FirstVersionSameDb = new Version("1.0.1.769");
        public static string DefautWaitFormErrorText;
        public static bool IsSetNullOnChangedTab { get; set; } = false;

        public static bool CheDoNhapKhongTinhToan = true;
        private static bool _isFullAccess
        {
            get
            {
                if (_banQuyenKeyInfo.Status == KeyStatus.Active &&
                    (_banQuyenKeyInfo.TypeCode == TypeStatus.KHOACUNG
                    || (_banQuyenKeyInfo.IsDateLimit && _banQuyenKeyInfo.TypeCode == TypeStatus.KHOAMEM && _banQuyenKeyInfo.LimitDate > 0)))
                {
                    return true;
                }
                else return false;
            }
        }

        public static bool DoBocChuanLoaded = false;
        public static bool KeHoachLoaded = false;
        public static bool VatLieuLoaded = false;
        public static bool NhanCongLoaded = false;
        public static bool MayThiCongLoaded = false;
        public static bool TongHopVatTuLoaded = false;
        public static bool IsForceLoad = false;
        public static bool IsLoadKiemSoat = false;
        public static bool IsLoadNhanhCham = false;
        public static bool IsLoadKLHN = false;

        public static Dictionary<string, string> dicFcnCode = new Dictionary<string, string>();

        private static bool _IsValidAccount { get; set; }

        public static bool IsFullAccess { get => _isFullAccess; }

        public static int count_noti = 0;

        public static bool IsValidAccount
        {
            get { return (IsFullAccess | _IsValidAccount); }
            set { _IsValidAccount = value; }
        }

        public static List<int> IndsCheckBoxDoBocChuan = new List<int>();
        public static List<int> IndsCheckBoxKeHoach = new List<int>();
        
        public static int IndFstCellDataDoBocChuan = 4;
        public static int IndFstCellDataKeHoach = 4;

        private static BanQuyenKeyInfo _banQuyenKeyInfo = new BanQuyenKeyInfo();
        public static BanQuyenKeyInfo BanQuyenKeyInfo { get => _banQuyenKeyInfo; set => _banQuyenKeyInfo = value; }

        private static string _idCongTrinh;
        public static string IdCongTrinh { get => _idCongTrinh; set => _idCongTrinh = value; }

        public static string _serialNumber;

        public static string SerialNumberHDD { get => _serialNumber; set => _serialNumber = value; }

        private static bool _isLimitDate = false;
        public static bool IsLimitDate { get => _isLimitDate; set => _isLimitDate = value; }



        private static BanQuyenOldViewModel _banQuyenOldViewModel = new BanQuyenOldViewModel();
        public static BanQuyenOldViewModel BanQuyenOldViewModel { get => _banQuyenOldViewModel; set => _banQuyenOldViewModel = value; }


        private static List<Provinces> _provinces;

        public static List<Provinces> Provinces { get => _provinces; set => _provinces = value; }
        public static string[] ProvincesHaveDonGia { get; set; }

        private static List<Department> departments;

        public static List<Department> Departments { get => departments; set => departments = value; }

        public static string m_path, m_resourceChatPath; //Thư mục làm việc - thư mục chứa mẫu WORD/EXCEL
        public static string m_tempPath { get; set; }

        public static string m_templatePath
        {
            get
            {
                return $@"{m_path}\Template";
            }
        }
        public static string m_TempFilePath { get; set; }
        private static string _crTempDATH { get; set; }
        //public static void OnDirChanged(object source, FileSystemEventArgs e)
        //{
        //    // Specify what is done when a file is changed.  
        //    BaseFrom.THDAChanged = true;
        //}
        public static string m_crTempDATH
        {
            get => _crTempDATH;
            set
            {
                _crTempDATH = value;
                THDAChanged = false;
                SharedControls.watcher.Path = m_FullTempathDA;
                SharedControls.watcher.EnableRaisingEvents = true;

                //SharedControls.watcher.Changed += new FileSystemEventHandler(OnDirChanged);
                //SharedControls.watcher.Created += new FileSystemEventHandler(OnDirChanged);
                //SharedControls.watcher.Deleted += new FileSystemEventHandler(OnDirChanged);
                //SharedControls.watcher.Renamed += new RenamedEventHandler(OnDirChanged);
            }
        }//Thư mục tạm - Tên dự án tổng hợp

        public static string m_FullTempathDA
        {
            get => Path.Combine(m_tempPath, _crTempDATH);
        }

        public static string m_UnsavedPath { get; set; }

        public static LoginResponse userinfo;
        public static LoginResponse UserInfo { get => userinfo; set => userinfo = value; }

        public static AllPermission allPermission { get; set; } = new AllPermission();

        //public static List<RoleDetailViewModel> _roleDetails = new List<RoleDetailViewModel>();
        //public static List<RoleDetailViewModel> RoleDetails { get => _roleDetails; set => _roleDetails = value; }

        public static List<UserWithRoleInGiaoViec> UsersWithRoles { get; set; } = new List<UserWithRoleInGiaoViec>();

        public static List<Guid> usersInAdmin
        {
            get
            { return UsersWithRoles.Where(x => x.CommandId == nameof(CommandCode.Add)).Select(x => x.UserId).ToList(); }
        }
        public static List<Guid> usersInView
        {
            get
            { return UsersWithRoles.Where(x => x.CommandId == nameof(CommandCode.View)).Select(x => x.UserId).ToList(); }
        }
        public static List<Guid> usersInEdit
        {
            get
            { return UsersWithRoles.Where(x => x.CommandId == nameof(CommandCode.Edit)).Select(x => x.UserId).ToList(); }
        }
        public static List<Guid> usersInApprove
        {
            get
            { return UsersWithRoles.Where(x => x.CommandId == nameof(CommandCode.Approve)).Select(x => x.UserId).ToList(); }
        }


        private static List<CongTac> _dinhMucTraCuus = null;
        public static List<CongTac> DinhMucTraCuus { get => _dinhMucTraCuus; set => _dinhMucTraCuus = value; }

        public static bool THDAChanged { get; set; } = false;

        private static List<CategoryDinhMuc> _listSelectCategoryDinhMucs;

        public static List<CategoryDinhMuc> ListSelectCategoryDinhMucs { get => _listSelectCategoryDinhMucs; set => _listSelectCategoryDinhMucs = value; }

        public static List<string> lsCTDinhMuc
        {
            get
            {
                return _listSelectCategoryDinhMucs.Where(x => x.Checked).Select(x => x.Name).ToList();
            }
        }

        public static bool IsShowDonGiaKeHoach
        {
            get
            {
                if (BaseFrom.allPermission.HaveInitProjectPermission)
                {
                    return true;
                }

                var crDA = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;
                if (crDA is null)
                    return false;
                else return crDA.IsShowDonGiaKeHoach;
            }
        } 
        
        public static bool IsShowKhoiLuongKeHoach
        {
            get
            {
                if (BaseFrom.allPermission.HaveInitProjectPermission)
                {
                    return true;
                }

                var crDA = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;
                if (crDA is null)
                    return false;
                else return crDA.IsShowKhoiLuongKeHoach;
            }
        } 
    }
}
