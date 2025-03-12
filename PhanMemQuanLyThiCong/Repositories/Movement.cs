using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.Namequyery;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhanMemQuanLyThiCong.Common.Helper.SeverHelper;
using AutoMapper;

namespace PhanMemQuanLyThiCong.Repositories
{
    public class Movement : IMovement
    {
        private IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        //= ConfigUnity.Container.Resolve<IUnitOfWork>();
        public Movement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _iUnitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<bool> InsertCommads()
        {
            var Data = await KeyHelper.ActSysCommandAsync();
            if (Data.MESSAGE_TYPECODE && Data.Dto.Count > 0)
            {
                _iUnitOfWork.AddOrUpdate(Quyery.INSERTCOMMAND, Data.Dto, false, true);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> InsertFunctions()
        {
            var Data = await KeyHelper.ActSysFunctionAsync();
            if (Data.MESSAGE_TYPECODE && Data.Dto.Count > 0)
            {
                _iUnitOfWork.AddOrUpdate(Quyery.INSERTFUNCTIONS, Data.Dto, false, true);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> InsertFunctionTypes()
        {
            var Data = await KeyHelper.ActSysFunctionTypeAsync();
            if (Data.MESSAGE_TYPECODE && Data.Dto.Count > 0)
            {
                _iUnitOfWork.AddOrUpdate<FunctionTypeViewModel>(Quyery.INSERTFUNCTIONTYPES, Data.Dto, false, true);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> InsertKeyStores()
        {
            var Data = await KeyHelper.ActSysKeyStoreAsync(BaseFrom.BanQuyenKeyInfo.SerialNo);
            if (Data.MESSAGE_TYPECODE && Data.Dto != null)
            {
                List<KeyInfoViewModel> keyInfos = new List<KeyInfoViewModel>();
                keyInfos.Add(Data.Dto);

                _iUnitOfWork.AddOrUpdate<KeyInfoViewModel>(Quyery.INSERTKEYSTORES, keyInfos, false, true);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> InsertPermissions()
        {
            var Data = await KeyHelper.ActSysPermissionAsync(BaseFrom.BanQuyenKeyInfo.SerialNo);
            if (Data.MESSAGE_TYPECODE && Data.Dto.Count > 0)
            {
                _iUnitOfWork.AddOrUpdate<PermissionKeyStoreViewModel>(Quyery.INSERTPERMISSIONKEYSTORES, Data.Dto, false, true);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> InsertTypeAccounts()
        {
            var Data = await KeyHelper.ActSysTypeAccountAsync();
            if (Data.MESSAGE_TYPECODE && Data.Dto.Count > 0)
            {
                _iUnitOfWork.AddOrUpdate<TypeAccountViewModel>(Quyery.INSERTTYPEACCOUNTS, Data.Dto, false, true);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> InsertUserApproves()
        {
            var Data = await KeyHelper.ActSysUserApproveAsync(BaseFrom.BanQuyenKeyInfo.SerialNo);
            if (Data.MESSAGE_TYPECODE && Data.Dto.Count > 0)
            {
                List<UserAppViewModel> userAppViewModels =
                    AutoMapper.Mapper.Map<List<UserInKeyViewModel>, List<UserAppViewModel>>(Data.Dto);

                _iUnitOfWork.AddOrUpdate<UserAppViewModel>(Quyery.INSERTUSERAPPROVES, userAppViewModels, false, true);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> InsertUsers()
        {
            var Data = await KeyHelper.ActSysAppUserAsync(BaseFrom.BanQuyenKeyInfo.SerialNo);
            if (Data.MESSAGE_TYPECODE && Data.Dto != null)
            {
                List<AppUserViewModel> UserViewModel = Data.Dto;

                _iUnitOfWork.AddOrUpdate<AppUserViewModel>(Quyery.INSERTAPPUSERS, UserViewModel, false, true);
                return true;
            }
            else
                return false;
        }
    }
}
