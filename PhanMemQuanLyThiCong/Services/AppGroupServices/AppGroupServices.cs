using PhanMemQuanLyThiCong.Common.Namequyery;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System;
using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Helper;

namespace PhanMemQuanLyThiCong.Repositories
{
    public class AppGroupServices : IAppGroupServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppGroupServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm mới AppGroup
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        ///  trả về false báo lỗi
        /// </returns>
        public bool Add(AppGroupViewModel entity)
        {
            if (entity != null)
            {
                int res = _unitOfWork.Execute(Quyery.INSERTAPPGROUPS, entity, false, true);
                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Add(IEnumerable<AppGroupViewModel> entities)
        {
            throw new System.NotImplementedException();
        }

        public List<AppDuAnGoupViewModel> AddAppDuAnGoups(List<string> liststring)
        {
            List<AppDuAnGoupViewModel> appDuAnGoupViewModels = new List<AppDuAnGoupViewModel>();
            foreach (var item in liststring)
            {
                AppDuAnGoupViewModel appDuAnGoupViewModel = new AppDuAnGoupViewModel();
                appDuAnGoupViewModel.TongHopDuAnId = item;
                appDuAnGoupViewModels.Add(appDuAnGoupViewModel);
            }
            return appDuAnGoupViewModels;
        }

        public List<AppRoleGroupViewModel> AddAppRoleGoups(List<string> liststring)
        {
            List<AppRoleGroupViewModel> appRoleGroupViewModels = new List<AppRoleGroupViewModel>();
            foreach (var item in liststring)
            {
                AppRoleGroupViewModel appRoleGroupViewModel = new AppRoleGroupViewModel();
                appRoleGroupViewModel.RoleId = item;
                appRoleGroupViewModels.Add(appRoleGroupViewModel);
            }
            return appRoleGroupViewModels;
        }

        public List<AppUserGroupViewModel> AddAppUserGroups(List<string> liststring)
        {
            List<AppUserGroupViewModel> appUserGroupViewModels = new List<AppUserGroupViewModel>();
            foreach (var item in liststring)
            {
                AppUserGroupViewModel  appUserGroupViewModel = new AppUserGroupViewModel();
                appUserGroupViewModel.UserId = item;
                appUserGroupViewModels.Add(appUserGroupViewModel);
            }
            return appUserGroupViewModels;
        }

        /// <summary>
        /// Thêm mới vào sql sever
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(AppGroupViewModel entity)
        {
            var data = await KeyHelper.ActAddAsync<AppGroupViewModel>(entity, RouteAPI.APPGROUP_ADD);
            return data.MESSAGE_TYPECODE;
        }

        public Task<bool> AddAsync(IEnumerable<AppGroupViewModel> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AppGroupViewModel> All()
        {

            return _unitOfWork.Query<AppGroupViewModel>(Quyery.SELECTAPPGROUPS, null, true, false).OrderBy(x=>x.Id);
        }

        public async Task<IEnumerable<AppGroupViewModel>> AllAsync()
        {
            List<AppGroupViewModel> appGroupViewModels = new List<AppGroupViewModel>();
            var data = await KeyHelper.ActSysGroupAsync();
            if(data.MESSAGE_TYPECODE)
            {
                appGroupViewModels = data.Dto;
            }
            return appGroupViewModels;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public AppGroupViewModel Find(object pksFields)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AppGroupViewModel> FindAsync(object pksFields)
        {
            int key = (int)pksFields;
            var data = await KeyHelper.ActGetByKeyAsync<AppGroupViewModel,int>(key, RouteAPI.APPGROUP_GETDETAIL);
            return data.Dto;
        }

        public DataTable GetAll(string keyWord)
        {
            throw new System.NotImplementedException();
        }

        public bool InstertOrUpdate(AppGroupViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> InstertOrUpdateAsync(AppGroupViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(object key)
        {
            if (key is object)
            {
                _unitOfWork.Execute(Quyery.DELETEAPPGROUPS, key, false, true);
            }
        }

        public async Task<bool> RemoveAsync(object key)
        {
            var data = await KeyHelper.ActDeleteGroupAsync((int)key);
            return data.MESSAGE_TYPECODE;
        }

        public async Task<bool> RemoveAsync<Tkey>(Tkey key)
        {
            var data = await KeyHelper.ActDeleteAsync<Tkey>(key, RouteAPI.APPGROUP_DELETE);
            return data.MESSAGE_TYPECODE;
        }

        public bool Update(AppGroupViewModel entity, object pks)
        {
            if (pks is object)
            {
                int res = _unitOfWork.Execute(Quyery.UPDATEAPPGROUPS, entity, false, true);
                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(AppGroupViewModel entity, object pks)
        {
            var data = await KeyHelper.ActUpdateGroupAsync(entity);
            return data.MESSAGE_TYPECODE;
        }
    }
}