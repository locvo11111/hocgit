using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Model;
using System.Collections.Generic;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Common.ViewModel;

namespace PhanMemQuanLyThiCong.IRepositories
{
    public interface IDataRepository
    {
        List<GiaoViecKeHoachNguoiThamGia> GetListUserByCodeCongTac(string codeCongTac);
        bool UpdateStateGiaoViecCongViecConFileDinhKem(List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles);
        List<GiaoViec_FileDinhKemExtensionViewModel> GetListByCodeCongTac(string codeCongTac);
        bool AddOrUpdateGiaoViecFileDinhKem(List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles);
        bool UpdateStateGiaoViecFileDinhKem(List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles);
        bool UpdateGiaoViec(List<GiaoViecExtensionViewModel> lstCongViecs);
    }
}