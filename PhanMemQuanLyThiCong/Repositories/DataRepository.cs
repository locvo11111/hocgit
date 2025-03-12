using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Model;
using System.Collections.Generic;
using VChatCore.ViewModels.SyncSqlite;
using System.Linq;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Common.Helper;

namespace PhanMemQuanLyThiCong.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private string _path;

        public DataRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public void SetOpenChangeDb()
        //{
        //    _path = DataProvider.InstanceTHDA.m_NameDb;
        //    if (!string.IsNullOrEmpty(_path))
        //        _unitOfWork.changePath(_path);
        //}

        public List<GiaoViecKeHoachNguoiThamGia> GetListUserByCodeCongTac(string codeCongTac)
        {
            //SetOpenChangeDb();
            return _unitOfWork.Query<GiaoViecKeHoachNguoiThamGia>("SELECT_GIAOVIEC_KEHOACH_NGUOITHAMGIABYCODE", new { CodeCongViecCha = codeCongTac }, true, false);
        }

        public bool AddOrUpdateGiaoViecFileDinhKem(List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles)
        {
            //SetOpenChangeDb();
            var res = _unitOfWork.AddOrUpdate<GiaoViec_FileDinhKemExtensionViewModel>("INSERT_OR_REPLACE_GIAOVIEC_FILEDINHKEM", lstFiles, false, true);
            return res > 0;
        }

        public List<GiaoViec_FileDinhKemExtensionViewModel> GetListByCodeCongTac(string codeCongTac)
        {
            //SetOpenChangeDb();
            return _unitOfWork.Query<GiaoViec_FileDinhKemExtensionViewModel>("SELECT_FILEDINHKEM_BYCODE", new { CodeCongViecCha = codeCongTac }, true, false);
        }

        public bool UpdateStateGiaoViecFileDinhKem(List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles)
        {
            //SetOpenChangeDb();
            var res = _unitOfWork.AddOrUpdate<GiaoViec_FileDinhKemExtensionViewModel>("UPDATE_GIAOVIEC_FILEDINHKEM", lstFiles, false, true);
            return res > 0;
        }
        
        public bool UpdateStateGiaoViecCongViecConFileDinhKem(List<GiaoViec_FileDinhKemExtensionViewModel> lstFiles)
        {
            //SetOpenChangeDb();
            var res = _unitOfWork.AddOrUpdate<GiaoViec_FileDinhKemExtensionViewModel>("UPDATE_GIAOVIEC_FILEDINHKEM", lstFiles, false, true);
            return res > 0;
        }

        public bool UpdateGiaoViec(List<GiaoViecExtensionViewModel> lstCongViecs)
        {
            //SetOpenChangeDb();
            var CVCha = lstCongViecs.Where(x => x.CodeCongViecCon is null).ToList();
            var CVCon = lstCongViecs.Where(x => x.CodeCongViecCon != null).ToList();

            int res = 0;

            if (CVCha.Any())
            {
                res = _unitOfWork.AddOrUpdate<GiaoViecExtensionViewModel>("UPDATE_GIAOVIEC", CVCha, false, true);
                foreach (var cha in CVCha)
                {
                    DinhMucHelper.capNhatTrangThaiCacBang(cha.CodeCongViecCha, Common.Enums.SourceDataEnum.KHGV, cha.TrangThai);
                }    
            }
            if (CVCon.Any()) { }
                res = _unitOfWork.AddOrUpdate<GiaoViecExtensionViewModel>("UPDATE_GIAOVIEC_CongViecCon", CVCon, false, true);

            return res > 0;
        }
        

    }
}